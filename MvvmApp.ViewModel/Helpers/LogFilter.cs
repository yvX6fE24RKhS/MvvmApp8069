//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AppLog.Enums;
//using MvvmApp.ViewModel.Enums;

//namespace MvvmApp.ViewModel.Helpers
//{
//   public class LogFilter
//   {
//      internal int SeverirtFilter { get; set; }
//      internal int[,] InitiatorCriteriaFilter { get; set; }

//      public LogFilter()
//      {
//         foreach (int severity in Enum.GetValues(typeof(Severity)))
//            if (severity != (int)Severity.debug)
//               SeverirtFilter += severity;

//         foreach (int initiator in Enum.GetValues(typeof(Initiator)))
//         {
//            switch (initiator)
//            {
//               case (int)Initiator.none:
//                  break;

//               case (int)Initiator.user:
//                  foreach (int category in Enum.GetValues(typeof(UserEventCaregory)))
//                     InitiatorCriteriaFilter.SetValue(initiator, category);

//                  break;

//               default:
//                  InitiatorCriteriaFilter.SetValue(initiator, 0);
//                  break;
//            }
//         }
//      }
//   }
//}
