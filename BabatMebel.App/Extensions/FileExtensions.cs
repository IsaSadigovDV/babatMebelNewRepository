namespace BabatMebel.App.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> SaveFileAsync(this IFormFile file, string root, string path)
        {
            if (file == null) throw new ArgumentNullException(nameof(file));
            if (root == null) throw new ArgumentNullException(nameof(root));
            if (path == null) throw new ArgumentNullException(nameof(path));

            string directory = Path.Combine(root, path);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string extension = Path.GetExtension(file.FileName);
            string filename = $"{Guid.NewGuid()}{extension}"; // unique olsun deye evveline guid yazdiq her defe yeni bir guid generate olacaq ve bu da onu temin edir ki hec vaxt file adlarini eyni olmayacaq

            string fullPath = Path.Combine(directory, filename);

            try
            {
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return Path.Combine(path, filename).Replace("\\", "/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
