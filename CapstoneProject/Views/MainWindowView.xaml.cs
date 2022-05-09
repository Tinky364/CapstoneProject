using System;
using System.Windows;
using CapstoneProject.Services;

namespace CapstoneProject;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindowView : Window
{
    public MainWindowView() { InitializeComponent(); }
        
    private void Window_SourceInitialized(object sender, EventArgs ea)
    {
        WindowAspectRatioService.Register((Window)sender);
    }
}
