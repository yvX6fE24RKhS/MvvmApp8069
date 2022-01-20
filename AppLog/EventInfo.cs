//1.0.8055.*//
using AppLog.Enums;
using AppLog.Interfaces;

namespace AppLog
{
   /// <summary>
   /// Описание события.
   /// </summary>   
   public class EventInfo : IEventInfo
   {
      #region IEventInfo Memebers

      public Severity Severity { get; set; } = Severity.info;
      public string Initiator { get; set; }
      public string Category { get; set; }
      public string Event { get; set; }
      public string Message { get; set; }

      #endregion IEventInfo Memebers

      #region Constructors

      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="EventInfo"/>.
      /// </summary>
      public EventInfo() {}

      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="EventInfo"/> с заданными значениями параметров.
      /// </summary>
      /// <param name="evnt">Строка, содержащая имя события.</param>
      /// <param name="msg">Строка сообщения описывающая событие.</param>
      /// <param name="init">Инициатор. Необязательный. Значение по умолчанию <c>0</c>.</param>
      /// <param name="cat">Категория. Необязательный. Значение по умолчанию <c>0</c>.</param>
      /// <param name="sevrt">Уровеньй серьёзности события. Необязательный. Значение по умолчанию <c>Severity.info</c>.</param>
      public EventInfo(string evnt, string msg, string init = "", string cat = "", Severity sevrt = Severity.info)
         => (Severity, Initiator, Category, Event, Message) = (sevrt, init, cat, evnt, msg);

      #endregion Constructors
   }
}
