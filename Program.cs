using System.Text;
using apicoletalixoreciclavel;
using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Asp.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);


#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion
// Add services to the container.
feature/residuo_eletronico
builder.Services.AddControllersWithViews();

#region Repositorios
builder.Services.AddScoped<IResiduoEletronicoRepository, ResiduoEletronicoRepository>();
#endregion

#region Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IResiduoEletronicoService, ResiduoEletronicoService>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c => {
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    c.CreateMap<ResiduoEletronicoModel, ResiduoEletronicoViewModel>();
    c.CreateMap<ResiduoEletronicoViewModel, ResiduoEletronicoModel>();
    
    c.CreateMap<CreateResiduoEletronicoViewModel, ResiduoEletronicoModel>();
    c.CreateMap<ResiduoEletronicoModel, CreateResiduoEletronicoViewModel>();
    
    c.CreateMap<UpdateResiduoEletronicoViewModel, ResiduoEletronicoModel>();
    c.CreateMap<ResiduoEletronicoModel, UpdateResiduoEletronicoViewModel>();
});

IMapper mapper = mapperConfig.CreateMapper();
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
#endregion
builder.Services.AddControllers();

#region Versionamento da API
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


builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

// Configuração avançada do Swagger
builder.Services.AddEndpointsApiExplorer();
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