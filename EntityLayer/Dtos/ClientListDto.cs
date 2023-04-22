using Microsoft.AspNetCore.Http;
using CoreLayer.EntityLayer.Abstract;

namespace EntityLayer.Dtos
{
	public class ClientListDto : IDto
	{
		public int Id { get; set; }
		public IFormFile ClientImage { get; set; }
		public string NameSurname { get; set; }
		public string ClientComment { get; set; }
		public bool Status { get; set; }
	}
}