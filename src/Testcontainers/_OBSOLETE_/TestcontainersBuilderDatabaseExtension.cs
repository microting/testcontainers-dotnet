namespace DotNet.Testcontainers.Builders
{
  using System.Linq;
  using DotNet.Testcontainers.Configurations;
  using DotNet.Testcontainers.Containers;
  using JetBrains.Annotations;

  /// <summary>
  /// This class applies the extended Testcontainer configurations for databases.
  /// </summary>
  [PublicAPI]
  public static class TestcontainersBuilderDatabaseExtension
  {
    public static ContainerBuilder<T> WithDatabase<T>(this ContainerBuilder<T> builder, TestcontainerDatabaseConfiguration configuration)
      where T : TestcontainerDatabase
    {
      builder = configuration.Environments.Aggregate(builder, (current, environment)
        => current.WithEnvironment(environment.Key, environment.Value));

      builder = configuration.ResourceMappings.Values.Aggregate(builder, (current, resourceMapping)
        => current.WithResourceMapping(resourceMapping));

      return builder
        .WithImage(configuration.Image)
        .WithExposedPort(configuration.DefaultPort)
        .WithPortBinding(configuration.Port, configuration.DefaultPort)
        .WithOutputConsumer(configuration.OutputConsumer)
        .WithWaitStrategy(configuration.WaitStrategy)
        .ConfigureContainer(container =>
        {
          container.ContainerPort = configuration.DefaultPort;
          container.Database = configuration.Database;
          container.Username = configuration.Username;
          container.Password = configuration.Password;
        });
    }
  }
}
