using static SeearchForTheLargestFile_Test.MessageService.MessageService;
using static SeearchForTheLargestFile_Test.Helpers.FileHelpers;

InfoMessage("Приложение для поиска самого большого файла в указанной директории");
InfoMessage("Укажите директорию");

string? disk = Console.ReadLine();

try
{
    // Проверка на пустую строку или несуществующую директорию
    if (string.IsNullOrWhiteSpace(disk) || !Directory.Exists(disk))
        throw new ArgumentException("Указанной директории не существует. Проверьте правильность написания и попробуйте ещё раз");

    // Получаем информацию о диске
    DriveInfo[] drives = DriveInfo.GetDrives();
    DriveInfo? selectedDrive = drives.FirstOrDefault(d => d.Name.StartsWith(disk, StringComparison.OrdinalIgnoreCase))
        ?? throw new ArgumentException("Указанный диск не найден");

    // Получаем корневую директорию диска
    string rootDirectory = selectedDrive.RootDirectory.FullName;

    // Настройка параметров перебора файла
    EnumerationOptions enumerationOptions = new()
    {
        RecurseSubdirectories = true,
        AttributesToSkip = FileAttributes.Hidden | FileAttributes.System,
        MaxRecursionDepth = 100
    };

    // Получаем список файлов
    FileInfo largestFile = new DirectoryInfo(rootDirectory)
        .EnumerateFiles("*.*",  enumerationOptions)
        .Where(file => file.Length > 0 )
        .OrderByDescending(file => file.Length)
        .Select(file => new FileInfo(file.FullName))
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


