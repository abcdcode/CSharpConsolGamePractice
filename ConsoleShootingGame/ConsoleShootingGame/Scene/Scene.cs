public abstract class Scene
{
    public abstract char[,] Render();
    public abstract void CheckInput();
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
    protected void DrawChar(int x, int y, char txt)
    {
        DrawString(x,y,txt.ToString());
    }
    protected void DrawString(int x, int y, string text)
    {
        if(x < 0 || y < 0) return;
        if(buffer.GetLength(0) <= y) return;
        for (int i = 0; i < text.Length; i++)
        {
            if (x + i >= buffer.GetLength(1))
                break;

            buffer[y, x + i] = text[i];
        }
    }
    protected void DrawObject(MapObject obj)
    {
        int x = obj.Position.X;
        int y = obj.Position.Y;
        DrawSString(x,y,obj.RenderShape());
    }
    protected void DrawSString(int x, int y, string[] ttext)
    {
        for(int i = 0 ; i < ttext.Length; i++)
        {
            DrawString(x,y+i,ttext[i]);
        }
    }
    public int CurrentWidth => Console.WindowWidth;
    public int CurrentHeight => Console.WindowHeight-4;
    public char[,] buffer;
}