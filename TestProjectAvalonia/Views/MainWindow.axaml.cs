using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI.Fody;
using ReactiveUI.Fody.Helpers;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestProjectAvalonia.Views
{
    public class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        
    }
}
