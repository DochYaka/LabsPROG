// Ebook.cs
using System;

public class EBook : MobileDevice
{
    public int Storage { get; private set; }
    public bool TouchScreen { get; private set; }

    public EBook()
        : base("Model", 1, 1000)
    {
        Storage = 1000;
        TouchScreen = false;
    }

    public EBook(int storage, bool touchScreen, string model, int weight, int price)
        : base(model, weight, price)
    {
        Storage = (storage < 100 || storage > 10000) ? 1000 : storage;
        TouchScreen = touchScreen;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Объем памяти: {Storage} МБ");
        Console.WriteLine($"Сенсорный экран: {(TouchScreen ? "Есть" : "Нет")}");
    }
}
