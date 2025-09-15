using Aspire.Hosting.ApplicationModel;

// Put extensions in the Aspire.Hosting namespace to ease discovery as referencing
// the .NET Aspire hosting package automatically adds this namespace.
namespace Aspire.Hosting;

public static class PureFtpdResourceBuilderExtensions
{
    /// <summary>
    /// Adds the <see cref="PureFtpdResource"/> to the given
    /// <paramref name="builder"/> instance. Uses the "latest" tag.
    /// </summary>
    /// <param name="builder">The <see cref="IDistributedApplicationBuilder"/>.</param>
    /// <param name="name">The name of the resource.</param>
    /// <param name="ftpPort">The FTP control port.</param>
    /// <param name="userName">The FTP username parameter. If not provided, a parameter named "ftp-username" will be created.</param>
    /// <param name="password">The FTP password parameter. If not provided, a secret parameter named "ftp-password" will be created.</param>
    /// <param name="homeDirectory">The FTP user home directory.</param>
    /// <param name="publicHost">The public host for passive connections.</param>
    /// <param name="passivePortRange">The passive port range (e.g., "30000:30009").</param>
    /// <returns>
    /// An <see cref="IResourceBuilder{PureFtpdResource}"/> instance that
    /// represents the added PureFtpd resource.
    /// </returns>
    public static IResourceBuilder<PureFtpdResource> AddPureFtpd(
        this IDistributedApplicationBuilder builder,
        string name,
        int? ftpPort = null,
        IResourceBuilder<ParameterResource>? userName = null,
        IResourceBuilder<ParameterResource>? password = null,
        string homeDirectory = "/home/ftpuser",
        string publicHost = "localhost",
        string passivePortRange = "30000:30009")
    {
        // The AddResource method is a core API within .NET Aspire and is
        // used by resource developers to wrap a custom resource in an
        // IResourceBuilder<T> instance. Extension methods to customize
        // the resource (if any exist) target the builder interface.
        
        var userNameParameter = userName ?? builder.AddParameter("ftp-username", "ftpuser");

        var ab = ParameterResourceBuilderExtensions.CreateDefaultPasswordParameter(builder, "ftp-password");


        //var passwordParameter = password ?? ParameterResourceBuilderExtensions.CreateDefaultPasswordParameter(builder, "ftp-password");
        var passwordParameter = userName ?? builder.AddParameter("a", value: ab.Value);//builder.AddParameter("ftp-password", "ftppassword");



        var resource = new PureFtpdResource(name)
        {
            UserNameParameter = userNameParameter,
            PasswordParameter = passwordParameter,
            HomeDirectory = homeDirectory,
            PublicHost = publicHost,
            PassivePortRange = passivePortRange
        };

        // Parse passive port range to get start and end ports
        var portParts = passivePortRange.Split(':');
        if (portParts.Length != 2 || !int.TryParse(portParts[0], out var startPort) || !int.TryParse(portParts[1], out var endPort))
        {
            throw new ArgumentException($"Invalid passive port range format: {passivePortRange}. Expected format: 'startPort:endPort' (e.g., '30000:30009')");
        }

        var resourceBuilder = builder.AddResource(resource)
                      .WithImage(PureFtpdContainerImageTags.Image)
                      .WithImageRegistry(PureFtpdContainerImageTags.Registry)
                      .WithImageTag(PureFtpdContainerImageTags.Tag)
                      .WithEndpoint(
                          targetPort: 21,
                          port: ftpPort,
                          name: PureFtpdResource.FtpEndpointName)
                      .WithEnvironment("FTP_USER_NAME", userNameParameter)
                      .WithEnvironment("FTP_USER_PASS", passwordParameter)
                      .WithEnvironment("FTP_USER_HOME", homeDirectory)
                      .WithEnvironment("PUBLICHOST", publicHost)
                      .WithEnvironment("FTP_PASSIVE_PORTS", passivePortRange)
                      .WithEnvironment("FTP_MAX_CLIENTS", "50");

        // Add passive port range with unique names to avoid conflicts
        for (int port = startPort; port <= endPort; port++)
        {
            resourceBuilder = resourceBuilder.WithEndpoint(
                targetPort: port, 
                port: port, 
                name: $"passive-{port}");
        }

        return resourceBuilder;
    }
}

// This class just contains constant strings that can be updated periodically
// when new versions of the underlying container are released.
internal static class PureFtpdContainerImageTags
{
    internal const string Registry = "docker.io";

    internal const string Image = "stilliard/pure-ftpd";

    internal const string Tag = "latest";
}