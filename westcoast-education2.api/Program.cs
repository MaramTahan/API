using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using westcoast_education2.api.Data;
using westcoast_education2.api.Models;
using westcoast_education2.api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add database support
builder.Services.AddDbContext<WestCoastEducationContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddIdentityCore<UserModel>(options =>
{
    options.User.RequireUniqueEmail = true;
})

.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<WestCoastEducationContext>();

builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//------------------------------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("tokenSettings:tokenKey").Value))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors();
//---------------------------------------------


var app = builder.Build();

//Seed the database
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<WestCoastEducationContext>();
    var userMgr = services.GetRequiredService<UserManager<UserModel>>();
    var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();

    await context.Database.MigrateAsync();
    
    await SeedData.LoadRolesAndUsers(userMgr, roleMgr);
    await SeedData.LoadCoursesData(context);
    await SeedData.LoadCoursesNameData(context);
    await SeedData.LoadStudentData(context);
    await SeedData.LoadTeacherData(context);
}
catch (Exception ex)
{
    Console.WriteLine("{0}", ex.Message);
    throw;
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(c => c.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://127.0.0.1:5500"));

app.MapControllers();

app.Run();
