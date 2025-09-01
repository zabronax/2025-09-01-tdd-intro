namespace Tests;

public class Example
{
    [Fact]
    public void TwoPlusTwo_Is_Four()
    {
        // Arrange
        int a = 2;
        int b = 2;

        // Act
        var result = a + b;

        // Assert
        Assert.Equal(4, result);
    }

    [Fact]
    public void ThreePlusThree_Is_Six()
    {
        // Arrange
        int a = 3;
        int b = 3;
        int expected = 6;

        // Act
        var result = a + b;

        // Assert
        Assert.Equal(expected, result);
    }
}
