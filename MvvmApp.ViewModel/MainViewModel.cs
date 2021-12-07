//1.0.8011.*//
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmApp.Model;
using MvvmApp.ViewModel.Commands;

namespace MvvmApp.ViewModel
{
   public class MainViewModel //: INotifyPropertyChanged
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

      public ObservableCollection<AssemblyName> AssemblyNames { get; set; } = new ObservableCollection<AssemblyName>();
      
      #endregion Properties

      //public event PropertyChangedEventHandler PropertyChanged;

      //private void OnPropertyChanged([CallerMemberName] string propertyName = "")
      //      => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}
