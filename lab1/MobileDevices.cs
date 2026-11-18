// MobileDevices.cs
using System;

public class MobileDevice
{
    public string Model { get; private set; }
    public int Weight { get; private set; }
    public int Price { get; private set; }

    public MobileDevice()
    {
        Model = "Model";
        Weight = 1;
        Price = 1000;
    }

    
    public MobileDevice(string model, int weight, int price)
    {
        Model = string.IsNullOrWhiteSpace(model) ? "Model" : model;
        Weight = (weight < 1 || weight > 2000) ? 1 : weight;
        Price = (price < 100 || price > 1000000) ? 1000 : price;
    }

    public virtual void Print()
    {
        Console.WriteLine($"\nМодель: {Model}");
        Console.WriteLine($"Вес: {Weight} г");
        Console.WriteLine($"Цена: {Price} руб.");
    }

    public void ChangeModel(string model) => Model = string.IsNullOrWhiteSpace(model) ? "Model" : model;
    public void ChangeWeight(int weight) => Weight = (weight < 1 || weight > 2000) ? 1 : weight;
    public void ChangePrice(int price) => Price = (price < 100 || price > 1000000) ? 1000 : price;
}
