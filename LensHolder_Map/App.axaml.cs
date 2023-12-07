using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LensHolder_Map.Plugins.Managing;
using LensHolder_Map.Plugins.Storing;
using LensHolder_Map.ViewModels;
using LensHolder_Map.Views;
using ReactiveUI;

namespace LensHolder_Map;

public partial class App : Application
{
    private IPluginStorage pluginStorage;
    private PluginsManager pluginsManager;

    internal IPluginStorage PluginStorage { get => pluginStorage; set => pluginStorage = value; }
    internal PluginsManager PluginsManager { get => pluginsManager; set => pluginsManager = value; }


    public App()
    {
        pluginStorage = new AndroidPluginStorage(); //TODO: Make platform-relative initialization
        pluginsManager = new PluginsManager(PluginStorage);
    }

    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        pluginsManager.Init();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView()
            {
                DataContext = new MainViewModel()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }

}
