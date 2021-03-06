﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymPassBokning.Models
{
    public class GymClass
    {
        public int GymClassId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime EndTime { get { return StartTime + Duration; } }
        public String Description { get; set; }
       public virtual ICollection<ApplicationUser> AttendingMembers { get; set; }
    }
}