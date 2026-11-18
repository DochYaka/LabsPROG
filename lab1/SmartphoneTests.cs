using Xunit;

public class SmartphoneTests
{
    [Fact]
    public void DefaultConstructor_ShouldSetDefaults()
    {
        var phone = new Smartphone();
        Assert.Equal("Model", phone.Model);
        Assert.Equal(1, phone.Weight);
        Assert.Equal(1000, phone.Price);
        Assert.Equal(5, phone.ScreenSize);
        Assert.Equal(TechOfScreen.Undefined, phone.ScreenTech);
    }

    [Fact]
    public void Constructor_ShouldSetValidValues()
    {
        var phone = new Smartphone(6, TechOfScreen.OLED, "Pixel", 180, 40000);
        Assert.Equal("Pixel", phone.Model);
        Assert.Equal(180, phone.Weight);
        Assert.Equal(40000, phone.Price);
        Assert.Equal(6, phone.ScreenSize);
        Assert.Equal(TechOfScreen.OLED, phone.ScreenTech);
    }

    [Fact]
    public void Constructor_ShouldCorrectInvalidValues()
    {
        var phone = new Smartphone(50, (TechOfScreen)99, "", -1, 10);
        Assert.Equal("Model", phone.Model);
        Assert.Equal(1, phone.Weight);
        Assert.Equal(1000, phone.Price);
        Assert.Equal(5, phone.ScreenSize);
        Assert.Equal(TechOfScreen.Undefined, phone.ScreenTech);
    }
}
