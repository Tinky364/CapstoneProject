using System;
using System.Windows;
using CapstoneProject.Services;

namespace CapstoneProject;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow() { InitializeComponent(); }
        
    private void Window_SourceInitialized(object sender, EventArgs ea)
    {
        WindowAspectRatioService.Register((Window)sender);
    }
}
