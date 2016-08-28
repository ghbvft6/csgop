using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace csgop.Functions
{
    class Algebra
    {
        public static float distance(float x1, float y1, float x2, float y2)
        {
            return (float)Math.Sqrt((float)Math.Pow((x1 - x2), 2) + (float)Math.Pow((y1 - y2), 2));
        }
    }
}
