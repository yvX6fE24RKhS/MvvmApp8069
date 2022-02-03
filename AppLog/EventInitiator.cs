using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLog
{
   public class EventInitiator
   {
      public int Key { get; set; }
      public string Value { get; set; }
      public Dictionary<int, string> Categories { get; set; } = new Dictionary<int, string>();
   }
}
