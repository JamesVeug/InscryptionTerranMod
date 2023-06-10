namespace TerranMod
{
    public interface IPortraitChanges
    {
        bool ShouldRefreshPortrait();
        void RefreshPortrait();
    }
}