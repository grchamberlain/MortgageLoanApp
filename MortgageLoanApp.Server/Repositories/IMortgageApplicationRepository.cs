using MortgageLoanApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// Interface for repository logic
public interface IMortgageApplicationRepository
{
    Task<IEnumerable<LoanModel>> GetAllAsync(decimal? minLoan, decimal? maxLoan);
    Task<LoanModel?> GetByIdAsync(Guid id);
    Task AddAsync(LoanModel application);
    Task UpdateAsync(LoanModel application);
}
