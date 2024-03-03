namespace Lms.Courses.Domain.Courses.ValueObjects;

public struct ArticleContent : IComparable
{
    private const int MaxLength = 1500;
    public readonly string Value;

    public ArticleContent(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{nameof(ArticleContent)} не может быть пустым");
        }

        if (MaxLength < value.Length)
        {
            throw new ArgumentException($"Размер статьи не должен превышать {MaxLength} символов");
        }

        Value = value;
    }

    public static ArticleContent Create(string value) => new ArticleContent(value);

    #region Overrides

    public bool Equals(ArticleContent other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is ArticleContent && Equals((ArticleContent)obj);
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

        if (obj is ArticleContent)
        {
            var other = (ArticleContent)obj;
            return string.Compare(Value, other.Value, StringComparison.Ordinal);
        }

        throw new ArgumentException($"Object is not an {nameof(ArticleContent)}");
    }

    public static bool operator ==(ArticleContent left, ArticleContent right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ArticleContent left, ArticleContent right)
    {
        return !Equals(left, right);
    }

    public static implicit operator string(ArticleContent id)
    {
        return id.Value;
    }

    #endregion
}