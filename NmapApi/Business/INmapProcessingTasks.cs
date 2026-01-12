using NmapApi.Models;

namespace NmapApi.Business
{
    public interface INmapProcessingTasks
    {
        Task<ApiResponse> SendNmapRequest(string hostName);
        Task<ApiResponse> GetAllNmapResults();
        Task<ApiResponse> GetNmapResultsByHostname(string hostName);
        Task<ApiResponse> GetMostRecentNmapResult(string hostName);
        Task<ScanReport> SendNmapRequestWithReport(string hostName);
    }
}