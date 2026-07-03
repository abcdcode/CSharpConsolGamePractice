using System.Text;

public class TestScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        DrawString(0,0,$"현재 시각 : {DateTime.Now}");
        DrawString(0,1,$"현재 화면 크기 : {Console.WindowWidth} , {Console.WindowHeight}");
        DrawString(0,2,Input);
        return buffer;
    }
    public override async void CheckInput()
    {
        Input = "Wait";
        while(true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);

                Input = key.Key.ToString();
                
            }
            await Task.Delay(1);
        }
    }
    public string Input = string.Empty;
}