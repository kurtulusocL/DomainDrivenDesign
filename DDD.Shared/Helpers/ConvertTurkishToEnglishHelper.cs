
namespace DDD.Shared.Helpers
{
    public static class ConvertTurkishToEnglishHelper
    {
        public static string ConvertTurkishToEnglish(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("ç", "c").Replace("Ç", "C")
                .Replace("ğ", "g").Replace("Ğ", "G")
                .Replace("ı", "i").Replace("I", "I")
                .Replace("ö", "o").Replace("Ö", "O")
                .Replace("ş", "s").Replace("Ş", "S")
                .Replace("ü", "u").Replace("Ü", "U")
                .Replace("İ", "I");
        }
    }
}
