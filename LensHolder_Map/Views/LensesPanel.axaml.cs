using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace LensHolder_Map.Views
{
    public partial class LensesPanel : UserControl
    {
        //Possible bug if Avalonia decided to generate this method finally
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public LensesPanel()
        {
            InitializeComponent();
        }
    }
}
