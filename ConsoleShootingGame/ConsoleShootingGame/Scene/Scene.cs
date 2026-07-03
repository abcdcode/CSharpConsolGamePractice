public abstract class Scene
{
    public abstract void Render();
    public char[,] buffer;
    public const int BufferWidth = 80;
    public const int BufferHeight = 60;
}