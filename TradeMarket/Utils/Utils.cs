namespace TradeMarket.Utilities
{
    public static class Utils
    {
        public static string HashUserPassword(string pw)
        {
            return BCrypt.Net.BCrypt.HashPassword(pw);
        }

        public static bool VerifyUserPassword(string pwInput, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(pwInput, storedHash);
        }
    }
}

