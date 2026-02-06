using System;
using MortgageLoanApp.Server.Models;

public record CreateMortgageApplicationRequest(string ApplicantName, decimal LoanAmount);

public record UpdateMortgageApplicationRequest(string ApplicantName, decimal LoanAmount);

public record UpdateMortgageStatusRequest(Guid Id, Status loanStatus);

