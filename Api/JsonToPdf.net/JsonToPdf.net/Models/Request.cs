namespace JsonToPdf.net.Models
{
    public class Request
    {
        public string Data { get; set; }
        public string Template { get; set; }
    }

    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
