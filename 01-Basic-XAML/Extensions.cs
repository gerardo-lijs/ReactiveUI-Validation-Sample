namespace ReactiveUIValidationSample;

using ReactiveUI;
using ReactiveUI.Validation.Abstractions;
using ReactiveUI.Validation.Components;
using ReactiveUI.Validation.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

public static class Extensions
{
    public static IDisposable BindValidationToToolTip<TView, TViewModel, TViewModelProperty>
        (
            this TView @this,
            TViewModel viewModel,
            Expression<Func<TViewModel, TViewModelProperty>> viewModelProperty,
            Expression<Func<TView, FrameworkElement>> viewProperty
        )
        where TView : IViewFor<TViewModel>
        where TViewModel : ReactiveObject, IValidatableViewModel
    {
        var frameworkElement = viewProperty.Compile().Invoke(@this);

        return @this.WhenAnyValue(view => view.ViewModel)
            .Select(vm => vm.ValidationContext.Validations
                // Observe to add ValidationRule
                .ToObservable()
                .Select(_ => viewModel
                    // Get the corresponding Validation Component
                    .ValidationContext.Validations
                    .OfType<BasePropertyValidation<TViewModel, string>>()
                    .Where(x => x.ContainsProperty(viewModelProperty))
                    // Combining ValidationStatusChange
                    .Select(x => x.ValidationStatusChange)
                    .CombineLatest())
                .Switch())
            .Switch()
            .Select(validations =>
                string.Join(
                    Environment.NewLine,
                    validations
                        .Where(x => !x.IsValid)
                        .SelectMany(x => x.Text)))
            .DistinctUntilChanged()
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(validationText =>
            {
                if (string.IsNullOrEmpty(validationText))
                {
                    ToolTipService.SetIsEnabled(frameworkElement, false);
                }
                else
                {
                    ToolTipService.SetIsEnabled(frameworkElement, true);
                    ToolTipService.SetToolTip(frameworkElement, validationText);
                }
            });
    }
}