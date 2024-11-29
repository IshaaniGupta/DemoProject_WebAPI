using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Services._Helper
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Check if Authorization header is present
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Authorization header missing.");
                    return;
                }

                // Retrieve the token (e.g., Bearer token)
                var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                if (string.IsNullOrEmpty(token) || !ValidateToken(token))
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid token.");
                    return;
                }

                // Add user identity to the context (if needed)
                context.Items["User"] = "AuthenticatedUser"; // Replace with actual user details
            }
            catch (Exception ex)
            {
                // Handle exceptions
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync($"An error occurred: {ex.Message}");
                return;
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }

        // Custom method to validate the token (add your logic here)
        private bool ValidateToken(string token)
        {
            // Example: Check if token matches a predefined value
            return token == "valid-token"; // Replace with your token validation logic
        }
    }
}
