using InsuranceWrapperApi.Application.DTOs.Common;
using InsuranceWrapperApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceWrapperApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class QuoteController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    private readonly ILogger<QuoteController> _logger;

    public QuoteController(IQuoteService quoteService, ILogger<QuoteController> logger)
    {
        _quoteService = quoteService;
        _logger = logger;
    }

    /// <summary>
    /// Get insurance quote based on generic request
    /// System automatically determines LOB and routes to appropriate provider
    /// </summary>
    /// <param name="request">Generic quote request with basic business information</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Quote response with premium and coverage details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GenericQuoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GenericQuoteResponse>> GetQuote(
        [FromBody] GenericQuoteRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received quote request for business: {BusinessName}", request.BusinessName);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _quoteService.ProcessQuoteAsync(request, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BindController : ControllerBase
{
    private readonly IBindService _bindService;
    private readonly ILogger<BindController> _logger;

    public BindController(IBindService bindService, ILogger<BindController> logger)
    {
        _bindService = bindService;
        _logger = logger;
    }

    /// <summary>
    /// Bind insurance policy based on quote
    /// </summary>
    /// <param name="request">Bind request with quote ID and payment information</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Bind response with policy number and details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GenericBindResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<GenericBindResponse>> BindPolicy(
        [FromBody] GenericBindRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Received bind request for QuoteId: {QuoteId}", request.QuoteId);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var response = await _bindService.ProcessBindAsync(request, cancellationToken);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
