using System.Globalization;
using System.Xml.Linq;
using LoanApprovalApp.Domain.Entities;

namespace LoanApprovalApp.Data.Providers
{
    public class XmlDataProvider : IDataProvider
    {
        public (List<Applicant> Applicants, List<LoanApplication> Loans) ReadData(string filePath)
        {
            var xmlDoc = XDocument.Load(filePath);
            var loans = xmlDoc.Descendants("Loan").Select(x => new LoanApplication
            {
                Id = Guid.TryParse(x.Element("Id")?.Value, out var lid) ? lid : Guid.NewGuid(),
                ApplicantId = Guid.TryParse(x.Element("ApplicantId")?.Value, out var aid) ? aid : Guid.NewGuid(),
                Amount = int.TryParse(x.Element("Amount")?.Value, out var amt) ? amt : 0,
                Grade = x.Element("Grade")?.Value ?? "0",
                InterestRate = double.TryParse(x.Element("InterestRate")?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var ir) ? ir : 0,
                PercentIncome = double.TryParse(x.Element("PercentIncome")?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var pi) ? pi : 0,
                Status = int.TryParse(x.Element("Status")?.Value, out var st) ? st : 0,
                Intent = new LoanIntent { Name = x.Element("Intent")?.Value ?? "UNKNOWN" },
                Applicant = new Applicant
                {
                    Id = Guid.TryParse(x.Element("Applicant")?.Element("Id")?.Value, out var appId) ? appId : Guid.NewGuid(),
                    Age = int.TryParse(x.Element("Applicant")?.Element("Age")?.Value, out var age) ? age : 0,
                    Gender = x.Element("Applicant")?.Element("Gender")?.Value ?? "N/A",
                    Education = x.Element("Applicant")?.Element("Education")?.Value ?? "N/A",
                    Income = int.TryParse(x.Element("Applicant")?.Element("Income")?.Value, out var inc) ? inc : 0,
                    HomeOwnership = x.Element("Applicant")?.Element("HomeOwnership")?.Value ?? "UNKNOWN",
                    EmploymentLengthYears = double.TryParse(x.Element("Applicant")?.Element("EmploymentLengthYears")?.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out var emp) ? emp : 0,
                    CreditHistoryLength = int.TryParse(x.Element("Applicant")?.Element("CreditHistoryLength")?.Value, out var crl) ? crl : 0,
                    DefaultOnFile = x.Element("Applicant")?.Element("DefaultOnFile")?.Value ?? "N"
                }
            }).ToList();

            var applicants = loans.Select(l => l.Applicant).Where(a => a != null).Distinct().ToList();
            return (applicants, loans);
        }

        public void WriteData(string filePath, List<LoanApplication> loans)
        {
            var doc = new XDocument(
                new XElement("LoanApplications",
                    loans.Select(l => new XElement("Loan",
                        new XElement("Id", l.Id),
                        new XElement("ApplicantId", l.ApplicantId),
                        new XElement("Intent", l.Intent?.Name),
                        new XElement("Amount", l.Amount),
                        new XElement("Grade", l.Grade),
                        new XElement("InterestRate", l.InterestRate),
                        new XElement("PercentIncome", l.PercentIncome),
                        new XElement("Status", l.Status),
                        new XElement("Applicant",
                            new XElement("Id", l.Applicant?.Id),
                            new XElement("Age", l.Applicant?.Age),
                            new XElement("Gender", l.Applicant?.Gender),
                            new XElement("Education", l.Applicant?.Education),
                            new XElement("Income", l.Applicant?.Income),
                            new XElement("HomeOwnership", l.Applicant?.HomeOwnership),
                            new XElement("EmploymentLengthYears", l.Applicant?.EmploymentLengthYears),
                            new XElement("CreditHistoryLength", l.Applicant?.CreditHistoryLength),
                            new XElement("DefaultOnFile", l.Applicant?.DefaultOnFile)
                        )
                    ))
                )
            );
            doc.Save(filePath);
        }
    }
}