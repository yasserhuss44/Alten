using System;

namespace Common.Messaging.Events
{
    public class PingVehicleRejected : IRejectedEvent
    {
        public Guid Id { get; }
        public string Reason { get; }
        public string Code { get; }

        protected PingVehicleRejected()
        {
        }

        public PingVehicleRejected(Guid id, 
            string reason, string code)
        {
            Id = id;
            Reason = reason;
            Code = code;
        }
    }
}