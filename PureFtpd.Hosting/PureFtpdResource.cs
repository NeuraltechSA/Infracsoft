namespace Aspire.Hosting.ApplicationModel;

public sealed class PureFtpdResource(string name) : ContainerResource(name), IResourceWithConnectionString
{
    // Constants used to refer to well known-endpoint names, this is specific
    // for each resource type. PureFtpd exposes an FTP endpoint and passive data ports.
    internal const string FtpEndpointName = "ftp";

    // An EndpointReference is a core .NET Aspire type used for keeping
    // track of endpoint details in expressions. Simple literal values cannot
    // be used because endpoints are not known until containers are launched.
    private EndpointReference? _ftpReference;

    public EndpointReference FtpEndpoint =>
        _ftpReference ??= new(this, FtpEndpointName);

    // Parameters for FTP configuration - following PostgreSQL pattern
    public required IResourceBuilder<ParameterResource> UserNameParameter { get; init; }
    public required IResourceBuilder<ParameterResource> PasswordParameter { get; init; }
    
    // Properties for FTP configuration
    public string HomeDirectory { get; set; } = "/home/ftpuser";
    public string PublicHost { get; set; } = "localhost";
    public string PassivePortRange { get; set; } = "30000:30009";

    // Required property on IResourceWithConnectionString. Represents a connection
    // string that applications can use to access the PureFtpd server. In this case
    // the connection string is composed of the FtpEndpoint endpoint reference.
    public ReferenceExpression ConnectionStringExpression =>
        ReferenceExpression.Create(
            $"ftp://{UserNameParameter.Resource}:{PasswordParameter.Resource}@{FtpEndpoint.Property(EndpointProperty.Host)}:{FtpEndpoint.Property(EndpointProperty.Port)}"
        );
}