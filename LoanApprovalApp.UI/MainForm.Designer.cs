using System;
using System.Windows.Forms;

namespace LoanApprovalApp.UI
{
    public partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvLoans = new DataGridView();
            statusStrip1 = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            cmbIntent = new ComboBox();
            btnApplyFilter = new Button();
            lblTotalAmount = new Label();
            lblAvgIncome = new Label();
            btnExportJson = new Button();
            btnExportXml = new Button();
            btnExportXlsx = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            btnExportDocx = new Button();
            btnExportCsv = new Button();
            menuStrip1 = new MenuStrip();
            файлToolStripMenuItem = new ToolStripMenuItem();
            menuOpenCsv = new ToolStripMenuItem();
            menuRecentFiles = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            lblCount = new Label();
            lblMinMax = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            cmbGrade = new ComboBox();
            cmbStatus = new ComboBox();
            tabPage2 = new TabPage();
            btnShowSecondChart = new Button();
            btnSaveSecondChart = new Button();
            plotCategories = new ScottPlot.WinForms.FormsPlot();
            btnShowFirstChart = new Button();
            btnSaveFirstChart = new Button();
            plotLoans = new ScottPlot.WinForms.FormsPlot();
            menuAbout = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvLoans).BeginInit();
            statusStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            menuStrip1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // dgvLoans
            // 
            dgvLoans.BackgroundColor = SystemColors.ButtonHighlight;
            dgvLoans.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLoans.Location = new Point(26, 178);
            dgvLoans.Name = "dgvLoans";
            dgvLoans.RowHeadersWidth = 51;
            dgvLoans.Size = new Size(1245, 466);
            dgvLoans.TabIndex = 1;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatus });
            statusStrip1.Location = new Point(0, 808);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1303, 26);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(133, 20);
            lblStatus.Text = "Готово до роботи";
            // 
            // cmbIntent
            // 
            cmbIntent.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbIntent.Font = new Font("Segoe UI", 11F);
            cmbIntent.FormattingEnabled = true;
            cmbIntent.Location = new Point(659, 34);
            cmbIntent.Name = "cmbIntent";
            cmbIntent.Size = new Size(344, 33);
            cmbIntent.TabIndex = 3;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.BackColor = Color.White;
            btnApplyFilter.Font = new Font("Segoe UI", 11F);
            btnApplyFilter.Location = new Point(1027, 81);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(244, 38);
            btnApplyFilter.TabIndex = 4;
            btnApplyFilter.Text = "Застосувати фільтр";
            btnApplyFilter.UseVisualStyleBackColor = false;
            btnApplyFilter.Click += btnApplyFilter_Click;
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Font = new Font("Segoe UI", 11F);
            lblTotalAmount.Location = new Point(92, 103);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(154, 25);
            lblTotalAmount.TabIndex = 5;
            lblTotalAmount.Text = "Сума кредитів: 0";
            // 
            // lblAvgIncome
            // 
            lblAvgIncome.AutoSize = true;
            lblAvgIncome.Font = new Font("Segoe UI", 11F);
            lblAvgIncome.Location = new Point(92, 138);
            lblAvgIncome.Name = "lblAvgIncome";
            lblAvgIncome.Size = new Size(161, 25);
            lblAvgIncome.TabIndex = 6;
            lblAvgIncome.Text = "Середній дохід: 0";
            // 
            // btnExportJson
            // 
            btnExportJson.BackColor = Color.White;
            btnExportJson.Font = new Font("Segoe UI", 12F);
            btnExportJson.Location = new Point(243, 718);
            btnExportJson.Name = "btnExportJson";
            btnExportJson.Size = new Size(201, 47);
            btnExportJson.TabIndex = 7;
            btnExportJson.Text = "Зберегти в JSON";
            btnExportJson.UseVisualStyleBackColor = false;
            btnExportJson.Click += btnExportJson_Click;
            // 
            // btnExportXml
            // 
            btnExportXml.BackColor = Color.White;
            btnExportXml.Font = new Font("Segoe UI", 12F);
            btnExportXml.Location = new Point(462, 718);
            btnExportXml.Name = "btnExportXml";
            btnExportXml.Size = new Size(201, 47);
            btnExportXml.TabIndex = 8;
            btnExportXml.Text = "Зберегти в XML";
            btnExportXml.UseVisualStyleBackColor = false;
            btnExportXml.Click += btnExportXml_Click;
            // 
            // btnExportXlsx
            // 
            btnExportXlsx.BackColor = Color.White;
            btnExportXlsx.Font = new Font("Segoe UI", 12F);
            btnExportXlsx.Location = new Point(681, 718);
            btnExportXlsx.Name = "btnExportXlsx";
            btnExportXlsx.Size = new Size(266, 47);
            btnExportXlsx.TabIndex = 9;
            btnExportXlsx.Text = "Зберегти в Excel (XLSX)";
            btnExportXlsx.UseVisualStyleBackColor = false;
            btnExportXlsx.Click += btnExportXlsx_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1303, 834);
            tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Transparent;
            tabPage1.Controls.Add(btnDelete);
            tabPage1.Controls.Add(btnEdit);
            tabPage1.Controls.Add(btnAdd);
            tabPage1.Controls.Add(btnExportDocx);
            tabPage1.Controls.Add(btnExportCsv);
            tabPage1.Controls.Add(menuStrip1);
            tabPage1.Controls.Add(lblCount);
            tabPage1.Controls.Add(lblMinMax);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(cmbGrade);
            tabPage1.Controls.Add(cmbStatus);
            tabPage1.Controls.Add(dgvLoans);
            tabPage1.Controls.Add(cmbIntent);
            tabPage1.Controls.Add(btnExportXlsx);
            tabPage1.Controls.Add(lblTotalAmount);
            tabPage1.Controls.Add(btnExportXml);
            tabPage1.Controls.Add(lblAvgIncome);
            tabPage1.Controls.Add(btnExportJson);
            tabPage1.Controls.Add(btnApplyFilter);
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1295, 801);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Таблиця даних";
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.White;
            btnDelete.Font = new Font("Segoe UI", 12F);
            btnDelete.Location = new Point(828, 660);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(253, 43);
            btnDelete.TabIndex = 22;
            btnDelete.Text = "Видалити";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.White;
            btnEdit.Font = new Font("Segoe UI", 12F);
            btnEdit.Location = new Point(518, 660);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(253, 43);
            btnEdit.TabIndex = 21;
            btnEdit.Text = "Редагувати";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.White;
            btnAdd.Font = new Font("Segoe UI", 12F);
            btnAdd.Location = new Point(200, 660);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(260, 43);
            btnAdd.TabIndex = 20;
            btnAdd.Text = "Додати";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnExportDocx
            // 
            btnExportDocx.BackColor = Color.White;
            btnExportDocx.Font = new Font("Segoe UI", 12F);
            btnExportDocx.Location = new Point(968, 718);
            btnExportDocx.Name = "btnExportDocx";
            btnExportDocx.Size = new Size(303, 47);
            btnExportDocx.TabIndex = 19;
            btnExportDocx.Text = "Згенерувати DOCX-звіт";
            btnExportDocx.UseVisualStyleBackColor = false;
            btnExportDocx.Click += btnExportDocx_Click;
            // 
            // btnExportCsv
            // 
            btnExportCsv.BackColor = Color.White;
            btnExportCsv.Font = new Font("Segoe UI", 12F);
            btnExportCsv.Location = new Point(26, 718);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(201, 47);
            btnExportCsv.TabIndex = 18;
            btnExportCsv.Text = "Зберегти в CSV";
            btnExportCsv.UseVisualStyleBackColor = false;
            btnExportCsv.Click += btnExportCsv_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { файлToolStripMenuItem });
            menuStrip1.Location = new Point(3, 3);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1289, 28);
            menuStrip1.TabIndex = 17;
            menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            файлToolStripMenuItem.BackColor = SystemColors.ButtonHighlight;
            файлToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuOpenCsv, menuRecentFiles, menuAbout, menuExit });
            файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            файлToolStripMenuItem.Size = new Size(59, 24);
            файлToolStripMenuItem.Text = "Файл";
            // 
            // menuOpenCsv
            // 
            menuOpenCsv.Name = "menuOpenCsv";
            menuOpenCsv.Size = new Size(224, 26);
            menuOpenCsv.Text = "Відкрити...";
            menuOpenCsv.Click += btnOpenCsv_Click;
            // 
            // menuRecentFiles
            // 
            menuRecentFiles.Name = "menuRecentFiles";
            menuRecentFiles.Size = new Size(224, 26);
            menuRecentFiles.Text = "Останні файли";
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(224, 26);
            menuExit.Text = "Вихід";
            menuExit.Click += menuExit_Click;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 11F);
            lblCount.Location = new Point(92, 34);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(171, 25);
            lblCount.TabIndex = 16;
            lblCount.Text = "Кількість заявок: 0";
            // 
            // lblMinMax
            // 
            lblMinMax.AutoSize = true;
            lblMinMax.Font = new Font("Segoe UI", 11F);
            lblMinMax.Location = new Point(92, 69);
            lblMinMax.Name = "lblMinMax";
            lblMinMax.Size = new Size(180, 25);
            lblMinMax.TabIndex = 15;
            lblMinMax.Text = "Мін/Макс кредит: 0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.Location = new Point(483, 133);
            label3.Name = "label3";
            label3.Size = new Size(158, 25);
            label3.TabIndex = 14;
            label3.Text = "Рейтинг кредиту:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.Location = new Point(496, 85);
            label2.Name = "label2";
            label2.Size = new Size(145, 25);
            label2.TabIndex = 13;
            label2.Text = "Статус кредиту:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(507, 37);
            label1.Name = "label1";
            label1.Size = new Size(134, 25);
            label1.TabIndex = 12;
            label1.Text = "Мета кредиту:";
            // 
            // cmbGrade
            // 
            cmbGrade.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrade.Font = new Font("Segoe UI", 11F);
            cmbGrade.FormattingEnabled = true;
            cmbGrade.Location = new Point(659, 130);
            cmbGrade.Name = "cmbGrade";
            cmbGrade.Size = new Size(344, 33);
            cmbGrade.TabIndex = 11;
            // 
            // cmbStatus
            // 
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Font = new Font("Segoe UI", 11F);
            cmbStatus.FormattingEnabled = true;
            cmbStatus.Location = new Point(659, 82);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.Size = new Size(344, 33);
            cmbStatus.TabIndex = 10;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnShowSecondChart);
            tabPage2.Controls.Add(btnSaveSecondChart);
            tabPage2.Controls.Add(plotCategories);
            tabPage2.Controls.Add(btnShowFirstChart);
            tabPage2.Controls.Add(btnSaveFirstChart);
            tabPage2.Controls.Add(plotLoans);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1295, 801);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Графіки";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnShowSecondChart
            // 
            btnShowSecondChart.BackColor = Color.White;
            btnShowSecondChart.Font = new Font("Segoe UI", 12F);
            btnShowSecondChart.Location = new Point(699, 667);
            btnShowSecondChart.Name = "btnShowSecondChart";
            btnShowSecondChart.Size = new Size(233, 40);
            btnShowSecondChart.TabIndex = 5;
            btnShowSecondChart.Text = "Побудувати графік";
            btnShowSecondChart.UseVisualStyleBackColor = false;
            btnShowSecondChart.Click += btnShowSecondChart_Click;
            // 
            // btnSaveSecondChart
            // 
            btnSaveSecondChart.BackColor = Color.White;
            btnSaveSecondChart.Font = new Font("Segoe UI", 12F);
            btnSaveSecondChart.Location = new Point(969, 667);
            btnSaveSecondChart.Name = "btnSaveSecondChart";
            btnSaveSecondChart.Size = new Size(255, 40);
            btnSaveSecondChart.TabIndex = 4;
            btnSaveSecondChart.Text = "Зберегти графік у PNG";
            btnSaveSecondChart.UseVisualStyleBackColor = false;
            btnSaveSecondChart.Click += btnSaveSecondChart_Click;
            // 
            // plotCategories
            // 
            plotCategories.BackColor = Color.White;
            plotCategories.Location = new Point(660, 47);
            plotCategories.Name = "plotCategories";
            plotCategories.Size = new Size(600, 600);
            plotCategories.TabIndex = 3;
            // 
            // btnShowFirstChart
            // 
            btnShowFirstChart.BackColor = Color.White;
            btnShowFirstChart.Font = new Font("Segoe UI", 12F);
            btnShowFirstChart.Location = new Point(78, 667);
            btnShowFirstChart.Name = "btnShowFirstChart";
            btnShowFirstChart.Size = new Size(233, 40);
            btnShowFirstChart.TabIndex = 2;
            btnShowFirstChart.Text = "Побудувати графік";
            btnShowFirstChart.UseVisualStyleBackColor = false;
            btnShowFirstChart.Click += btnShowFirstChart_Click;
            // 
            // btnSaveFirstChart
            // 
            btnSaveFirstChart.BackColor = Color.White;
            btnSaveFirstChart.Font = new Font("Segoe UI", 12F);
            btnSaveFirstChart.Location = new Point(348, 667);
            btnSaveFirstChart.Name = "btnSaveFirstChart";
            btnSaveFirstChart.Size = new Size(255, 40);
            btnSaveFirstChart.TabIndex = 1;
            btnSaveFirstChart.Text = "Зберегти графік у PNG";
            btnSaveFirstChart.UseVisualStyleBackColor = false;
            btnSaveFirstChart.Click += btnSaveFirstChart_Click;
            // 
            // plotLoans
            // 
            plotLoans.BackColor = Color.White;
            plotLoans.Location = new Point(40, 47);
            plotLoans.Name = "plotLoans";
            plotLoans.Size = new Size(600, 600);
            plotLoans.TabIndex = 0;
            // 
            // menuAbout
            // 
            menuAbout.Name = "menuAbout";
            menuAbout.Size = new Size(224, 26);
            menuAbout.Text = "Про програму";
            menuAbout.Click += menuAbout_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1303, 834);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl1);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Аналізатор кредитних заяв";
            ((System.ComponentModel.ISupportInitialize)dgvLoans).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvLoans;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatus;
        private ComboBox cmbIntent;
        private Button btnApplyFilter;
        private Label lblTotalAmount;
        private Label lblAvgIncome;
        private Button btnExportJson;
        private Button btnExportXml;
        private Button btnExportXlsx;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button btnShowFirstChart;
        private Button btnSaveFirstChart;
        private ScottPlot.WinForms.FormsPlot plotLoans;
        private ScottPlot.WinForms.FormsPlot plotCategories;
        private Button btnShowSecondChart;
        private Button btnSaveSecondChart;
        private Label label3;
        private Label label2;
        private Label label1;
        private ComboBox cmbGrade;
        private ComboBox cmbStatus;
        private Label lblMinMax;
        private Label lblCount;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem файлToolStripMenuItem;
        private ToolStripMenuItem menuOpenCsv;
        private ToolStripMenuItem menuRecentFiles;
        private ToolStripMenuItem menuExit;
        private Button btnExportCsv;
        private Button btnExportDocx;
        private Button btnDelete;
        private Button btnEdit;
        private Button btnAdd;
        private ToolStripMenuItem menuAbout;
    }
}