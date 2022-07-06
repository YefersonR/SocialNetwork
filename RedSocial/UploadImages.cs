using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RedSocial
{
    public class UploadImages
    {
        public string UploadImage(IFormFile file, int id, string folder = "", bool isEditMode = false, string imgUrl = "")
        {
            if (isEditMode && file == null)
            {
                return imgUrl;
            }
            string relativePath = $"/Images/{folder}/{id}";
            string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{relativePath}");
            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);

            string filename = guid + fileInfo.Extension;
            string FinalPath = Path.Combine(absolutePath, filename);

            using (var stream = new FileStream(FinalPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldPath = imgUrl.Split("/");
                string oldName = oldPath[^1];
                string FinalOldPath = Path.Combine(absolutePath, oldName);

                if (System.IO.File.Exists(FinalOldPath))
                {
                    System.IO.File.Delete(FinalOldPath);
                }
            }

            return $"{relativePath}/{filename}";
        }
    }
}
