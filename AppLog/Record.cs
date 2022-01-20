//1.0.8055.*//
using AppLog.Enums;
using AppLog.Interfaces;
using System;

namespace AppLog
{
   /// <summary>
   /// Запись в журнале.
   /// </summary>
   [Serializable]
   public class Record : IRecord
   {
      #region Properties 
      #region IRecord Memebers

      public DateTime Moment { get; set; } = DateTime.Now;
      public string Sender { get; set; }

      #region IEventInfo Memebers

      public Severity Severity { get; set; }
      public string Initiator { get; set; }
      public string Category { get; set; }
      public string Event { get; set; }
      public string Message { get; set; }

      #endregion IEventInfo Memebers
      #endregion IRecord Memebers
      #endregion Properties

      #region Overrided Methods

      /// <summary>
      /// Возвращает строку, представляющую текущий объект.
      /// </summary>
      /// <returns>Строка, представляющая текущий объект.</returns>
      public override string ToString()
      {
         return $"" +
         $"Moment: {Moment.ToLongTimeString()}; " +
         $"Severity: {Severity}; " +
         $"Initiator: {Initiator}; " +
         $"Category: {Category}; " +
         $"Sender: {Sender}; " +
         $"Event: {Event}; " +
         $"Message: {Message};";
      }

      #endregion Overrided Methods
   }
}
