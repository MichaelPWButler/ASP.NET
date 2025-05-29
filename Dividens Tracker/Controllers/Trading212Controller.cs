using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class Trading212Controller : ControllerBase
{
    private readonly Trading212ApiService _api;

    public Trading212Controller(Trading212ApiService api)
    {
        _api = api;
    }

    [HttpGet("account")]
    public async Task<IActionResult> GetAccount([FromQuery] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return BadRequest("API key is required");

        try
        {
            var account = await _api.GetAccountInfoAsync(apiKey);
            return Ok(account);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }

    [HttpGet("portfolio")]
    public async Task<IActionResult> GetPortfolio([FromQuery] string apiKey)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            return BadRequest("API key is required");

        try
        {
            var portfolio = await _api.GetPortfolioAsync(apiKey);
            return Ok(portfolio);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
