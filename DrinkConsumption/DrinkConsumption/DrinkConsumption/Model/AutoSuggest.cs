using System;
using System.Collections.Generic;
using System.Text;

namespace DrinkConsumption.Model
{

    public class Autosuggest
    {
        public string _Type { get; set; }
        public Instrumentation Instrumentation { get; set; }
        public QueryContext QueryContext { get; set; }
        public List<Suggestion> SuggestionGroups { get; set; }
    }

    public class Instrumentation
    {
        public string PingUrlBase { get; set; }
        public string PageLoadPingUrl { get; set; }
    }

    public class QueryContext
    {
        public string OriginalQuery { get; set; }
    }

    public class Suggestion
    {
        public string Name { get; set; }
        public List<SearchSuggestion> SearchSuggestions { get; set; }
    }

    public class SearchSuggestion
    {
        public string Url { get; set; }
        public string UrlPingSuffix { get; set; }
        public string DisplayText { get; set; }
        public string Query { get; set; }
        public string SearchKind { get; set; }
    }

}
