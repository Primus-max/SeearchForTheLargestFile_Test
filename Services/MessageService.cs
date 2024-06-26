﻿namespace SearchForTheLargestFile_Test.MessageService;

public class MessageService
{
    /// <summary>
    /// Выводит в консоль информационное сообщение
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void InfoMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
    }

    /// <summary>
    /// Выводит в консоль успешное сообщение
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void SuccessMessage(string message)
    {
        if(string.IsNullOrWhiteSpace(message)) return;
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
    }

    /// <summary>
    /// Выводит в консоль ошибку
    /// </summary>
    /// <param name="message">Сообщение</param>
    public static void ErrorMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message)) return;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Error.WriteLine(message);
    }
}

