using Data;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class KeywordService : IKeywordService
    {
        private readonly KeywordExtractor _keywordExtractor;

        public KeywordService()
        {
            _keywordExtractor = new KeywordExtractor();
        }

        public List<string> GetKeywords(string text)
        {
            return _keywordExtractor.ExtractKeywords(text);
        }
    }
}
