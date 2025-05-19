//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace _202020Pro.Models
//{
//    internal class GamingModeManager
//    {
//    }
//}

using System;

namespace _202020Pro.Models
{
    public static class GamingModeManager
    {
        public static bool IsGamingModeEnabled => AppSettings.IsGamingMode;

        public static DateTime? GamingStartTime { get; private set; } = null;

        //public static TimeSpan AllowedPerDay = TimeSpan.FromHours(3);
        public static TimeSpan AllowedPerDay => TimeSpan.FromMinutes(AppConfig.GamingModeMinutes);


        public static TimeSpan TotalUsedToday
        {
            get
            {
                if (GamingStartTime == null) return TimeSpan.Zero;
                return DateTime.Now - GamingStartTime.Value;
            }
        }

        public static void EnableGamingMode()
        {
            GamingStartTime = DateTime.Now;
            AppSettings.IsGamingMode = true;
        }

        public static void DisableGamingMode()
        {
            GamingStartTime = null;
            AppSettings.IsGamingMode = false;
        }

        public static bool CanEnableGamingMode()
        {
            if (GamingStartTime != null)
            {
                return TotalUsedToday < AllowedPerDay;
            }
            else
            {
                return true; // لم يتم التفعيل اليوم
            }
        }
    }
}
