using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Avalonia.Platform.Storage;
using System.Linq;
using Avalonia;
using Avalonia.Controls;

namespace DiceMasters.Grimoire.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
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

    [ObservableProperty] private ObservableCollection<AreaViewModel> _areas = [];

    [ObservableProperty] private AreaViewModel? _selectedArea;

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
        if (Areas.Remove(area) && SelectedArea == area)
        {
            SelectedArea = Areas.Count > 0 ? Areas[0] : null;
        }
    }

    [RelayCommand]
    private async Task SaveState()
    {
        var storageProvider = GetStorageProvider();
        if (storageProvider == null) return;

        var file = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            DefaultExtension = "json",
            FileTypeChoices = new[]
            {
                new FilePickerFileType("JSON Files") { Patterns = new[] { "*.json" } }
            }
        });

        if (file != null)
        {
            var             options = new JsonSerializerOptions { WriteIndented = true };
            var             json    = JsonSerializer.Serialize(Areas, options);
            await using var stream  = await file.OpenWriteAsync();
            await using var writer  = new StreamWriter(stream);
            await writer.WriteAsync(json);
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
                new FilePickerFileType("JSON Files") { Patterns = new[] { "*.json" } }
            }
        });

        if (files.Count > 0)
        {
            var             file        = files[0];
            await using var stream      = await file.OpenReadAsync();
            using var       reader      = new StreamReader(stream);
            var             json        = await reader.ReadToEndAsync();
            var             loadedAreas = JsonSerializer.Deserialize<ObservableCollection<AreaViewModel>>(json);
            if (loadedAreas != null)
            {
                Areas        = loadedAreas;
                SelectedArea = Areas.Count > 0 ? Areas[0] : null;
            }
        }
    }

    [RelayCommand]
    private void StartEditAreaName(AreaViewModel area)
    {
        area.IsEditing = true;
    }
}