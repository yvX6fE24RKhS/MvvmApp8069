using System;
using System.Xml.Serialization;

namespace AppLog
{
   [Serializable]
   public struct KeyValuePair<K, V>
   {
      [XmlAttribute]
      public K Key { get; set; }
      [XmlAttribute]
      public V Value { get; set; }

      public KeyValuePair(K key, V value)
      {
         Key = key;
         Value = value;
      }
   }
}
