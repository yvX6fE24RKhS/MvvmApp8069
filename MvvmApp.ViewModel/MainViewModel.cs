//1.0.8048.* : 1.0.8045.*//
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Data;
using System.Windows.Input;
using MvvmApp.Model;
using MvvmApp.ViewModel.Commands;
using AppLog;
using AppLog.Enums;
using MvvmApp.ViewModel.Helpers;
using System.Windows;

namespace MvvmApp.ViewModel
{
   [DataContract]
   public class MainViewModel : Foundation.ViewModel
   {

      #region ApplicationLog Properties

      /// <summary>
      /// Фильтр журналируемых событий.
      /// </summary>
      [DataMember]
      public LogFilter AppLogFilter
      {
         get => Get(() => AppLogFilter);
         set => Set(() => AppLogFilter, value);
      }

      public Log AppLog { get; set; }

      #endregion ApplicationLog Properties

      #region AssemblyViewModel

      /// <summary>
      /// Коллекция сборок. 
      /// </summary>
      public ObservableCollection<AssemblyName> AssemblyNames { get; set; } = new ObservableCollection<AssemblyName>();

      /// <summary>
      /// Версия сборки. 
      /// </summary>
      [DataMember]
      public string AssemblyVersion
      {
         get => Get(() => AssemblyVersion);
         set => Set(() => AssemblyVersion, value);
      }

      public string TestStr
      {
         get => Get(() => TestStr, "DefaultTestString");
         set => Set(() => TestStr, value);
      }

      ///// <summary>
      ///// Версия сборки представления. 
      ///// </summary>
      //public string ViewAssemblyVersion
      //{
      //   get => Get(() => ViewAssemblyVersion);
      //   set
      //   {
      //      Set(() => ViewAssemblyVersion, value);
      //      if(!string.IsNullOrWhiteSpace(value)) 
      //         OnGetViewAssembly(value);
      //   }
      //}

      ///// <summary>
      ///// Команда заполнения коллекции сборок.
      ///// </summary>
      //public ICommand GetViewAssemblyCommand => Get(() => GetViewAssemblyCommand, new RelayCommand(FillAssemblyRepository));

      /// <summary>
      /// Метод, добавляющий в коллекцию текущие сборки представления, модели представления и приложения.
      /// </summary>
      /// <param name="parameter">Полное имя представления.</param>
      public void FillAssemblyRepository(object parameter)
      {
         AssemblyName assemblyName = new AssemblyName((string)parameter);
         if (assemblyName != null)
         {
            AssemblyRepository assemblyRepository = new AssemblyRepository();
            assemblyRepository.Add(assemblyName); // MvvmApp.View
            assemblyRepository.Add(Assembly.GetExecutingAssembly().GetName()); // MvvmApp.ViewModel
            assemblyRepository.Add(Assembly.GetEntryAssembly().GetName()); // MvvmApp
            assemblyRepository.GetVersions().ForEach(x => AssemblyNames.Add(x));

            AssemblyVersion = assemblyRepository.LastVersion.ToString();
         }
      }

      #endregion AssemblyViewModel

      #region Methods

      /// <summary> Метод вызывается после того как объект будет десериализован. Используется вместо конструктора. </summary>
      /// <param name="context">Поток сериализации.</param>
      [OnDeserialized]
      private void Initialize(StreamingContext context = default(StreamingContext))
      {
         if (AssemblyNames is null)
            AssemblyNames = new ObservableCollection<AssemblyName>();

         if (AppLogFilter is null)
            AppLogFilter = DefaultLogFilter.Get();

         InitialazeCommandCollection();
      }

      private void InitialazeCommandCollection() 
      {
         this[ViewModelCommands.Test01Command].Executed += (sender, args) => ViewModelCommands.RunTest01();
         this[ViewModelCommands.Test02Command].Executed += (sender, args) => ViewModelCommands.RunTest02(args);
         this[ViewModelCommands.Test03Command].Executed += (sender, args) => ViewModelCommands.RunTest03(sender, args);
         this[ViewModelCommands.Test04Command].Executed += (sender, args) => ViewModelCommands.RunTest04(sender, args);
      }

      #endregion Methods
   }
}
