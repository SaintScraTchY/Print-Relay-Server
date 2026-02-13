namespace PrintRelayServer.Domain.Base.Contracts;

/// <summary>
/// Contract for user-tracked entities
/// </summary>
public interface IUserTracked
{
    Guid CreatedBy { get; }
    Guid? ModifiedBy { get; }
}