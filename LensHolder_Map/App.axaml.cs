﻿using Avalonia;
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
    internal IPluginStorage pluginStorage;
    PluginsManager pluginsManager;

    public App()
    {
        pluginStorage = new AndroidPluginStorage(); //TODO: Make platform-relative initialization
        pluginsManager = new PluginsManager(pluginStorage);
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MapViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainWindow
            {
                DataContext = new MapViewModel()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }

}
