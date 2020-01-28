using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace ReactiveUIValidationSample
{
    public class MainViewModel : ReactiveValidationObject<MainViewModel>
    {
        [Reactive] public string FirstName { get; set; }
        [Reactive] public string LastName { get; set; }

        [Reactive] public string SaveResult { get; set; }

        public ReactiveCommand<Unit, Unit> Save { get; }

        public MainViewModel() : base(RxApp.TaskpoolScheduler)
        {
            // IsValid extension method returns true when all validations succeed.
            var canSave = this.IsValid();

            Save = ReactiveCommand.Create(() => { SaveResult = $"{LastName.ToUpperInvariant()}, {FirstName}"; }, canSave);

            // TODO: Add some optional Supression for ErrorsChanged here with a flag so Windows loads without errors

            // Validation rules
            this.ValidationRule(viewModel => viewModel.FirstName,
                firstName => !string.IsNullOrWhiteSpace(firstName), "You must specify a valid first name");

            this.ValidationRule(viewModel => viewModel.FirstName,
                firstName => firstName?.Length >= 5, "First name must have at least five characters");

            this.ValidationRule(viewModel => viewModel.LastName,
                lastName => !string.IsNullOrWhiteSpace(lastName), "You must specify a valid last name");

            // TODO: Enable ErrorsChanged now so notification works after user makes changes to fields

            // NOTE: Only the first ValidationRule get's shown the first time the Window is displayed.
        }
    }
}
