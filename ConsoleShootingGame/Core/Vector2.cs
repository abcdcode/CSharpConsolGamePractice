using System.Diagnostics.CodeAnalysis;

/// <summary>
/// 2차원 좌표 구조체(유용하다)
/// </summary>
public struct Vector2
{
    public Vector2()
    {
        X = 0;
        Y = 0;
    }
    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }
    public static Vector2 operator +(Vector2 left, Vector2 right)
    {
        left.X += right.X;
        left.Y += right.Y;
        return left;
    }
    public static Vector2 operator -(Vector2 left, Vector2 right)
    {
        left.X -= right.X;
        left.Y -= right.Y;
        return left;
    }
    public static bool operator ==(Vector2 left, Vector2 right)
    {
        return left.Equals(right);
    }
    public static bool operator !=(Vector2 left, Vector2 right)
    {
        return left.Equals(right);
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Vector2 other)
        {
            return Equals(other);
        }

        return false;
    }
    public bool Equals(Vector2 other)
    {
        if (X == other.X)
        {
            return Y == other.Y;
        }
        return false;
    }
        public override readonly int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
    public int X;
    public int Y;
}