using Auth0.AspNetCore.Authentication;
using FullStack.API.Authorization;
using FullStack.API.Data;
using FullStack.API.Ripositories.Implementation;
using FullStack.API.Ripositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FullStackDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FullStackConnectionString"));
});
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddMvc();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FullStack.Web.Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        }, new List<string>()
        }
    });
});


string domain = builder.Configuration["Auth0:Domain"];
string audience = builder.Configuration["Auth0:ApiIdentifier"];

// configure authentication services
builder.Services.AddAuthentication(o =>
{
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = audience;
    options.RequireHttpsMetadata = false;
    options.Events = new JwtBearerEvents
    {

        OnTokenValidated = context =>
        {
            // Grab the raw value of the token, and store it as a claim so we can retrieve it again later in the request pipeline
            // Have a look at the ValuesController.UserInformation() method to see how to retrieve it and use it to retrieve the
            // user's information from the /userinfo endpoint
            if (context.SecurityToken is JwtSecurityToken token)
            {
                if (context.Principal.Identity is ClaimsIdentity identity)
                {
                    identity.AddClaim(new Claim("access_token", token.RawData));
                }
            }

            return Task.FromResult(0);
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("read:userData", policy => policy.Requirements.Add(new HasScopeRequirement("read:userData", domain)));

});

builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddControllersWithViews();
var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
