// StructWages.cs

using System;
using System.Xml.Serialization;

[Serializable]
public struct Wages
{
    private double wages;

    [XmlElement]
    public double Value
    {
        get => wages;
        set => wages = (value < 999.9 || value > 999999.9) ? 999.0 : value;
    }

    public Wages(double wages)
    {
        this.wages = 1000.0;
        Value = wages;
    }

    public override string ToString() => $"{Value:N0} руб.";
}