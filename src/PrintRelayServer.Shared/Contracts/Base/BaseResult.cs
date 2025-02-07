namespace PrintRelayServer.Shared.Contracts.Base;

public class BaseResult<TEntity>
{
    public bool IsSucceeded { get; set; }
    public string Message { get; set; }
    public TEntity Result { get; set; }
}

public static class ReturnResult<TEntity>
{
    private const string SucceedText = "Succeed";
    private const string ErrorText = "Error";
    private const string WarningText = "Warning";
    public static BaseResult<TEntity> Success(TEntity result, string? message = null) =>
        new BaseResult<TEntity>()
        {
            IsSucceeded = true,
            Message = message ?? SucceedText,
        };
    
    
    public static BaseResult<TEntity> Error(TEntity result, string? message = null) =>
        new BaseResult<TEntity>()
        {
            IsSucceeded = false,
            Message = message ?? ErrorText,
        };
    
    
    public static BaseResult<TEntity> Warning(bool isSucceed,TEntity result, string? message = null) =>
        new BaseResult<TEntity>()
        {
            IsSucceeded = isSucceed,
            Message = message ?? WarningText
        };
}