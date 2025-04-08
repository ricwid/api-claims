namespace api_claims;


public record Claim
{
    public required int Id { get; set; }
    public required string PolicyNumber { get; set; }
    public required decimal ClaimAmount { get; set; }
    public required string Description { get; set; }
}
