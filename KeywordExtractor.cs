using System.Collections.Generic;
using System.Linq;

namespace ChatBotAPI.Models
{
    public class KeywordExtractor
    {
        private static readonly HashSet<string> StopWords = new HashSet<string>
        {
            "benim", "senin", "bizim", "için", "ve", "veya", "ile", "ama", "çünkü", "gibi",
            "bu", "şu", "o", "bir", "birkaç", "her", "bazı", "tüm", "çok", "az", "daha", "daha sonra",
            "fakat", "neden", "ne", "nasıl", "kim", "nerede", "ne zaman", "hangi", "kaç", "gün", "hafta", "ay", "yıl",
            "derece", "yüzde"
        };

        public static List<string> ExtractKeywords(string text)
        {
            var words = text.Split(' ');
            var keywords = words.Where(word => !StopWords.Contains(word.ToLower())).ToList();
            return keywords;
        }
    }
}
