
namespace CBEsApi.Models
{
    public partial class Response
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}