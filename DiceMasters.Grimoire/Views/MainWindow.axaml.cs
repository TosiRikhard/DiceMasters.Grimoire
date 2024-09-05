using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DiceMasters.Grimoire.ViewModels;

namespace DiceMasters.Grimoire.Views {
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBlock_DoubleTapped(object? sender, TappedEventArgs e)
        {
            if (sender is TextBlock textBlock &&
                textBlock.DataContext is AreaViewModel areaViewModel)
            {
                areaViewModel.IsEditing = true;
            }
        }

        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox &&
                textBox.DataContext is AreaViewModel areaViewModel)
            {
                if (e.Key == Key.Enter)
                {
                    areaViewModel.IsEditing = false;
                    e.Handled               = true;
                }
                else if (e.Key == Key.Escape)
                {
                    areaViewModel.IsEditing = false;
                    // Revert the name change
                    textBox.Text = areaViewModel.Name;
                    e.Handled    = true;
                }
            }
        }

        private void TextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox &&
                textBox.DataContext is AreaViewModel areaViewModel)
            {
                areaViewModel.IsEditing = false;
            }
        }
    }
}