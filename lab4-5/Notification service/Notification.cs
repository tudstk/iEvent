using System;
using System.Collections.Generic;

namespace NotificationService
{
    public class Notification
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string Destination { get; set; }

        public Notification(string type, string message, string destination)
        {
            Type = type;
            Message = message;
            Destination = destination;
        }

        public override string ToString()
        {
            return $"Type: {Type}, Message: {Message}, Destination: {Destination}";
        }
    }
}
