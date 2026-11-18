// StructExperience.cs

using System;
using System.Xml.Serialization;

[Serializable]
public struct Experience
{
    private double experience;

    [XmlElement]
    public double Value
    {
        get => experience;
        set => experience = (value < 1.0 || value > 50) ? 1.0 : value;
    }

    public Experience(double experience)
    {
        this.experience = 1.0;
        Value = experience;
    }

    public override string ToString()
    {
        int years = (int)Value;

        string yearsWord;
        if (years == 1)
            yearsWord = "год";
        else if (years >= 2 && years <= 4)
            yearsWord = "года";
        else
            yearsWord = "лет";

        return $"{Value} {yearsWord}";
    }
}
