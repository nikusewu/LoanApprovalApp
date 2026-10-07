using LoanApprovalApp.Domain.Entities;

namespace LoanApprovalApp.UI
{
    public partial class LoanEditForm : Form
    {
        public LoanApplication Loan { get; private set; }

        public LoanEditForm(List<string> intents, LoanApplication loan = null)
        {
            InitializeComponent();

            if (intents != null && intents.Count > 0)
            {
                cmbIntent.Items.AddRange(intents.ToArray());
            }

            if (loan != null)
            {
                Loan = loan;
                this.Text = "Редагувати заявку";

                numAge.Value = loan.Applicant?.Age ?? 0;
                txtGender.Text = loan.Applicant?.Gender;
                txtEducation.Text = loan.Applicant?.Education;
                numIncome.Value = loan.Applicant?.Income ?? 0;
                txtHomeOwnership.Text = loan.Applicant?.HomeOwnership;
                numEmpLength.Value = (decimal)(loan.Applicant?.EmploymentLengthYears ?? 0);
                numCredHist.Value = loan.Applicant?.CreditHistoryLength ?? 0;
                txtDefault.Text = loan.Applicant?.DefaultOnFile;

                numAmount.Value = loan.Amount;
                numInterestRate.Value = (decimal)loan.InterestRate;
                numPercentIncome.Value = (decimal)loan.PercentIncome;
                txtGrade.Text = loan.Grade;
                numStatus.Value = loan.Status;

                if (cmbIntent.Items.Contains(loan.Intent?.Name))
                {
                    cmbIntent.SelectedItem = loan.Intent?.Name;
                }
                else if (cmbIntent.Items.Count > 0)
                {
                    cmbIntent.SelectedIndex = 0;
                }
            }
            else
            {
                this.Text = "Додати нову заявку";
                Loan = new LoanApplication { Applicant = new Applicant(), Intent = new LoanIntent() };
                if (cmbIntent.Items.Count > 0) cmbIntent.SelectedIndex = 0;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Loan.Applicant == null) Loan.Applicant = new Applicant();
                if (Loan.Intent == null) Loan.Intent = new LoanIntent();

                Loan.Applicant.Age = (int)numAge.Value;
                Loan.Applicant.Gender = string.IsNullOrWhiteSpace(txtGender.Text) ? "N/A" : txtGender.Text;
                Loan.Applicant.Education = string.IsNullOrWhiteSpace(txtEducation.Text) ? "N/A" : txtEducation.Text;
                Loan.Applicant.Income = (int)numIncome.Value;
                Loan.Applicant.HomeOwnership = string.IsNullOrWhiteSpace(txtHomeOwnership.Text) ? "UNKNOWN" : txtHomeOwnership.Text;
                Loan.Applicant.EmploymentLengthYears = (double)numEmpLength.Value;
                Loan.Applicant.CreditHistoryLength = (int)numCredHist.Value;
                Loan.Applicant.DefaultOnFile = string.IsNullOrWhiteSpace(txtDefault.Text) ? "N" : txtDefault.Text;

                Loan.Amount = (int)numAmount.Value;
                Loan.Intent.Name = cmbIntent.SelectedItem?.ToString() ?? "UNKNOWN";
                Loan.InterestRate = (double)numInterestRate.Value;
                Loan.PercentIncome = (double)numPercentIncome.Value;
                Loan.Grade = string.IsNullOrWhiteSpace(txtGrade.Text) ? "0" : txtGrade.Text;
                Loan.Status = (int)numStatus.Value;

                Loan.ApplicantId = Loan.Applicant.Id;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла неочікувана помилка: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}