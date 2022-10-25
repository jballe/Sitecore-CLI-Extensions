using Clockify.Net;

namespace Timereg.Clockify.IntegrationTests.Commands.Tasks
{
    public class GetTasksTaskShould
    {
        private const string ApiKey = "Mzg4YzZmOWQtNTAxYy00MmEwLWI0NjEtNzAzZjg4MzRhMzVh";
        private const string Workspace = "62ffe4360e68ad2eb21f74a7";
        private const string Project = "630cb1424a05b20faf676617";

        [Fact]
        public async Task NativeClientCanGetTasks()
        {
            var sut = new ClockifyClient(ApiKey);
            var response = await sut.FindAllTasksAsync(Workspace, Project);
            var actual = response.Data;

            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
        }
    }
}
