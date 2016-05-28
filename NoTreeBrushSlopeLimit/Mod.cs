using ICities;

namespace NoTreeBrushSlopeLimit
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public string Name
        {
            get { return "No Tree Brush Slope Limit"; }
        }

        public string Description
        {
            get { return "Allows to place trees on any slopes"; }
        }

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            TerrainManagerDetour.Deploy();
        }

        public override void OnReleased()
        {
            base.OnReleased();
            TerrainManagerDetour.Revert();
        }
    }
}
