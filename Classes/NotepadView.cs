namespace Notepad
{
    public class NotepadView : Access
    {
        public void IncreaseFontSize() => GetMethod().ChangeFontSize(StateFontSize.INCREASE);
        public void DecreaseFontSize() => GetMethod().ChangeFontSize(StateFontSize.DECREASE);
        public void InitializeFontSize() => GetMethod().ChangeFontSize(StateFontSize.NORMAL);
    }
}