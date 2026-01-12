namespace NmapApi.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public List<NmapResult> Results { get; set; }
    }
}
