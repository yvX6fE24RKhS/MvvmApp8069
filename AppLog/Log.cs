//1.0.8055.*//
using System;
using System.Collections.ObjectModel;
using AppLog.Enums;
using AppLog.Interfaces;

namespace AppLog
{
   /// <summary>
   /// Журнал приложения.
   /// </summary>
   public class Log : ILog
   {
      #region Properties
      #region ILog Members

      /// <summary>
      /// Расширение файла журнала.
      /// </summary>
      public SerializationFormat SerializationFormat { get; }

      /// <summary>
      /// Дата журнала.
      /// </summary>
      /// <remarks> Дата журнала событий текущего сеанса равна <c>DateTime.MinValue</c>. </remarks>
      public DateTime LogDate { get; }

      /// <summary>
      /// Содержание журнала.
      /// </summary>
      public ObservableCollection<Record> Records { get; private set; } = new ObservableCollection<Record>();

      /// <summary>
      /// Последнее событие.
      /// </summary>
      public Record LastRecord { get; private set; }

      #endregion ILog Members
      #endregion Properties

      #region Constructors

      /// <summary>
      ///  Инициализирует новый экземпляр класса <see cref="Log"/> для журнала событий текущего сеанса.
      /// </summary>
      public Log() { }

      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="Log"/> для журнала событий текущего сеанса с заданным значением расширения файла.
      /// </summary>
      /// <param name="serializationFormat">Значение перечисления <see cref="Enums.SerializationFormat"/>.</param>
      public Log(SerializationFormat serializationFormat) => SerializationFormat = serializationFormat;

      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="Log"/> с заданным значением даты журнала.
      /// </summary>
      /// <param name="date"><c>DateTime</c>, представляющий дату журнала.</param>
      public Log(DateTime date)
      {
         LogDate = date;
         Open();
      }

      /// <summary>
      ///  Инициализирует новый экземпляр класса <see cref="Log"/> с заданными значениями даты журнала и расширения файла.
      /// </summary>
      /// <param name="date"><c>DateTime</c>, представляющий дату журнала.</param>
      /// <param name="serializationFormat">Значение перечисления <see cref="Enums.SerializationFormat"/>.</param>
      public Log(DateTime date, SerializationFormat serializationFormat)
      {
         LogDate = date;
         SerializationFormat = serializationFormat;
         Open();
      }

      #endregion Constructors

      #region Methods
      #region ILog Members

      /// <summary>
      /// Добавление информации о событии в журнал.
      /// </summary>
      /// <param name="logEvent">Запись о событии.</param>
      public void Append(Record logEvent)
      {
         // защита от перзаписи журнал событий текущего сеанса
         if (LogDate == DateTime.MinValue)
         {
            LastRecord = logEvent;
            Records.Add(logEvent);
         }
      }

      /// <summary>
      /// Добавление информации о событии в журнал.
      /// </summary>
      /// <param name="sender">Объект, создавший событие.</param>
      /// <param name="e">Данные события.</param>
      public void Append(object sender, IEventInfo e)
      {
         Record record = new Record
         {
            Severity = e.Severity,
            Initiator = e.Initiator,
            Category = e.Category,
            Sender = $"{sender.GetType()}",
            Event = e.Event,
            Message = e.Message
         };

         Append(record);
      }

      /// <summary>
      /// Сохранение журнала в файл.
      /// </summary>
      public void Save()
      {
         LogSerializer logSerializer = new LogSerializer(LogDate, SerializationFormat, Records);
         logSerializer.Serialize();
      }

      /// <summary>
      /// Чтение файла журнала.
      /// </summary>
      public void Open()
      {
         LogSerializer logSerializer = new LogSerializer(LogDate, SerializationFormat, Records);
         Records = logSerializer.Deserialize();
      }

      #endregion ILog Members
      #endregion Methods
   }
}
