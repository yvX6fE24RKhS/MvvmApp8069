//1.0.8055.*//
using System;

namespace AppLog.Interfaces
{
   /// <summary>
   /// Интерфейс класса записи информации о событии в журнале приложения.
   /// </summary>
   public interface IRecord : IEventInfo
   {
      #region Properties

      /// <summary>
      /// Момент создания записи.
      /// </summary>
      DateTime Moment { get; }

      /// <summary>
      /// Объект сделавший запись.
      /// </summary>
      string Sender { get; set; }

      #endregion Properties
   }
}
