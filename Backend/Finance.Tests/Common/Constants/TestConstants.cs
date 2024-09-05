namespace Finance.Tests.Common.Constants
{
    internal static class TestConstants
    {
        public const decimal AMOUNT = 100.000M;
        public const decimal INITIAL_BALANCE = 150.000M;
        public const decimal EXPENSE_AMOUNT = 500.000M;
        public const string PASSWORD = "Password123!";
        public const string REFRESH_TOKEN = "e6b6c233-545b-4033-8f50-7bbb76553ebb";



        // URLs

        public const string REGISTER_URL = "api/authentication/register";
        public const string LOGIN_URL = "api/authentication/login";
        public const string REFRESH_TOKEN_URL = "api/authentication/refresh-token";

        public const string TRANSACTION_URL = "api/transaction/add-transaction";
        public const string GET_ACCOUNT_URL = "api/account/get-by-userId";

        public const string TRANSFER_URL = "api/transfer";
        public const string ACCOUNT_URL = "api/account/";


    }
}
