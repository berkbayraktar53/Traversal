using System;
using System.IO;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Concrete
{
	public class FileManager : IFileService
	{
		public void FileDelete(string name)
		{
			var resource = Directory.GetCurrentDirectory();
			var path = resource + "/wwwroot/images/user/" + name;
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		public async Task<string> FileSave(IFormFile file)
		{
			var resource = Directory.GetCurrentDirectory();
			var extension = Path.GetExtension(file.FileName);
			var imageName = Guid.NewGuid() + extension;
			var saveLocation = resource + "/wwwroot/images/user/" + imageName;
			var stream = new FileStream(saveLocation, FileMode.Create);
			await file.CopyToAsync(stream);
			return imageName;
		}
	}
}