using System.Text.Json;
using System.Text.Json.Serialization;
using LoanApprovalApp.Domain.Entities;

namespace LoanApprovalApp.Data.Providers
{
    public class JsonDataProvider : IDataProvider
    {
        public (List<Applicant> Applicants, List<LoanApplication> Loans) ReadData(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            string jsonText = File.ReadAllText(filePath);
            var loans = JsonSerializer.Deserialize<List<LoanApplication>>(jsonText, options) ?? new List<LoanApplication>();
            var applicants = loans.Select(l => l.Applicant).Where(a => a != null).Distinct().ToList();

            return (applicants, loans);
        }
        public void WriteData(string filePath, List<LoanApplication> loans)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            string jsonString = JsonSerializer.Serialize(loans, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
}