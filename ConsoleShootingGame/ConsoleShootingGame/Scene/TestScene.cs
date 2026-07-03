using System.Text;

public class TestScene : Scene
{
    public override char[,] Render()
    {
        BufferClear();
        DrawString(0,0,$"현재 시각 : {DateTime.Now}");
        DrawString(0,1,$"현재 화면 크기 : {Console.WindowWidth} , {Console.WindowHeight}");
        return buffer;
    }
}