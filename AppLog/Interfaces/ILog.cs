//1.0.8055.*//
using System;
using System.Collections.ObjectModel;
using AppLog.Enums;

namespace AppLog.Interfaces
{
   /// <summary>
   /// Интерфейс класса журнала приложения.
   /// </summary>
   public interface ILog
   {
      #region Properties

      /// <summary>
      /// Расширение файла журнала.
      /// </summary>
      SerializationFormat SerializationFormat { get; }

      /// <summary>
      /// Дата журнала.
      /// </summary>
      DateTime LogDate { get; }

      /// <summary>
      /// Содержание журнала.
      /// </summary>
      ObservableCollection<Record> Records { get; }

      /// <summary>
      /// Последнее событие.
      /// </summary>
      Record LastRecord { get; }

      #endregion Properties

      #region Methods

      /// <summary>
      /// Добавление информации о событии в журнал.
      /// </summary>
      /// <param name="record">Информация о событии.</param>
      void Append(Record record);

      /// <summary>
      /// Добавление информации о событии в журнал.
      /// </summary>
      /// <param name="sender">Объект, создавший событие.</param>
      /// <param name="e">Данные события.</param>
      void Append(object sender, IEventInfo e);

      /// <summary>
      /// Запись журнала в файл.
      /// </summary>
      void Save();

      /// <summary>
      /// Чтение файла журнала.
      /// </summary>
      void Open();

      #endregion Methods
   }
}
