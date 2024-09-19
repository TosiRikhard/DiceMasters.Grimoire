using System;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProtoBuf;

namespace DiceMasters.Grimoire.ViewModels;

[ProtoContract]
public partial class DiceViewModel : ObservableObject {
    [ProtoMember(1)]
    [ObservableProperty]
    private int _quantity = 1;

    [ProtoMember(2)]
    [ObservableProperty]
    private int _sides = 6;

    [ProtoMember(3)]
    [ObservableProperty]
    private int _modifier = 0;

    [ProtoMember(4)]
    [ObservableProperty]
    private string _description = "";

    [ObservableProperty]
    private int? _result;

    [ObservableProperty]
    private ObservableCollection<string> _individualRolls = new();

    [ObservableProperty]
    private bool _hasRolled = false;

    public string FormattedModifier => Modifier == 0 ? "" : Modifier > 0 ? $"(+{Modifier})" : $"(-{Math.Abs(Modifier)})";

    public DiceViewModel() { } // Parameterless constructor for serialization

    [RelayCommand]
    private void Roll()
    {
        var random = new Random();
        IndividualRolls.Clear();

        for (var i = 0; i < Quantity; i++)
        {
            IndividualRolls.Add($"[{random.Next(1, Sides + 1)}]");
        }

        Result    = IndividualRolls.Sum(r => int.Parse(r.Trim('[', ']'))) + Modifier;
        HasRolled = true;
        OnPropertyChanged(nameof(FormattedModifier));
    }
}