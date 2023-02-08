using System;
using Microsoft.AspNetCore.Http;
using CoreLayer.EntityLayer.Abstract;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Dtos
{
    public class DestinationListDto : IDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Resim boş geçilemez")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Şehir boş geçilemez")]
        public string City { get; set; }

        [Required(ErrorMessage = "Açıklama boş geçilemez")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Gece gündüz boş geçilemez")]
        public string DayNight { get; set; }

        [Required(ErrorMessage = "Kapasite boş geçilemez")]
        public int Capacity { get; set; }

        [Required(ErrorMessage = "Fiyat boş geçilemez")]
        public decimal Price { get; set; }

        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}