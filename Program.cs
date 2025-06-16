using System.Text;
using apicoletalixoreciclavel.Data.Contexts;
using apicoletalixoreciclavel.Data.Repository;
using apicoletalixoreciclavel.Models;
using apicoletalixoreciclavel.Services;
using apicoletalixoreciclavel.ViewModels;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

#region INICIALIZANDO O BANCO DE DADOS
var connectionString = builder.Configuration.GetConnectionString("OracleConnection");
builder.Services.AddDbContext<DatabaseContext>(
    opt => opt.UseOracle(connectionString).EnableSensitiveDataLogging(true)
);
#endregion

#region Repositorios
builder.Services.AddScoped<IResiduoEletronicoRepository, ResiduoEletronicoRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>(); 
builder.Services.AddScoped<IColetaRepository, ColetaRepository>();
builder.Services.AddScoped<IPontoColetaRepository, PontoColetaRepository>();
builder.Services.AddScoped<IDestinacaoRepository, DestinacaoRepository>();
#endregion

#region Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IResiduoEletronicoService, ResiduoEletronicoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>(); 
builder.Services.AddScoped<IColetaService, ColetaService>();
builder.Services.AddScoped<IPontoColetaService, PontoColetaService>();
builder.Services.AddScoped<IDestinacaoService, DestinacaoService>();
#endregion

#region AutoMapper
var mapperConfig = new AutoMapper.MapperConfiguration(c =>
{
    c.AllowNullCollections = true;
    c.AllowNullDestinationValues = true;

    // Mapeamentos ResiduoEletronico
    c.CreateMap<ResiduoEletronicoModel, ResiduoEletronicoViewModel>();
    c.CreateMap<ResiduoEletronicoViewModel, ResiduoEletronicoModel>();
    c.CreateMap<CreateResiduoEletronicoViewModel, ResiduoEletronicoModel>()
        .ForMember(dest => dest.ResiduoEletronicoId, opt => opt.Ignore())
        .ForMember(dest => dest.Usuario, opt => opt.Ignore());
    c.CreateMap<UpdateResiduoEletronicoViewModel, ResiduoEletronicoModel>()
        .ForMember(dest => dest.ResiduoEletronicoId, opt => opt.Ignore())
        .ForMember(dest => dest.Usuario, opt => opt.Ignore());

    // Mapeamentos Relatório
    c.CreateMap<CreateRelatorioViewModel, RelatorioModel>()
        .ForMember(dest => dest.RelatorioId, opt => opt.Ignore())
        .ForMember(dest => dest.DataGeracao, opt => opt.MapFrom(src => DateTime.Now));
    c.CreateMap<RelatorioModel, RelatorioViewModel>();
    c.CreateMap<UpdateRelatorioViewModel, RelatorioModel>()
        .ForMember(dest => dest.RelatorioId, opt => opt.Ignore())
        .ForMember(dest => dest.DataGeracao, opt => opt.Ignore());

    // Mapeamentos Usuario
    c.CreateMap<UsuarioModel, UsuarioViewModel>();
    c.CreateMap<CreateUsuarioViewModel, UsuarioModel>()
        .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
        .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.ResiduosEletronicos, opt => opt.Ignore());
    c.CreateMap<UpdateUsuarioViewModel, UsuarioModel>()
        .ForMember(dest => dest.UsuarioId, opt => opt.Ignore())
        .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
        .ForMember(dest => dest.ResiduosEletronicos, opt => opt.Ignore());
    c.CreateMap<LoginViewModel, UsuarioModel>();

    // Mapeamentos Coleta
    c.CreateMap<ColetaModel, ColetaViewModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ColetaId))
        .ForMember(dest => dest.Data_Coleta, opt => opt.MapFrom(src => src.DataColeta))
        .ForMember(dest => dest.Residuo_Id, opt => opt.MapFrom(src => src.ResiduoId))
        .ForMember(dest => dest.Ponto_Coleta_Id, opt => opt.MapFrom(src => src.PontoColetaId))
        .ForMember(dest => dest.PontoColetaNome, opt => opt.MapFrom(src => src.PontoColeta.Nome))
        .ForMember(dest => dest.ResiduoTipo, opt => opt.MapFrom(src => src.Residuo.Tipo));
    c.CreateMap<CreateColetaViewModel, ColetaModel>()
        .ForMember(dest => dest.ColetaId, opt => opt.Ignore())
        .ForMember(dest => dest.PontoColeta, opt => opt.Ignore())
        .ForMember(dest => dest.Residuo, opt => opt.Ignore());
    c.CreateMap<UpdateColetaViewModel, ColetaModel>()
        .ForMember(dest => dest.ColetaId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.DataColeta, opt => opt.MapFrom(src => src.DataColeta))
        .ForMember(dest => dest.ResiduoId, opt => opt.MapFrom(src => src.ResiduoId))
        .ForMember(dest => dest.PontoColetaId, opt => opt.MapFrom(src => src.PontoColetaId))
        .ForMember(dest => dest.PontoColeta, opt => opt.Ignore())
        .ForMember(dest => dest.Residuo, opt => opt.Ignore());

    // Mapeamentos PontoColeta
    c.CreateMap<PontoColetaModel, PontoColetaViewModel>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PontoColetaId))
        .ForMember(dest => dest.ColetaCount, opt => opt.MapFrom(src => src.Coletas != null ? src.Coletas.Count : 0));
    c.CreateMap<CreatePontoColetaViewModel, PontoColetaModel>()
        .ForMember(dest => dest.PontoColetaId, opt => opt.Ignore())
        .ForMember(dest => dest.Coletas, opt => opt.Ignore());
    c.CreateMap<UpdatePontoColetaViewModel, PontoColetaModel>()
        .ForMember(dest => dest.PontoColetaId, opt => opt.MapFrom(src => src.Id))
        .ForMember(dest => dest.Coletas, opt => opt.Ignore());
        
         // Mapeamentos Destinação
    c.CreateMap<DestinacaoModel, DestinacaoViewModel>();
    c.CreateMap<CreateDestinacaoViewModel, DestinacaoModel>()
        .ForMember(dest => dest.DestinacaoId, opt => opt.Ignore())
        .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.Now))
        .ForMember(dest => dest.DataAtualizacao, opt => opt.Ignore())
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Ativo"));
    c.CreateMap<UpdateDestinacaoViewModel, DestinacaoModel>()
        .ForMember(dest => dest.DestinacaoId, opt => opt.Ignore())
        .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
        .ForMember(dest => dest.DataAtualizacao, opt => opt.MapFrom(src => DateTime.Now));

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

// Controllers para API (não MVC)
builder.Services.AddControllers();

#region Swagger e Versionamento
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "API Coleta Lixo Reciclável",
        Version = "v1",
        Description = "API para gerenciamento de coleta de lixo reciclável"
    });
    
    // Configuração JWT no Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando Bearer scheme. Exemplo: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Coleta Lixo Reciclável v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();