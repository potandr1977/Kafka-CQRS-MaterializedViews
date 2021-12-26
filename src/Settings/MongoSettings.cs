namespace Settings
{
    public static class MongoSettings
    {
        public const string ConnectionString = "mongodb://accounting_mongodb:27017/accdb/";
        public const string DbName = "accdb";
        public const string AccountsCollectionName = "Accounts";
        public const string PaymentsCollectionName = "Payments";
        public const string PersonsCollectionName = "Persons";
    }
}
