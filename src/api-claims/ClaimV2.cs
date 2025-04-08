namespace api_claims;

public record ClaimV2
{
    public required int Id { get; set; }
    public required string PolicyNumber { get; set; }
    public required decimal ClaimAmount { get; set; }
    public required string Description { get; set; }
    public required string Category { get; set; }
}