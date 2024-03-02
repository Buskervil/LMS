namespace Lms.Core.Domain.Primitives
{
    public abstract class Entity : IEquatable<Entity>
    {
        protected Entity()
        {

        }

        public abstract bool Equals(Entity other);
        public abstract override int GetHashCode();
    }
}
