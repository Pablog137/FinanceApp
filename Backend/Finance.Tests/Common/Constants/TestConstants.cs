using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Tests.Common.Constants
{
    internal static class TestConstants
    {
        public const decimal AMOUNT = 100.000M;
        public const decimal BALANCE = 150.000M;
        public const decimal EXPENSE_AMOUNT = 500.000M;
        public const string PASSWORD = "Password123!";


        // URLs

        public const string REGISTER_URL = "api/authentication/register";
        public const string LOGIN_URL = "api/authentication/login";

        public const string TRANSACTION_URL = "api/transaction/add-transaction";
        public const string GET_ACCOUNT_URL = "api/account/get-by-userId";

        public const string TRANSFER_URL = "api/transfer";
        public const string ACCOUNT_URL = "api/account/";

    }
}
