public class GameManager
{
    public void Start()
    {
        curScene = new TestScene();
        MainLoop();
    }
    public async void MainLoop()
    {
        while(true)
        {
            Console.SetCursorPosition(0, 0);
            curScene.Render();
            await Task.Delay(1);
        }
    }
    public Scene curScene;
}