namespace SearchForTheLargestFile_Test.Helpers
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
            if (bytes == 0) return "0";

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

        /// <summary>
        /// Поиск самого большого файла в директории
        /// </summary>
        /// <param name="directory">Путь к директории</param>
        /// <returns> Найденный самый больший файл</returns>
        public static FileInfo FindLargestFile(string directory)
        {
            if (string.IsNullOrWhiteSpace(directory)) return null!;

            DirectoryInfo dirInfo = new(directory);
            FileInfo? largestFile = null;

            try
            {
                largestFile = GetLargestFile(dirInfo.GetFiles());
                Console.WriteLine($"Пройденная директория: {directory}");

                var subDirectories = dirInfo.GetDirectories();
                if (subDirectories.Length != 0)
                {
                    largestFile = subDirectories                        
                        .Select(subDir => FindLargestFile(subDir.FullName))
                        .Concat(new[] { largestFile })
                        .OrderByDescending(file => file?.Length ?? 0)
                        .FirstOrDefault();
                }
            }
            catch (UnauthorizedAccessException)
            {
                // Пропускаем директорию, если у нас нет доступа к ней
            }

            return largestFile;
        }


        static FileInfo GetLargestFile(FileInfo[] files)
        {
            if (files.Length == 0) return null!;
            return files.OrderByDescending(f => f.Length).FirstOrDefault();
        }

    }
}
