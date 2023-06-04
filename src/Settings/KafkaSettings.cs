namespace Settings
{
    public static class KafkaSettings
    {
        public static string BootstrapServers = "kafka1:9092,kafka2:9092,kafka3:9092";

        public static class CommandTopics
        {
            public static string UpdateAccountTopicName = "update-account-topic";
            public static string SaveAccountTopicName = "update-account-topic";
            public static string DeleteAccountTopicName = "update-account-topic";

            public static string UpdatePaymentTopicName = "update-payment-topic";
            public static string SavePaymentTopicName = "save-payment-topic";
            public static string DeletePaymentTopicName = "delete-payment-topic";

            public static string UpdatePersonTopicName = "update-person-topic";
            public static string SavePersonTopicName = "save-person-topic";
            public static string DeletePersonTopicName = "delete-person-topic";
        }
        public static class ProjectionTopics
        {
            public static string UpdateAccountTopicName = "update-account-projection-topic";
            public static string SaveAccountTopicName = "save-account-projection-topic";
            public static string DeleteAccountTopicName = "delete-account-projection-topic";

            public static string UpdatePaymentTopicName = "update-payment-projection-topic";
            public static string SavePaymentTopicName = "save-payment-projection-topic";
            public static string DeletePaymentTopicName = "delete-payment-projection-topic";

            public static string UpdatePersonTopicName = "update-person-projection-topic";
            public static string SavePersonTopicName = "save-person-projection-topic";
            public static string DeletePersonTopicName = "delete-person-projection-topic";
        }

        public static class Groups
        {
            public static string BusinessGroupId = "business-group";
            public static string MailingGroupId = "mailing-group";
        }
    }

}

