namespace ECom.Constants
{
    public static class ValidationConsts
    {
        public const string EMAIL_REGEX = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";
        public const string PASSWORD_REGEX = @"^(?=.*\d)(?=.*[a-z])(?=.*\W)(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$";
        public const string PHONE_REGEX = "^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$";
        public const string NAME_REGEX = "^[A-Za-z-]+$";
    }
}
