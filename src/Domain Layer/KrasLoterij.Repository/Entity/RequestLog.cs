using System;

namespace NederlandseLoterij.KrasLoterij.Repository.Entity
{
    public class RequestLog
    {
        public int RequestStatusCode { get; set; }
        public DateTime RequestTime { get; set; }
        public Guid Id { get; set; }
        public bool IsException { get; set; }
        public string RequestDetail { get; set; }
    }
}
