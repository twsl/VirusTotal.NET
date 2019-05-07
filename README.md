# Nerdicus.VirusTotal.NET
[![Build Status](https://dev.azure.com/twsl/VirusTotal.NET/_apis/build/status/twsI.VirusTotal.NET?branchName=master)](https://dev.azure.com/twsl/VirusTotal.NET/_build/latest?definitionId=2&branchName=master)
[![Nuget](https://img.shields.io/nuget/v/Nerdicus.VirusTotal.NET.svg)](https://www.nuget.org/packages/Nerdicus.VirusTotal.NET/)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Nerdicus.VirusTotal.NET.svg)](https://www.nuget.org/packages/Nerdicus.VirusTotal.NET/)

Interact with a implementation of the public interface without the struggle of API limitations.
This repository is based on https://github.com/Genbox/VirusTotal.Net/ which should be used for most use cases.

## Features
* Fully asynchronous API
* Scan and get reports of scanned files
* Scan and get reports of URLs
* Get reports for IP addresses and domains

## Example
```csharp
VirusTotal virusTotal = new VirusTotal();

//Create the EICAR test virus. See http://www.eicar.org/86-0-Intended-use.html
byte[] eicar = Encoding.ASCII.GetBytes(@"X5O!P%@AP[4\PZX54(P^)7CC)7}$EICAR-STANDARD-ANTIVIRUS-TEST-FILE!$H+H*");

//Check if the file has been scanned before.
var fileReport = await virusTotal.GetFileReportAsync(eicar);

Console.WriteLine($"Seen before: {fileReport.Data.Attributes.LastAnalysisDate}");
```
