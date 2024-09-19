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
                mainViewModel.HandleGlobalClick();
            }

            if (Equals(hitTestResult, this))
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

            if (DataContext is MainWindowViewModel mainViewModel)
            {
                mainViewModel.StartEditAreaNameCommand.Execute(areaViewModel);
            }

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
            if (sender is not TextBox { DataContext: AreaViewModel areaViewModel } textBox ||
                DataContext is not MainWindowViewModel mainViewModel)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Enter or Key.Tab:
                    mainViewModel.AcceptAreaChangesCommand.Execute(areaViewModel);
                    e.Handled = true;
                    break;
                case Key.Escape:
                    mainViewModel.CancelAreaEditingCommand.Execute(areaViewModel);
                    e.Handled = true;
                    break;
            }
        }

        private void TextBox_LostFocus(object? sender,
            RoutedEventArgs                    e)
        {
            if (sender is TextBox { DataContext: AreaViewModel areaViewModel } &&
                DataContext is MainWindowViewModel mainViewModel)
            {
                mainViewModel.AcceptAreaChangesCommand.Execute(areaViewModel);
            }
        }

        private void DeleteArea_Click(object? sender,
            RoutedEventArgs                   e)
        {
            if (sender is not Button { DataContext: AreaViewModel areaViewModel } ||
                DataContext is not MainWindowViewModel mainViewModel)
            {
                return;
            }

            mainViewModel.RemoveAreaCommand.Execute(areaViewModel);
        }
    }
}