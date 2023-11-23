namespace ReactiveUIValidationSample;

using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Formatters;
using System;
using System.Reactive.Disposables;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        this.WhenActivated(disposableRegistration =>
        {
            this.WhenAnyValue(viewModel => viewModel.ViewModel).BindTo(this, view => view.DataContext).DisposeWith(disposableRegistration);

            this.OneWayBind(ViewModel, viewModel => viewModel.SaveResult, view => view.SaveResultTextBlock.Text).DisposeWith(disposableRegistration);
            this.BindCommand(ViewModel, viewModel => viewModel.Save, view => view.SaveButton).DisposeWith(disposableRegistration);

            // 2019-10-31 - For Validation to work in WPF with ReactiveUI, we can't use code behind bindings. We need to bind in XAML.
            // https://reactivex.slack.com/archives/C02AJB872/p1571739816086600?thread_ts=1571711238.085100
            //this.Bind(ViewModel, viewModel => viewModel.FirstName, view => view.FirstNameTextBox.Text).DisposeWith(disposableRegistration);
            //this.Bind(ViewModel, viewModel => viewModel.LastName, view => view.LastNameTextBox.Text).DisposeWith(disposableRegistration);

            // Validation summary
            var formatter = new SingleLineFormatter(Environment.NewLine);
            this.BindValidation(ViewModel, view => view.ErrorsTextBlock.Text, formatter).DisposeWith(disposableRegistration);
        });
    }
}
