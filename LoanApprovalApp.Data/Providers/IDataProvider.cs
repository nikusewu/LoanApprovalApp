using LoanApprovalApp.Domain.Entities;

namespace LoanApprovalApp.Data.Providers
{
    public interface IDataProvider
    {
        (List<Applicant> Applicants, List<LoanApplication> Loans) ReadData(string filePath);
    }
}