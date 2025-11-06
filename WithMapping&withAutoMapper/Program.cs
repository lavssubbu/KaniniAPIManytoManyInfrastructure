using APIMMwithoutJunctionModel.Data;
using APIMMwithoutJunctionModel.Interface;
using APIMMwithoutJunctionModel.Mapping;
using APIMMwithoutJunctionModel.Models;
using APIMMwithoutJunctionModel.Repository;
using APIMMwithoutJunctionModel.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DocPatientContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn")));
builder.Services.AddScoped<IDocPatient<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IDocPatient<User>, UserRepository>();
builder.Services.AddScoped<IService,DoctorService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddControllers()
    .AddJsonOptions(
    op => op.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(op =>
    {
        op.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Key"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
    { 
        Name = "Authorization", 
        Type = SecuritySchemeType.Http, 
        Scheme = "Bearer", 
        BearerFormat = "JWT", 
        In = ParameterLocation.Header, 
        Description = "JWT Authorization header using the Bearer scheme."
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
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowReact", pol => pol.WithOrigins("http://localhost:5174", "https://localhost:5174","https://localhost:4200","http://localhost:4200")
    .AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
