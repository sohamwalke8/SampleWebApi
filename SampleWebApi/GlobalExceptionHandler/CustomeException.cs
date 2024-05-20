using Microsoft.AspNetCore.Diagnostics;

namespace SampleWebApi.GlobalExceptionHandler
{
    public class CustomeException:IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context,Exception exception,CancellationToken cancellationToken)
        {
            if (exception == null)
            {
                context.Response.WriteAsJsonAsync("This is Null Value Exception ");
            }
            else  if( exception is DivideByZeroException)
            {
                context.Response.WriteAsJsonAsync("this is Divide by zero Exception", cancellationToken: cancellationToken);
            }
            return true;    
          
        }

    }
}
