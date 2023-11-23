namespace ReactiveUIValidationSample;

using System.Windows;

public partial class App : Application
{
    // NB: For ReactiveUI Validation to work properly we can't use StartupUri
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}
