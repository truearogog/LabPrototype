using LabPrototype.Domain.IServices;
using LabPrototype.Domain.IStores;
using LabPrototype.Domain.Models.Presentation;
using LabPrototype.Services.WindowService;
using LabPrototype.ViewModels.Dialogs.Settings;
using LabPrototype.ViewModels.Models;
using LabPrototype.Views;
using LabPrototype.Views.Dialogs.Settings;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LabPrototype.ViewModels.Components.Settings
{
    public class MeterListingViewModel : ViewModelBase
    {
        private readonly WindowViewModelBase _parentWindow;
        private readonly IWindowService _windowService;
        private readonly IMeterService _meterService;
        private readonly IMeterStore _meterStore;
        public ObservableCollection<MeterListingItemViewModel> ListingItems { get; } = new();
        public ICommand OpenCreateModelCommand { get; }

        public MeterListingViewModel(WindowViewModelBase parentWindow)
        {
            _parentWindow = parentWindow;
            _windowService = GetRequiredService<IWindowService>();

            _meterStore = GetRequiredService<IMeterStore>();
            _meterStore.ModelCreated += _MeterCreated;
            _meterStore.ModelUpdated += _MeterUpdated;
            _meterStore.ModelDeleted += _MeterDeleted;

            _meterService = GetRequiredService<IMeterService>();
            AddModels(_meterService.GetAll());

            OpenCreateModelCommand = ReactiveCommand.CreateFromTask(ShowCreateDialogAsync);
        }

        public override void Dispose()
        {
            _meterStore.ModelCreated -= _MeterCreated;
            _meterStore.ModelUpdated -= _MeterUpdated;
            _meterStore.ModelDeleted -= _MeterDeleted;

            base.Dispose();
        }

        private void _MeterCreated(Meter meter)
        {
            AddModel(meter);
        }

        private void _MeterUpdated(Meter meter)
        {
            if (meter is not null)
            {
                var listingItemViewModel = ListingItems.FirstOrDefault(x => x.Model?.Id == meter.Id);
                if (listingItemViewModel is not null)
                {
                    listingItemViewModel.Model = meter;
                }
            }
        }

        private void _MeterDeleted(int id)
        {
            var listingItemViewModel = ListingItems.FirstOrDefault(x => x.Model?.Id == id);
            if (listingItemViewModel is not null)
            {
                ListingItems.Remove(listingItemViewModel);
            }
        }

        private void AddModels(IEnumerable<Meter> models)
        {
            ListingItems.Clear();

            foreach (var model in models)
            {
                AddModel(model);
            }
        }

        private void AddModel(Meter model)
        {
            var viewModel = new MeterListingItemViewModel
            {
                Model = model
            };
            viewModel.Activate(
                GetOpenModelDialogCommandFactory<MeterUpdateDialog, MeterUpdateDialogViewModel>(), 
                GetOpenModelDialogCommandFactory<MeterDeleteDialog, MeterDeleteDialogViewModel>());
            ListingItems.Add(viewModel);
        }

        private Func<Meter, ICommand> GetOpenModelDialogCommandFactory<TView, TViewModel>()
            where TView : DialogWindowBase, new()
            where TViewModel : ParametrizedDialogViewModelBase<ModelNavigationParameter<Meter>>, new()
        {
            return model => ReactiveCommand.CreateFromTask(async ()
                => await _windowService.ShowDialogAsync<TView, TViewModel, ModelNavigationParameter<Meter>>(_parentWindow, new ModelNavigationParameter<Meter> { Model = model }));
        }

        private async Task ShowCreateDialogAsync()
            => await _windowService.ShowDialogAsync<MeterCreateDialog, MeterCreateDialogViewModel>(_parentWindow);
    }
}
