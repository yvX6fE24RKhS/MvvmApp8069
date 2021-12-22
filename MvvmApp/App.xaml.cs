//1.0.8026.* : 1.0.0.0//
using Foundation;
using System.Windows;

namespace MvvmApp
{
   /// <summary>
   /// Логика взаимодействия для App.xaml
   /// </summary>
   public partial class App : Application
   {
      //protected override void OnStartup(StartupEventArgs e)
      //{
      //   base.OnStartup(e);

      //   MainView mainWindow = new MainView();
      //   //MainViewModel context = new MainViewModel();
      //   object context = Store(MainViewModel);
      //   mainWindow.DataContext = context;
      //   mainWindow.Show();
      //}

      protected override void OnExit(ExitEventArgs e)
      {
         // Сериализация свойств объектов.
         Store.Snapshot();

         base.OnExit(e);
      }
   }
}
