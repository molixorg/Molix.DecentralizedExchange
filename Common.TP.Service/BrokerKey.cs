namespace Common.TP.Service
{
    public static class BrokerTopic
    {
        public const string MarketDataRealtime = "topic_marketData_realtime";
        public const string MarketSummaryDataRealtime = "topic_marketSummaryData_realtime";
        public const string AnalysisRealtime = "topic_analysis_realtime";
        public const string MarketDataRealtimeTest = "topic_marketData_realtime_test";
    }

    public static class BrokerQueue
    {
        public const string MarketDataStatic = "queue_marketData_static";
        public const string CustomerInfo = "queue_customer_info";
        public const string UserInfo = "queue_user_info";
        public const string MarketCustomerInfo = "queue_market_customer_info";

    }

    public static class BrokerRoute
    {
        public const string AllStocks = "stock.#";
        public const string AllSummaries = "summary.#";
        public const string AllAnalysis = "analysis.#";
    }
}
