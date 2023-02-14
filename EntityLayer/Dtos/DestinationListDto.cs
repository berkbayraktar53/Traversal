using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Dtos
{
    public class DestinationListDto : Destination, IDto
    {
        public IFormFile DestinationImage { get; set; }
    }
}