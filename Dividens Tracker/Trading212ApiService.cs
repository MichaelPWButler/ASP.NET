using Dividens_Tracker.Models;
using Newtonsoft.Json;

public class Trading212ApiService
{
    public async Task<Account?> GetAccountInfoAsync(string apiKey)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://live.trading212.com")
        };

        client.DefaultRequestHeaders.Add("Authorization", apiKey);

        var response = await client.GetAsync("/api/v0/equity/account/info");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error: {response.StatusCode} - {content}");

        return JsonConvert.DeserializeObject<Account>(content);
    }

    public async Task<List<Instrument>> GetPortfolioAsync(string apiKey)
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("https://live.trading212.com")
        };

        client.DefaultRequestHeaders.Add("Authorization", apiKey);

        var response = await client.GetAsync("/api/v0/equity/portfolio");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error: {response.StatusCode} - {content}");

        return JsonConvert.DeserializeObject<List<Instrument>>(content);
    }
}
