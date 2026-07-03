using System.Text;

public class TestScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        //DrawString(0,0,$"현재 시각 : {DateTime.Now}");
        //DrawString(0,1,$"현재 화면 크기 : {Console.WindowWidth} , {Console.WindowHeight}");
        //DrawString(0,2,Input);
        DrawMap();
        DrawObjects();
        return buffer;
    }
    public void DrawMap()
    {
        var x = buffer.GetLength(0) > 10 ? 10 : buffer.GetLength(0);
        var y = buffer.GetLength(1) > 10 ? 10 : buffer.GetLength(1);
        for(int i = 0 ; i < x; i++)
        {
            for(int j = 0 ; j < y; j++)
            {
                buffer[j,i] = '.';
            }
        }
    }
    public void DrawObjects()
    {
        var p = GameManager.Instance.State.player;
        DrawObject(p);
    }
    public override async void CheckInput()
    {
        Input = "Wait";
        while(true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                var p = GameManager.Instance.State.player;
                if(key.Key == ConsoleKey.LeftArrow)
                {
                    p.Move(new Vector2(-1,0));
                }
                if(key.Key == ConsoleKey.RightArrow)
                {
                    p.Move(new Vector2(1,0));
                }
                if(key.Key == ConsoleKey.UpArrow)
                {
                    p.Move(new Vector2(0,-1));
                }
                if(key.Key == ConsoleKey.DownArrow)
                {
                    p.Move(new Vector2(0,1));
                }
            }
            await Task.Delay(1);
        }
    }
    public string Input = string.Empty;
}