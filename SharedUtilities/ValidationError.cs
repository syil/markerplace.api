namespace MarketplaceApi.Utilities;

public record ValidationError(IDictionary<string, string[]> ErrorMessages)
{
	public ValidationError(string errorMessage)
		: this(new Dictionary<string, string[]> { ["General"] = new[] { errorMessage } })
	{

	}
}