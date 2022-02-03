//1.0.8048.*//
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Foundation;

namespace MvvmApp.ViewModel.Commands
{
   public class ViewModelCommands
   {
      private static Type OwnerType { get; set; } = typeof(ViewModelCommands);

      /// <summary>
      /// Тестовая команда.
      /// </summary>
      public static ICommand Test01Command { get; private set; } = new RelayCommand(obj => RunTest01(), obj => true);
      public static void RunTest01()
      {
#if DEBUG
         Debug.WriteLine(message: $"Отладка: ViewModelCommands.RunTest01 executing.");
#endif

      }

      /// <summary>
      /// Тестовая команда.
      /// </summary>
      public static ICommand Test02Command { get; private set; } = new RelayCommand(obj => RunTest02(obj), obj => true);
      public static void RunTest02(object obj) =>
#if DEBUG
         Debug.WriteLine(message: $"Отладка: ViewModelCommands.RunTest02 executing.\n" +
                                  $"         obj: {obj}"
                                  );
#endif

      /// <summary>
      /// Тестовая команда.
      /// </summary>
      public static RoutedCommand Test03Command { get; private set; } = new RoutedCommand("Test03Command", OwnerType) { };
      public static void RunTest03(object sender, ExecutedRoutedEventArgs e) =>
#if DEBUG
         Debug.WriteLine(message: $"Отладка: ViewModelCommands.RunTest03 executing.\n" +
                                  $"         sender: {sender}\n" +
                                  $"         e.Source.GetType(): {e.Source.GetType()}"
                                  );
#endif

      /// <summary>
      /// Тестовая команда.
      /// </summary>
      public static RoutedCommand Test04Command { get; private set; } = new RoutedCommand("Test04Command", OwnerType);
      public static void RunTest04(object sender, ExecutedRoutedEventArgs e) =>
#if DEBUG
         Debug.WriteLine(message: $"Отладка: ViewModelCommands.RunTest03 executing.\n" +
                                  $"         sender: {sender}\n" +
                                  $"         e.Source.GetType(): {e.Source.GetType()}\n" +
                                  $"         e.Parameter: {e.Parameter}"
                                  );
#endif
   }
}
