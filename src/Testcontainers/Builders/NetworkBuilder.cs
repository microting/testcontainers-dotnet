﻿namespace DotNet.Testcontainers.Builders
{
  using System;
  using System.Collections.Generic;
  using Docker.DotNet.Models;
  using DotNet.Testcontainers.Configurations;
  using DotNet.Testcontainers.Networks;
  using JetBrains.Annotations;

  /// <summary>
  /// A fluent Docker network builder.
  /// </summary>
  /// <example>
  ///   The default configuration is equivalent to:
  ///   <code>
  ///   _ = new NetworkBuilder()
  ///     .WithDockerEndpoint(TestcontainersSettings.OS.DockerEndpointAuthConfig)
  ///     .WithLabel(DefaultLabels.Instance)
  ///     .WithCleanUp(true)
  ///     .WithDriver(NetworkDriver.Bridge)
  ///     .Build();
  ///   </code>
  /// </example>
  [PublicAPI]
  public class NetworkBuilder : AbstractBuilder<NetworkBuilder, INetwork, NetworksCreateParameters, INetworkConfiguration>, INetworkBuilder<NetworkBuilder>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkBuilder" /> class.
    /// </summary>
    public NetworkBuilder()
      : this(new NetworkConfiguration())
    {
      this.DockerResourceConfiguration = this.Init().DockerResourceConfiguration;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NetworkBuilder" /> class.
    /// </summary>
    /// <param name="dockerResourceConfiguration">The Docker resource configuration.</param>
    protected NetworkBuilder(INetworkConfiguration dockerResourceConfiguration)
      : base(dockerResourceConfiguration)
    {
      this.DockerResourceConfiguration = dockerResourceConfiguration;
    }

    /// <inheritdoc />
    protected override INetworkConfiguration DockerResourceConfiguration { get; }

    /// <inheritdoc />
    public NetworkBuilder WithName(string name)
    {
      return this.Merge(this.DockerResourceConfiguration, new NetworkConfiguration(name: name));
    }

    /// <inheritdoc />
    public NetworkBuilder WithDriver(NetworkDriver driver)
    {
      return this.Merge(this.DockerResourceConfiguration, new NetworkConfiguration(driver: driver));
    }

    /// <inheritdoc />
    public NetworkBuilder WithOption(string name, string value)
    {
      var options = new Dictionary<string, string> { { name, value } };
      return this.Merge(this.DockerResourceConfiguration, new NetworkConfiguration(options: options));
    }

    /// <inheritdoc />
    public override INetwork Build()
    {
      this.Validate();
      return new DockerNetwork(this.DockerResourceConfiguration, TestcontainersSettings.Logger);
    }

    /// <inheritdoc />
    protected sealed override NetworkBuilder Init()
    {
      return base.Init().WithName(Guid.NewGuid().ToString("D")).WithDriver(NetworkDriver.Bridge);
    }

    /// <inheritdoc />
    protected override void Validate()
    {
      base.Validate();

      _ = Guard.Argument(this.DockerResourceConfiguration.Name, nameof(INetworkConfiguration.Name))
        .NotNull()
        .NotEmpty();
    }

    /// <inheritdoc />
    protected override NetworkBuilder Clone(IResourceConfiguration<NetworksCreateParameters> resourceConfiguration)
    {
      return this.Merge(this.DockerResourceConfiguration, new NetworkConfiguration(resourceConfiguration));
    }

    /// <inheritdoc />
    protected override NetworkBuilder Merge(INetworkConfiguration oldValue, INetworkConfiguration newValue)
    {
      return new NetworkBuilder(new NetworkConfiguration(oldValue, newValue));
    }
  }
}
