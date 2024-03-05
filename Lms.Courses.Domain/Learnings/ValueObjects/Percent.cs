namespace Lms.Courses.Domain.Learnings.ValueObjects;

public struct Percent : IComparable
{
    public readonly int Value;

    public Percent(int value)
    {
        if (value < 0 || value > 100)
        {
            throw new ArgumentException($"{nameof(Percent)} должен находиться в границах от 0 до 100");
        }

        Value = value;
    }
    
    public static Percent Create(int value) => new Percent(value);
    
    public static Percent FromDouble(double value)
    {
        var intPercent = (int)Math.Round(value * 100);
        return Create(intPercent);
    }

    #region Overrides

    public bool Equals(Percent other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is Percent && Equals((Percent)obj);
    }

    public override string ToString()
    {
        return Value.ToString();
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public int CompareTo(object obj)
    {
        if (obj == null)
        {
            return 1;
        }

        if (obj is Percent)
        {
            var other = (Percent)obj;
            return Value.CompareTo(other.Value);
        }

        throw new ArgumentException($"Object is not an {nameof(Percent)}");
    }

    public static bool operator ==(Percent left, Percent right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Percent left, Percent right)
    {
        return !Equals(left, right);
    }

    public static implicit operator int(Percent id)
    {
        return id.Value;
    }

    #endregion
}