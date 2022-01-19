using System;
using System.Collections.Generic;
using System.Text;

namespace Settings
{
    public static class ElasticSettings
    {
        public const string Url = "http://accounting_elastic:9200";
        public const string AccountsIndexName = "accounts";
        public const string PaymentsIndexName = "payments";
        public const string PersonsIndexName = "persons";
        
    }
}
