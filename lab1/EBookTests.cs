using Xunit;

public class EBookTests
{
    [Fact]
    public void DefaultConstructor_ShouldSetDefaults()
    {
        var book = new EBook();
        Assert.Equal("Model", book.Model);
        Assert.Equal(1, book.Weight);
        Assert.Equal(1000, book.Price);
        Assert.Equal(1000, book.Storage);
        Assert.False(book.TouchScreen);
    }

    [Fact]
    public void Constructor_ShouldSetValidValues()
    {
        var book = new EBook(2000, true, "Kindle", 300, 15000);
        Assert.Equal("Kindle", book.Model);
        Assert.Equal(300, book.Weight);
        Assert.Equal(15000, book.Price);
        Assert.Equal(2000, book.Storage);
        Assert.True(book.TouchScreen);
    }

    [Fact]
    public void Constructor_ShouldCorrectInvalidValues()
    {
        var book = new EBook(50, false, "", -5, 10);
        Assert.Equal("Model", book.Model);
        Assert.Equal(1, book.Weight);
        Assert.Equal(1000, book.Price);
        Assert.Equal(1000, book.Storage);
        Assert.False(book.TouchScreen);
    }
}
