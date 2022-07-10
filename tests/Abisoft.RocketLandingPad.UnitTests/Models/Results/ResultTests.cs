using Abisoft.RocketLandingPad.Models.Results;

namespace Abisoft.RocketLandingPad.UnitTests.Models.Results;

[Trait("Category", "Unit")]
public class ResultTests
{
    [Fact]
    public void Success_Property_IsAlwaysNewResult()
    {
        // Arrange
        var successResult = Result.Success;
        var expectedResult = new Result();

        // Act
        var result = successResult.Equals(expectedResult);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Result_WhenCreatedForSuccess_ThenDoesNotContainError()
    {
        // Arrange
        var expectedResult = Result.Success;

        // Act
        var result = new Result();

        // Assert
        result.IsError.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Result_WhenCreatedWithError_ThenContainsError()
    {
        // Arrange
        var error = new Exception("Test error");
        Result expectedResult = error;

        // Act
        var result = new Result(error);

        // Assert
        result.IsError.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GenericResult_WhenCreatedForSuccess_ThenDoesNotContainError()
    {
        // Arrange
        var expectedResult = new Result<bool>(true);

        // Act
        var result = new Result<bool>(true);

        // Assert
        result.IsError.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GenericResult_WhenCreatedWithError_ThenContainsError()
    {
        // Arrange
        var error = new Exception("Test error");
        Result<bool> expectedResult = error;

        // Act
        var result = new Result<bool>(error);

        // Assert
        result.IsError.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void GenericResult_ToResult_PerformsImplicitConversion()
    {
        // Arrange
        Result<bool> expectedResult = new Exception("Test error");

        // Act
        Result result = expectedResult;

        // Assert
        result.IsError.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().BeEquivalentTo(expectedResult.Error);
    }

    [Fact]
    public void GenericValue_ToGenericResult_PerformsConversion()
    {
        // Arrange
        const bool expectedResult = true;

        // Act
        Result<bool> result = expectedResult;

        // Assert
        result.IsError.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(expectedResult);
    }

    [Fact]
    public void GenericResult_ToGenericValue_PerformsConversion()
    {
        // Arrange
        var expectedResult = new Result<bool>(true);

        // Act
        bool result = expectedResult;

        // Assert
        result.Should().Be(expectedResult.Value);
    }
}
