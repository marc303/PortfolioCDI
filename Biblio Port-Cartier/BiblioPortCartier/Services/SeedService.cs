using BiblioPortCartier.Data;
using BiblioPortCartier.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BiblioPortCartier.Services
{
    public class SeedService
    {

        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                //Ensure the database is ready
                logger.LogInformation("Ensuring the database is created.");
                await context.Database.EnsureCreatedAsync();

                //Add roles
                logger.LogInformation("Seeding roles.");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "Employee");
                await AddRoleAsync(roleManager, "Member");

                //Add admin user
                logger.LogInformation("Seeding admin user.");
                var adminEmail = "admin@codehub.com";
                if(await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new Employee
                    {
                        Id = "ADM-101010",
                        FirstName = "Code",
                        LastName = "Hub",
                        Gender = "Male",
                        PhoneNumber = "101 101-0101",
                        HiredDate = DateTime.Now,
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    
                    if (result.Succeeded) {
                        logger.LogInformation("Assigning Admin role to the admin user.");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create admin user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }

                //TODO : Add Employee
                logger.LogInformation("Seeding employee user.");
                var employeeEmail = "marcandre@employee.com";
                if (await userManager.FindByEmailAsync(employeeEmail) == null)
                {
                    var employeeUser = new Employee
                    {
                        Id = "EMP-010101",
                        FirstName = "Marc-André",
                        LastName = "Marchand",
                        Gender = "Male",
                        PhoneNumber = "010 010-1010",
                        HiredDate = DateTime.Now,
                        UserName = employeeEmail,
                        NormalizedUserName = employeeEmail.ToUpper(),
                        Email = employeeEmail,
                        NormalizedEmail = employeeEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(employeeUser, "Marc@123");

                    if (result.Succeeded) 
                    {
                        logger.LogInformation("Assigning Employee role to the employee user.");
                        await userManager.AddToRoleAsync(employeeUser, "Employee");
                    }
                    else
                    {
                        logger.LogError("Failed to create employee user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
                //TODO : Add Member
                logger.LogInformation("Seeding member user.");
                var memberEmail = "jill@valentine.com";
                if (await userManager.FindByEmailAsync(memberEmail) == null)
                {
                    var address = new Address(
                        1111,
                        "11B",
                        "Rue de l'étoile",
                        "Racoon City",
                        "QC",
                        "S1T 0R5"
                    );

                    var birthDate = new DateTime(1974, 02, 14);

                    var memberUser = new Member
                    {
                        Id = "MEM-111111",
                        FirstName = "Jill",
                        LastName = "Valentine",
                        Gender = "Female",
                        PhoneNumber = "111 111-7827",
                        Address = address,
                        BirthdayDate = birthDate,
                        RegisterDate = DateTime.Now,
                        UserName = memberEmail,
                        NormalizedUserName = memberEmail.ToUpper(),
                        Email = memberEmail,
                        NormalizedEmail = memberEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()

                    };

                    var result = await userManager.CreateAsync(memberUser, "Jill@123");

                    if (result.Succeeded)
                    {
                        logger.LogInformation("Assigning Member role to the member user.");
                        await userManager.AddToRoleAsync(memberUser, "Member");
                    }
                    else
                    {
                        logger.LogError("Failed to create member user: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured while seeding the database.");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role '{roleName}': {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
