//1.0.8055.*//
using System;
using System.Xml.Serialization;

namespace AppLog.Enums
{
   /// <summary>
   /// Перечисление уровней серьёзности событий.
   /// </summary>
   [Flags]
   public enum Severity
   {
      /// <summary> Неиспользуемое значение.</summary>
      none = 0b00000000,
      /// <summary> Авария: система непригодна для использования. </summary>
      [XmlEnum(Name = "emergency")]
      emergency = 0b00000001,
      /// <summary> Предупреждение: необходимо немедленно принять меры. </summary>
      [XmlEnum(Name = "alert")]
      alert     = 0b00000010,
      /// <summary> Критическое: критические условия. /// </summary>
      [XmlEnum(Name = "critical")]
      critical  = 0b00000100,
      /// <summary> Ошибка: условия ошибки. </summary>
      [XmlEnum(Name = "error")]
      error     = 0b00001000,
      /// <summary> Предупреждение: условия предупреждения. </summary>
      [XmlEnum(Name = "warning")]
      warning   = 0b00010000,
      /// <summary> Примечание: нормальное, но серьезное состояние. </summary>
      [XmlEnum(Name = "notice")]
      notice    = 0b00100000,
      /// <summary> Информаця: информационные сообщения. </summary>
      [XmlEnum(Name = "info")]
      info      = 0b01000000,
      /// <summary> Отладка: сообщения уровня отладки, полная трассировка. </summary>
      [XmlEnum(Name = "debug")]
      debug     = 0b10000000
   }
}