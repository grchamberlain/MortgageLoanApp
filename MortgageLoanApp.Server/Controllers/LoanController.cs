using Microsoft.AspNetCore.Mvc;

namespace MortgageLoanApp.Server.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class LoanController : ControllerBase
    {
        // Controller depends on the service layer
        private readonly IMortgageApplicationService _service;

        // Inject the service via constructor
        public LoanController(IMortgageApplicationService service)
        {
            _service = service;
        }

        // Get all loan applications with optional filtering by loan amount
        [HttpGet]
        public async Task<IActionResult> GetAll(decimal? minLoan, decimal? maxLoan)
        {
            var applications = await _service.GetAllAsync(minLoan, maxLoan);
            return Ok(applications);
        }

        // Get specific loan by ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var application = await _service.GetByIdAsync(id);
            return application is null ? NotFound() : Ok(application);
        }

        // Create a new loan application
        [HttpPost]
        public async Task<IActionResult> Create(CreateMortgageApplicationRequest request)
        {
            try
            {
                var application = await _service.CreateAsync(request.ApplicantName, request.LoanAmount);
                return CreatedAtAction(nameof(GetById), new { id = application.Id }, application);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update a loan
        [HttpPost("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateMortgageApplicationRequest request)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, request.ApplicantName, request.LoanAmount);

                if (updated is null)
                    return NotFound();

                return Ok(updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        // Update the loan status of an application
        [HttpPost("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, UpdateMortgageStatusRequest request)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(id, request.loanStatus);
                if (updated is null)
                    return NotFound();
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
