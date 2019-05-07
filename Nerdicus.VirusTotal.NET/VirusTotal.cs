using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nerdicus.VirusTotalNET.Helpers;
using Nerdicus.VirusTotalNET.Models.Url;
using Nerdicus.VirusTotalNET.Models.IP;
using Nerdicus.VirusTotalNET.Models.Domain;
using Nerdicus.VirusTotalNET.Models.File.Upload;
using Nerdicus.VirusTotalNET.Models.File;
using Nerdicus.VirusTotalNET.Models.File.Analysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nerdicus.VirusTotalNET.Exceptions;
using Nerdicus.VirusTotalNET.Models.Base;
using Nerdicus.VirusTotalNET.Models.Url.Scan;

namespace Nerdicus.VirusTotalNET
{
    public class VirusTotal : IDisposable
    {

        private readonly HttpClient _client;
        private readonly HttpClientHandler _httpClientHandler;
        private readonly JsonSerializer _serializer;
        private const string _baseUrl = "https://www.virustotal.com";
        private const string _defaultUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0";

        public VirusTotal()
        {
            _httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };

            _serializer = JsonSerializer.Create();
            _serializer.NullValueHandling = NullValueHandling.Ignore;

            _client = new HttpClient(_httpClientHandler);
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            _client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("de,en;q=0.7,en-US;q=0.3");
            _client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate, br");
            _client.DefaultRequestHeaders.Referrer = new Uri(_baseUrl);
            _client.DefaultRequestHeaders.Connection.ParseAdd("keep-alive");

            UserAgent = _defaultUserAgent;
        }

        #region Events
        /// <summary>
        /// Occurs when the raw JSON response is received from VirusTotal.
        /// </summary>
        public event Action<byte[]> OnRawResponseReceived;

        /// <summary>
        /// Occurs just before we send a request to VirusTotal.
        /// </summary>
        public event Action<HttpRequestMessage> OnHTTPRequestSending;

        /// <summary>
        /// Occurs right after a response has been received from VirusTotal.
        /// </summary>
        public event Action<HttpResponseMessage> OnHTTPResponseReceived;
        #endregion

        #region Properties
        /// <summary>
        /// The user-agent to use when doing queries
        /// </summary>
        public string UserAgent
        {
            get => _client.DefaultRequestHeaders.UserAgent.ToString();
            set => _client.DefaultRequestHeaders.Add("User-Agent", value);
        }

        /// <summary>
        /// Get or set the proxy.
        /// </summary>
        public IWebProxy Proxy
        {
            get => _httpClientHandler.Proxy;
            set => _httpClientHandler.Proxy = value;
        }

        /// <summary>
        /// Get or set the timeout.
        /// </summary>
        public TimeSpan Timeout
        {
            get => _client.Timeout;
            set => _client.Timeout = value;
        }
        #endregion

        // File
        #region ScanFile
        /// <summary>
        /// Scan a file.
        /// Note: It is highly encouraged to get the report of the file before scanning, in case it has already been scanned before.
        /// </summary>
        /// <param name="file">The file to scan</param>
        public Task<FileScanResult> ScanFileAsync(FileInfo file)
        {
            if (!file.Exists)
            {
                throw new FileNotFoundException("The file was not found.", file.Name);
            }

            return ScanFileAsync(file.OpenRead(), file.Name);
        }

        /// <summary>
        /// Scan a file.
        /// Note: It is highly encouraged to get the report of the file before scanning, in case it has already been scanned before.
        /// Note: You are also strongly encouraged to provide the filename as it is rich metadata for the Virus Total database.
        /// </summary>
        /// <param name="file">The file to scan</param>
        /// <param name="filename">The filename of the file</param>
        public Task<FileScanResult> ScanFileAsync(byte[] file, string filename)
        {
            return ScanFileAsync(new MemoryStream(file), filename);
        }

