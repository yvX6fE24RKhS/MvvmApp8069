//1.0.8011.*//
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace MvvmApp.Model
{
   /// <summary>
   /// Список сборок в решении.
   /// </summary>
   public class AssemblyRepository
   {
      #region Fields

      /// <summary> Список сборок. </summary>
      private List<AssemblyName> items = new List<AssemblyName>();

      #endregion Fields

      #region Properties

      /// <summary> Версию последней сборки в решении. </summary>
      public Version LastVersion
      {
         get
         {
            List<Version> items1 = items.Where(x => x.Version.Major == items.Max(y => y.Version.Major)).Select(x => x.Version).ToList();
            List<Version> items2 = items1.Where(x => x.Minor == items1.Max(y => y.Minor)).ToList();
            List<Version> items3 = items2.Where(x => x.Build == items2.Max(y => y.Build)).ToList();
            List<Version> items4 = items3.Where(x => x.Revision == items3.Max(y => y.Revision)).ToList();
            return items4[0];
         }
      }

      #endregion Properties

      #region Constructors

      /// <summary> Инициализирует новый экземпляр класса <see cref="AssemblyRepository"/>. </summary>
      public AssemblyRepository() => items.Add(Assembly.GetExecutingAssembly().GetName());

      #endregion Constructors

      #region Methods

      /// <summary>
      /// Добавляет элемент в коллекцию.
      /// </summary>
      /// <param name="name">Строка, содержащая имя сборки.</param>
      /// <param name="version">Объект <c>System.Version</c>, содержащий версию сборки.</param>
      public void Add(AssemblyName assemblyName) => items.Add(assemblyName);

      /// <summary>
      /// Возвращает список всех сборок решения. 
      /// </summary>
      /// <returns>Список сборок.</returns>
      public List<AssemblyName> GetVersions() => items;

      #endregion Methods
   }
}
