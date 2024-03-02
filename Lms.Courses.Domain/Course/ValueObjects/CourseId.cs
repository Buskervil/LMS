using System;

namespace Lms.Courses.Domain.Course.ValueObjects;

public struct CourseId : IComparable
{
    public readonly Guid Value;

    public CourseId(Guid value)
    {
        Value = value;

        if (Value == Guid.Empty)
        {
            throw new ArgumentException($"{nameof(CourseId)} не может быть пустым");
        }
    }

    public static CourseId Create() => Create(Guid.NewGuid());

    public static CourseId Create(Guid value) => new CourseId(value);

    #region Overrides

    public bool Equals(CourseId other)
    {
        return Equals(Value, other.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is CourseId && Equals((CourseId)obj);
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

        if (obj is CourseId)
        {
            var other = (CourseId)obj;
            return Value.CompareTo(other.Value);
        }

        throw new ArgumentException($"Object is not an {nameof(CourseId)}");
    }

    public static bool operator ==(CourseId left, CourseId right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(CourseId left, CourseId right)
    {
        return !Equals(left, right);
    }

    public static implicit operator Guid(CourseId id)
    {
        return id.Value;
    }

    #endregion
}