using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;
using ddvcs;
using DataDog;

namespace ddvcsTest
{
    public class ListDashboardListsUseCaseTests
    {
        [Fact]
        public void Execute_WithNoDashBoardLists_ShouldReturnNoListsAndWriteOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            dataDogGateway.Setup(useCase => useCase.GetDashboardLists()).Returns(new List<DataDogDashboardListsItem>());

            ListDashboardListsUseCase sut = new ListDashboardListsUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute().Should().BeFalse();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Execute_WithOneDashBoardList_ShouldReturnListsAndWriteSingleOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            var dashboardList = new List<DataDogDashboardListsItem>();
            dashboardList.Add(new DataDogDashboardListsItem { Id = 1, Name = "foo" }); 
            dataDogGateway.Setup(useCase => useCase.GetDashboardLists()).Returns(dashboardList);

            ListDashboardListsUseCase sut = new ListDashboardListsUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute().Should().BeTrue();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Execute_WithMultipleDashBoardLists_ShouldReturnListsAndWriteMultipleOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            var dashboardList = new List<DataDogDashboardListsItem>();
            dashboardList.Add(new DataDogDashboardListsItem { Id = 1, Name = "foo" });
            dashboardList.Add(new DataDogDashboardListsItem { Id = 2, Name = "bar" });
            dataDogGateway.Setup(useCase => useCase.GetDashboardLists()).Returns(dashboardList);

            ListDashboardListsUseCase sut = new ListDashboardListsUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute().Should().BeTrue();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Exactly(dashboardList.Count));
        }
    }
}
