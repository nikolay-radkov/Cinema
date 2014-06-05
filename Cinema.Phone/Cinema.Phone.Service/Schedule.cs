using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
    
namespace Cinema.Phone.Service
{
    [DataContract]
    public class Schedule
    {
        private int id;
        private DateTime date;
        private float price;

        public Schedule(int id, DateTime date, float price)
        {
            this.Id = id;
            this.Date = date;
            this.Price = price;
        }

        [DataMember]
        public int Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        [DataMember]
        public DateTime Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
            }
        }

        [DataMember]
        public float Price
        {
            get
            {
                return this.price;
            }

            set
            {
                this.price = value;
            }
        }
    }
}
