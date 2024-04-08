using System.Text.Json.Serialization;

namespace MoonHotels.Hub.Domain.Contracts.Search.Response;

public class Rate
{
    [JsonPropertyName("mealPlanId")]
    public int MealPlanId { get; set; }

    [JsonPropertyName("isCancellable")]
    public bool IsCancellable { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }
}