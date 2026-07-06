public interface IInputable
{
    public void CheckInput(List<KeyAction> keyInputs);
}
public struct KeyAction
{
    public KeyAction(ConsoleKey k, Action a)
    {
        key = [k];
        action = a;
    }
    public KeyAction(ConsoleKey[] k, Action a)
    {
        key = k;
        action = a;
    }
    public ConsoleKey[] key;
    public Action action;
}