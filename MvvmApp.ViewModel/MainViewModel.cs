//1.0.8026.* : 1.0.8025.*//
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
      #region Commands

      private ICommand _getViewAssemblyCommand;
      public ICommand GetViewAssemblyCommand => _getViewAssemblyCommand ?? (_getViewAssemblyCommand = new RelayCommand(OnGetViewAssembly));

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
         }
      }
      #endregion Commands

      #region Properties

      private CollectionViewSource _assemblies;
      public ICollectionView Assemblies
      {
         get
         {
            if (_assemblies is null)
            {
               _assemblies = new CollectionViewSource();
               _assemblies.Source = AssemblyNames;
               _assemblies.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
            }
            return _assemblies.View;
         }
      }

      public ObservableCollection<AssemblyName> AssemblyNames { get; set; } = new ObservableCollection<AssemblyName>();

      #endregion Properties

      #region Methods

      /// Метод вызывается после того как объект будет десериализован. Используется вместо конструктора.
      [OnDeserialized]
      private void Initialize(StreamingContext context = default(StreamingContext))
      {
         if(AssemblyNames is null)
            AssemblyNames = new ObservableCollection<AssemblyName>();
      }

      #endregion Methods
   }
}
