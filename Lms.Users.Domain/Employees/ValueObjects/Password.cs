using Lms.Users.Domain.Services;

namespace Lms.Users.Domain.Employees.ValueObjects;

public struct Password : IComparable
{
    public readonly string Value;
    private const int MinLength = 8;

    public Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{nameof(Password)} не может быть пустым");
        }

        if (value.Length < MinLength)
        {
            throw new ArgumentException($"Пароль должен состоять минимум из {MinLength} символов");
        }
        
        var encrypted = Encryption.EncryptPassword(value);
        return new Password(encrypted);
    }

    public bool Verify(string value)
    {
        return Encryption.VerifyPassword(value, Value);
    }

    #region Overrides

    public bool Equals(Password other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is Password && Equals((Password)obj);
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

        if (obj is Password)
        {
            var other = (Password)obj;
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        throw new ArgumentException($"Object is not an {nameof(Password)}");
    }

    public static bool operator ==(Password left, Password right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Password left, Password right)
    {
        return !Equals(left, right);
    }

    public static implicit operator string(Password id)
    {
        return id.Value;
    }

    #endregion
}