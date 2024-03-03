namespace Lms.Courses.Domain.Courses.ValueObjects;

public struct EntityName : IComparable
{
    private const int MaxLength = 150;
    public readonly string Value;

    public EntityName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{nameof(EntityName)} не может быть пустым");
        }

        if (MaxLength < value.Length)
        {
            throw new ArgumentException($"Размер наименования не должен превышать {MaxLength} символов");
        }

        Value = value;
    }

    public static EntityName Create(string value) => new EntityName(value);


    #region Overrides

    public bool Equals(EntityName other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is EntityName && Equals((EntityName)obj);
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

        if (obj is EntityName)
        {
            var other = (EntityName)obj;
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        throw new ArgumentException($"Object is not an {nameof(EntityName)}");
    }

    public static bool operator ==(EntityName left, EntityName right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(EntityName left, EntityName right)
    {
        return !Equals(left, right);
    }

    public static implicit operator string(EntityName id)
    {
        return id.Value;
    }

    #endregion
}