

namespace Server
{
    internal static class Validator
    {
        private static string[] EMAIL_ENDINGS = { "@gmail.com", "@yahoo.com", "@hotmail.com",
        "@yandex.ru", "@list.ru", "@mail.ru", "@inbox.ru", "@bk.ru",
        "@internet.ru", "@xmail.ru" };

        private static int MIN_PASS_LENGTH;
        public static bool EmailIsValid(string email)
        {
            foreach (string ending in EMAIL_ENDINGS)
            {
                if (email.EndsWith(ending)) return true;
            }
            return false;
        }

        public static string PasswordIsValid(string password)
        {
            string errorText = "";
            if (string.IsNullOrEmpty(password) || password.Length < MIN_PASS_LENGTH)
            {
                errorText = $"Минимальная длина пароля равна {MIN_PASS_LENGTH}";
            }
            if (!password.Any(char.IsDigit))
            {
                errorText = "Пароль должен содержать буквы";
            }
            if (!password.Any(char.IsUpper))
            {
                errorText = "Пароль должен содержать буквы верхнего регистра";
            }
            if (!password.Any(char.IsLower))
            {
                errorText = "Пароль должен содержать буквы верхнего регистра";
            }
            return errorText;
        }

        public static bool StyleIsValid(string style)
        {
            return style.StartsWith("<style>") && style.EndsWith("</style>");
        }
    }
}
