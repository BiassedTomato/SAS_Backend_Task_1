using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1
{
    public class ServiceB : IServiceB1, IServiceB2
    {
        static int constructionCount=0;

        public ServiceBSettings Settings { get; private set; }

        public ServiceB(IOptions<ServiceBSettings> settings)
        {
            Settings = settings.Value;
            constructionCount++;
        }

        public int GetConstructionCount()
        {
            return constructionCount;
        }
    }
}
