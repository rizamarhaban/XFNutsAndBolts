using XFNutsAndBolts.Models;

namespace XFNutsAndBolts.Settings
{
    public interface IAppSettingsTest
    {
        AppSettings AppSettings { get; set; }
        string Name1 { get; set; }
        string Name2 { get; set; }
    }
}