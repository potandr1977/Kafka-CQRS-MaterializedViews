using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Queries.Core.Enums
{
    public enum PaymentTypeEnum
    {
        [Description("Salary")]
        Salary,

        [Description("Credit")]
        BankCredit,

        [Description("Gift")]
        Gift
    }
}
