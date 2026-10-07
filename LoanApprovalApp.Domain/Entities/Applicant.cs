namespace LoanApprovalApp.Domain.Entities
{
    public class Applicant
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Education { get; set; }
        public int Income { get; set; }
        public string HomeOwnership { get; set; }
        public double EmploymentLengthYears { get; set; }
        public int CreditHistoryLength { get; set; }
        public string DefaultOnFile { get; set; }

        public List<LoanApplication> Loans { get; set; } = new List<LoanApplication>();

        public override string ToString()
        {
            return $"Клієнт (Вік: {Age}, Дохід: {Income:N0} $)";
        }
    }
}