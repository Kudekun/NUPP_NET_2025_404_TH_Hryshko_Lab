namespace Cinema.Common.Interfaces;

/// <summary>
/// Interface for entities with Id
/// </summary>
public interface IEntity
{
    Guid Id { get; set; }
}
