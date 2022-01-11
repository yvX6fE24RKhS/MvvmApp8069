//1.0.8045.* : 1.0.8026.*//
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Data;
using System.Windows.Input;
using MvvmApp.Model;
using MvvmApp.ViewModel.Commands;

namespace MvvmApp.ViewModel
{
   [DataContract]
   public class MainViewModel : Foundation.ViewModel
   {

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

      /// <summary>
      /// Команда заполнения коллекции сборок.
      /// </summary>
      public ICommand GetViewAssemblyCommand => Get(() => GetViewAssemblyCommand, new RelayCommand(OnGetViewAssembly));

      /// <summary>
      /// Метод, добавляющий в коллекцию текущие сборки представления, модели представления и приложения.
      /// </summary>
      /// <param name="parameter">Полное имя представления.</param>
      private void OnGetViewAssembly(object parameter)
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
      }

      #endregion Methods
   }
}
