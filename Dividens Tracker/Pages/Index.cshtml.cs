using Dividens_Tracker.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dividens_Tracker.Pages;

public class IndexModel : PageModel
{
    private readonly Trading212ApiService _apiService;

    public IndexModel(Trading212ApiService apiService)
    {
        _apiService = apiService;
    }

    public List<Instrument> Portfolio { get; set; } = new();

    public async Task OnGetAsync()
    {
        Portfolio = await _apiService.GetPortfolioAsync();
    }
}
