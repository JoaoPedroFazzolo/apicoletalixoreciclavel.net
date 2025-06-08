using System.Text;
using apicoletalixoreciclavel;
using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.ViewModels;
using apicoletalixoreciclavel.Services;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Oracle.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using ConfigureSwaggerOptions = apicoletalixoreciclavel.Configurations.ConfigureSwaggerOptions;

var builder = WebApplication.CreateBuilder(args);

#region INICIALIZANDO O BANCO DE DADOS
builder.Services.AddDbContext<apicoletalixoreciclavel.Data.Contexts.DatabaseContext>(options =>
{
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection"));
});
#endregion

#region Repositorios
builder.Services.AddScoped<IResiduoEletronicoRepository, ResiduoEletronicoRepository>();
#endregion

#region Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IResiduoEletronicoService, ResiduoEletronicoService>();
#endregion


#region AutoMapper

// Configura��o do AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    // Permite que cole��es nulas sejam mapeadas
    c.AllowNullCollections = true;
    // Permite que valores de destino nulos sejam mapeados
    c.AllowNullDestinationValues = true;

    c.CreateMap<ResiduoEletronicoModel, ResiduoEletronicoViewModel>();
    c.CreateMap<ResiduoEletronicoViewModel, ResiduoEletronicoModel>();

});

// Cria o mapper com base na configura��o definida
IMapper mapper = mapperConfig.CreateMapper();

// Registra o IMapper como um servi�o singleton no container de DI do ASP.NET Core
builder.Services.AddSingleton(mapper);
#endregion


#region Autenticacao
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });



builder.Services.AddControllers();
#endregion

#region Versionamento
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in app.DescribeApiVersions())
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();