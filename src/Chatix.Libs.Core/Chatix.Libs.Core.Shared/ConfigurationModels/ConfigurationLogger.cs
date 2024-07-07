namespace Chatix.Libs.Core.Shared.ConfigurationModels;


public class ConfigurationLogger
{
    public const string Key = "LoggerFolderPath";

    public string? PathToLog { get; set; }

    public string? FileToLog { get; set; }

}