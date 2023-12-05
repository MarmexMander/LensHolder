using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;

namespace LensHolder_Map.Views
{
    public partial class PluginsPanel : UserControl
    {
        //Possible bug if Avalonia decided to generate this method finally
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        public PluginsPanel()
        {
            InitializeComponent();
        }
        public async void AddPluginClick(object sender, RoutedEventArgs args)
        {
            // Get top level from the current control. Alternatively, you can use Window reference instead.
            var topLevel = TopLevel.GetTopLevel(this);

            // Start async operation to open the dialog.
            var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false,
            });
            return;
        }
    }
}
