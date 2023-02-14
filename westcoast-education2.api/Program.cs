using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using westcoast_education2.api.Data;
using westcoast_education2.api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Add database support
builder.Services.AddDbContext<WestCoastEducationContext>(options => {
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
});

builder.Services.AddIdentityCore<UserModel>()
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<WestCoastEducationContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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


app.UseAuthorization();

app.MapControllers();

app.Run();
