namespace api_claims;


public record Claim
{
    public int Id { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public decimal ClaimAmount { get; set; }
    public string Description { get; set; } = string.Empty;
}
