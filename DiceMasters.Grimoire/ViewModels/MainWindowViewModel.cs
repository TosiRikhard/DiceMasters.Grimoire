﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using Avalonia;
using ProtoBuf;
using System.Linq;

namespace DiceMasters.Grimoire.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<AreaViewModel> _areas = [];
    [ObservableProperty] private AreaViewModel?                      _selectedArea;
    [ObservableProperty] private ConfirmationOverlayViewModel        _confirmationOverlay = new();

    private IStorageProvider? GetStorageProvider()
    {
        if (Application.Current is null) return null;
        if (Application.Current.ApplicationLifetime is
            Avalonia.Controls.ApplicationLifetimes.IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.MainWindow?.StorageProvider;
        }

        return null;
    }

    [RelayCommand]
    private void AddArea()
    {
        var newArea = new AreaViewModel { Name = $"Area {Areas.Count + 1}" };
        Areas.Add(newArea);
        SelectedArea = newArea;
    }


    [RelayCommand]
    private void RemoveArea(AreaViewModel area)
    {
        ConfirmationOverlay.Show(
            $"Are you sure you want to delete the area '{area.Name}'?",
            () =>
            {
                if (area.IsEditing)
                {
                    CancelAreaEditing(area);
                }

                if (Areas.Remove(area) && SelectedArea == area)
                {
                    SelectedArea = Areas.Count > 0 ? Areas[0] : null;
                }
            }
        );
    }

    [RelayCommand]
    private void StartEditAreaName(AreaViewModel area)
    {
        area.OriginalName = area.Name;
        area.IsEditing    = true;
    }

    [RelayCommand]
    private void AcceptAreaChanges(AreaViewModel area)
    {
        area.IsEditing = false;
    }

    [RelayCommand]
    private void CancelAreaEditing(AreaViewModel area)
    {
        area.Name      = area.OriginalName;
        area.IsEditing = false;
    }

    [RelayCommand]
    private async Task SaveState()
    {
        var storageProvider = GetStorageProvider();
        if (storageProvider == null) return;

        var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            DefaultExtension = "dmg",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("DiceMaster's Grimoire Files") { Patterns = ["*.dmg"] }
            }
        });

        if (file is not null)
        {
            await using var stream = await file.OpenWriteAsync();
            Serializer.Serialize(stream, Areas);
        }
    }

    [RelayCommand]
    private async Task LoadState()
    {
        var storageProvider = GetStorageProvider();
        if (storageProvider == null) return;

        var files = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            AllowMultiple = false,
            FileTypeFilter = new[]
            {
                new FilePickerFileType("DiceMaster's Grimoire Files") { Patterns = ["*.dmg"] }
            }
        });

        if (files.Count > 0)
        {
            var             file        = files[0];
            await using var stream      = await file.OpenReadAsync();
            var             loadedAreas = Serializer.Deserialize<ObservableCollection<AreaViewModel>>(stream);
            if (loadedAreas != null)
            {
                Areas        = loadedAreas;
                SelectedArea = Areas.Count > 0 ? Areas[0] : null;
            }
        }
    }

    public void HandleGlobalClick()
    {
        var editingArea = Areas.FirstOrDefault(a => a.IsEditing);
        if (editingArea != null)
        {
            AcceptAreaChanges(editingArea);
        }
    }
}