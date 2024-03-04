using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LMSApi;

// ReSharper disable once ClassNeverInstantiated.Global
public class MyHeaderFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();
 
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "lms-auth-token",
            In = ParameterLocation.Header,
            Required = false
        });
    }
}