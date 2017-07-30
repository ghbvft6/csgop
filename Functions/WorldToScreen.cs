using CSGOP.Core.Data;
using CSGOP.Games.CSGO;
using CSGOP.Unmanaged;

namespace CSGOP.Functions {
    class WorldToScreen {

        private External.IValues<float> view;

        public WorldToScreen(External.IValues<float> view) {
            this.view = view;
        }

        public bool conversion(IVector3 C3D, float[] C2D, int width, int height) {
            float w = ((view[12] * C3D.X) + (view[13] * C3D.Y) + (view[14] * C3D.Z) + view[15]);
            if (w > 0.01) {
                C2D[0] = (width / 2) + (0.5f * (((view[0] * C3D.X) + (view[1] * C3D.Y) + (view[2] * C3D.Z) + view[3]) * 1 / w) * width + 0.5f);
                C2D[1] = (height / 2) - (0.5f * (((view[4] * C3D.X) + (view[5] * C3D.Y) + (view[6] * C3D.Z) + view[7]) * 1 / w) * height + 0.5f);
                return true;
            }
            return false;
        }
    }
}