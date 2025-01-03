using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatapp.api.Common;
using chatapp.api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace chatapp.api.Endpoints
{
    public static class AccountEndpoint
    {
        public static RouteGroupBuilder MapAccountEndpoint(this WebApplication app)
        {
            var group = app.MapGroup("/api/register").WithTags("account");

            group.MapPost("/register", async (HttpContext context, UserManager<AppUser> userManager, [FromForm] string fullName, [FromForm] string email, [FromForm] string password) =>
            {
                var userFromDb = await userManager.FindByEmailAsync(email);

                if (userFromDb is not null)
                {
                    return Results.BadRequest(Response<string>.Failure("User already exists"));
                }

                var user = new AppUser
                {
                    Email = email,
                    FullName = fullName,
                };

                var result = await userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    return Results.BadRequest(Response<string>.Failure(result.
                    Errors.Select(x => x.Description).FirstOrDefault()!));
                }

                return Results.Ok(Response<string>.Success("User created successfully"));
            });

            return group;
        }
    }
}