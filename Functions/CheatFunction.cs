using CSGOP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csgop.Functions {
    abstract class CheatFunction {
        protected IClient client;

        public CheatFunction(IClient client) {
            this.client = client;
        }

        public abstract void Run();
    }
}
