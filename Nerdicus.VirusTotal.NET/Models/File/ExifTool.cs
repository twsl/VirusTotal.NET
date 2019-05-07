using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Nerdicus.VirusTotalNET.Models.File
{
    public class ExifTool
    {
        [JsonProperty("BuildID")]
        public string BuildID { get; set; }

        [JsonProperty("CharacterSet")]
        public string CharacterSet { get; set; }

        [JsonProperty("CodeSize")]
        public string CodeSize { get; set; }

        [JsonProperty("CompanyName")]
        public string CompanyName { get; set; }

        [JsonProperty("EntryPoint")]
        public string EntryPoint { get; set; }

        [JsonProperty("FileFlagsMask")]
        public string FileFlagsMask { get; set; }

        [JsonProperty("FileOS")]
        public string FileOS { get; set; }

        [JsonProperty("FileSubtype")]
        public string FileSubtype { get; set; }

        [JsonProperty("FileType")]
        public string FileType { get; set; }

        [JsonProperty("FileTypeExtension")]
        public string FileTypeExtension { get; set; }

        [JsonProperty("FileVersion")]
        public string FileVersion { get; set; }

        [JsonProperty("FileVersionNumber")]
        public string FileVersionNumber { get; set; }

        [JsonProperty("ImageFileCharacteristics")]
        public string ImageFileCharacteristics { get; set; }

        [JsonProperty("ImageVersion")]
        public string ImageVersion { get; set; }

        [JsonProperty("InitializedDataSize")]
        public string InitializedDataSize { get; set; }

        [JsonProperty("LanguageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("LegalCopyright")]
        public string LegalCopyright { get; set; }

        [JsonProperty("LegalTrademarks")]
        public string LegalTrademarks { get; set; }

        [JsonProperty("LinkerVersion")]
        public string LinkerVersion { get; set; }

        [JsonProperty("MIMEType")]
        public string MIMEType { get; set; }

        [JsonProperty("MachineType")]
        public string MachineType { get; set; }

        [JsonProperty("OSVersion")]
        public string OSVersion { get; set; }

        [JsonProperty("ObjectFileType")]
        public string ObjectFileType { get; set; }

        [JsonProperty("OriginalFileName")]
        public string OriginalFileName { get; set; }

        [JsonProperty("PEType")]
        public string PEType { get; set; }

        [JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [JsonProperty("ProductVersion")]
        public string ProductVersion { get; set; }

        [JsonProperty("ProductVersionNumber")]
        public string ProductVersionNumber { get; set; }

        [JsonProperty("Subsystem")]
        public string Subsystem { get; set; }

        [JsonProperty("SubsystemVersion")]
        public string SubsystemVersion { get; set; }

        [JsonProperty("TimeStamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("UninitializedDataSize")]
        public string UninitializedDataSize { get; set; }
    }
}
