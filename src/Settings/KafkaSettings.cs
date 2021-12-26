﻿using System;

namespace Settings
{
    public static class KafkaSettings
    {
        public static string Host = "kafka";
        public static string Port = "9092";
        public static string AccountTopicName = "account-topic";
        public static string PaymentTopicName = "payment-topic";
        public static string PersonTopicName = "person-topic";
        public static string BusinessGroupId = "business-group";
        public static string MailingGroupId = "mailing-group";
    }
}
