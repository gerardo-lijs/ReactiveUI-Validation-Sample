namespace ReactiveUIValidationSample;

using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using System.Reactive.Disposables;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        this.WhenActivated(disposableRegistration =>
        {
            this.WhenAnyValue(view => view.ViewModel).BindTo(this, view => view.DataContext).DisposeWith(disposableRegistration);

            this.Bind(ViewModel, viewModel => viewModel.SaveResult, view => view.SaveResultTextBlock.Text).DisposeWith(disposableRegistration);
            this.BindCommand(ViewModel, viewModel => viewModel.Save, view => view.SaveButton).DisposeWith(disposableRegistration);

            // 2019-10-31 - For Validation to work in WPF with ReactiveUI, we can't use code behind bindings. We need to bind in XAML.
            // https://reactivex.slack.com/archives/C02AJB872/p1571739816086600?thread_ts=1571711238.085100
            //this.Bind(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox.Text).DisposeWith(disposableRegistration);
            //this.Bind(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox.Text).DisposeWith(disposableRegistration);

            // Validation extension methods. They are not working as expected or I'm using them in the wrong way...
            // Binding to a TextBlock text I get errors concatenated. Tried with ListView, etc with no luck.
            this.BindValidation(ViewModel, view => view.ErrorsTextBlock.Text).DisposeWith(disposableRegistration);

            // Validation extension methods. They are not working as expected or I'm using them in the wrong way...
            // Getting System.ArgumentOutOfRangeException: 'Index was out of range. Must be non-negative and less than the size of the collection. Parameter name: index'
            // in the second call to BindValidation on a different Property
            //this.BindValidation(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox.ToolTip).DisposeWith(disposableRegistration);
            //this.BindValidation(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox.ToolTip).DisposeWith(disposableRegistration);

            this.BindValidationToToolTip(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox).DisposeWith(disposableRegistration);
            this.BindValidationToToolTip(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox).DisposeWith(disposableRegistration);
        });
    }
}