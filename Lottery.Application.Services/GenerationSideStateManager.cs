using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.Application.Services
{
    public class GenerationSideStateManager
    {
        private bool _serverSideGeneration = false;

        public void Set(bool serverSide) => _serverSideGeneration = serverSide;

        public bool ServerSide => _serverSideGeneration;
    }
}
