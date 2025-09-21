using Clinic.API.Extensions;
using Clinic.Application.Extensions;
using Clinic.Application.Interfaces.Services;
using Clinic.Domain.Entities;
using Clinic.Infrastructure.Data.Seeds;
using Clinic.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddPresentaion();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

#region Seed Roles and Users
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();

var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

await DefaultRoles.SeedRoles(roleManager);
await DefaultUsers.SeedUsers(userManager);
#endregion

app.MapControllers();

app.Run();
