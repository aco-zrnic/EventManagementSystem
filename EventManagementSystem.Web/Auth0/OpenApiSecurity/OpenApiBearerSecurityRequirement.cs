using Microsoft.OpenApi.Models;

namespace EventManagementSystem.Web.Auth0.OpenApiSecurity
{
    public class OpenApiBearerSecurityRequirement : OpenApiSecurityRequirement
    {
        public OpenApiBearerSecurityRequirement(OpenApiSecurityScheme securityScheme)
        {
            this.Add(securityScheme, new[] { "Bearer" });
        }
    }
}
