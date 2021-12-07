//1.0.8011.*//
using System;
using System.Windows;
using System.Windows.Input;

namespace MvvmApp.ViewModel.Commands
{
   #region Delegates

   /// <summary>Делегат передающий свои функции методам выполнения команд.</summary>
   /// <param name="parameter">Параметр команды</param>
   public delegate void ExecuteHandler(object parameter);

   /// <summary>Делегат передающий свои функции методам разрешающим выполнение команд.</summary>
   /// <param name="parameter">Параметр команды</param>
   public delegate bool CanExecuteHandler(object parameter);

   #endregion Delegates

   /// <summary>Класс реализующий интерфейс ICommand, пердающий свои функции другим объектам, для создания WPF команд</summary>
   /// <see href="https://www.cyberforum.ru/post13535649.html">author: Элд Хасп</see>
   /// <version>1.0..* : 1.0..*</version>
   public class RelayCommand : ICommand
   {
      #region Delegates

      private readonly CanExecuteHandler _canExecute;
      private readonly ExecuteHandler _onExecute;
      private readonly EventHandler _requerySuggested;

      #endregion Delegates

      #region Events

      /// <summary>Событие извещающее об изменении состояния команды</summary>
      public event EventHandler CanExecuteChanged;

      #endregion Events

      #region Constructors

      /// <summary>Конструктор команды</summary>
      /// <param name="execute">Выполняемый метод команды</param>
      /// <param name="canExecute">Метод разрешающий выполнение команды</param>
      public RelayCommand(ExecuteHandler execute, CanExecuteHandler canExecute = null)
      {
         _onExecute = execute;
         _canExecute = canExecute;
         _requerySuggested = (o, e) => Invalidate();
         CommandManager.RequerySuggested += _requerySuggested;
      }

      #endregion Constructors

      #region Methods

      /// <summary>Вызов разрешающего метода команды</summary>
      /// <param name="parameter">Параметр команды</param>
      /// <returns>True - если выполнение команды разрешено</returns>
      public bool CanExecute(object parameter) => _canExecute == null ? true : _canExecute.Invoke(parameter);

      /// <summary>Вызов выполняющего метода команды</summary>
      /// <param name="parameter">Параметр команды</param>
      public void Execute(object parameter) => _onExecute?.Invoke(parameter);

      /// <summary>Вызов позволяет обновить текущую команду.</summary>
      public void Invalidate()
          => Application.Current.Dispatcher.BeginInvoke(new Action(() =>
          {
             CanExecuteChanged?.Invoke(this, EventArgs.Empty);
          }), null);

      #endregion Methods
   }
}
