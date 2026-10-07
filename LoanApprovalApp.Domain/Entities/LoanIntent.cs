namespace LoanApprovalApp.Domain.Entities
{
    public class LoanIntent
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
