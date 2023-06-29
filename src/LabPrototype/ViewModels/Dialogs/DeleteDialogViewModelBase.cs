using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.ViewModels.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Dialogs
{
    public class DeleteDialogViewModelBase<T, TService, TStore> : ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>
        where T : PresentationModelBase, new()
        where TService : IServiceBase<T>
        where TStore : IStoreBase<T>
    {
        private T _model = new();
        public T Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }

        public ICommand CancelCommand { get; }
        public ICommand DeleteCommand { get; }

        public DeleteDialogViewModelBase()
        {
            var service = GetRequiredService<TService>();
            var store = GetRequiredService<TStore>();

            CancelCommand = CloseCommand;
            DeleteCommand = ReactiveCommand.Create(() =>
            {
                store.Delete(service, _model.Id);
                Close();
            });
        }

        public override void Activate(ModelNavigationParameter<T> parameter)
        {
            Model = parameter.Model ?? throw new Exception();
        }
    }
}
