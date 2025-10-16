using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoListapp.Data;
using TodoListapp.Mappings;
using TodoListapp.Repositories;
using Microsoft.OpenApi.Models;
using TodoListapp.Interfaces;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = " Todo List App Api", Version = "v1" });
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme,


    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement

   {
          {
        new OpenApiSecurityScheme
        {
             Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = JwtBearerDefaults.AuthenticationScheme
          },

             Scheme ="Oauth2",
             Name = JwtBearerDefaults.AuthenticationScheme,
             In = ParameterLocation.Header

          },

        new List<string>()
        }
   });

}
);

builder.Services.AddDbContext<Todolistdbcontext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TodolistappConnectionString")));  //Add dbcontext


builder.Services.AddDbContext<TodoListAuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("TodolistappAuthConnectionString")));  //Add dbcontext


builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));                                             //Add automapperprofiles
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();                                 //Add repository
builder.Services.AddScoped<ITokenRepository, TokenRepository>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());   //Add enum converter to string
    });

builder.Services.AddDataProtection();                                    //Add data protection befor identity core ,can ignore if authentication is already addeded


builder.Services.AddIdentityCore<IdentityUser>()                       //Add identity core
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("TodoListapp")
    .AddEntityFrameworkStores<TodoListAuthDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>                       //Add identity optiona
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)   // Add authentication
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
