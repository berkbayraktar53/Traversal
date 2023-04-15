using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
	public interface IFileService
	{
		void DeleteUserPicture(string path);
		Task<string> AddUserPicture(IFormFile file);
	}
}