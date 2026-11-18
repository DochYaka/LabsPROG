using System;
using System.IO;
using Xunit;

public class InputFuncTests
{
    [Fact]
    public void ReadString_ShouldReturnDefault_WhenEmpty()
    {
        var sw = new StringWriter();
        var sr = new StringReader("\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        string result = InputFunc.ReadString("Введите модель: ");
        Assert.Equal("Model", result);
    }

    [Fact]
    public void ReadString_ShouldReturnInput_WhenValid()
    {
        var sw = new StringWriter();
        var sr = new StringReader("Samsung\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        string result = InputFunc.ReadString("Введите модель: ");
        Assert.Equal("Samsung", result);
    }

    [Fact]
    public void ReadInt_ShouldAcceptValidNumber()
    {
        var sw = new StringWriter();
        var sr = new StringReader("5\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        int result = InputFunc.ReadInt("Введите число: ", 1, 10);
        Assert.Equal(5, result);
    }

    [Fact]
    public void ReadInt_ShouldRetryUntilValid()
    {
        var sw = new StringWriter();
        var sr = new StringReader("abc\n100\n");
        Console.SetOut(sw);
        Console.SetIn(sr);

        int result = InputFunc.ReadInt("Введите число: ", 50, 200);
        Assert.InRange(result, 50, 200);
    }
}
