using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Dtos
{
    public class GuideListDto : Guide, IDto
    {
        public IFormFile Image { get; set; }
    }
}