using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAS_Backend_Task_1
{
    public class ServiceA : IServiceA
    {
        static int constructionCount = 0;

        private IServiceB1 _serviceB1;

        public ServiceASettings Settings { get; private set; }

        public ServiceA(IServiceB1 service, IOptionsSnapshot<ServiceASettings> settings)
        {
            Settings = settings.Value;
            _serviceB1 = service;
            constructionCount++;
        }

        public int GetConstructionCount()
        {
            return constructionCount;
        }
    }
}
