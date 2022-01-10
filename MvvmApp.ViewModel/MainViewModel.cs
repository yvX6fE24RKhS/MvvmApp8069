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

      /// <summary> коллекция сборок </summary>
      public ObservableCollection<AssemblyName> AssemblyNames { get; set; } = new ObservableCollection<AssemblyName>();

      /// <summary> версия сборки </summary>
      [DataMember]
      public string AssemblyVersion
      {
         get => Get(() => AssemblyVersion);
         set => Set(() => AssemblyVersion, value);
      }

      /// <summary> команда создания коллекции сборок </summary>
      public ICommand GetViewAssemblyCommand => Get(() => GetViewAssemblyCommand, new RelayCommand(OnGetViewAssembly));

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

      /// Метод вызывается после того как объект будет десериализован. Используется вместо конструктора.
      [OnDeserialized]
      private void Initialize(StreamingContext context = default(StreamingContext))
      {
         if (AssemblyNames is null)
            AssemblyNames = new ObservableCollection<AssemblyName>();
      }

      #endregion Methods
   }
}
