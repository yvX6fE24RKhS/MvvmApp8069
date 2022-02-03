//1.0.8055.* : 1.0.8026.0//
using System;
using System.Diagnostics;
using System.Windows;
using AppLog;
using AppLog.Enums;
using Foundation;
using MvvmApp.View;
using MvvmApp.ViewModel;
using MvvmApp.ViewModel.Enums;

namespace MvvmApp
{
   /// <summary>
   /// Логика взаимодействия для App.xaml
   /// </summary>
   public partial class App : Application
   {
      #region Fields
      private KeyValuePair<int, string> _init = new KeyValuePair<int, string>((int) Initiator.app, Initiator.app.ToString());
      #endregion Fields

      #region Properties

      public MainView MainView { get; private set; }

      public MainViewModel MainViewModel { get; private set; }

      /// <summary>
      /// Журнал текущей сессии.
      /// </summary>
      public static Log Log { get; set; }

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
               init: _init,
               evnt: "Startup",
               msg: $"Пользователь {System.Security.Principal.WindowsIdentity.GetCurrent().Name} запустил приложение {AppDomain.CurrentDomain.FriendlyName} ({System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()})."
            )
         );
         
         Exit += (sender, args) => Log.Append(
            this,
            new EventInfo(
               init: _init,
               evnt: "Exit",
               msg: $"Работа приложения {AppDomain.CurrentDomain.FriendlyName} завершилась с кодом {args.ApplicationExitCode}."
            )
         );

         Exit += (sender, args) => Log.Save();
      }

      #endregion Constructors

      protected override void OnStartup(StartupEventArgs e)
      {

#if DEBUG
         Debug.WriteLine(message: $"Отладка: App.OnStartup executing.");
#endif

         base.OnStartup(e);
         MainViewModel = Store.OfType<MainViewModel>();
         MainViewModel.AppLog = Log;
         MainView = new MainView
         {
            DataContext = MainViewModel
         };
         MainViewModel.FillAssemblyRepository(MainView.GetAssemblyVersion());
         MainView.Show();
      }

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
               init: _init,
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
               init: _init,
               evnt: "Exit",
               msg: "Сериализация свойств объектов приложения завершена."
            )
         );
         base.OnExit(e);
      }
   }
}
