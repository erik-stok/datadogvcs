using Xunit;
using Moq;
using ddvcs;
using DataDog;
using FluentAssertions;

namespace ddvcsTest
{
    public class ValidateKeyUseCaseTests
    {
        [Fact]
        public void Execute_WithInvalidKey_ShouldReturnInvalidAndWriteOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            dataDogGateway.Setup(useCase => useCase.ApiKeyIsValid()).Returns(false);

            ValidateKeyUseCase sut = new ValidateKeyUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute().Should().BeFalse();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Execute_WithValidKey_ShouldReturnValidAndWriteOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            dataDogGateway.Setup(useCase => useCase.ApiKeyIsValid()).Returns(true);

            ValidateKeyUseCase sut = new ValidateKeyUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute().Should().BeTrue();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Once());
        }
    }
}
