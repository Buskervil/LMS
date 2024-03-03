namespace Lms.Users.Domain.Employees.ValueObjects;

public struct Login : IComparable
{
    public readonly string Value;

    public Login(string value)
    {
        Value = value;

        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{nameof(Login)} не может быть пустым");
        }
    }

    public static Login Create(string value) => new Login(value);


    #region Overrides

    public bool Equals(Login other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is Login && Equals((Login)obj);
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

        if (obj is Login)
        {
            var other = (Login)obj;
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        throw new ArgumentException($"Object is not an {nameof(Login)}");
    }

    public static bool operator ==(Login left, Login right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Login left, Login right)
    {
        return !Equals(left, right);
    }

    public static implicit operator string(Login id)
    {
        return id.Value;
    }

    #endregion
}