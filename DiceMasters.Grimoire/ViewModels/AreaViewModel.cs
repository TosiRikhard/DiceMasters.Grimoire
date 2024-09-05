using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DiceMasters.Grimoire.ViewModels;
public partial class AreaViewModel : ObservableObject {

    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private ObservableCollection<CreatureViewModel> _creatures = [];

    [ObservableProperty]
    private bool _isEditing = false;

    [RelayCommand]
    private void AddCreature()
    {
        Creatures.Add(new CreatureViewModel { Name = $"Creature {Creatures.Count + 1}" });
    }

    [RelayCommand]
    private void RemoveCreature(CreatureViewModel creature)
    {
        Creatures.Remove(creature);
    }

    [RelayCommand]
    private void FinishEditAreaName()
    {
        IsEditing = false;
    }

    [RelayCommand]
    private void CancelEditAreaName()
    {
        IsEditing = false;
    }
}