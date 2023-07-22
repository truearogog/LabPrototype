using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Services.WindowService;
using LabPrototype.ViewModels.Components.SettingsListingItems;
using LabPrototype.ViewModels.Models;
using LabPrototype.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.SettingsListings
{
    public abstract class SettingsListingViewModelBase<
        T,
        TService,
        TStore,
        TCreateView, TCreateViewModel, 
        TUpdateView, TUpdateViewModel, 
        TDeleteView, TDeleteViewModel, 
        TListingItemViewModel> 
    : ViewModelBase
        where T : PresentationModelBase 
        where TService : IServiceBase<T> 
        where TStore : IStoreBase<T>
        where TCreateView : DialogWindowBase, new() where TCreateViewModel : DialogViewModelBase, new()
        where TUpdateView : DialogWindowBase, new() where TUpdateViewModel : ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>, new()
        where TDeleteView : DialogWindowBase, new() where TDeleteViewModel : ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>, new()
        where TListingItemViewModel : SettingsListingItemViewModelBase<T>, new()
    {
        private readonly WindowViewModelBase _parentWindow;
        private readonly IWindowService _windowService;
        private readonly TService _modelService;
        private readonly TStore _modelStore;

        public ObservableCollection<SettingsListingItemViewModelBase<T>> ListingItems { get; } = new();

        public ICommand OpenCreateModelCommand { get; }

        public SettingsListingViewModelBase(WindowViewModelBase parentWindow)
        {
            _parentWindow = parentWindow;
            _windowService = GetRequiredService<IWindowService>();

            _modelStore = GetRequiredService<TStore>();
            _modelStore.ModelCreated += _ModelCreated;
            _modelStore.ModelUpdated += _ModelUpdated;
            _modelStore.ModelDeleted += _ModelDeleted;

            _modelService = GetRequiredService<TService>();
            AddModels(_modelService.GetAll().ToList());

            OpenCreateModelCommand = ReactiveCommand.CreateFromTask(ShowCreateDialogAsync);
        }

        public override void Dispose()
        {
            _modelStore.ModelCreated -= _ModelCreated;
            _modelStore.ModelUpdated -= _ModelUpdated;
            _modelStore.ModelDeleted -= _ModelDeleted;

            base.Dispose();
        }

        private void AddModels(IEnumerable<T> models)
        {
            ListingItems.Clear();

            foreach (var model in models)
            {
                AddModel(model);
            }
        }

        private void AddModel(T model)
        {
            var viewModel = new TListingItemViewModel();
            viewModel.Model = model;
            viewModel.Activate(GetOpenModelDialogCommandFactory<TUpdateView, TUpdateViewModel>(), GetOpenModelDialogCommandFactory<TDeleteView, TDeleteViewModel>());
            ListingItems.Add(viewModel);
        }

        private Func<T, ICommand> GetOpenModelDialogCommandFactory<TView, TViewModel>()
            where TView : DialogWindowBase, new() 
            where TViewModel : ParametrizedDialogViewModelBase<ModelNavigationParameter<T>>, new()
        {
            return model => ReactiveCommand.CreateFromTask(async () 
                => await _windowService.ShowDialogAsync<TView, TViewModel, ModelNavigationParameter<T>>(_parentWindow, new ModelNavigationParameter<T> { Model = model }));
        }

        private void _ModelCreated(T model)
        {
            AddModel(model);
        }

        private void _ModelUpdated(T? model)
        {
            if (model is not null)
            {
                var listingItemViewModel = ListingItems.FirstOrDefault(x => x.Model?.Id == model.Id);
                if (listingItemViewModel is not null)
                {
                    listingItemViewModel.Model = model;
                }
            }
        }

        private void _ModelDeleted(int id)
        {
            var listingItemViewModel = ListingItems.FirstOrDefault(x => x.Model?.Id == id);
            if (listingItemViewModel is not null)
            {
                ListingItems.Remove(listingItemViewModel);
            }
        }

        private async Task ShowCreateDialogAsync()
            => await _windowService.ShowDialogAsync<TCreateView, TCreateViewModel>(_parentWindow);
    }
}
