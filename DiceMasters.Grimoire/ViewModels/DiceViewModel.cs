using System;
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

    public DiceViewModel() { } // Parameterless constructor for serialization

    [RelayCommand]
    private void Roll()
    {
        var random = new Random();
        var total  = 0;
        for (var i = 0; i < Quantity; i++)
        {
            total += random.Next(1, Sides + 1);
        }
        Result = total + Modifier;
    }
}