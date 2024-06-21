namespace PosApp.Dommain.Extensions;

public class Result<TResponse> where TResponse : class 
{
    public Result(TResponse response)
    {
        Response = response;    
    }

    public TResponse? Response {get;}

}