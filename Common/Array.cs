using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGOP.Common {
    class Array {
        public static T[] New<T>(int length, Func<int, T> constructor) {
            var array = new T[length];
            for (var i = 0; i < length; ++i) {
                array[i] = constructor(i);
            }
            return array;
        }
    }
}
