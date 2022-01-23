using System;

namespace Settings
{
    public static class KafkaSettings
    {
        public static string BootstrapServers = "kafka1:9092,kafka2:9092,kafka3:9092";

        public static class CommandTopics
        {
            public static string AccountTopicName = "account-topic";
            public static string PaymentTopicName = "payment-topic";
            public static string PersonTopicName = "person-topic";
        }
        public static class ProjectionTopics
        {
            public static string AccountTopicName = "account-projection-topic";
            public static string PaymentTopicName = "payment-projection-topic";
            public static string PersonTopicName = "person-projection-topic";
        }

        public static class Groups
        {
            public static string BusinessGroupId = "business-group";
            public static string MailingGroupId = "mailing-group";
        }
    }

}

