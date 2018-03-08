using CSGOP.Games.CSGO;
using CSGOP.OS;
using System.Threading;
using System;
using CSGOP.Memory;
using CSGOP.Data;
using CSGOP.Core;
using csgop.Functions;
using System.Collections.Generic;
using System.Text;

namespace CSGOP.Functions {
    class TextReader : CheatFunction {

        public TextReader(IClient client) : base(client) {
        }

        public override void Run() {
            while (true) {
                Thread.Sleep(1000);
                var list = new List<byte>();
                for (var i = 0; i < client.Text.Length; ++i) { // TODO client.Text.ToArray();
                    list.Add(client.Text[i]);
                }
                Console.WriteLine(Encoding.Unicode.GetString(list.ToArray()));
            }
        }
    }
}