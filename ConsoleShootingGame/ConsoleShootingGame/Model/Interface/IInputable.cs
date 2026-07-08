/// <summary>
/// 키보드 입력 받는 인터페이스로 만들려 했는데 결과적으로 안 쓴거
/// </summary>
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