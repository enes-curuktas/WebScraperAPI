using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Data
{
    public class KeywordExtractor
    {
        private static readonly HashSet<string> StopWords = new HashSet<string>
    {
        "benim", "senin", "bizim", "için", "ve", "veya", "ile", "ama", "çünkü", "gibi",
        "bu", "şu", "o", "bir", "birkaç", "her", "bazı", "tüm", "çok", "az", "daha", "daha sonra",
        "fakat", "neden", "ne", "nasıl", "kim", "nerede", "ne zaman", "hangi", "kaç", "gün", "hafta", "ay", "yıl",
        "derece", "yüzde", "şimdi", "sonra", "önce", "şöyle", "böyle", "şurada", "burada", "orada", "şunda",
        "bunda", "onda", "şunun", "bunun", "onun", "şurası", "burası", "orası", "şöyle ki", "şu şekilde",
        "şu anda", "şu sıralar", "her zaman", "her zaman ki", "her şey", "bazen", "sıklıkla", "genellikle",
        "çoğu zaman", "hiçbir zaman", "kesinlikle", "muhtemelen", "muhtemelen de", "kesinlikle de"
    };

        public List<string> ExtractKeywords(string text)
        {
            //var words = text.Split(' ');
            //var keywords = words.Where(word => !StopWords.Contains(word.ToLower())).ToList();
            //return keywords;

            var words = Regex.Split(text, @"\W+").Where(word => !StopWords.Contains(word.ToLower())).ToList();
            return words;
        }
    }
}
