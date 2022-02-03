//1.0..*//
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MvvmApp.ViewModel.Enums
{
   /// <summary>
   /// Перечисление категорий событий инициированных пользователем.
   /// </summary>
   [Flags]
   public enum UserEventCaregory
   {
      /// <summary> Неиспользуемое значение.</summary>
      none = 0b00000000,
      /// <summary> Действия в приложении. </summary>
      [XmlEnum(Name = "get")]
      [Description("просмотр")]
      get = 0b00000001,
      /// <summary> Изменение значений. </summary>
      [XmlEnum(Name = "set")]
      [Description("изменение")]
      set = 0b00000010,
      /// <summary> Отправка комманд. </summary>
      [XmlEnum(Name = "exec")]
      [Description("исполнение")]
      exec = 0b00000100,
      /// <summary> Иное. </summary>
      [XmlEnum(Name = "other")]
      [Description("иное")]
      other = 0b00001000
   }
}