namespace DotNet.Testcontainers.Configurations
{
  using DotNet.Testcontainers.Builders;
  using JetBrains.Annotations;

  /// <inheritdoc cref="TestcontainerDatabaseConfiguration" />
  [PublicAPI]
  public class MariaDbTestcontainerConfiguration : TestcontainerDatabaseConfiguration
  {
    private const string MariaDbImage = "mariadb:10.4.10";

    private const int MariaDbPort = 3306;

    /// <summary>
    /// Initializes a new instance of the <see cref="MySqlTestcontainerConfiguration" /> class.
    /// </summary>
    public MariaDbTestcontainerConfiguration()
      : this(MariaDbImage)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MySqlTestcontainerConfiguration" /> class.
    /// </summary>
    /// <param name="image">The Docker image.</param>
    public MariaDbTestcontainerConfiguration(string image)
      : base(image, MariaDbPort)
    {
      this.Environments["MYSQL_ALLOW_EMPTY_PASSWORD"] = "yes";
    }

    /// <inheritdoc />
    public override string Database
    {
      get => this.Environments["MYSQL_DATABASE"];
      set => this.Environments["MYSQL_DATABASE"] = value;
    }

    /// <inheritdoc />
    public override string Username
    {
      get => this.Environments["MYSQL_USER"];
      set => this.Environments["MYSQL_USER"] = value;
    }

    /// <inheritdoc />
    public override string Password
    {
      get => this.Environments["MYSQL_PASSWORD"];
      set => this.Environments["MYSQL_PASSWORD"] = value;
    }

    /// <summary>
    /// Gets or sets this specifies the password that will be set for the MariaDB root superuser account.
    /// </summary>
    public string RootPassword
    {
      get => this.Environments["MARIADB_ROOT_PASSWORD"];
      set => this.Environments["MARIADB_ROOT_PASSWORD"] = value;
    }

    /// <inheritdoc />
    public override IWaitForContainerOS WaitStrategy => Wait.ForUnixContainer()
      .UntilCommandIsCompleted("mysql", "--host=localhost", $"--port={this.DefaultPort}", $"--user={this.Username}", $"--password={this.Password}", "--protocol=TCP", "--execute=SHOW DATABASES");
  }
}
