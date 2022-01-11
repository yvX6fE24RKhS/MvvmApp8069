//1.0.8026.* : 1.0.0.0//
using Foundation;
using System.Diagnostics;
using System.Windows;
using System;

namespace MvvmApp
{
   /// <summary>
   /// Логика взаимодействия для App.xaml
   /// </summary>
   public partial class App : Application
   {
      /// <summary>
      /// Вызывает событие <c>System.Windows.Application.Exit</c>.
      /// </summary>
      /// <param name="e">Объект класса <c>System.Windows.ExitEventArgs</c>, содержащий данные события.</param>
      protected override void OnExit(ExitEventArgs e)
      {

#if DEBUG
         Debug.WriteLine(message: $"Отладка: App.OnExit executing.");
#endif

         // Сериализация свойств объектов.
         Store.Snapshot();

         base.OnExit(e);
      }
   }
}
