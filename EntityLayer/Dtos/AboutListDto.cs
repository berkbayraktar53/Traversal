using Microsoft.AspNetCore.Http;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Dtos
{
	public class AboutListDto : IDto
	{
		public int Id { get; set; }
		public IFormFile Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public bool Status { get; set; }
	}
}