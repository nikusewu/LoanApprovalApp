using LoanApprovalApp.Data.Providers;
using LoanApprovalApp.Domain.Entities;

namespace LoanApprovalApp.UI
{
    public partial class MainForm : Form
    {
        private IDataProvider _dataProvider;
        private List<LoanApplication> _allLoans = new List<LoanApplication>();
        private string recentFilesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "recent_files.txt");
        private List<string> recentFiles = new List<string>();

        public MainForm()
        {
            InitializeComponent();
            _dataProvider = new CsvDataProvider();

            if (File.Exists(recentFilesPath))
            {
                recentFiles = File.ReadAllLines(recentFilesPath).ToList();
            }
            UpdateRecentFilesMenu();
        }

        private void btnOpenCsv_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Supported Files (*.csv;*.json;*.xml;*.xlsx)|*.csv;*.json;*.xml;*.xlsx|CSV files (*.csv)|*.csv|JSON files (*.json)|*.json|XML files (*.xml)|*.xml|Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    OpenFileByPath(ofd.FileName);
                }
            }
        }

        private void OpenFileByPath(string filePath)
        {
            try
            {
                List<LoanApplication> loans = new List<LoanApplication>();
                List<Applicant> applicants = new List<Applicant>();

                string extension = Path.GetExtension(filePath).ToLower();

                if (extension == ".csv")
                {
                    _dataProvider = new CsvDataProvider();
                    var data = _dataProvider.ReadData(filePath);
                    applicants = data.Applicants;
                    loans = data.Loans;
                }
                else if (extension == ".json")
                {
                    var jsonProvider = new JsonDataProvider();
                    var data = jsonProvider.ReadData(filePath);
                    applicants = data.Applicants;
                    loans = data.Loans;
                }
                else if (extension == ".xml")
                {
                    var xmlProvider = new XmlDataProvider();
                    var data = xmlProvider.ReadData(filePath);
                    applicants = data.Applicants;
                    loans = data.Loans;
                }
                else if (extension == ".xlsx")
                {
                    var xlsxProvider = new XlsxDataProvider();
                    var data = xlsxProvider.ReadData(filePath);
                    applicants = data.Applicants;
                    loans = data.Loans;
                }
                else
                {
                    MessageBox.Show("Цей формат не підтримується для імпорту.", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _allLoans = loans;
                dgvLoans.DataSource = _allLoans;

                var intents = _allLoans.Select(l => l.Intent?.Name).Where(n => n != null).Distinct().ToList();
                intents.Insert(0, "Всі");
                cmbIntent.DataSource = intents;

                var grades = _allLoans.Where(l => !string.IsNullOrEmpty(l.Grade))
                                     .Select(l => l.Grade).Distinct().OrderBy(g => g).ToList();
                grades.Insert(0, "Всі");
                cmbGrade.DataSource = grades;

                cmbStatus.Items.Clear();
                cmbStatus.Items.Add("Всі");
                cmbStatus.Items.Add("Схвалено (1)");
                cmbStatus.Items.Add("Відхилено (0)");
                cmbStatus.SelectedIndex = 0;

                btnApplyFilter_Click(this, EventArgs.Empty);
                AddToRecentFiles(filePath);

                lblStatus.Text = $"Файл успішно відкрито: {Path.GetFileName(filePath)}";
                AppLogger.Log($"ІМПОРТ: Успішно відкрито файл {Path.GetFileName(filePath)}. Завантажено заявок: {loans.Count}");
                MessageBox.Show($"Файл успішно імпортовано!\nЗаявок: {loans.Count}", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                AppLogger.Log($"ПОМИЛКА ІМПОРТУ: {ex.Message}");
                MessageBox.Show($"Помилка читання файлу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            if (_allLoans == null || !_allLoans.Any()) return;

            var filteredLoans = _allLoans.AsEnumerable();

            string selectedIntent = cmbIntent.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedIntent) && selectedIntent != "Всі")
            {
                filteredLoans = filteredLoans.Where(l => l.Intent?.Name == selectedIntent);
            }

            string selectedGrade = cmbGrade.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedGrade) && selectedGrade != "Всі")
            {
                filteredLoans = filteredLoans.Where(l => l.Grade == selectedGrade);
            }

            string selectedStatus = cmbStatus.SelectedItem?.ToString();
            if (selectedStatus == "Схвалено (1)")
            {
                filteredLoans = filteredLoans.Where(l => l.Status == 1);
            }
            else if (selectedStatus == "Відхилено (0)")
            {
                filteredLoans = filteredLoans.Where(l => l.Status == 0);
            }

            var resultList = filteredLoans.ToList();
            dgvLoans.DataSource = resultList;

            if (resultList.Any())
            {
                int count = resultList.Count;
                double totalAmount = resultList.Sum(l => (double)l.Amount);
                double avgIncome = resultList.Average(l => (double)(l.Applicant?.Income ?? 0));
                int maxLoan = resultList.Max(l => l.Amount);
                int minLoan = resultList.Min(l => l.Amount);

                lblCount.Text = $"Кількість заявок: {count}";
                lblTotalAmount.Text = $"Сума кредитів: {totalAmount:N0} $";
                lblAvgIncome.Text = $"Середній дохід клієнтів: {avgIncome:N0} $";
                lblMinMax.Text = $"Мін/Макс кредит: {minLoan:N0} $ / {maxLoan:N0} $";
                lblStatus.Text = $"Фільтр застосовано. Знайдено записів: {count}";
            }
            else
            {
                lblCount.Text = "Кількість заявок: 0";
                lblTotalAmount.Text = "Сума кредитів: 0 $";
                lblAvgIncome.Text = "Середній дохід клієнтів: 0 $";
                lblMinMax.Text = "Мін/Макс кредит: 0 $ / 0 $";
                lblStatus.Text = "Записів не знайдено";
            }
        }

        private void btnExportCsv_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || currentData.Count == 0)
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV files (*.csv)|*.csv";
                sfd.FileName = "FilteredLoans.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var csvProvider = new CsvDataProvider();
                        csvProvider.WriteData(sfd.FileName, currentData);
                        AppLogger.Log($"ЕКСПОРТ: Дані збережено у CSV ({sfd.FileName})");
                        MessageBox.Show("Дані успішно збережено у CSV!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ЕКСПОРТУ CSV: {ex.Message}");
                        MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportJson_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || currentData.Count == 0)
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "JSON files (*.json)|*.json";
                sfd.FileName = "FilteredLoans.json";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var jsonProvider = new JsonDataProvider();
                        jsonProvider.WriteData(sfd.FileName, currentData);
                        AppLogger.Log($"ЕКСПОРТ: Дані збережено у JSON ({sfd.FileName})");
                        MessageBox.Show("Дані успішно збережено у JSON!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ЕКСПОРТУ JSON: {ex.Message}");
                        MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportXml_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || currentData.Count == 0)
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "XML files (*.xml)|*.xml";
                sfd.FileName = "FilteredLoans.xml";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var xmlProvider = new XmlDataProvider();
                        xmlProvider.WriteData(sfd.FileName, currentData);
                        AppLogger.Log($"ЕКСПОРТ: Дані збережено у XML ({sfd.FileName})");
                        MessageBox.Show("Дані успішно збережено у форматі XML!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ЕКСПОРТУ XML: {ex.Message}");
                        MessageBox.Show($"Помилка збереження XML: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportXlsx_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || currentData.Count == 0)
            {
                MessageBox.Show("Немає даних для експорту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel files (*.xlsx)|*.xlsx";
                sfd.FileName = "LoanReport.xlsx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        RenderCharts(currentData);

                        string dir = AppDomain.CurrentDomain.BaseDirectory;
                        string chart1 = Path.Combine(dir, "loan_chart_1.png");
                        string chart2 = Path.Combine(dir, "loan_chart_2.png");

                        plotLoans.Plot.SavePng(chart1, 600, 400);
                        plotCategories.Plot.SavePng(chart2, 600, 400);

                        var xlsxProvider = new XlsxDataProvider();
                        xlsxProvider.WriteData(sfd.FileName, currentData, chart1, chart2);
                        AppLogger.Log($"ЗВІТ: Згенеровано XLSX-звіт ({sfd.FileName})");
                        MessageBox.Show("Звіт Excel успішно згенеровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ЗВІТУ XLSX: {ex.Message}");
                        MessageBox.Show($"Помилка збереження Excel: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnExportDocx_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || currentData.Count == 0)
            {
                MessageBox.Show("Немає даних для генерації звіту!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Word Document (*.docx)|*.docx";
                sfd.FileName = "LoanReport.docx";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        RenderCharts(currentData);

                        string dir = AppDomain.CurrentDomain.BaseDirectory;
                        string chart1 = Path.Combine(dir, "loan_chart_1.png");
                        string chart2 = Path.Combine(dir, "loan_chart_2.png");

                        plotLoans.Plot.SavePng(chart1, 600, 400);
                        plotCategories.Plot.SavePng(chart2, 600, 400);

                        var docxService = new DocxReportService();
                        docxService.GenerateReport(sfd.FileName, currentData, chart1, chart2);
                        AppLogger.Log($"ЗВІТ: Згенеровано DOCX-звіт ({sfd.FileName})");
                        MessageBox.Show("DOCX-звіт успішно згенеровано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ЗВІТУ DOCX: {ex.Message}");
                        MessageBox.Show($"Помилка генерації DOCX: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnShowFirstChart_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || !currentData.Any())
            {
                MessageBox.Show("Спочатку завантажте дані!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RenderFirstChart(currentData);
        }

        private void btnShowSecondChart_Click(object sender, EventArgs e)
        {
            var currentData = dgvLoans.DataSource as List<LoanApplication>;
            if (currentData == null || !currentData.Any())
            {
                MessageBox.Show("Спочатку завантажте дані!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            RenderSecondChart(currentData);
        }

        private void RenderFirstChart(List<LoanApplication> data)
        {
            if (data == null || !data.Any()) return;

            plotLoans.Plot.Clear();
            double[] amounts = data.Take(50).Select(l => (double)l.Amount).ToArray();
            double[] indexes = Enumerable.Range(0, amounts.Length).Select(i => (double)i).ToArray();
            plotLoans.Plot.Add.Bars(indexes, amounts);
            plotLoans.Plot.Axes.Title.Label.Text = "Розподіл сум кредитів за заявками";
            plotLoans.Plot.Axes.Bottom.Label.Text = "Номер заявки";
            plotLoans.Plot.Axes.Left.Label.Text = "Сума ($)";
            plotLoans.Plot.Axes.AutoScale();
            plotLoans.Refresh();
        }

        private void RenderSecondChart(List<LoanApplication> data)
        {
            if (data == null || !data.Any()) return;

            plotCategories.Plot.Clear();
            var intentGroups = data
                .GroupBy(l => l.Intent?.Name ?? "UNKNOWN")
                .Select(g => new
                {
                    Intent = g.Key.Length > 8 ? g.Key.Substring(0, 8) + "." : g.Key,
                    Total = (double)g.Sum(x => x.Amount)
                })
                .ToList();

            double[] values = intentGroups.Select(g => g.Total).ToArray();
            double[] positions = Enumerable.Range(0, values.Length).Select(i => (double)i).ToArray();
            plotCategories.Plot.Add.Bars(positions, values);
            plotCategories.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                positions,
                intentGroups.Select(g => g.Intent).ToArray()
            );
            plotCategories.Plot.Axes.Bottom.TickLabelStyle.FontSize = 11;
            plotCategories.Plot.Axes.Title.Label.Text = "Загальна сума кредитів за категоріями";
            plotCategories.Plot.Axes.Bottom.Label.Text = "Категорія мети";
            plotCategories.Plot.Axes.Left.Label.Text = "Сума ($)";
            plotCategories.Plot.Axes.AutoScale();
            plotCategories.Refresh();
        }

        private void RenderCharts(List<LoanApplication> data)
        {
            RenderFirstChart(data);
            RenderSecondChart(data);
        }

        private void btnSaveFirstChart_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image (*.png)|*.png";
                sfd.FileName = "loan_chart_1.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        plotLoans.Plot.SavePng(sfd.FileName, 600, 400);
                        AppLogger.Log($"ЕКСПОРТ: Збережено графік ({sfd.FileName})");
                        MessageBox.Show("Перший графік успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ГРАФІКА: {ex.Message}");
                        MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSaveSecondChart_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "PNG Image (*.png)|*.png";
                sfd.FileName = "loan_chart_2.png";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        plotCategories.Plot.SavePng(sfd.FileName, 600, 400);
                        AppLogger.Log($"ЕКСПОРТ: Збережено графік ({sfd.FileName})");
                        MessageBox.Show("Другий графік успішно збережено!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        AppLogger.Log($"ПОМИЛКА ГРАФІКА: {ex.Message}");
                        MessageBox.Show($"Помилка збереження: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AddToRecentFiles(string filePath)
        {
            if (recentFiles.Contains(filePath))
                recentFiles.Remove(filePath);

            recentFiles.Insert(0, filePath);

            if (recentFiles.Count > 5)
                recentFiles = recentFiles.Take(5).ToList();

            File.WriteAllLines(recentFilesPath, recentFiles);
            UpdateRecentFilesMenu();
        }

        private void UpdateRecentFilesMenu()
        {
            menuRecentFiles.DropDownItems.Clear();

            if (recentFiles.Count == 0)
            {
                var emptyItem = new ToolStripMenuItem("Немає історії");
                emptyItem.Enabled = false;
                menuRecentFiles.DropDownItems.Add(emptyItem);
                return;
            }

            foreach (var file in recentFiles)
            {
                var item = new ToolStripMenuItem(Path.GetFileName(file));
                item.Tag = file;
                item.Click += RecentFile_Click;
                menuRecentFiles.DropDownItems.Add(item);
            }
        }

        private void RecentFile_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            if (item != null)
            {
                string filePath = item.Tag.ToString();
                if (File.Exists(filePath))
                {
                    OpenFileByPath(filePath);
                }
                else
                {
                    MessageBox.Show("Файл більше не існує або був переміщений!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    recentFiles.Remove(filePath);
                    File.WriteAllLines(recentFilesPath, recentFiles);
                    UpdateRecentFilesMenu();
                }
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void RefreshData()
        {
            dgvLoans.DataSource = null;
            dgvLoans.DataSource = _allLoans;

            btnApplyFilter_Click(null, EventArgs.Empty);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new LoanEditForm(GetUniqueIntents()))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _allLoans.Insert(0, form.Loan);
                    RefreshData();
                    AppLogger.Log("CRUD: Додано новий запис.");
                    MessageBox.Show("Запис успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvLoans.SelectedRows.Count == 0 && dgvLoans.CurrentCell != null)
                dgvLoans.Rows[dgvLoans.CurrentCell.RowIndex].Selected = true;

            if (dgvLoans.SelectedRows.Count > 0)
            {
                var loan = dgvLoans.SelectedRows[0].DataBoundItem as LoanApplication;
                if (loan != null)
                {
                    using (var form = new LoanEditForm(GetUniqueIntents(), loan))
                    {
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            RefreshData();
                            AppLogger.Log($"CRUD: Відредаговано запис ID: {loan.Id}");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть рядок для редагування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvLoans.SelectedRows.Count == 0 && dgvLoans.CurrentCell != null)
                dgvLoans.Rows[dgvLoans.CurrentCell.RowIndex].Selected = true;

            if (dgvLoans.SelectedRows.Count > 0)
            {
                var loan = dgvLoans.SelectedRows[0].DataBoundItem as LoanApplication;
                if (loan != null)
                {
                    if (MessageBox.Show("Ви дійсно хочете видалити цей запис?", "Підтвердження", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _allLoans.Remove(loan);
                        RefreshData();
                        AppLogger.Log($"CRUD: Видалено запис ID: {loan.Id}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть рядок для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private List<string> GetUniqueIntents()
        {
            if (_allLoans == null || !_allLoans.Any())
                return new List<string> { "PERSONAL" };

            return _allLoans
                .Where(l => l.Intent != null && !string.IsNullOrEmpty(l.Intent.Name))
                .Select(l => l.Intent.Name!)
                .Distinct()
                .ToList();
        }

        private void menuAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Аналізатор кредитних заяв. Версія 1.0\nРозроблено Висоцькою Веронікою, група КН-21.", "Про програму", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}