using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ProtoBuf;

namespace DiceMasters.Grimoire.ViewModels;

[ProtoContract]
public partial class CreatureViewModel : ObservableObject {
    [ProtoMember(1)]
    [ObservableProperty]
    private string _name = "";

    [ProtoMember(2)]
    [ObservableProperty]
    private ObservableCollection<DiceViewModel> _dice = [];

    [ProtoMember(3)]
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