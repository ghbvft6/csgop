using csgop.CSGO;
using csgop.Imported;
using System;
using System.Diagnostics;
using System.Threading;

namespace csgop.Functions {
    class WorldToScreen {

        private View view;

        public WorldToScreen(View view) {
            this.view = view;
        }

        public bool conversion(BonesVector C3D, float[] C2D, int width, int height) {

            float w = ((view[12] * C3D.X) + (view[13] * C3D.Y) + (view[14] * C3D.Z) + view[15]);

            if (w > 0.01) {
                C2D[0] = (width / 2) + ((float)0.5 * (((view[0] * C3D.X) + (view[1] * C3D.Y) + (view[2] * C3D.Z) + view[3]) * 1 / w) * width + (float)0.5);
                C2D[1] = (height / 2) - ((float)0.5 * (((view[4] * C3D.X) + (view[5] * C3D.Y) + (view[6] * C3D.Z) + view[7]) * 1 / w) * height + (float)0.5);
                return true;
            }

            return false;
        }
    }
}