using MortgageLoanApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class InMemoryMortgageApplicationRepository : IMortgageApplicationRepository
{
    private readonly List<LoanModel> _applications = new();

    // Add loan to in-memory list
    public Task AddAsync(LoanModel application)
    {
        _applications.Add(application);
        return Task.CompletedTask;
    }

    // Get all loans with optional filtering by min and max loan amount
    public Task<IEnumerable<LoanModel>> GetAllAsync(decimal? minLoan, decimal? maxLoan)
    {
        IEnumerable<LoanModel> query = _applications;


        if (minLoan.HasValue)
            query = query.Where(a => a.LoanAmount >= minLoan.Value);


        if (maxLoan.HasValue)
            query = query.Where(a => a.LoanAmount <= maxLoan.Value);


        return Task.FromResult(query);
    }

    // Get a specific loan using its Guid
    public Task<LoanModel?> GetByIdAsync(Guid id)
    => Task.FromResult(_applications.FirstOrDefault(a => a.Id == id));

    // Update an existing loan
    public Task UpdateAsync(LoanModel application)
    {
        var index = _applications.FindIndex(a => a.Id == application.Id);
        if (index != -1)
        {
            _applications[index] = application;
        }
        return Task.CompletedTask;
    }
}
