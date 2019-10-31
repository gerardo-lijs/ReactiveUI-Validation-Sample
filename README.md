# ReactiveUI Validation Sample
Sample WPF project using ReactiveUI.Validation

* [Basic XAML sample](01-Basic-XAML)  
Sample source code of a WPF application using ReactiveUI.Validation with just XAML. It looks ugly but I want it to keep it simple.

* [Material Design sample](01-Basic-XAML)  
Sample source code of a WPF application using ReactiveUI.Validation and MaterialDesign for better UI.

## Notes / Current Issues
* Code behind binding with ReactiveUI this.Bind does not work with WPF. Binding must be done in XAML. Hopefully this will be solved in the future. https://reactivex.slack.com/archives/C02AJB872/p1571739816086600?thread_ts=1571711238.085100

* Only the first ValidationRule get's shown the first time the Window is displayed. This feels strange.

* I was not able to get the BindValidation extension methods to work as expected. Probably someone else can point me in the right direction here...
