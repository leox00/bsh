using BSH.Core;

namespace BSH.Tests
{
    [TestFixture]
    public class CommandHandlerTests
    {
        private CommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new CommandHandler();
        }

        [Test]
        public void Execute_ValidCommand_ShouldNotThrowException()
        {
            // Arrange
            string command = "echo Hello NUnit";

            // Act
            TestDelegate act = () => _handler.ExecuteCommand(command);

            // Assert
            Assert.DoesNotThrow(act);
        }

        [Test]
        public void Execute_InvalidCommand_ShouldHandleErrorGracefully()
        {
            // Arrange
            string command = "nonexistentcommand";

            // Act & Assert
            Assert.DoesNotThrow(() => _handler.ExecuteCommand(command));
        }
    }
}