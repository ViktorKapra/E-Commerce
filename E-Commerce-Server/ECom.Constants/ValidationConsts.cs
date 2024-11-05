namespace ECom.Constants
{
    public static class ValidationConsts
    {
        public const string EmailRegex = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
        public const string PasswordRegex = @"^(?=.*\d)(?=.*[a-z])(?=.*\W)(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
    }
}
