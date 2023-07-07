using AutoMapper;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Framework.Models;
using ReactiveUI;
using System;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.ModelSettings
{
    public abstract class SettingsFormViewModelBase<T, TStore, TForm> : ViewModelBase
        where T : PresentationModelBase, new()
        where TStore : IStoreBase<T>
        where TForm : FormBase, new()
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

        private TForm _form = new();
        public TForm Form
        {
            get => _form;
            set => this.RaiseAndSetIfChanged(ref _form, value);
        }

        private readonly TStore _store;
        private readonly IMapper _mapper;

        public ICommand? CancelCommand { get; private set; }
        public ICommand? SubmitCommand { get; private set; }

        public SettingsFormViewModelBase()
        {
            _store = GetRequiredService<TStore>();
            _mapper = GetRequiredService<IMapper>();
        }

        public virtual void Activate(ICommand cancelCommand, Func<TStore, T, T?>? submitAction = null)
        {
            CancelCommand = cancelCommand;
            this.RaisePropertyChanged(nameof(CancelCommand));
            SubmitCommand = ReactiveCommand.Create(() =>
            {
                if (Form is not null && Form.Validate(out _))
                {
                    BeforeSubmit();
                    if (submitAction != default)
                    {
                        var model = submitAction?.Invoke(_store, _model);
                        AfterSubmit(model);
                    }
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
            Model = _mapper.Map<T>(Form);
        }

        protected virtual void OnModelSet()
        {
            Form = _mapper.Map<TForm>(Model);
        }
    }
}
