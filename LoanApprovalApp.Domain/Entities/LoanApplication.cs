namespace LoanApprovalApp.Domain.Entities
{
    public class LoanApplication
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ApplicantId { get; set; }

        public LoanIntent Intent { get; set; }
        public string Grade { get; set; }
        public int Amount { get; set; }
        public double InterestRate { get; set; }
        public double PercentIncome { get; set; }
        public int Status { get; set; }

        public Applicant Applicant { get; set; }
    }
}