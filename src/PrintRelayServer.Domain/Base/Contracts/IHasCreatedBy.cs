namespace PrintRelayServer.Domain.Base.Contracts;

public interface IHasCreatedBy
{
    Guid CreatedById { get; }
}