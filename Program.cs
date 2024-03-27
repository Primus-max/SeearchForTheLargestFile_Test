using static SearchForTheLargestFile_Test.MessageService.MessageService;
using static SearchForTheLargestFile_Test.Helpers.FileHelpers;

InfoMessage("Приложение для поиска самого большого файла в указанной директории");
InfoMessage("Укажите директорию");

string? disk = Console.ReadLine();

try
{
    // Проверка на пустую строку или несуществующую директорию
    if (string.IsNullOrWhiteSpace(disk) || !Directory.Exists(disk))
        throw new ArgumentException("Указанной директории не существует");

    // Получаем информацию о диске
    DriveInfo[] drives = DriveInfo.GetDrives();
    DriveInfo? selectedDrive = drives.FirstOrDefault(d => d.Name.StartsWith(disk, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException("Указанный диск не найден");

    // Получаем корневую директорию диска
    string rootDirectory = selectedDrive.RootDirectory.FullName;
   
    // Получаем самый большой файл
    FileInfo largestFile = new DirectoryInfo(rootDirectory)
        .EnumerateFiles("*.*")
        .Where(file => file.Length > 0 )
        .OrderByDescending(file => file.Length)
        .First();

    string formattedSize = FormatFileSize(largestFile.Length);
    SuccessMessage($"Имя файла: {largestFile.Name}");
    SuccessMessage($"Размер файла: {formattedSize}");
}
catch (ArgumentException ex)
{
    ErrorMessage(ex.Message);
}
catch (InvalidOperationException ex)
{
    ErrorMessage(ex.Message);
}
catch (Exception ex)
{
    ErrorMessage($"Произошла ошибка: {ex.Message}");  
}

Console.ReadKey();


