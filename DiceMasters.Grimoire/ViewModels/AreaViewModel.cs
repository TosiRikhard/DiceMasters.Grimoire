using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ProtoBuf;

namespace DiceMasters.Grimoire.ViewModels;

[ProtoContract]
public partial class AreaViewModel : ObservableObject {
    [ProtoMember(1)]
    [ObservableProperty]
    private string _name = "";

    [ProtoMember(2)]
    [ObservableProperty]
    private ObservableCollection<CreatureViewModel> _creatures = [];

    [ObservableProperty]
    private string _originalName = "";

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
}