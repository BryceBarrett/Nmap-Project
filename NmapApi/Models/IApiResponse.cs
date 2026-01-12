namespace NmapApi.Models
{
    public interface IApiResponse
    {
        bool IsSuccess { get; set; }
        string ErrorMessage { get; set; }
        List<NmapResult> Results { get; set; }
    }
}
