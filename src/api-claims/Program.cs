using api_claims;

var claims = new List<Claim>
{
    new Claim { Id = 1, PolicyNumber = "P123456", ClaimAmount = 1500, Description = "Accident claim" },
    new Claim { Id = 2, PolicyNumber = "P234567", ClaimAmount = 3200, Description = "Theft claim" }
};

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();
app.UseHttpsRedirection();

app.MapGet("/claims", () =>
{
    return Results.Ok(claims.OrderBy(e => e.Id));
}).WithName("GetAllClaims").WithTags("Claims");

app.MapGet("/claims/{id:int}", (int id) =>
{
    var claim = claims.SingleOrDefault(c => c.Id == id);
    return claim == null ? Results.NotFound() : Results.Ok(claim);
}).WithName("GetClaimById").WithTags("Claims");

app.MapPost("/claims", (Claim newClaim) =>
{
    if (claims.Any(c => c.Id == newClaim.Id))
    {
        return Results.BadRequest("A claim with the same ID already exists.");
    }

    claims.Add(newClaim);
    return Results.Created($"/claims/{newClaim.Id}", newClaim);
}).WithName("CreateClaim").WithTags("Claims");

app.MapPut("/claims/{id:int}", (int id, Claim updatedClaim) =>
{
    var existingClaim = claims.SingleOrDefault(c => c.Id == id);
    if (existingClaim == null)
    {
        return Results.NotFound();
    }

    // Update the claim details
    existingClaim.PolicyNumber = updatedClaim.PolicyNumber;
    existingClaim.ClaimAmount = updatedClaim.ClaimAmount;
    existingClaim.Description = updatedClaim.Description;

    return Results.Ok(existingClaim);
}).WithName("UpdateClaim").WithTags("Claims");

app.MapDelete("/claims/{id:int}", (int id) =>
{
    var claimToRemove = claims.FirstOrDefault(c => c.Id == id);
    if (claimToRemove == null)
    {
        return Results.NotFound();
    }

    claims = new List<Claim>(claims.Where(c => c.Id != id));
    return Results.NoContent();
}).WithName("DeleteClaim").WithTags("Claims");

app.MapGet("/ping", () => Results.Ok("pong"))
    .WithName("Ping")
    .WithTags("Claims");

app.Run();



