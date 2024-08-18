using Microsoft.AspNetCore.Identity;

namespace BrightStarPhase2AssessmentTestEventManagement.Models
{
    public class RolesInitializer : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public RolesInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                await CreateRoles(roleManager);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        private async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Organiser", "Participant", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }

}
