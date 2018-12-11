using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;
using ddvcs;
using DataDog;

namespace ddvcsTest
{
    public class ListDashboardsUseCaseTests
    {
        [Fact]
        public void Execute_WithEmptyDashBoardList_ShouldReturnNoDashboardsAndWriteOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            dataDogGateway.Setup(useCase => useCase.GetDashboardListItems(It.IsAny<ulong>())).Returns(new List<DataDogDashboard>());
            dataDogGateway.Setup(useCase => useCase.GetDashboardLists()).Returns(
                new List<DataDogDashboardListsItem>() { new DataDogDashboardListsItem() { Name = "Test" } });

            ListDashboardsUseCase sut = new ListDashboardsUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute("Test").Should().BeFalse();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void Execute_WithExistingDashBoardList_ShouldReturnDashboardsAndWriteOutput()
        {
            var outputWriter = new Mock<IOutputWriter>();
            var dataDogGateway = new Mock<IDataDogGateway>();
            var dashboardList = new List<DataDogDashboard>();
            dashboardList.Add(new DataDogDashboard { Id = 1, Title = "foo" });
            dashboardList.Add(new DataDogDashboard { Id = 2, Title = "bar" });
            dataDogGateway.Setup(useCase => useCase.GetDashboardLists()).Returns(
                new List<DataDogDashboardListsItem>() { new DataDogDashboardListsItem() { Name = "Test" } });
            dataDogGateway.Setup(useCase => useCase.GetDashboardListItems(It.IsAny<ulong>())).Returns(dashboardList);

            ListDashboardsUseCase sut = new ListDashboardsUseCase(outputWriter.Object, dataDogGateway.Object);

            sut.Execute("Test").Should().BeTrue();
            outputWriter.Verify(o => o.Write(It.IsAny<string>()), Times.Exactly(dashboardList.Count));
        }
    }
}
