using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ReactiveUIValidationSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            this.WhenActivated(disposableRegistration =>
            {
                this.WhenAnyValue(viewModel => viewModel.ViewModel).BindTo(this, view => view.DataContext).DisposeWith(disposableRegistration);

                this.Bind(ViewModel, viewModel => viewModel.SaveResult, view => view.SaveResultTextBlock.Text).DisposeWith(disposableRegistration);
                this.BindCommand(ViewModel, viewModel => viewModel.Save, view => view.SaveButton).DisposeWith(disposableRegistration);

                // 2019-10-31 - For Validation to work in WPF with ReactiveUI, we can't use code behind bindings. We need to bind in XAML.
                // https://reactivex.slack.com/archives/C02AJB872/p1571739816086600?thread_ts=1571711238.085100
                //this.Bind(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox.Text).DisposeWith(disposableRegistration);
                //this.Bind(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox.Text).DisposeWith(disposableRegistration);

                // Validation extension methods. They are not working as expected or I'm using them in the wrong way...
                // Binding to a TextBlock text I get errors concatenated. Tried with ListView, etc with no luck.
                //this.BindValidation(ViewModel, view => view.ErrorsTextBlock.Text).DisposeWith(disposableRegistration);

                // Validation extension methods. They are not working as expected or I'm using them in the wrong way...
                // Getting System.ArgumentOutOfRangeException: 'Index was out of range. Must be non-negative and less than the size of the collection. Parameter name: index'
                // in the second call to BindValidation on a different Property
                // 2019-11-01 - Found source of issue here -> ValidationContext.Validation.Count is only 1 so it fails in the second BindValidation which  can't be resolved
                //this.BindValidation(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox.ToolTip);
                //this.BindValidation(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox.ToolTip);
            });
        }

        private void ReactiveWindow_ContentRendered(object sender, EventArgs e)
        {
            // 2019-11-01 If bind here I have ValidationContext.Validation completely loaded and it works
            // Of course we shouldn't bind here and also use Disposable...
            this.BindValidation(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox.ToolTip);
            this.BindValidation(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox.ToolTip);

            // 2019-11-01 - Also, if I raise the event ErrorsChnged here manually I get both Fields validated when Window loads
            // This should also be fixed automatically if we can get ValidationContext.Validations to be loaded at the end of the ViewModel
            //ViewModel.RaiseErrorsChanged("FirstName");
            //ViewModel.RaiseErrorsChanged("LastName");
        }
    }
}
