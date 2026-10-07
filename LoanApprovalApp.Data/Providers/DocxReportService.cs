using Xceed.Document.NET;
using Xceed.Words.NET;
using LoanApprovalApp.Domain.Entities;

namespace LoanApprovalApp.Data.Providers
{
    public class DocxReportService
    {
        public void GenerateReport(string filePath, List<LoanApplication> loans, string chart1Path, string chart2Path)
        {
            using (DocX document = DocX.Create(filePath))
            {
                document.InsertParagraph("\n\n\n\n\n\n");

                var title = document.InsertParagraph("Лабораторна робота\n")
                    .Font("Times New Roman").FontSize(16).Bold().Alignment = Alignment.center;

                document.InsertParagraph("Тема: Робота з файлами та даними у C#\n")
                    .Font("Times New Roman").FontSize(14).Alignment = Alignment.center;

                document.InsertParagraph("Виконала: Висоцька Вероніка, група КН-21\n")
                    .Font("Times New Roman").FontSize(14).Alignment = Alignment.center;

                document.InsertParagraph("Варіант: Аналізатор кредитних заяв")
                    .Font("Times New Roman").FontSize(14).Alignment = Alignment.center;

                document.InsertParagraph().InsertPageBreakAfterSelf();

                document.InsertParagraph("1. Опис даних").Font("Times New Roman").FontSize(14).Bold();
                document.InsertParagraph($"Загальна кількість опрацьованих заявок: {loans.Count}\n" +
                                         $"Загальна сума кредитів: {loans.Sum(l => (double)l.Amount):N0} $\n" +
                                         $"Середній дохід заявників: {loans.Average(l => (double)l.Applicant.Income):N0} $\n" +
                                         $"Мінімальна сума кредиту: {loans.Min(l => l.Amount):N0} $\n" +
                                         $"Максимальна сума кредиту: {loans.Max(l => l.Amount):N0} $\n")
                        .Font("Times New Roman").FontSize(12);

                document.InsertParagraph("2. Статистика за категоріями кредитування").Font("Times New Roman").FontSize(14).Bold();
                document.InsertParagraph("");

                var intentGroups = loans.GroupBy(l => l.Intent.Name).ToList();
                var table = document.AddTable(intentGroups.Count + 1, 3);
                table.Design = TableDesign.TableGrid;
                table.Alignment = Alignment.center;

                table.Rows[0].Cells[0].Paragraphs.First().Append("Мета кредиту").Bold().Alignment = Alignment.center;
                table.Rows[0].Cells[1].Paragraphs.First().Append("Кількість заявок").Bold().Alignment = Alignment.center;
                table.Rows[0].Cells[2].Paragraphs.First().Append("Загальна сума ($)").Bold().Alignment = Alignment.center;

                for (int i = 0; i < intentGroups.Count; i++)
                {
                    var g = intentGroups[i];
                    table.Rows[i + 1].Cells[0].Paragraphs.First().Append(g.Key).Alignment = Alignment.center;
                    table.Rows[i + 1].Cells[1].Paragraphs.First().Append(g.Count().ToString()).Alignment = Alignment.center;
                    table.Rows[i + 1].Cells[2].Paragraphs.First().Append(g.Sum(x => (double)x.Amount).ToString("N0")).Alignment = Alignment.center;
                }

                document.InsertTable(table);
                document.InsertParagraph("\n");

                document.InsertParagraph("3. Візуалізація даних").Font("Times New Roman").FontSize(14).Bold();

                if (File.Exists(chart1Path))
                {
                    var image1 = document.AddImage(chart1Path);
                    var picture1 = image1.CreatePicture();
                    picture1.Width = 500;
                    picture1.Height = 330;
                    document.InsertParagraph().InsertPicture(picture1).Alignment = Alignment.center;
                    document.InsertParagraph("Рисунок 1. Розподіл сум кредитів за заявками\n").Font("Times New Roman").FontSize(12).Italic().Alignment = Alignment.center;
                }

                if (File.Exists(chart2Path))
                {
                    var image2 = document.AddImage(chart2Path);
                    var picture2 = image2.CreatePicture();
                    picture2.Width = 500;
                    picture2.Height = 330;
                    document.InsertParagraph().InsertPicture(picture2).Alignment = Alignment.center;
                    document.InsertParagraph("Рисунок 2. Загальна сума кредитів за категоріями\n").Font("Times New Roman").FontSize(12).Italic().Alignment = Alignment.center;
                }

                document.InsertParagraph("Висновки").Font("Times New Roman").FontSize(14).Bold();
                document.InsertParagraph("У ході виконання роботи було створено систему імпорту, фільтрації та експорту кредитних заявок. " +
                    "Дані було успішно проаналізовано, агреговано за категоріями та візуалізовано за допомогою інтегрованих графічних компонентів.")
                    .Font("Times New Roman").FontSize(12);

                document.Save();
            }
        }
    }
}