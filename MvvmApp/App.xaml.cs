//1.0.8055.* : 1.0.8026.0//
using System;
using System.Diagnostics;
using System.Windows;
using AppLog;
using AppLog.Enums;
using Foundation;
using MvvmApp.ViewModel.Enums;

namespace MvvmApp
{
   /// <summary>
   /// Логика взаимодействия для App.xaml
   /// </summary>
   public partial class App : Application
   {
      #region Properties

      /// <summary>
      /// Журнал текущей сессии.
      /// </summary>
      internal static Log Log { get; set; }

      #endregion Properties

      #region Constructors

      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="App"./>
      /// </summary>
      public App()
      {
         Log = new Log(); // = newLog(SerializationFormat.json)
         //Log = new Log(SerializationFormat.xml);

         Startup += (sender, args) => Log.Append(
            this,
            new EventInfo(
               init: Initiator.app.ToString(),
               evnt: "Startup",
               msg: $"Пользователь {System.Security.Principal.WindowsIdentity.GetCurrent().Name} запустил приложение {AppDomain.CurrentDomain.FriendlyName} ({System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()})."
            )
         );
         //Exit += (sender, args) => App_OnExit(sender, args);
         Exit += (sender, args) => Log.Append(
            this,
            new EventInfo(
               init: Initiator.app.ToString(),
               evnt: "Exit",
               msg: $"Работа приложения {AppDomain.CurrentDomain.FriendlyName} завершилась с кодом {args.ApplicationExitCode}."
            )
         );

         Exit += (sender, args) => Log.Save();
      }
      
      #endregion Constructors

      /// <summary>
      /// Вызывает событие <c>System.Windows.Application.Exit</c>.
      /// </summary>
      /// <param name="e">Объект класса <c>System.Windows.ExitEventArgs</c>, содержащий данные события.</param>
      protected override void OnExit(ExitEventArgs e)
      {

#if DEBUG
         Debug.WriteLine(message: $"Отладка: App.OnExit executing.");
#endif

         // Журналирование
         Log.Append(
            this,
            new EventInfo(
               init: Initiator.app.ToString(),
               evnt: "Exit",
               msg: "Началась сериализация свойств объектов приложения."
            )
         );

         // Сериализация свойств объектов.
         Store.Snapshot();

         // Журналирование
         Log.Append(
            this,
            new EventInfo(
               init: Initiator.app.ToString(),
               evnt: "Exit",
               msg: "Сериализация свойств объектов приложения завершена."
            )
         );
         base.OnExit(e);
      }
   }
}
