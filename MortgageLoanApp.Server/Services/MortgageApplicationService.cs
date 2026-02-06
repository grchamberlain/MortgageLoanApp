using MortgageLoanApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MortgageApplicationService : IMortgageApplicationService
{
    private const decimal MinLoan = 10_000;
    private const decimal MaxLoan = 1_000_000;

    // Service layer depends on the repository
    private readonly IMortgageApplicationRepository _repository;

    // Inject the repository via constructor
    public MortgageApplicationService(IMortgageApplicationRepository repository)
    {
        _repository = repository;
    }

    // Get all loans
    public Task<IEnumerable<LoanModel>> GetAllAsync(decimal? minLoan, decimal? maxLoan)
    => _repository.GetAllAsync(minLoan, maxLoan);

    // Get specific loan by ID
    public Task<LoanModel?> GetByIdAsync(Guid id)
    => _repository.GetByIdAsync(id);

    // Create a loan, validate applicant name input (non nullable) and loan amount between min and max. Could expand with more validation or
    // specific JSON response codes.
    public async Task<LoanModel> CreateAsync(string applicantName, decimal loanAmount)
    {
        Validate(applicantName, loanAmount);

        var application = new LoanModel
        {
            ApplicantName = applicantName,
            LoanAmount = loanAmount
        };


        await _repository.AddAsync(application);
        return application;
    }

    public async Task<LoanModel> UpdateAsync(Guid id, string applicantName, decimal loanAmount)
    {
        Validate(applicantName, loanAmount);

        var application = await _repository.GetByIdAsync(id);
        if (application is null)
            return null;

        if (application.LoanStatus != Status.Submitted)
            throw new InvalidOperationException("Only submitted applications can be edited.");

        application.ApplicantName = applicantName;
        application.LoanAmount = loanAmount;

        await _repository.UpdateAsync(application);
        return application;
    }

    public async Task<LoanModel> UpdateStatusAsync(Guid id, Status loanStatus)
    {
        var application = await _repository.GetByIdAsync(id);
        if (application is null)
            return null;
        if (application.LoanStatus == Status.Approved || application.LoanStatus == Status.Denied)
            throw new InvalidOperationException("Cannot update status of a finalized application.");

        application.LoanStatus = loanStatus;
        await _repository.UpdateAsync(application);
        return application;
    }

    // Reusable validation method for validating applicant name input and loan amount
    private static void Validate(string applicantName, decimal loanAmount)
    {
        if (string.IsNullOrWhiteSpace(applicantName))
            throw new ArgumentException("Applicant name is required");

        if (loanAmount < MinLoan || loanAmount > MaxLoan)
            throw new ArgumentException($"Loan amount must be between {MinLoan:N0} and {MaxLoan:N0}");
    }
}
