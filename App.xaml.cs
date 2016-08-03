namespace WinKeyHandler
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            Keyboard.Hook();
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            Keyboard.Unhook();
        }
    }
}
