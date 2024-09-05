using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace DiceMasters.Grimoire.ViewModels;
public partial class CreatureViewModel : ObservableObject {
    [ObservableProperty]
    private string _name = "";

    [ObservableProperty]
    private ObservableCollection<DiceViewModel> _dice = [];

    [ObservableProperty]
    private bool _isExpanded = true;

    public CreatureViewModel() { } // Parameterless constructor for serialization

    [RelayCommand]
    private void AddDice()
    {
        Dice.Add(new DiceViewModel());
    }

    [RelayCommand]
    private void RemoveDice(DiceViewModel diceViewModel)
    {
        Dice.Remove(diceViewModel);
    }
}