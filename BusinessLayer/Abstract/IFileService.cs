using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
	public interface IFileService
	{
		void FileDelete(string path);
		Task<string> FileSave(IFormFile file);
	}
}