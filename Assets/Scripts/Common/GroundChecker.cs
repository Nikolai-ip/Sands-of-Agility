namespace Common
{
    public class GroundChecker : LayerChecker
    {
        protected override void SetLayer() => _checkingLayer = Config.GroundLayer;
    }
}