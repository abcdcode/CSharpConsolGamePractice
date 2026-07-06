public interface IInputable
{
    public void CheckInput();
    public static void DefaultInputCheck(params KeyAction[] keyActions)
    {
        if(keyActions == null || keyActions.Length == 0 || !Console.KeyAvailable) return;
        var key = Console.ReadKey(true);
        foreach(var ka in keyActions)
        {
            if(ka.key == key.Key || ka.keyChar == key.KeyChar)
            {
                ka.action();
            }
        }
    }
}
public struct KeyAction
{
    public KeyAction(ConsoleKey k, Action a)
    {
        key = k;
        action = a;
        keyChar = ' ';
    }
    public KeyAction(ConsoleKey k, Action a, char kc)
    {
        key = k;
        action = a;
        keyChar = kc;
    }
    public KeyAction(char kc, Action a)
    {
        key = ConsoleKey.None;
        action = a;
        keyChar = kc;
    }
    public ConsoleKey key;
    public char keyChar;
    public Action action;
}