using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Servise.Helper
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // Get Folder path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot//Files" ,folderName);

            // Get File Name
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            // combine folderPath + filePath
            var filePath = Path.Combine(folderPath,fileName);

            // save file
            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return filePath ;

        }
    }
}
