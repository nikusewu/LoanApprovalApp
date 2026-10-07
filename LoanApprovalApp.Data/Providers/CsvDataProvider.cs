using CsvHelper;
using CsvHelper.Configuration;
using LoanApprovalApp.Domain.Entities;
using System.Globalization;

namespace LoanApprovalApp.Data.Providers
{
    public class CsvDataProvider : IDataProvider
    {
        public (List<Applicant> Applicants, List<LoanApplication> Loans) ReadData(string filePath)
        {
            var applicants = new List<Applicant>();
            var loans = new List<LoanApplication>();
            var intentCache = new Dictionary<string, LoanIntent>();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<LoanRecordDto>().ToList();
                var groupedRecords = records.GroupBy(r => new { r.Age, r.Income, r.HomeOwnership });

                foreach (var group in groupedRecords)
                {
                    var first = group.First();

                    var applicant = new Applicant
                    {
                        Id = Guid.NewGuid(),
                        Age = (int)first.Age,
                        Gender = first.Gender ?? "N/A",
                        Education = first.Education ?? "N/A",
                        Income = (int)first.Income,
                        HomeOwnership = first.HomeOwnership ?? "UNKNOWN",
                        EmploymentLengthYears = first.EmploymentExperience,
                        CreditHistoryLength = (int)first.CreditHistoryLength,
                        DefaultOnFile = first.DefaultOnFile ?? "N"
                    };

                    foreach (var row in group)
                    {
                        if (!intentCache.TryGetValue(row.Intent ?? "UNKNOWN", out var currentIntent))
                        {
                            currentIntent = new LoanIntent { Name = row.Intent ?? "UNKNOWN" };
                            intentCache[row.Intent ?? "UNKNOWN"] = currentIntent;
                        }

                        var loan = new LoanApplication
                        {
                            Id = Guid.NewGuid(),
                            ApplicantId = applicant.Id,
                            Applicant = applicant,
                            Intent = currentIntent,
                            Grade = row.CreditScore.ToString(CultureInfo.InvariantCulture),
                            Amount = (int)row.Amount,
                            InterestRate = row.InterestRate ?? 0,
                            PercentIncome = row.PercentIncome,
                            Status = row.Status
                        };

                        applicant.Loans.Add(loan);
                        loans.Add(loan);
                    }
                    applicants.Add(applicant);
                }
            }
            return (applicants, loans);
        }

        public void WriteData(string filePath, List<LoanApplication> loans)
        {
            var exportData = loans.Select(l => new LoanRecordDto
            {
                Age = l.Applicant?.Age ?? 0,
                Gender = l.Applicant?.Gender ?? "N/A",
                Education = l.Applicant?.Education ?? "N/A",
                Income = l.Applicant?.Income ?? 0,
                EmploymentExperience = l.Applicant?.EmploymentLengthYears ?? 0,
                HomeOwnership = l.Applicant?.HomeOwnership ?? "UNKNOWN",
                Amount = l.Amount,
                Intent = l.Intent?.Name ?? "UNKNOWN",
                InterestRate = l.InterestRate,
                PercentIncome = l.PercentIncome,
                CreditHistoryLength = l.Applicant?.CreditHistoryLength ?? 0,
                CreditScore = double.TryParse(l.Grade, NumberStyles.Any, CultureInfo.InvariantCulture, out var grade) ? grade : 0,
                DefaultOnFile = l.Applicant?.DefaultOnFile ?? "N",
                Status = l.Status
            }).ToList();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true };
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(exportData);
            }
        }
    }
}