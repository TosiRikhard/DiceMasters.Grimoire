using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using DiceMasters.Grimoire.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace DiceMasters.Grimoire.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PointerPressed += Window_PointerPressed;
        }

        private void Window_PointerPressed(object? sender,
            PointerPressedEventArgs                e)
        {
            var hitTestResult = this.InputHitTest(e.GetPosition(this));

            if (DataContext is MainWindowViewModel mainViewModel)
            {
                var editingArea = mainViewModel.Areas.FirstOrDefault(a => a.IsEditing);
                if (editingArea != null)
                {
                    if (hitTestResult is TextBox textBox && textBox.DataContext == editingArea)
                    {
                        // Clicked inside the editing TextBox, do nothing
                        return;
                    }

                    // Clicked outside the editing TextBox, accept changes
                    AcceptChanges(editingArea, null);
                }
            }

            if (hitTestResult == this)
            {
                Focus();
            }
        }

        private async void TextBlock_DoubleTapped(object? sender,
            TappedEventArgs                               e)
        {
            if (sender is not TextBlock { DataContext: AreaViewModel areaViewModel } textBlock)
            {
                return;
            }

            areaViewModel.OriginalName = areaViewModel.Name;
            areaViewModel.IsEditing    = true;
            await Task.Delay(10);

            if (textBlock.Parent is not Grid grid)
            {
                return;
            }

            var textBox = grid.Children.OfType<TextBox>().FirstOrDefault();

            if (textBox is not null)
            {
                textBox.Focus();
                textBox.CaretIndex = textBox.Text?.Length ?? 0;
            }
        }

        private void TextBox_KeyDown(object? sender,
            KeyEventArgs                     e)
        {
            if (sender is not TextBox { DataContext: AreaViewModel areaViewModel } textBox)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Enter or Key.Tab:
                    AcceptChanges(areaViewModel, textBox);
                    e.Handled = true;
                    break;
                case Key.Escape:
                    CancelChanges(areaViewModel, textBox);
                    e.Handled = true;
                    break;
            }
        }

        private void TextBox_LostFocus(object? sender,
            RoutedEventArgs                    e)
        {
            if (sender is TextBox { DataContext: AreaViewModel areaViewModel } textBox)
            {
                AcceptChanges(areaViewModel, textBox);
            }
        }

        private void AcceptChanges(AreaViewModel areaViewModel,
            TextBox?                             textBox)
        {
            if (textBox?.Text is not null)
            {
                areaViewModel.Name = textBox.Text;
            }

            areaViewModel.IsEditing = false;
        }

        private void CancelChanges(AreaViewModel areaViewModel,
            TextBox?                             textBox)
        {
            areaViewModel.Name      = areaViewModel.OriginalName;
            if (textBox != null) textBox.Text = areaViewModel.Name;
            areaViewModel.IsEditing = false;
        }

        private void DeleteArea_Click(object? sender,
            RoutedEventArgs                   e)
        {
            if (sender is not Button { DataContext: AreaViewModel areaViewModel } ||
                DataContext is not MainWindowViewModel mainViewModel)
            {
                return;
            }

            if (areaViewModel.IsEditing)
            {
                CancelChanges(areaViewModel, null);
            }

            mainViewModel.RemoveAreaCommand.Execute(areaViewModel);
        }
    }
}