using System;

namespace WebApi.Services
{
    public class Databaselogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DatabaseLogger] - "+message);
        }
    }
}