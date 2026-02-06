using MortgageLoanApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Service interface logic
public interface IMortgageApplicationService
{
    Task<IEnumerable<LoanModel>> GetAllAsync(decimal? minLoan, decimal? maxLoan);
    Task<LoanModel?> GetByIdAsync(Guid id);
    Task<LoanModel> CreateAsync(string applicantName, decimal loanAmount);
    Task<LoanModel> UpdateAsync(Guid id, string applicantName, decimal loanAmount);
    Task<LoanModel> UpdateStatusAsync(Guid id, Status loanStatus);
}