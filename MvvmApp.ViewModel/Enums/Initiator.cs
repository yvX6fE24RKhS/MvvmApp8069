//1.0.8055.*//
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace MvvmApp.ViewModel.Enums
{
   /// <summary>
   /// Перечисление возможных инициаторов событий.
   /// </summary>
   [Flags]
   public enum Initiator
   {
      /// <summary> Неиспользуемое значение.</summary>
      none = 0b00000000,
      /// <summary> Приложение. </summary>
      [XmlEnum(Name = "app")]
      [Description("приложение")]
      app = 0b00000001,
      /// <summary> Торговая система. </summary>
      [XmlEnum(Name = "quik")]
      [Description("торговая система")]
      quik = 0b00000010,
      /// <summary> Пользователь. </summary>
      [XmlEnum(Name = "user")]
      [Description("пользователь")]
      user = 0b00000100
   }
}