// InputFunc.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InputFunc
{
    // Безопасный ввод числа с проверкой диапазона
    public static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value))
            {
                if (value >= min && value <= max)
                    return value;
                else
                    Console.WriteLine($"Введите число от {min} до {max}!");
            }
            else
            {
                Console.WriteLine("Неверный ввод — требуется число!");
            }
        }
    }

    // Ввод строки
    public static string ReadString(string prompt)
    {
        Console.Write(prompt);
        string s = Console.ReadLine();
        return string.IsNullOrWhiteSpace(s) ? "Model" : s;
    }
}
