using AccesoDatos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Negocio;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1", new OpenApiInfo{ Title="Roomya API", Version="v1"});
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        In = ParameterLocation.Header, Name = "Authorization", Type = SecuritySchemeType.Http, Scheme="bearer", BearerFormat="JWT", Description="Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        { new OpenApiSecurityScheme{ Reference = new OpenApiReference{ Type=ReferenceType.SecurityScheme, Id="Bearer"} }, new string[]{} }
    });
});

// DI AccesoDatos
builder.Services.AddSingleton<SqlConnectionFactory>();
builder.Services.AddSingleton<MongoBitacora>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<ClientesRepository>();
builder.Services.AddScoped<HabitacionesRepository>();
builder.Services.AddScoped<ReservasRepository>();
builder.Services.AddScoped<ReportesRepository>();

// Inyección para la bitácora (Mongo)
builder.Services.AddSingleton<IBitacoraService, MongoBitacora>();


// DI Negocio
builder.Services.AddScoped<IClientesLN, ClientesLN>();
builder.Services.AddScoped<IHabitacionesLN, HabitacionesLN>();
builder.Services.AddScoped<IReservasLN, ReservasLN>();

// JWT
var jwtKey = builder.Configuration["jwt:key"] ?? "SuperSecretKeyForRoomya_ChangeThis";
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        RoleClaimType = ClaimTypes.Role,
        NameClaimType = ClaimTypes.Name
    };
});


// Políticas de autorización personalizadas
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanEditClientes", policy =>
        policy.RequireRole("Administrador", "Recepcionista", "Operación", "Operacion"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment()){ app.UseSwagger(); app.UseSwaggerUI(); }

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
