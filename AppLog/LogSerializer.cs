//1.0.8055.*//
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Xml.Serialization;
using AppLog.Enums;

namespace AppLog
{
   /// <summary>
   /// Каталог для журналов на диске.
   /// </summary>
   internal class LogSerializer
   {
      #region Properties

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

      #endregion Properties

      #region Constructors
      /// <summary>
      /// Инициализирует новый экземпляр класса <see cref="LogSerializer"/> с заданными параметрами.
      /// </summary>
      /// <param name="logDate">Дата журнала.</param>
      /// <param name="serializationFormat">Расширение файла журнала.</param>
      /// <param name="records">Содержание журнала.</param>
      internal LogSerializer(DateTime logDate, SerializationFormat serializationFormat, ObservableCollection<Record> records)
         => (LogDate, SerializationFormat, Records) = (logDate, serializationFormat, records);
      #endregion Constructors

      #region Methods

      /// <summary>
      /// Формирует путь до каталога для журналов на диске.
      /// </summary>
      /// <returns>Строка, содержащая путь.</returns>
      private string GetPathName() => $"{Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)}\\Log\\";

      /// <summary>
      /// Проверяет наличие каталога для журналов на диске. 
      /// </summary>
      /// <returns>Возвращает <c>true</c> если каталог существует. В противном случае <c>false</c></returns>
      private bool PatExists() => Directory.Exists(GetPathName());

      /// <summary>
      /// Создает каталог для журналов на диске.
      /// </summary>
      private void PatCreate() => Directory.CreateDirectory(GetPathName());

      /// <summary>
      /// Формирует полное имя файла, включая путь и расширение.
      /// </summary>
      /// <returns>Строка, содержащая полное имя файла.</returns>
      private string GetFullName() => $"{GetPathName()}{((LogDate == DateTime.MinValue) ? DateTime.Today : LogDate):yyyyMMdd}.log.{SerializationFormat}";

      /// <summary>
      /// Проверяет, существует ли файл.
      /// </summary>
      /// <param name="fullname">Строка, содержащая полное имя и путь к файлу журнала.</param>
      /// <returns><c>true</c>, если файл существует; в противном случае <c>false</c>.</returns>
      private bool FileExists(string fullname) => string.IsNullOrWhiteSpace(fullname)
            ? throw new ArgumentException("message", nameof(fullname))
            : new FileInfo(fullname).Exists;

      /// <summary>
      /// Предоставляет параметры для использования с <c>System.Text.Json.JsonSerializer</c>.
      /// </summary>
      /// <returns>Объект <c>JsonSerializerOptions</c></returns>
      private JsonSerializerOptions GetOptions() => new JsonSerializerOptions
      {
         Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
         Converters = { new JsonStringEnumConverter() },
         WriteIndented = true
      };

      /// <summary>
      /// Чтение файла журнала.
      /// </summary>
      internal ObservableCollection<Record> Deserialize()
      {
         ObservableCollection<Record> records = new ObservableCollection<Record>();

         string fullName = GetFullName();

         if (FileExists(fullName))
         {
            switch (SerializationFormat)
            {
               case SerializationFormat.json:
                  records = JsonSerializer.Deserialize<ObservableCollection<Record>>(File.ReadAllText(fullName), options: GetOptions());
                  break;

               case SerializationFormat.xml:
                  XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Record>));

                  using (FileStream fs = new FileStream(fullName, FileMode.Open))
                     records = (ObservableCollection<Record>)formatter.Deserialize(fs);
                  break;

               default:
                  break;
            }
         }

         return records;
      }

      /// <summary>
      /// Сохранение журнала в файл.
      /// </summary>
      internal async void Serialize()
      {
         // Защита от перезаписи архивного журнала.
         if (LogDate == DateTime.MinValue)
         {
            // Создать новый файл или дописать существующий.

            if (!PatExists())
               PatCreate();

            string fullName = GetFullName();

            ObservableCollection<Record> records = Deserialize();

            foreach (Record record in Records)
               records.Add(record);

            if (records.Count > 0)
            {
               FileInfo fileInfo = new FileInfo(fullName);

               if (FileExists(fullName))
                  fileInfo.Delete();

               switch (SerializationFormat)
               {
                  case SerializationFormat.json:
                     using (FileStream fs = new FileStream(fullName, FileMode.OpenOrCreate))
                        await JsonSerializer.SerializeAsync(fs, records, options: GetOptions());
                     break;

                  case SerializationFormat.xml:
                     XmlSerializer formatter = new XmlSerializer(typeof(ObservableCollection<Record>));

                     using (FileStream fs = new FileStream(fullName, FileMode.OpenOrCreate))
                        formatter.Serialize(fs, records);
                     break;

                  default:
                     break;
               }
            }
         }
      }

      #endregion Methods
   }
}
