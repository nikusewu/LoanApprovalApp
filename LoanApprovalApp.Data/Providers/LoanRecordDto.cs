using CsvHelper.Configuration.Attributes;

namespace LoanApprovalApp.Data.Providers
{
    public class LoanRecordDto
    {
        [Name("person_age")] public double Age { get; set; }
        [Name("person_gender")] public string Gender { get; set; }
        [Name("person_education")] public string Education { get; set; }
        [Name("person_income")] public double Income { get; set; }
        [Name("person_emp_exp")] public double EmploymentExperience { get; set; }
        [Name("person_home_ownership")] public string HomeOwnership { get; set; }

        [Name("loan_amnt")] public double Amount { get; set; }
        [Name("loan_intent")] public string Intent { get; set; }
        [Name("loan_int_rate")] public double? InterestRate { get; set; }
        [Name("loan_percent_income")] public double PercentIncome { get; set; }

        [Name("cb_person_cred_hist_length")] public double CreditHistoryLength { get; set; }
        [Name("credit_score")] public double CreditScore { get; set; }
        [Name("previous_loan_defaults_on_file")] public string DefaultOnFile { get; set; }
        [Name("loan_status")] public int Status { get; set; }
    }
}