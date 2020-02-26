namespace AlgoLoan.Models
{
    public class RepaymentViewModel
    {
        public decimal AmountLeft { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal Rate { get; set; }
        public decimal PercentagePaid { get; set; }
    }
}