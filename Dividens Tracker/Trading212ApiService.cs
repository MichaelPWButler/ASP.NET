using Dividens_Tracker.Models;
using Newtonsoft.Json;

public class Trading212ApiService
{
    private readonly HttpClient _httpClient;

    public Trading212ApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Trading212");
    }

    public async Task<Account?> GetAccountInfoAsync()
    {
        var response = await _httpClient.GetAsync("/api/v0/equity/account/info");
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Response: {content}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Error: {response.StatusCode} - {content}");
        }

        return JsonConvert.DeserializeObject<Account>(content);
    }

    public async Task<List<Instrument>> GetPortfolioAsync()
    {
        var response = await _httpClient.GetAsync("/api/v0/equity/portfolio");
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API Error: {response.StatusCode} - {content}");
        }

        return JsonConvert.DeserializeObject<List<Instrument>>(content);
    }
}
