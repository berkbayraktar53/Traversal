using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Abstract
{
	public interface IFileService
	{
		void FileDelete(string imageName, string folderName);
		string UserFileSave(IFormFile file, string oldFileName);
		Task<string> FileSave(IFormFile file, string oldFileName, string folderName);
	}
}