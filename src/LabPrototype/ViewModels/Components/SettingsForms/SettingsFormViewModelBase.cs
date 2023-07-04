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

        public ICommand? CancelCommand { get; private set; }
        public ICommand? SubmitCommand { get; private set; }

        public SettingsFormViewModelBase()
        {
            _store = GetRequiredService<TStore>();
        }

        public virtual void Activate(ICommand cancelCommand, Func<TStore, T, T?>? submitAction = null)
        {
            CancelCommand = cancelCommand;
            this.RaisePropertyChanged(nameof(CancelCommand));
            SubmitCommand = ReactiveCommand.Create(() =>
            {
                BeforeSubmit();
                if (submitAction != default)
                {
                    var model = submitAction?.Invoke(_store, _model);
                    AfterSubmit(model);
                }
            });
            this.RaisePropertyChanged(nameof(SubmitCommand));
        }

        protected virtual void BeforeSubmit()
        {
            PrepareModel();
        }

        protected virtual void AfterSubmit(T? model)
        {
        }

        public virtual void PrepareModel()
        {
        }

        protected virtual void OnModelSet()
        {
        }
    }
}
