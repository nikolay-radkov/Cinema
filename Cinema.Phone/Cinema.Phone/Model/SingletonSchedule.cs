using Cinema.Phone.ExecuteQueryService;
using Cinema.Phone.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Phone.Model
{
    public class SingletonSchedule
    {
        private static Schedule schedule = null;

        public static Schedule Schedule
        {
            get
            {
                if (schedule == null)
                {
                    schedule = new Schedule();
                }

                return SingletonSchedule.schedule;
            }
        }

        private SingletonSchedule()
        {
        }
    }
}
