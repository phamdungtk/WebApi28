namespace WebApi28.Entities
{
    public class ResponseListMessage
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public long totalItem { get; set; }
        public dynamic data { get; set; }
    }
}
