namespace ECom.Constants
{
    public static class ValidationConsts
    {
        public const string EMAIL_REGEX = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
        public const string PASSWORD_REGEX = @"^(?=.*\d)(?=.*[a-z])(?=.*\W)(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
    }
}
