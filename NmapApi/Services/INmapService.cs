using NmapApi.Models;

namespace NmapApi.Services
{
    public interface INmapService
    {
        Task<List<NmapResult>> GetAllAsync();
        Task<List<NmapResult>> GetAsync(string hostName);
        Task CreateAsync(NmapResult scanResult);
        Task CreateAsync(List<NmapResult> scanResults);
        Task<NmapResult?> GetMostRecentAsync(string hostName);
        Task<List<NmapResult>> GetResultsByHostName(string hostName);
    }
}
