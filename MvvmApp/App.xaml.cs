//1.0.0.0//
using MvvmApp.View;
using MvvmApp.ViewModel;
using System.Windows;

namespace MvvmApp
{
   /// <summary>
   /// Логика взаимодействия для App.xaml
   /// </summary>
   public partial class App : Application
   {
      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);

         MainView mainWindow = new MainView();
         MainViewModel context = new MainViewModel();
         mainWindow.DataContext = context;
         mainWindow.Show();
      }
   }
}
