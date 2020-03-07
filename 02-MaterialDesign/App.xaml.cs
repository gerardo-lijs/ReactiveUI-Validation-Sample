using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ReactiveUIValidationSample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
		// NOTE: For ReactiveUI Validation to work properly we can't use StartupUri
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			var mainWindow = new MainWindow();
			mainWindow.Show();
		}
	}
}
