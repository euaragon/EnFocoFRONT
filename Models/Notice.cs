using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EnFocoFRONT.Models
{

    public enum NoticeCategory
    {
        [Display(Name = "Tribunal de Cuentas")]
        tribunal,
        [Display(Name = "Fiscalía de Estado")]
        fiscalia,
        [Display(Name = "Ética Pública Mendoza")]
        etica
    }

    public enum NoticeSection
    {
        Editorial,
        Noticias,
        Analisis
    }

    public class Notice
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("subtitle")]
        public string? Subtitle { get; set; }

        [JsonPropertyName("issue")]
        public string? Issue { get; set; }

        [JsonPropertyName("text")]
        public string? Text { get; set; }

        [JsonPropertyName("img")]
        public string? Img { get; set; }

        [JsonPropertyName("isFeatured")]
        public bool IsFeatured { get; set; }

        [JsonPropertyName("category")]
        public int Category { get; set; } // El backend devuelve un entero

        [JsonPropertyName("section")]
        public int Section { get; set; } // El backend devuelve un entero

        [JsonPropertyName("imageUrl")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("imageFile")]
        public object? ImageFile { get; set; }
    }

    
}
