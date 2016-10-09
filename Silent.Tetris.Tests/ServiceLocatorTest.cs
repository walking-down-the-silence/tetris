using Silent.Tetris.Contracts;
using Xunit;

namespace Silent.Tetris.Tests
{
    public class ServiceLocatorTest
    {
        [Fact]
        public void Register_WithServiceInstance_ShouldContainOneRegistration()
        {
            // Arrange
            IContainer container = new ServiceLocator();
            IService stub = new StubService();
            int expected = 1;

            // Act
            container.Register<IService>(stub);
            int result = container.Registrations.Count;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Register_WithServiceFactory_ShouldContainOneRegistration()
        {
            // Arrange
            IContainer container = new ServiceLocator();
            int expected = 1;

            // Act
            container.Register<IService>(locator => new StubService());
            int result = container.Registrations.Count;

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Resolve_WithServiceInstance_ShouldReturnSameService()
        {
            // Arrange
            IContainer container = new ServiceLocator();
            IService stub = new StubService();

            // Act
            container.Register<IService>(stub);
            IService result = container.Resolve<IService>();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IService>(result);
            Assert.Same(stub, result);
        }

        [Fact]
        public void Resolve_WithServiceFactory_ShouldReturnNewServices()
        {
            // Arrange
            IContainer container = new ServiceLocator();

            // Act
            container.Register<IService>(locator => new StubService());
            IService result1 = container.Resolve<IService>();
            IService result2 = container.Resolve<IService>();

            // Assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.IsAssignableFrom<IService>(result1);
            Assert.IsAssignableFrom<IService>(result2);
            Assert.NotSame(result1, result2);
        }

        private interface IService
        {
        }

        private class StubService : IService
        {
        }
    }
}
