namespace PrintRelayServer.Domain.Base.Contracts;

public interface IHasModifiedBy
{
    Guid? ModifiedById { get; }
}