﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class TeacherMeeting
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int MeetingId { get; set; }
    }
}