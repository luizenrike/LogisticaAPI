using Microsoft.OpenApi.Models;

namespace LogisticaAPI.Configurations
{
    public static class SwaggerConfig
    {
        public static void AddSwagger(this IServiceCollection Services)
        {
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LogisticaAPI", Version = "v1" });
                var security = new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br/>
                            Enter a token in a <strong> 'Bearer {token}' </strong> format to authenticate to this API.",
                    Name = "Authorization",
                    In = ParameterLocation.Header
                };
                c.AddSecurityDefinition("Bearer", security);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            new List<string>()
                        }
                    });
            });

        }
    }
}
