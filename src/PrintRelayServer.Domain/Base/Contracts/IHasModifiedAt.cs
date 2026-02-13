namespace PrintRelayServer.Domain.Base.Contracts;

public interface IHasModifiedAt
{
    DateTime? ModifiedAt { get; }
}