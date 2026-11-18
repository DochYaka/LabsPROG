// Smartphone.cs
using System;

public enum TechOfScreen { Undefined, LCD, OLED, AMOLED }

public class Smartphone : MobileDevice
{
    public int ScreenSize { get; private set; }
    public TechOfScreen ScreenTech { get; private set; }

    public Smartphone()
        : base("Model", 1, 1000)
    {
        ScreenSize = 5;
        ScreenTech = TechOfScreen.Undefined;
    }

    public Smartphone(int screenSize, TechOfScreen screenTech, string model, int weight, int price)
        : base(model, weight, price)
    {
        ScreenSize = (screenSize < 1 || screenSize > 10) ? 5 : screenSize;
        ScreenTech = (screenTech == TechOfScreen.LCD || screenTech == TechOfScreen.OLED || screenTech == TechOfScreen.AMOLED)
            ? screenTech : TechOfScreen.Undefined;
    }

    public override void Print()
    {
        base.Print();
        Console.WriteLine($"Размер экрана: {ScreenSize} дюймов");
        Console.WriteLine($"Тип экрана: {ScreenTech}");
    }
}
