namespace Lms.Courses.Domain.Courses.ValueObjects;

public struct EntityDescription : IComparable
{
    private const int MaxLength = 300;
    public readonly string Value;

    public EntityDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{nameof(EntityDescription)} не может быть пустым");
        }

        if (MaxLength < value.Length)
        {
            throw new ArgumentException($"Размер описания не должен превышать {MaxLength} символов");
        }

        Value = value;
    }

    public static EntityDescription Create(string value) => new EntityDescription(value);


    #region Overrides

    public bool Equals(EntityDescription other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is EntityDescription && Equals((EntityDescription)obj);
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

        if (obj is EntityDescription)
        {
            var other = (EntityDescription)obj;
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        throw new ArgumentException($"Object is not an {nameof(EntityDescription)}");
    }

    public static bool operator ==(EntityDescription left, EntityDescription right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityDescription left, EntityDescription right)
    {
        return !Equals(left, right);
    }

    public static implicit operator string(EntityDescription id)
    {
        return id.Value;
    }

    #endregion
}