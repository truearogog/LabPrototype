using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.ModelSettings
{
    public abstract class SettingsFormViewModelBase<T, TStore> : ViewModelBase
        where T : PresentationModelBase, new()
        where TStore : IStoreBase<T>
    {
        private T _model = new();
        public T Model
        {
            get => _model;
            set
            {
                this.RaiseAndSetIfChanged(ref _model, value);
                OnModelSet();
            }
        }

        private readonly TStore _store;

        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        public SettingsFormViewModelBase(ICommand cancelCommand, Action<TStore, T> submitAction)
        {
            _store = GetRequiredService<TStore>();

            CancelCommand = cancelCommand;
            SubmitCommand = ReactiveCommand.Create(() =>
            {
                OnSubmit();
                submitAction(_store, _model);
            });
        }

        protected abstract void OnSubmit();
        protected abstract void OnModelSet();
    }
}
