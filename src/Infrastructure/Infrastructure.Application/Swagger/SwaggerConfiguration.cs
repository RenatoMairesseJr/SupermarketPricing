namespace Infrastructure.Application.Swagger;

public  class SwaggerConfiguration
{
    /// <summary>
    /// Gets the Project Version from the configuration
    /// AppSetting: Swagger:ProjectVersion
    /// </summary>
    public string ProjectVersion {get;set;} = string.Empty; 

    /// <summary>
    /// Gets the Project Name from the configuration
    /// AppSetting: Swagger:ProjectName
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;

    /// <summary>
    /// Gets the Version from the configuration
    /// AppSettings: Swagger:Version
    /// </summary>
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Gets Project description
    /// </summary>
    public string ProjectDescription { get; set; } = string.Empty;
}
