namespace ACME.DataAccess.Entities
{
    public interface IACMEEntity<out T>
    {
        T Id { get; }
    }
}