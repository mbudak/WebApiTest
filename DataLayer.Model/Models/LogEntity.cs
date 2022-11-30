﻿using DataAccessLayer.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model.Models
{
    public class LogEntity
    {
        public Guid? Guid { get; set; }
        public LogRecordType RecordType { get; set; }
        public string RequestUri { get; set; }
        public string RequestMethod { get; set; }
        public string RequestBody { get; set; }
        public string ResponseContent { get; set; }
        public int StatusCode { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
