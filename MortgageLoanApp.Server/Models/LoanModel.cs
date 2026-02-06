namespace MortgageLoanApp.Server.Models
{
    public class LoanModel
    {
        // Create Guid automatically when creating a LoanModel object. Set CreatedOn to current UTC time and default LoanStatus to Submitted.
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ApplicantName { get; set; }
        public decimal LoanAmount { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Status LoanStatus { get; set; } = Status.Submitted;
    }
    // Loan application status enum, can only be either Submitted, Approved or Denied
    public enum Status
    {
        Submitted,
        Approved,
        Denied
    }
}
