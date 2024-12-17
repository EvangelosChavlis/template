// packages
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

// source
using server.src.Persistence;
using server.src.Persistence.Interfaces;
using server.src.Persistence.Repositories;
using server.src.Persistence.Contexts;

namespace server.test.Persistence.Unit.Tests
{
    public class DependencyInjectionTests
    {
        [Fact]
        public void AddPersistence_ShouldRegisterDataContext()
        {
            // Arrange
            var services = new ServiceCollection();

            // Use ConfigurationBuilder to build configuration
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[] { 
                    new KeyValuePair<string, string>("ConnectionStrings:DefaultConnection", "DataSource=:memory:") 
                }!)
                .Build();

            // Act
            services.AddPersistence(configuration);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var dataContext = serviceProvider.GetService<DataContext>();
            
            // Verify that the DataContext is correctly registered
            Assert.NotNull(dataContext); // DataContext should be registered
        }

        [Fact]
        public void AddPersistence_ShouldRegisterCommonRepository()
        {
            // Arrange
            var services = new ServiceCollection();

            // Use ConfigurationBuilder to build configuration
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[] { 
                    new KeyValuePair<string, string>("ConnectionStrings:DefaultConnection", "DataSource=:memory:") 
                }!)
                .Build();

            // Act
            services.AddPersistence(configuration);

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var commonRepository = serviceProvider.GetService<ICommonRepository>();

            // Verify that ICommonRepository is registered and resolves to CommonRepository
            Assert.NotNull(commonRepository);
            Assert.IsType<CommonRepository>(commonRepository); // Ensure it resolves to the correct implementation
        }
    }
}
