namespace TransactionAggregationApi.Application.Domain.Constants;

public static class AppConstants
{
    public static class Kafka
    {
        public const string TransactionEventsTopic = "transaction-events";
    }

    public static class StoredProcedures
    {
        public const string FetchTransactions = "usp_FetchTransactions";
        public const string FetchTransactionById = "usp_FetchTransactionById";
        public const string FetchClientTransactions = "usp_FetchClientTransactions";
        public const string FetchAggregationSummary = "usp_FetchAggregationSummary";
        public const string FetchClientAggregation = "usp_FetchClientAggregation";
        public const string FetchClientAggregationByCategory = "usp_FetchClientAggregationByCategory";
        public const string FetchClientAggregationBySource = "usp_FetchClientAggregationBySource";
        public const string FetchClientMonthlyAggregation = "usp_FetchClientMonthlyAggregation";
        public const string FetchTopSpenders = "usp_FetchTopSpenders";
    }

    public static class Categories
    {
        public static readonly IReadOnlyDictionary<int, string> All = new Dictionary<int, string>
        {
            { 0, "Unknown" }, { 1, "Groceries" }, { 2, "Fuel" }, { 3, "Utilities" },
            { 4, "Entertainment" }, { 5, "Dining" }, { 6, "Healthcare" }, { 7, "Travel" },
            { 8, "Shopping" }, { 9, "Salary" }, { 10, "Transfer" }, { 11, "ATM" },
            { 12, "Insurance" }, { 13, "Education" }, { 14, "Subscriptions" }, { 99, "Other" }
        };
    }
}
