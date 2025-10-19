namespace Project.Scripts.GamePlay.Windows
{
    public interface IWindowService
    {
        void Open(WindowId windowId);
        void Close(WindowId windowId);
    }
}