using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Restaurant.Web.Helpers
{
    public static class FileHelper
    {
        public static async Task<(string fileName, string fileExtension)> CreateImageFile(string webRootPath, IFormFileCollection files, string folderPath)
        {
            var fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(webRootPath, folderPath);
            var extension = Path.GetExtension(files[0].FileName);
            await using var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create);
            await files[0].CopyToAsync(fileStream);

            return (fileName, extension);
        }
    }
}