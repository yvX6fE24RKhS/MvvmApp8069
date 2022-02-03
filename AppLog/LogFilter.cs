using System.Collections.Generic;
using AppLog.Enums;

namespace AppLog
{
   public class LogFilter
   {
      public int SeverityFilter { get; set; }

      public Dictionary<int, EventInitiator> InitiatorCategoryFilter { get; set; } = new Dictionary<int, EventInitiator>();

      internal bool Contains(Severity severity, KeyValuePair<int, string>? initiator, KeyValuePair<int, string>? category)
      {
         if ((SeverityFilter & (int)severity) != (int)severity)
            return false;

         if (!initiator.HasValue)
            return true;
         else
         {
            if (!InitiatorCategoryFilter.ContainsKey(((KeyValuePair<int, string>)initiator).Key))
               return false;
            else
            {
               if (!category.HasValue || InitiatorCategoryFilter[((KeyValuePair<int, string>)initiator).Key].Categories.ContainsKey(((KeyValuePair<int, string>)category).Key))
                  return true;
               else
                  return false;
            }
         }
      }

      internal bool Contains(Record record)
      { 
         return Contains(record.Severity, record.Initiator, record.Category);
      }
   }
}
