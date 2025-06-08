using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace apicoletalixoreciclavel.Configurations
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Title = $"API Coleta Lixo Reciclável - {description.GroupName.ToUpper()}",
                    Version = description.ApiVersion.ToString(),
                    Description = "Documentação gerada automaticamente via Swagger com suporte a versionamento.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipe de Desenvolvimento",
                        Email = "contato@seudominio.com"
                    }
                });
            }
        }
    }
}