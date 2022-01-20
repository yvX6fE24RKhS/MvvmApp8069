//1.0.8055.*//
using AppLog.Enums;

namespace AppLog.Interfaces
{
   /// <summary>
   /// Интерфейс класса описания события.
   /// </summary>
   public interface IEventInfo
   {
      #region Properties

      /// <summary>
      /// Уровень серьёзности события.
      /// </summary>
      Severity Severity { get; set; }

      /// <summary>
      /// Инициатор события.
      /// </summary>
      string Initiator { get; set; }

      /// <summary>
      /// Категория события.
      /// </summary>
      string Category { get; set; }

      /// <summary>
      /// Событие приведшее к созданию записи.
      /// </summary>
      string Event { get; set; }

      /// <summary>
      /// Строка сообщения.
      /// </summary>
      string Message { get; set; }

      #endregion Properties
   }
}
