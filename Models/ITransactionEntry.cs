namespace Bolt.Models
{
    public class ITransactionEntry
    {
        public string Account { get; set; }
        public string Type { get; set; }
        public string AccountTerm { get; set; }
        public double Value { get; set; }
        public double? MonthlyPayment { get; set; }
    }
}