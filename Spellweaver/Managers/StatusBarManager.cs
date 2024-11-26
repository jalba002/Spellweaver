namespace Spellweaver.Managers;
public class StatusBarManager
{
    private static StatusBarManager instance;
    public static StatusBarManager Instance
    {
        get => instance;
        set
        {
            if (instance == null)
            {
                instance = value;
            }
        }
    }
    public StatusBarManager()
    {
        Instance = this;
    }

    private List<string> messageLogs = new List<string>();
    private string CurrentMessage;

    public string GetInfo() => CurrentMessage;

    public void SetInfo(string newMessage)
    {
        messageLogs.Add(CurrentMessage);
        CurrentMessage = newMessage;
    }
}
