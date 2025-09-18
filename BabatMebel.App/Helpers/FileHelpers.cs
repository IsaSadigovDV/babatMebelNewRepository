namespace BabatMebel.App.Helpers
{
    public class FileHelpers
    {
        public static bool IsImage(IFormFile file)
        {
            if (file == null) return false;

            string[] allowedExtensions =
            {
                "image/jpeg","image/jpg","image/png"
            };

            return allowedExtensions.Contains(file.ContentType.ToLower());
        }

        public static bool IsSizeValid(IFormFile file, int maxMb)
        {
            if (file == null) return false;

            long maxSize = maxMb * 1024 * 1024;

            return file.Length <= maxSize;
        }
    }
}
