using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    private readonly Trading212ApiService _apiService;

    public IndexModel(Trading212ApiService apiService)
    {
        _apiService = apiService;
    }

    [BindProperty]
    public string ApiKey { get; set; }

    public string Message { get; set; }
    public bool IsSuccess { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(ApiKey))
        {
            Message = "API key is required.";
            IsSuccess = false;
            return Page();
        }

        try
        {
            var account = await _apiService.GetAccountInfoAsync(ApiKey); // Validate key by calling account endpoint

            if (account != null)
            {
                Message = "API key is valid!";
                IsSuccess = true;
                // Optionally: store the key for future use
            }
            else
            {
                Message = "API key is invalid.";
                IsSuccess = false;
            }
        }
        catch (Exception ex)
        {
            Message = "API key failed: " + ex.Message;
            IsSuccess = false;
        }

        return Page();
    }
}