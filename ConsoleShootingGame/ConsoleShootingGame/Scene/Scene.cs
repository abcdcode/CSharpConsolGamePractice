public abstract class Scene : IInputable
{
    public abstract char[,] Render();
    public abstract void Update();
    public virtual void OnChangeScene(Scene prevScene)
    {
        
    }
    public void BufferClear()
    {

        buffer = new char[CurrentHeight <= 0 ? 1 : CurrentHeight,CurrentWidth];
        for(int x = 0 ; x < buffer.GetLength(1); x++)
        {
            for(int y = 0 ; y < buffer.GetLength(0); y++)
            {
                buffer[y,x] = ' ';
            }
        }
    }
    // 맵 그리기. 항상 좌상단 0,0부터 그림. x와 y는 맵 크기
    public void DrawMap(int x, int y)
    {
        x = buffer.GetLength(1) > x ? x : buffer.GetLength(1);
        y = buffer.GetLength(0) > y ? y : buffer.GetLength(0);
        for(int i = 0 ; i < x; i++)
        {
            for(int j = 0 ; j < y; j++)
            {
                DrawChar(i,j,'.');
            }
        }
    }
    protected void DrawChar(int x, int y, char txt)
    {
        DrawString(x,y,txt.ToString());
    }
    protected void DrawString(int x, int y, string text, int mX = 999, int mY = 999)
    {
        if(x < 0 || y < 0) return;
        if(Math.Min(buffer.GetLength(0),mY) <= y) return;
        for (int i = 0; i < text.Length; i++)
        {
            if (x + i >= Math.Min(buffer.GetLength(1),mX))
                break;

            buffer[y, x + i] = text[i];
        }
    }
    protected void DrawObject(MapObject obj)
    {
        int x = obj.Position.X;
        int y = obj.Position.Y;
        DrawSString(x,y,obj.RenderShape(), GameState.MapSizeX,GameState.MapSizeY);
    }
    protected void DrawSString(int x, int y, string[] ttext, int mX = 999, int mY = 999)
    {
        for(int i = 0 ; i < ttext.Length; i++)
        {
            DrawString(x,y+i,ttext[i],mX,mY);
        }
    }

    public abstract void CheckInput(List<KeyAction> keyInputs);

    public int CurrentWidth => Console.WindowWidth;
    public int CurrentHeight => Console.WindowHeight-4;
    public char[,] buffer;
}