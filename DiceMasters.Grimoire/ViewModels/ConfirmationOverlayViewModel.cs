using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;

namespace DiceMasters.Grimoire.ViewModels
{
    public partial class ConfirmationOverlayViewModel : ObservableObject
    {
        [ObservableProperty] private bool _isVisible;

        [ObservableProperty] private string _message = "";

        public Action? OnConfirm { get; set; }
        public Action? OnCancel  { get; set; }

        [RelayCommand]
        private void Confirm()
        {
            IsVisible = false;
            OnConfirm?.Invoke();
        }

        [RelayCommand]
        private void Cancel()
        {
            IsVisible = false;
            OnCancel?.Invoke();
        }

        public void Show(string message,
            Action              onConfirm,
            Action?             onCancel = null)
        {
            Message   = message;
            OnConfirm = onConfirm;
            OnCancel  = onCancel;
            IsVisible = true;
        }
    }
}