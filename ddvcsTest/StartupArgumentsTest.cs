using System;
using System.Collections.Generic;
using System.Text;
using ddvcs;
using FluentAssertions;
using Xunit;

namespace ddvcsTest
{
    public class StartupArgumentsTest
    {
        [Fact]
        public void Creation_WithNoReadArgument_Defaults()
        {
            var sut = new StartupArguments();

            sut.ApiKey.Should().Be("");
            sut.AppKey.Should().Be("");
            sut.DashboardList.Should().Be("");
            sut.Folder.Should().Be("");
            sut.ShouldListDashboardLists.Should().BeFalse();
            sut.ShouldListDashboards.Should().BeFalse();
            sut.ShouldPullDashboards.Should().BeFalse();
            sut.ShouldValidateKey.Should().BeFalse();
        }

        [Fact]
        public void ReadArgument_WithNoArguments_Defaults()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[0];

            sut.ReadArguments(arguments);

            sut.ApiKey.Should().Be("");
            sut.AppKey.Should().Be("");
            sut.DashboardList.Should().Be("");
            sut.Folder.Should().Be("");
            sut.ShouldListDashboardLists.Should().BeFalse();
            sut.ShouldListDashboards.Should().BeFalse();
            sut.ShouldPullDashboards.Should().BeFalse();
            sut.ShouldValidateKey.Should().BeFalse();
        }

        [Fact]
        public void ReadArgument_WithApiKey_SetsApiKey()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[2] {"-ApiKey:Test", "-AppKey:SomeOtherValue" };
            sut.ReadArguments(arguments);

            sut.ApiKey.Should().Be("Test");
        }

        [Fact]
        public void ReadArgument_WithAppKey_SetsAppKey()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[2] { "-AppKey:Test", "-ApiKey:SomeOtherValue" };
            sut.ReadArguments(arguments);

            sut.AppKey.Should().Be("Test");
        }

        [Fact]
        public void ReadArgument_WithFolder_SetsFolder()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[3] { "-ApiKey:SomeValue", "-AppKey:SomeOtherValue", "-Folder:Test" };
            sut.ReadArguments(arguments);

            sut.Folder.Should().Be("Test");
        }

        [Fact]
        public void ReadArgument_WithDashboardList_SetsDashboardList()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[3] { "-ApiKey:SomeValue", "-AppKey:SomeOtherValue", "-DashboardList:Test" };
            sut.ReadArguments(arguments);

            sut.DashboardList.Should().Be("Test");
        }

        [Fact]
        public void ReadArgument_WithValidateKey_SetsValidateKey()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[2] { "-ValidateKey", "-ApiKey:SomeValue" };
            sut.ReadArguments(arguments);

            sut.ShouldValidateKey.Should().BeTrue();
        }

        [Fact]
        public void ReadArgument_WithList_SetsListDashboardLists()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[3] { "-ApiKey:SomeValue", "-AppKey:SomeOtherValue", "-List" };
            sut.ReadArguments(arguments);

            sut.ShouldListDashboardLists.Should().BeTrue();
        }

        [Fact]
        public void ReadArgument_WithContent_SetsListDashboards()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[4] { "-ApiKey:SomeValue", "-AppKey:SomeOtherValue", "-Content", "-DashboardList:Test" };
            sut.ReadArguments(arguments);

            sut.DashboardList.Should().Be("Test");
            sut.ShouldListDashboards.Should().BeTrue();
        }

        [Fact]
        public void ReadArgument_WithPull_SetsPullDashboards()
        {
            var sut = new StartupArguments();

            string[] arguments = new string[5] { "-ApiKey:SomeValue", "-AppKey:SomeOtherValue", "-Pull", "-DashboardList:Test", "-Folder:TestFolder" };
            sut.ReadArguments(arguments);

            sut.ShouldPullDashboards.Should().BeTrue();
            sut.DashboardList.Should().Be("Test");
            sut.Folder.Should().Be("TestFolder");
        }
    }
}
