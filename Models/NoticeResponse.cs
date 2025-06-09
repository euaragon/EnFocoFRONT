using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnFocoFRONT.Models
{
    public class NoticeResponse
    {
        [JsonPropertyName("data")]
        public List<Notice>? Data { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}