using RestApiSample.Web.Utilities.Statics;

namespace RestApiSample.Web.Utilities.Extensions
{
    public static class ExtensionMethods
    {
        public static string AddIdToApiUrl(this string url, string id)
        {
            return $"{url}/{id}";
        }

        public static string GenerateProductImageFullPath(this string fileName)
        {
            return StaticDetails.SiteAddress + StaticDetails.ProductImagePath + fileName;
        }

        public static bool UploadFile(this IFormFile file, string fileName, string path,
            List<string>? validFormats = null)
        {
            if (validFormats != null && validFormats.Any())
            {
                var fileFormat = Path.GetExtension(file.FileName);

                if (validFormats.All(s => s != fileFormat))
                {
                    return false;
                }
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var finalPath = path + fileName;

            using var stream = new FileStream(finalPath, FileMode.Create);

            file.CopyTo(stream);

            return true;
        }

        public static void DeleteFile(this string fileName, string path)
        {
            var finalPath = path + fileName;

            if (File.Exists(finalPath))
            {
                File.Delete(finalPath);
            }
        }
    }
}
