namespace SeearchForTheLargestFile_Test.Helpers
{
    public class FileHelpers
    {
        /// <summary>
        /// Формат отображения размера файла
        /// </summary>
        /// <param name="bytes">Размер файла</param>
        /// <returns>Строка для отображения</returns>
        public static string FormatFileSize(long bytes)
        {
            string[] suffixes = ["B", "KB", "MB", "GB", "TB"];

            int suffixIndex = 0;
            double size = bytes;

            while (size >= 1024 && suffixIndex < suffixes.Length - 1)
            {
                suffixIndex++;
                size /= 1024;
            }

            return $"{size:0.##} {suffixes[suffixIndex]}";
        }
    }
}
