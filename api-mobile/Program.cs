using api_mobile.Configs;
using api_mobile.Data;
using api_mobile.DTOs;
using api_mobile.Middlewares;
using api_mobile.Repository;
using api_mobile.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<DTOValidationFilter>();
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;

    });
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseConnection")));
builder.Services.AddServiceSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddHttpClient();


#region ADICIONANDO SERVICES
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<ProdutoService>();
builder.Services.AddScoped<EstoqueRepository>();
builder.Services.AddScoped<EstoqueService>();
builder.Services.AddScoped<ProdutoCategoriaRepository>();
builder.Services.AddScoped<CategoriaRepository>();
builder.Services.AddScoped<CategoriaService>();
builder.Services.AddScoped<ValidadeRepository>();
builder.Services.AddScoped<ValidadeService>();
builder.Services.AddScoped<MovimentacaoRepository>();

#endregion
builder.Services.AddServiceJwt();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var corsName = "cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsName,
        builder =>
        {
            builder.WithOrigins("http://localhost:8100")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();
app.UseMiddleware(typeof(ErrorMiddleware));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsName);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();