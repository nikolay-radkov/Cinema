using Cinema.Phone.ExecuteQueryService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Phone.Model
{
    public class SingletonQuery
    {
        private static QueryClient queryClient = null;

        public static QueryClient QueryClient
        {
            get
            {
                if (queryClient == null)
                {
                    queryClient = new QueryClient();
                }

                return SingletonQuery.queryClient;
            }
        }

        private SingletonQuery()
        {
        }
    }
}
