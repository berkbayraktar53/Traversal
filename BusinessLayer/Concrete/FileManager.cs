using System;
using System.IO;
using BusinessLayer.Abstract;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Concrete
{
	public class FileManager : IFileService
	{
		public string UserFileSave(IFormFile file, string oldFileName)
		{
			var resource = Directory.GetCurrentDirectory();
			var oldImagePath = resource + "/wwwroot/images/user/" + oldFileName;
			if (File.Exists(oldImagePath))
			{
				File.Delete(oldImagePath);
			}
			var extension = Path.GetExtension(file.FileName);
			var newImageName = Guid.NewGuid() + extension;
			var saveLocation = resource + "/wwwroot/images/user/" + newImageName;
			var stream = new FileStream(saveLocation, FileMode.Create);
			file.CopyToAsync(stream);
			stream.Close();
			return newImageName;
		}
	}
}