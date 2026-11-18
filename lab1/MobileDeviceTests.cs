using Xunit;

public class MobileDeviceTests
{
    [Fact]
    public void DefaultConstructor_ShouldSetDefaults()
    {
        var device = new MobileDevice();
        Assert.Equal("Model", device.Model);
        Assert.Equal(1, device.Weight);
        Assert.Equal(1000, device.Price);
    }

    [Fact]
    public void Constructor_ShouldSetValidValues()
    {
        var device = new MobileDevice("iPhone", 200, 50000);
        Assert.Equal("iPhone", device.Model);
        Assert.Equal(200, device.Weight);
        Assert.Equal(50000, device.Price);
    }

    [Fact]
    public void Constructor_ShouldCorrectInvalidValues()
    {
        var device = new MobileDevice("", -10, 10);
        Assert.Equal("Model", device.Model);
        Assert.Equal(1, device.Weight);
        Assert.Equal(1000, device.Price);
    }

    [Fact]
    public void MobileDevice_ChangeMethods_UpdatePropertiesCorrectly()
    {
        var device = new MobileDevice("Old", 100, 1000);

        device.ChangeModel("NewModel");
        device.ChangeWeight(200);
        device.ChangePrice(5000);

        Assert.Equal("NewModel", device.Model);
        Assert.Equal(200, device.Weight);
        Assert.Equal(5000, device.Price);
    }

}
