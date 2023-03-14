﻿namespace DotNet.Testcontainers.Configurations
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using System.Threading.Tasks;
  using Docker.DotNet.Models;
  using DotNet.Testcontainers.Builders;
  using DotNet.Testcontainers.Containers;
  using DotNet.Testcontainers.Images;
  using DotNet.Testcontainers.Networks;
  using JetBrains.Annotations;

  /// <inheritdoc cref="IContainerConfiguration" />
  [PublicAPI]
  public class ContainerConfiguration : ResourceConfiguration<CreateContainerParameters>, IContainerConfiguration
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerConfiguration" /> class.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <param name="imagePullPolicy">The image pull policy.</param>
    /// <param name="name">The name.</param>
    /// <param name="hostname">The hostname.</param>
    /// <param name="macAddress">The MAC address.</param>
    /// <param name="workingDirectory">The working directory.</param>
    /// <param name="entrypoint">The entrypoint.</param>
    /// <param name="command">The command.</param>
    /// <param name="environments">A dictionary of environment variables.</param>
    /// <param name="exposedPorts">A dictionary of exposed ports.</param>
    /// <param name="portBindings">A dictionary of port bindings.</param>
    /// <param name="resourceMappings">A dictionary of resource mappings.</param>
    /// <param name="mounts">A list of mounts.</param>
    /// <param name="networks">A list of networks.</param>
    /// <param name="networkAliases">A list of network-scoped aliases.</param>
    /// <param name="outputConsumer">The output consumer.</param>
    /// <param name="waitStrategies">The wait strategies.</param>
    /// <param name="startupCallback">The startup callback.</param>
    /// <param name="autoRemove">A value indicating whether Docker removes the container after it exits or not.</param>
    /// <param name="privileged">A value indicating whether the privileged flag is set or not.</param>
    public ContainerConfiguration(
      IImage image = null,
      Func<ImagesListResponse, bool> imagePullPolicy = null,
      string name = null,
      string hostname = null,
      string macAddress = null,
      string workingDirectory = null,
      IEnumerable<string> entrypoint = null,
      IEnumerable<string> command = null,
      IReadOnlyDictionary<string, string> environments = null,
      IReadOnlyDictionary<string, string> exposedPorts = null,
      IReadOnlyDictionary<string, string> portBindings = null,
      IReadOnlyDictionary<string, IResourceMapping> resourceMappings = null,
      IEnumerable<IMount> mounts = null,
      IEnumerable<INetwork> networks = null,
      IEnumerable<string> networkAliases = null,
      IOutputConsumer outputConsumer = null,
      IEnumerable<IWaitUntil> waitStrategies = null,
      Func<IContainer, CancellationToken, Task> startupCallback = null,
      bool? autoRemove = null,
      bool? privileged = null)
    {
      this.AutoRemove = autoRemove;
      this.Privileged = privileged;
      this.Image = image;
      this.ImagePullPolicy = imagePullPolicy;
      this.Name = name;
      this.Hostname = hostname;
      this.MacAddress = macAddress;
      this.WorkingDirectory = workingDirectory;
      this.Entrypoint = entrypoint;
      this.Command = command;
      this.Environments = environments;
      this.ExposedPorts = exposedPorts;
      this.PortBindings = portBindings;
      this.ResourceMappings = resourceMappings;
      this.Mounts = mounts;
      this.Networks = networks;
      this.NetworkAliases = networkAliases;
      this.OutputConsumer = outputConsumer;
      this.WaitStrategies = waitStrategies;
      this.StartupCallback = startupCallback;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public ContainerConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
      : base(resourceConfiguration)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerConfiguration" /> class.
    /// </summary>
    /// <param name="resourceConfiguration">The Docker resource configuration.</param>
    public ContainerConfiguration(IContainerConfiguration resourceConfiguration)
      : this(new ContainerConfiguration(), resourceConfiguration)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContainerConfiguration" /> class.
    /// </summary>
    /// <param name="oldValue">The old Docker resource configuration.</param>
    /// <param name="newValue">The new Docker resource configuration.</param>
    public ContainerConfiguration(IContainerConfiguration oldValue, IContainerConfiguration newValue)
      : base(oldValue, newValue)
    {
      this.Image = BuildConfiguration.Combine(oldValue.Image, newValue.Image);
      this.ImagePullPolicy = BuildConfiguration.Combine(oldValue.ImagePullPolicy, newValue.ImagePullPolicy);
      this.Name = BuildConfiguration.Combine(oldValue.Name, newValue.Name);
      this.Hostname = BuildConfiguration.Combine(oldValue.Hostname, newValue.Hostname);
      this.MacAddress = BuildConfiguration.Combine(oldValue.MacAddress, newValue.MacAddress);
      this.WorkingDirectory = BuildConfiguration.Combine(oldValue.WorkingDirectory, newValue.WorkingDirectory);
      this.Entrypoint = BuildConfiguration.Combine(oldValue.Entrypoint, newValue.Entrypoint);
      this.Command = BuildConfiguration.Combine(oldValue.Command, newValue.Command);
      this.Environments = BuildConfiguration.Combine(oldValue.Environments, newValue.Environments);
      this.ExposedPorts = BuildConfiguration.Combine(oldValue.ExposedPorts, newValue.ExposedPorts);
      this.PortBindings = BuildConfiguration.Combine(oldValue.PortBindings, newValue.PortBindings);
      this.ResourceMappings = BuildConfiguration.Combine(oldValue.ResourceMappings, newValue.ResourceMappings);
      this.Mounts = BuildConfiguration.Combine(oldValue.Mounts, newValue.Mounts);
      this.Networks = BuildConfiguration.Combine(oldValue.Networks, newValue.Networks);
      this.NetworkAliases = BuildConfiguration.Combine(oldValue.NetworkAliases, newValue.NetworkAliases);
      this.OutputConsumer = BuildConfiguration.Combine(oldValue.OutputConsumer, newValue.OutputConsumer);
      this.WaitStrategies = BuildConfiguration.Combine<IEnumerable<IWaitUntil>>(oldValue.WaitStrategies, newValue.WaitStrategies);
      this.StartupCallback = BuildConfiguration.Combine(oldValue.StartupCallback, newValue.StartupCallback);
      this.AutoRemove = (oldValue.AutoRemove.HasValue && oldValue.AutoRemove.Value) || (newValue.AutoRemove.HasValue && newValue.AutoRemove.Value);
      this.Privileged = (oldValue.Privileged.HasValue && oldValue.Privileged.Value) || (newValue.Privileged.HasValue && newValue.Privileged.Value);
    }

    /// <inheritdoc />
    public bool? AutoRemove { get; }

    /// <inheritdoc />
    public bool? Privileged { get; }

    /// <inheritdoc />
    public IImage Image { get; }

    /// <inheritdoc />
    public Func<ImagesListResponse, bool> ImagePullPolicy { get; }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public string Hostname { get; }

    /// <inheritdoc />
    public string MacAddress { get; }

    /// <inheritdoc />
    public string WorkingDirectory { get; }

    /// <inheritdoc />
    public IEnumerable<string> Entrypoint { get; }

    /// <inheritdoc />
    public IEnumerable<string> Command { get; }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> Environments { get; }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> ExposedPorts { get; }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> PortBindings { get; }

    /// <inheritdoc />
    public IReadOnlyDictionary<string, IResourceMapping> ResourceMappings { get; }

    /// <inheritdoc />
    public IEnumerable<IMount> Mounts { get; }

    /// <inheritdoc />
    public IEnumerable<INetwork> Networks { get; }

    /// <inheritdoc />
    public IEnumerable<string> NetworkAliases { get; }

    /// <inheritdoc />
    public IOutputConsumer OutputConsumer { get; }

    /// <inheritdoc />
    public IEnumerable<IWaitUntil> WaitStrategies { get; }

    /// <inheritdoc />
    public Func<IContainer, CancellationToken, Task> StartupCallback { get; }
  }
}
