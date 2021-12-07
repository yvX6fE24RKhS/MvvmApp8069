//1.0.8011.*//
using System.Windows;

namespace MvvmApp.View
{
   /// <summary>
   /// Логика взаимодействия для MainWindow.xaml
   /// </summary>
   public partial class MainView : Window
   {
      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="MainView"/>. </summary>
      public MainView()
      {
         InitializeComponent();
         // сборка текущего проекта.
         txbAssemblyVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().FullName;
      }

      #endregion Constructors
   }
}
