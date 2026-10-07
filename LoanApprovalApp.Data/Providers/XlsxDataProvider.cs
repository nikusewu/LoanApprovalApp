using ClosedXML.Excel;
using LoanApprovalApp.Domain.Entities;
using System.Globalization;

namespace LoanApprovalApp.Data.Providers
{
    public class XlsxDataProvider : IDataProvider
    {
        public (List<Applicant> Applicants, List<LoanApplication> Loans) ReadData(string filePath)
        {
            var applicants = new List<Applicant>();
            var loans = new List<LoanApplication>();
            var intentCache = new Dictionary<string, LoanIntent>();

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheets.First();
                var rows = ws.RangeUsed().RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    if (row.Cell(1).IsEmpty()) continue;

                    string intentName = row.Cell(8).GetString();
                    if (!intentCache.TryGetValue(intentName, out var currentIntent))
                    {
                        currentIntent = new LoanIntent { Name = intentName };
                        intentCache[intentName] = currentIntent;
                    }

                    var applicant = new Applicant
                    {
                        Id = Guid.NewGuid(),
                        Age = row.Cell(1).TryGetValue<int>(out var age) ? age : 0,
                        Gender = row.Cell(2).GetString(),
                        Education = row.Cell(3).GetString(),
                        Income = row.Cell(4).TryGetValue<int>(out var inc) ? inc : 0,
                        EmploymentLengthYears = row.Cell(5).TryGetValue<double>(out var emp) ? emp : 0,
                        HomeOwnership = row.Cell(6).GetString(),
                        CreditHistoryLength = row.Cell(11).TryGetValue<int>(out var cred) ? cred : 0,
                        DefaultOnFile = row.Cell(13).GetString()
                    };

                    var loan = new LoanApplication
                    {
                        Id = Guid.NewGuid(),
                        ApplicantId = applicant.Id,
                        Applicant = applicant,
                        Intent = currentIntent,
                        Amount = row.Cell(7).TryGetValue<int>(out var amt) ? amt : 0,
                        InterestRate = row.Cell(9).TryGetValue<double>(out var rate) ? rate : 0,
                        PercentIncome = row.Cell(10).TryGetValue<double>(out var pct) ? pct : 0,
                        Grade = row.Cell(12).GetString(),
                        Status = row.Cell(14).TryGetValue<int>(out var status) ? status : 0
                    };

                    applicants.Add(applicant);
                    loans.Add(loan);
                }
            }
            return (applicants.Distinct().ToList(), loans);
        }

        public void WriteData(string filePath, List<LoanApplication> loans, string chart1Path = null, string chart2Path = null)
        {
            using (var workbook = new XLWorkbook())
            {
                var wsData = workbook.Worksheets.Add("Data");
                var exportData = loans.Select(l => new
                {
                    person_age = l.Applicant?.Age ?? 0,
                    person_gender = l.Applicant?.Gender ?? "N/A",
                    person_education = l.Applicant?.Education ?? "N/A",
                    person_income = l.Applicant?.Income ?? 0,
                    person_emp_exp = l.Applicant?.EmploymentLengthYears ?? 0,
                    person_home_ownership = l.Applicant?.HomeOwnership ?? "UNKNOWN",
                    loan_amnt = l.Amount,
                    loan_intent = l.Intent?.Name ?? "UNKNOWN",
                    loan_int_rate = l.InterestRate,
                    loan_percent_income = l.PercentIncome,
                    cb_person_cred_hist_length = l.Applicant?.CreditHistoryLength ?? 0,
                    credit_score = double.TryParse(l.Grade, NumberStyles.Any, CultureInfo.InvariantCulture, out var g) ? g : 0,
                    previous_loan_defaults_on_file = l.Applicant?.DefaultOnFile ?? "N",
                    loan_status = l.Status
                }).ToList();

                wsData.Cell(1, 1).InsertTable(exportData, "LoanData", true);
                wsData.Columns().AdjustToContents();

                var wsSummary = workbook.Worksheets.Add("Summary");
                wsSummary.Cell("A1").Value = "Статистика кредитів за категоріями мети";
                wsSummary.Cell("A1").Style.Font.Bold = true;
                wsSummary.Cell("A1").Style.Font.FontSize = 13;

                var summaryData = loans.GroupBy(l => l.Intent?.Name ?? "UNKNOWN").Select(g => new
                {
                    Категорія = g.Key,
                    Кількість_заявок = g.Count(),
                    Загальна_сума = g.Sum(x => (double)x.Amount),
                    Середній_дохід = g.Average(x => (double)(x.Applicant?.Income ?? 0))
                }).ToList();

                var tableCell = wsSummary.Cell("A3");
                var table = tableCell.InsertTable(summaryData, "SummaryTable", true);
                table.Theme = XLTableTheme.TableStyleMedium9;
                wsSummary.Columns().AdjustToContents();

                if ((!string.IsNullOrEmpty(chart1Path) && File.Exists(chart1Path)) || (!string.IsNullOrEmpty(chart2Path) && File.Exists(chart2Path)))
                {
                    var wsCharts = workbook.Worksheets.Add("Charts");
                    if (!string.IsNullOrEmpty(chart1Path) && File.Exists(chart1Path))
                    {
                        wsCharts.Cell("A1").Value = "Рисунок 1";
                        wsCharts.AddPicture(chart1Path).MoveTo(wsCharts.Cell("A2")).Scale(0.7);
                    }
                    if (!string.IsNullOrEmpty(chart2Path) && File.Exists(chart2Path))
                    {
                        wsCharts.Cell("A30").Value = "Рисунок 2";
                        wsCharts.AddPicture(chart2Path).MoveTo(wsCharts.Cell("A31")).Scale(0.7);
                    }
                }
                workbook.SaveAs(filePath);
            }
        }
    }
}