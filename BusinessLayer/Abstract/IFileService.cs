using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
	public interface IFileService
	{
		string UserFileSave(IFormFile file, string oldFileName);
	}
}