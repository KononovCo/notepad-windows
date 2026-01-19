namespace Notepad
{
    public class NotepadReference : Access
    {
        public void Program() => GetMethod().ShowDialog(new InfoWindow());
    }
}