using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DiceMasters.Grimoire.ViewModels;
using System.Threading.Tasks;
using Avalonia.VisualTree;
using System.Linq;

namespace DiceMasters.Grimoire.Views {
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
            this.PointerPressed += Window_PointerPressed;
        }

        private void Window_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            var hitTestResult = this.InputHitTest(e.GetPosition(this));
            if (hitTestResult == this)
            {
                this.Focus();
            }
        }

        private async void TextBlock_DoubleTapped(object? sender, TappedEventArgs e)
        {
            if (sender is TextBlock textBlock &&
                textBlock.DataContext is AreaViewModel areaViewModel)
            {
                areaViewModel.OriginalName = areaViewModel.Name;
                areaViewModel.IsEditing = true;
                await Task.Delay(10);
                if (textBlock.Parent is Grid grid)
                {
                    var textBox = grid.Children.OfType<TextBox>().FirstOrDefault();
                    if (textBox != null)
                    {
                        textBox.Focus();
                        textBox.CaretIndex = textBox.Text?.Length ?? 0;
                    }
                }
            }
        }

        private void TextBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (sender is TextBox textBox &&
                textBox.DataContext is AreaViewModel areaViewModel)
            {
                if (e.Key == Key.Enter || e.Key == Key.Tab)
                {
                    AcceptChanges(areaViewModel, textBox);
                    e.Handled = true;
                }
                else if (e.Key == Key.Escape)
                {
                    CancelChanges(areaViewModel, textBox);
                    e.Handled = true;
                }
            }
        }

        private void TextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox &&
                textBox.DataContext is AreaViewModel areaViewModel)
            {
                AcceptChanges(areaViewModel, textBox);
            }
        }

        private void AcceptChanges(AreaViewModel areaViewModel, TextBox textBox)
        {
            areaViewModel.Name = textBox.Text;
            areaViewModel.IsEditing = false;
        }

        private void CancelChanges(AreaViewModel areaViewModel, TextBox textBox)
        {
            areaViewModel.Name = areaViewModel.OriginalName;
            textBox.Text = areaViewModel.Name;
            areaViewModel.IsEditing = false;
        }

        private void DeleteArea_Click(object? sender, RoutedEventArgs e)
        {
            if (sender is Button button &&
                button.DataContext is AreaViewModel areaViewModel &&
                DataContext is MainWindowViewModel mainViewModel)
            {
                if (areaViewModel.IsEditing)
                {
                    CancelChanges(areaViewModel, null);
                }
                mainViewModel.RemoveAreaCommand.Execute(areaViewModel);
            }
        }
    }
}