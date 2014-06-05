using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Cinema.Phone.Service
{
    [DataContract]
    public class Seat
    {
        private int id;
        private int row;
        private int position;
        private int scheduleId;
        private int userId;
        private string email;

        public Seat(int row, int position, int id, int scheduleId, int userId, string email)
        {
            this.Id = id;
            this.Row = row;
            this.Position = position;
            this.ScheduleId = scheduleId;
            this.UserId = userId;
            this.Email = email;
        }

        public Seat(int row, int position, int scheduleId, int userId, string email)
            : this(row, position, -1, scheduleId, userId, email)
        {
        }

        public Seat(int id, int row, int position)
            : this(row, position, id, -1, -1, string.Empty)
        {
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
        public int Row
        {
            get
            {
                return this.row;
            }

            set
            {
                this.row = value;
            }
        }

        [DataMember]
        public int Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        [DataMember]
        public int ScheduleId
        {
            get
            {
                return this.scheduleId;
            }

            set
            {
                this.scheduleId = value;
            }
        }

        [DataMember]
        public int UserId
        {
            get
            {
                return this.userId;
            }

            set
            {
                this.userId = value;
            }
        }

        [DataMember]
        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
            }
        }
    }
}
