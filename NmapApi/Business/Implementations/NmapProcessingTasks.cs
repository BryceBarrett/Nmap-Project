using NmapApi.Helpers.CompareExtensions;
using NmapApi.Models;
using NmapApi.Services;

namespace NmapApi.Business
{
    public class NmapProcessingTasks : INmapProcessingTasks
    {
        private readonly INmapService _nmapService;

        public NmapProcessingTasks(INmapService nmapService) =>
            _nmapService = nmapService;

        public async Task<ApiResponse> SendNmapRequest(string hostName)
        {
            try
            {
                // Keeping this simple and only allowing 1 host/IP to be searched at a time. In the
                // future, we can add support for multiple hosts to be scanned at once.
                if (hostName.Contains(' '))
                {
                    return new ApiResponse()
                    {
                        IsSuccess = false,
                        ErrorMessage = $"Multiple host/IP mappings scans are currently not supported. Please try again."
                    };
                }

                var res = NmapHelper.RunNmapProcess(hostName);

                // If we have no results, don't save the request.
                if (res.Count > 0)
                    await _nmapService.CreateAsync(res);

                // We're going to simplify this and make the assumption that no results means
                // an invalid IP/Hostname was sent in. It's possible though that no ports are open.
                if (res.Count == 0)
                {
                    return new ApiResponse()
                    {
                        IsSuccess = false,
                        ErrorMessage = $"The hostname/IP address given ({hostName}) is invalid or has no ports open currently. Please try again."
                    };
                }

                return new ApiResponse()
                {
                    IsSuccess = true,
                    Results = res
                };
            }
            catch(Exception ex) {
                // Add logging here
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public async Task<ScanReport> SendNmapRequestWithReport(string hostName)
        {
            var newReport = new ScanReport() { HostName = hostName };

            // Keeping this simple and only allowing 1 host/IP to be searched
            if (hostName.Contains(' '))
            {
                newReport.ErrorMessage = "Please only pass in 1 IP/Host to generate a report.";
                return newReport;
            }

            // Retrieve most recent scan
            var recentScan = await _nmapService.GetMostRecentAsync(hostName);

            // Send error message and don't run nmap to save processing power since
            // there is nothing to compare the report to
            if (recentScan == null)
            {
                newReport.ErrorMessage = $"No recent scans exist for {hostName}. No report can be generated.";
                return newReport;
            }

            try
            {
                // Run a new scan
                var res = NmapHelper.RunNmapProcess(hostName).FirstOrDefault();

                // Save new nmap scan if we have one
                if (res != null)
                    await _nmapService.CreateAsync(res);

                // Gets items that are in the new nmap results open ports that don't exist
                // in the most recent scan results. This means a new port has been opened.
                newReport.NewOpenPorts = res.OpenPorts.Except(recentScan.OpenPorts, new PortComparer()).ToList();

                foreach (var port in recentScan.OpenPorts)
                {
                    // Checks if there doesn't exist a port within the new nmap result scan but
                    // was open is the most recent. That means the port was closed.
                    if (!res.OpenPorts.Exists(x => x.PortId == port.PortId))
                    {
                        port.IsOpen = false;
                        newReport.NewClosedPorts.Add(port);
                    }
                }

                // Build a human readable time difference between the scan results
                var timeDiff = res.ScanCompletedAt.Subtract(recentScan.ScanCompletedAt);
                newReport.TimeBetweenScans = $"It has been {timeDiff.Days} day(s), {timeDiff.Hours} hour(s)," +
                    $" {timeDiff.Minutes} minute(s), and {timeDiff.Seconds} second(s) since the last scan.";

                // Go ahead and set the recent scan object and return our result
                newReport.NewResult = res;
                return newReport;
            }
            catch (Exception ex) {
                // Add logging here
                return new ScanReport()
                {
                    HostName = hostName,
                    ErrorMessage = "Error processing your request. Error: " + ex.Message
                };
            }
        }

        public async Task<ApiResponse> GetAllNmapResults()
        {
            try
            {
                var res = await _nmapService.GetAllAsync();

                return new ApiResponse()
                {
                    IsSuccess = true,
                    Results = res
                };
            }
            catch (Exception ex)
            {
                // Add logging here
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public async Task<ApiResponse> GetNmapResultsByHostname(string hostName)
        {
            try
            {
                var res = await _nmapService.GetResultsByHostName(hostName);

                return new ApiResponse()
                {
                    IsSuccess = true,
                    Results = res
                };
            }
            catch (Exception ex)
            {
                // Add logging here
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.ToString()
                };
            }
        }

        public async Task<ApiResponse> GetMostRecentNmapResult(string hostName)
        {
            try
            {
                var recentScan = await _nmapService.GetMostRecentAsync(hostName);

                return new ApiResponse()
                {
                    IsSuccess = true,
                    Results = recentScan != null ? new List<NmapResult> { recentScan} : new List<NmapResult>()
                };
            }
            catch (Exception ex)
            {
                // Add logging here
                return new ApiResponse()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.ToString()
                };
            }
        }
    }
}