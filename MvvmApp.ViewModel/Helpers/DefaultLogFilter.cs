using System;
using AppLog;
using AppLog.Enums;
using MvvmApp.ViewModel.Enums;

namespace MvvmApp.ViewModel.Helpers
{
   internal class DefaultLogFilter
   {
      internal static LogFilter Get()
      {
         LogFilter logFilter = new LogFilter();

         // Фильтр серьёзности событий.
         foreach (int i in Enum.GetValues(typeof(Severity)))
         {
#if DEBUG
            logFilter.SeverityFilter += i;
#else
            if (i != (int)Severity.debug)
               logFilter.SeverirtFilter += i;
#endif
         }

         // Фильтр инициаторов событий и их категорий.
         foreach (int i in Enum.GetValues(typeof(Initiator)))
         {
            if (i != (int)Initiator.none)
            {
               EventInitiator eventInitiator = new EventInitiator
               {
                  Key = i,
                  Value = Enum.GetName(typeof(Initiator), i)
               };

               if (i == (int)Initiator.user)
               {
                  foreach (UserEventCaregory category in Enum.GetValues(typeof(UserEventCaregory)))
                  {
                     switch (category)
                     {
                        case UserEventCaregory.exec:
                        case UserEventCaregory.set:
                           eventInitiator.Categories.Add((int)category, category.ToString());
                           break;
                        case UserEventCaregory.none:
                        case UserEventCaregory.get:
                        case UserEventCaregory.other:
                        default:
                           break;
                     }
                  }
               }

               logFilter.InitiatorCategoryFilter.Add(i, eventInitiator);
            }
         }

         return logFilter;
      }
   }
}
