using Serilog;
using System.Windows;

namespace HeroSkin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            Utils.Log.logger = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .CreateLogger();

            Utils.Log.logger.Information($"=== START LOGGING : HEROSKIN {Utils.Constants.HSVersion} ===");
        }
    }
}
