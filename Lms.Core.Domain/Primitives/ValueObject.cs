using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Lms.Core.Domain.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        private int? _cacheHashCode;

        public bool Equals(ValueObject obj)
        {
            return Equals(obj as object);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return GetProperties().All(p => PropertiesAreEqual(obj, p));
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode()
        {
            if(_cacheHashCode.HasValue == false)
            {
                _cacheHashCode = GetProperties()
                    .Select(prop => prop.GetValue(this, null))
                    .Aggregate(11, HashValue);
            }
            
            return _cacheHashCode.Value;
        }

        public static bool operator ==(ValueObject obj1, ValueObject obj2)
        {
            return Equals(obj1, null) ? Equals(obj2, null) : obj1.Equals(obj2);
        }

        public static bool operator !=(ValueObject obj1, ValueObject obj2)
        {
            return (obj1 == obj2) == false;
        }

        private bool PropertiesAreEqual(object obj, PropertyInfo p)
        {
            return Equals(p.GetValue(this, null), p.GetValue(obj, null));
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            return GetType().GetProperties(bindingFlags);
        }

        private static int HashValue(int seed, object value)
        {
            var currentHash = value != null ? value.GetHashCode() : 0;

            return seed * 31 + currentHash;
        }
    }
}