        /// <summary>
        /// Scan a file.
        /// Note: It is highly encouraged to get the report of the file before scanning, in case it has already been scanned before.
        /// Note: You are also strongly encouraged to provide the filename as it is rich metadata for the Virus Total database.
        /// </summary>
        /// <param name="stream">The file to scan</param>
        /// <param name="filename">The filename of the file</param>
        public async Task<FileScanResult> ScanFileAsync(Stream stream, string filename)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream), "You must provide a stream that is not null");
            }

            if (stream.Length <= 0)
            {
                throw new ArgumentException("You must provide a stream with content", nameof(stream));
            }

            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentException("You must provide a filename. Preferably the original filename.");
            }

            var url = await GetResult<UploadUrlResult>($"{_baseUrl}/ui/files/upload_url", HttpMethod.Get, null);

            var multi = new MultipartFormDataContent
            {
                CreateFileContent(stream, filename),
                CreateItemPart()
            };
            var upload = await GetResult<UploadResult>(url.Data, HttpMethod.Post, multi);

            var result = await AnalyzeFileAsync(upload.Data.Id);
            return result;
        }

        public async Task<FileScanResult> AnalyzeFileAsync(string id)
        {
            var result = await GetResult<FileScanResult>($"{_baseUrl}/ui/analyses/{id}", HttpMethod.Get, null);
            return result;
        }
        #endregion

        #region FileReport
        /// <summary>
        /// Gets the report of the file.
        /// Note: This does not send the files to VirusTotal. It hashes the file and sends that instead.
        /// </summary>
        /// <param name="file">The file you want to get a report on.</param>
        public async Task<FileReport> GetFileReportAsync(byte[] file)
        {
            return await GetFileReportAsync(ResourcesHelper.GetResourceIdentifier(file));
        }

        /// <summary>
        /// Gets the report of the file.
        /// Note: This does not send the files to VirusTotal. It hashes the file and sends that instead.
        /// </summary>
        /// <param name="file">The file you want to get a report on.</param>
        public async Task<FileReport> GetFileReportAsync(System.IO.FileInfo file)
        {
            return await GetFileReportAsync(ResourcesHelper.GetResourceIdentifier(file));
        }

        /// <summary>
        /// Gets the report of the file.
        /// Note: This does not send the files to VirusTotal. It hashes the file and sends that instead.
        /// </summary>
        /// <param name="stream">The stream you want to get a report on.</param>
        public async Task<FileReport> GetFileReportAsync(Stream stream)
        {
            return await GetFileReportAsync(ResourcesHelper.GetResourceIdentifier(stream));
        }

        /// <summary>
        /// Gets the report of the file.
        /// Note: This does not send the files to VirusTotal. It hashes the file and sends that instead.
        /// </summary>
        /// <param name="resource">The resource (MD5, SHA1 or SHA256) you wish to get a report on.</param>
        public async Task<FileReport> GetFileReportAsync(string resource)
        {
            resource = ResourcesHelper.ValidateResources(resource, ResourceType.AnyHash | ResourceType.ScanId);

            var result = await GetResult<FileReport>($"{_baseUrl}/ui/files/{resource}", HttpMethod.Get, null);
            return result;
        }

        /// <summary>
        /// Gets a list of reports of the files.
        /// Note: This does not send the files to VirusTotal. It hashes the files and sends them instead.
        /// </summary>
        /// <param name="files">The files you want to get reports on.</param>
        public Task<IEnumerable<FileReport>> GetFileReportsAsync(IEnumerable<byte[]> files)
        {
            return GetFileReportsAsync(ResourcesHelper.GetResourceIdentifier(files));
        }

        /// <summary>
        /// Gets a list of reports of the files.
        /// Note: This does not send the files to VirusTotal. It hashes the files and sends them instead.
        /// </summary>
        /// <param name="files">The files you want to get reports on.</param>
        public Task<IEnumerable<FileReport>> GetFileReportsAsync(IEnumerable<FileInfo> files)
        {
            return GetFileReportsAsync(ResourcesHelper.GetResourceIdentifier(files));
        }

        /// <summary>
        /// Gets a list of reports of the files.
        /// Note: This does not send the content of the streams to VirusTotal. It hashes the content of the stream and sends that instead.
        /// </summary>
        /// <param name="streams">The streams you want to get reports on.</param>
        public Task<IEnumerable<FileReport>> GetFileReportsAsync(IEnumerable<Stream> streams)
        {
            return GetFileReportsAsync(ResourcesHelper.GetResourceIdentifier(streams));
        }

        /// <summary>
        /// Gets the report of the file represented by its hash or scan ID.
        /// Keep in mind that URLs sent using the API have the lowest scanning priority, depending on VirusTotal's load, it may take several hours before the file is scanned,
        /// so query the report at regular intervals until the result shows up and do not keep submitting the file over and over again.
        /// </summary>
        /// <param name="resourceList">SHA1, MD5 or SHA256 of the file. It can also be a scan ID of a previous scan.</param>
        public async Task<IEnumerable<FileReport>> GetFileReportsAsync(IEnumerable<string> resourceList)
        {
            resourceList = ResourcesHelper.ValidateResourcea(resourceList, ResourceType.AnyHash | ResourceType.ScanId);

            string[] resources = resourceList as string[] ?? resourceList.ToArray();

            //IEnumerable<Task<FileReport>> list = from res in resources select GetFileReportAsync(res);
            IEnumerable<Task<FileReport>> list = resources.Select(async res => await GetFileReportAsync(res));

            return await Task.WhenAll(list);
        }
        #endregion

        // Url
        #region ScanUrl
        /// <summary>
        /// Scan the given URL. The URL will be downloaded by VirusTotal and processed.
        /// Note: Before performing your submission, you should retrieve the latest report on the URL.
        /// </summary>
        /// <param name="url">The URL to process.</param>
        public async Task<UrlScanResult> ScanUrlAsync(string url)
        {
            url = ResourcesHelper.ValidateResources(url, ResourceType.URL);

            var upload = await GetResult<UploadResult>($"{_baseUrl}/ui/urls?url={WebUtility.UrlEncode(url)}", HttpMethod.Post, null);
            var result = await AnalyzeUrlAsync(upload.Data.Id);
            return result;
        }

        public async Task<UrlScanResult> AnalyzeUrlAsync(string id)
        {
            var result = await GetResult<UrlScanResult>($"{_baseUrl}/ui/analyses/{id}", HttpMethod.Get, null);
            return result;
        }

        /// <summary>
        /// Scan the given URL. The URL will be downloaded by VirusTotal and processed.
        /// Note: Before performing your submission, you should retrieve the latest report on the URL.
        /// </summary>
        /// <param name="url">The URL to process.</param>
        public Task<UrlScanResult> ScanUrlAsync(Uri url)
        {
            return ScanUrlAsync(url.ToString());
        }

        /// <summary>
        /// Scan the given URLs. The URLs will be downloaded by VirusTotal and processed.
        /// Note: Before performing your submission, you should retrieve the latest reports on the URLs.
        /// </summary>
        /// <param name="urls">The URLs to process.</param>
        public async Task<IEnumerable<UrlScanResult>> ScanUrlsAsync(IEnumerable<string> urls)
        {
            urls = ResourcesHelper.ValidateResourcea(urls, ResourceType.URL);

            string[] urlCast = urls as string[] ?? urls.ToArray();

            IEnumerable<Task<UrlScanResult>> list = urlCast.Select(async res => await ScanUrlAsync(res));

            return await Task.WhenAll(list);
        }

        /// <summary>
        /// Scan the given URLs. The URLs will be downloaded by VirusTotal and processed.
        /// Note: Before performing your submission, you should retrieve the latest reports on the URLs.
        /// </summary>
        /// <param name="urlList">The URLs to process.</param>
        public Task<IEnumerable<UrlScanResult>> ScanUrlsAsync(IEnumerable<Uri> urlList)
        {
            return ScanUrlsAsync(urlList.Select(x => x.ToString()));
        }
        #endregion

        #region GetUrlReport
        /// <summary>
        /// Gets a scan report from an URL
        /// </summary>
        /// <param name="url">The URL you wish to get the report on.</param>
        /// <param name="scanIfNoReport">Set to true if you wish VirusTotal to scan the URL if it is not present in the database.</param>
        public async Task<UrlReport> GetUrlReportAsync(string url)
        {
            string hash = ResourcesHelper.IsValidURL(url, out url) ? HashHelper.GetSHA256(url) : url;

            hash = ResourcesHelper.ValidateResources(hash, ResourceType.AnyHash | ResourceType.ScanId);

            //Output
            return await GetResult<UrlReport>($"{_baseUrl}/ui/urls/{hash}", HttpMethod.Get, null);
        }

        /// <summary>
        /// Gets a scan report from an URL
        /// </summary>
        /// <param name="url">The URL you wish to get the report on.</param>
        /// <param name="scanIfNoReport">Set to true if you wish VirusTotal to scan the URL if it is not present in the database.</param>
        public Task<UrlReport> GetUrlReportAsync(Uri url)
        {
            return GetUrlReportAsync(url.ToString());
        }

        /// <summary>
        /// Gets a scan report from a list of URLs
        /// </summary>
        /// <param name="urls">The URLs you wish to get the reports on.</param>
        /// <param name="scanIfNoReport">Set to true if you wish VirusTotal to scan the URLs if it is not present in the database.</param>
        public async Task<IEnumerable<UrlReport>> GetUrlReportsAsync(IEnumerable<string> urls)
        {
            urls = ResourcesHelper.ValidateResourcea(urls, ResourceType.URL);

            string[] urlCast = urls as string[] ?? urls.ToArray();

            IEnumerable<Task<UrlReport>> list = urlCast.Select(async res => await GetUrlReportAsync(res));

            return await Task.WhenAll(list);
        }

        /// <summary>
        /// Gets a scan report from a list of URLs
        /// </summary>
        /// <param name="urlList">The URLs you wish to get the reports on.</param>
        /// <param name="scanIfNoReport">Set to true if you wish VirusTotal to scan the URLs if it is not present in the database.</param>
        public Task<IEnumerable<UrlReport>> GetUrlReportsAsync(IEnumerable<Uri> urlList)
        {
            return GetUrlReportsAsync(urlList.Select(x => x.ToString()));
        }
        #endregion

        // IP
        #region GetIPReport
        /// <summary>
        /// Gets a scan report from an IP
        /// </summary>
        /// <param name="ip">The IP you wish to get the report on.</param>
        public Task<IPReport> GetIPReportAsync(string ip)
        {
            ip = ResourcesHelper.ValidateResources(ip, ResourceType.IP);

            return GetResult<IPReport>($"{_baseUrl}/ui/ip_addresses/{ip}", HttpMethod.Get, null);
        }

        /// <summary>
        /// Gets a scan report from an IP
        /// </summary>
        /// <param name="ip">The IP you wish to get the report on.</param>
        public Task<IPReport> GetIPReportAsync(IPAddress ip)
        {
            return GetIPReportAsync(ip.ToString());
        }
        #endregion

        // Domain
        #region GetDomainReport
        /// <summary>
        /// Gets a scan report from a domain
        /// </summary>
        /// <param name="domain">The domain you wish to get the report on.</param>
        public Task<DomainReport> GetDomainReportAsync(string domain)
        {
            domain = ResourcesHelper.ValidateResources(domain, ResourceType.Domain);

            return GetResult<DomainReport>($"{_baseUrl}/ui/domains/{domain}", HttpMethod.Get, null);
        }

        /// <summary>
        /// Gets a scan report from a domain
        /// </summary>
        /// <param name="domain">The domain you wish to get the report on.</param>
        public Task<DomainReport> GetDomainReportAsync(Uri domain)
        {
            return GetDomainReportAsync(domain.Host);
        }
        #endregion

        // Utilities
        #region GetPublicFileScanLink
        /// <summary>
        /// Gives you a link to a file analysis based on its hash.
        /// </summary>
        public string GetPublicFileScanLink(string resource)
        {
            resource = ResourcesHelper.ValidateResources(resource, ResourceType.AnyHash);

            return ResourcesHelper.NormalizeUrl($"www.virustotal.com/gui/file/{resource}/detection");
        }

        /// <summary>
        /// Gives you a link to a file analysis based on its hash.
        /// Note: This actually hashes the file - if you have the hash already, use the overload that takes in a string.
        /// </summary>
        public string GetPublicFileScanLink(System.IO.FileInfo file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            if (!file.Exists)
                throw new FileNotFoundException("The file you provided does not exist.", file.FullName);

            return GetPublicFileScanLink(ResourcesHelper.GetResourceIdentifier(file));
        }

        /// <summary>
        /// Gives you a link to a URL analysis.
        /// </summary>
        /// <returns>A link to VirusTotal that contains the report</returns>
        public string GetPublicUrlScanLink(string url)
        {
            url = ResourcesHelper.ValidateResources(url, ResourceType.URL);

            return ResourcesHelper.NormalizeUrl($"www.virustotal.com/gui/url/{ResourcesHelper.GetResourceIdentifier(url)}/detection");
        }
        #endregion

        #region Request Utilities
        private async Task<T> GetResult<T>(string url, HttpMethod method, HttpContent content)
        {
            HttpResponseMessage response = await SendRequest(url, method, content).ConfigureAwait(false);

            using (Stream responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                using (var sr = new StreamReader(responseStream, Encoding.UTF8))
                {
                    using (var jsonTextReader = new JsonTextReader(sr))
                    {
                        jsonTextReader.CloseInput = false;

                        SaveResponse(responseStream);

                        return _serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }
        }

        private async Task<HttpResponseMessage> SendRequest(string url, HttpMethod method, HttpContent content)
        {
            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };

            OnHTTPRequestSending?.Invoke(request);

            HttpResponseMessage response = await _client.SendAsync(request).ConfigureAwait(false);

            OnHTTPResponseReceived?.Invoke(response);

            if (response.StatusCode == HttpStatusCode.NotFound)
                throw new NotFoundException("Not Found");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("Error response code: " + response.StatusCode);
            }

            if (string.IsNullOrWhiteSpace(response.Content.ToString()))
            {
                throw new Exception("There were no content in the response.");
            }

            return response;
        }

        private void SaveResponse(Stream stream)
        {
            if (OnRawResponseReceived == null)
            {
                return;
            }

            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                OnRawResponseReceived(ms.ToArray());
            }

            stream.Position = 0;
        }

        private HttpContent CreateItemPart()
        {
            HttpContent content = new StringContent("undefined");
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"item\""
            };
            content.Headers.ContentType = null;
            return content;
        }

        private HttpContent CreateFileContent(Stream stream, string fileName)
        {
            var fileContent = new StreamContent(stream);
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "\"file\"",
                FileName = "\"" + fileName + "\"",
                //Size = stream.Length // caused malformed request body
            };
            fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return fileContent;
        }
        #endregion

        public void Dispose()
        {
            _client.Dispose();
            _httpClientHandler.Dispose();
        }
    }
}