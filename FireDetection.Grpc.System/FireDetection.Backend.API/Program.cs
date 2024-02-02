using FireDetection.Backend.API;
using FireDetection.Backend.API.Middleware.GraphQL;
using FireDetection.Backend.Domain;
using FireDetection.Backend.Domain.Helpers;
using FireDetection.Backend.Infrastructure.Helpers.ErrorHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddGraphQLServer() 
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
// Add authentication and configure JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWTSecretKey:Issuer"],
            ValidAudience = builder.Configuration["JWTSecretKey:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSecretKey:SecretKey"])),
            ClockSkew = TimeSpan.FromSeconds(1)
        };
    });

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
// Add CORS
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "DressUpExchange-API", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });
    opt.OperationFilter<AuthorizeOperationFilter>();
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var conn = builder.Configuration.GetConnectionString("DefaultConnections");
builder.Services.AddDbContext<FireDetectionDbContext>(options =>
    options.UseNpgsql(conn),
    ServiceLifetime.Scoped);

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<MyMemoryCache>();
builder.Services.AddWebAPIService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.DefaultModelExpandDepth(0);
        c.DefaultModelsExpandDepth(-1);
        c.DefaultModelRendering(ModelRendering.Example);
        c.DisplayOperationId();
        c.DisplayRequestDuration();
        c.DocExpansion(DocExpansion.None);
        c.EnableDeepLinking();
        c.EnableFilter();
        c.ShowExtensions();
        c.EnableValidator();
    });
}

app.Use((context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
    context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
    return next.Invoke();
});

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<CustomExceptionMiddleware>();
app.MapGraphQL();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
