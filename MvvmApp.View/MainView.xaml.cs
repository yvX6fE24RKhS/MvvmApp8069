//1.0.8046.* : 1.0.8011.*//
using System.Windows;
using System.Windows.Input;

namespace MvvmApp.View
{
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   public partial class MainView : Window
   {
      #region Constructors

      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="MainView"/>.
      /// </summary>
      public MainView()
      {
         InitializeComponent();
         // Сборка текущего проекта.
         // txbAssemblyVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().FullName;
      }

      #endregion Constructors

      #region ApplicationCommands.Close

      /// <summary>
      /// Метод, который определяет, может ли быть закрыто главное окно приложения.
      /// </summary>
      /// <param name="sender">Объект, источник события.</param>
      /// <param name="e">Объект класса <c>System.Windows.Input.CanExecuteRoutedEventArgs</c>, содержащий данные маршрутизируемого события.</param>
      private void CanExecuteCloseCommand(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

      /// <summary>
      /// Метод, который закрывает главное окно приложения.
      /// </summary>
      /// <param name="sender">Объект, источник события.</param>
      /// <param name="e">Объект класса <c>System.Windows.Input.ExecutedRoutedEventArgs</c>, содержащий данные маршрутизируемого события.</param>
      private void ExecutedCloseCommand(object sender, ExecutedRoutedEventArgs e) => Close();

      #endregion ApplicationCommands.Close

      public string GetAssemblyVersion() => System.Reflection.Assembly.GetExecutingAssembly().FullName;
   }
}
