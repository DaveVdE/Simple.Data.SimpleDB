using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Attribute = Amazon.SimpleDB.Model.Attribute;

namespace Simple.Data.SimpleDB.Tests.Fakes
{
    partial class FakeAmazonSimpleDB
    {
        public static Regex QueryAnalyzer = new Regex(@"select\s+(.*)\s+from\s+(.*)", RegexOptions.Singleline);
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Domains { get; set; }
        public int MaxItems { get; set; }
        public int SelectCount { get; set; }
        public int PutAttributesCount { get; set; }

        public FakeAmazonSimpleDB()
        {
            Domains = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
            MaxItems = int.MaxValue;
        }

        private static Attribute IntoAttribute(KeyValuePair<string, string> from)
        {
            return new Attribute().WithName(from.Key).WithValue(from.Value);
        }

        private static Attribute[] IntoAttribute(Dictionary<string, string> from)
        {
            return from.Select(IntoAttribute).ToArray();
        }

        private static Item IntoItem(KeyValuePair<string, Dictionary<string, string>> from)
        {
            return new Item().WithName(from.Key).WithAttribute(IntoAttribute(from.Value));
        }

        private static Item[] IntoItem(IEnumerable<KeyValuePair<string, Dictionary<string, string>>> from)
        {
            return from.Select(IntoItem).ToArray();
        }

        SelectResponse AmazonSimpleDB.Select(SelectRequest request)
        {
            var match = QueryAnalyzer.Match(request.SelectExpression);

            if (!match.Success)
            {
                throw new InvalidOperationException("The specified request does not contain a valid select expression.");
            }

            var attributes = match.Groups[1].Value;
            var domain = match.Groups[2].Value;
            Dictionary<string, Dictionary<string, string>> items;

            if (!Domains.TryGetValue(domain, out items))
            {
                throw new InvalidOperationException("The specified domain does not exist.");
            }

            var skip = 0;

            if (request.IsSetNextToken())
            {
                int.TryParse(request.NextToken, out skip);
            }
            
            var result = new SelectResponse()
                .WithSelectResult(new SelectResult()
                    .WithItem(IntoItem(items
                        .Skip(skip)
                        .Take(MaxItems))));

            if (result.SelectResult.Item.Count == MaxItems)
            {
                result.SelectResult.WithNextToken((skip + MaxItems).ToString());
            }

            ++SelectCount;
            return result;
        }

        PutAttributesResponse AmazonSimpleDB.PutAttributes(PutAttributesRequest request)
        {
            Dictionary<string, Dictionary<string, string>> domain;

            if (!Domains.TryGetValue(request.DomainName, out domain))
            {
                throw new InvalidOperationException("The specified domain does not exist.");
            }

            Dictionary<string, string> item;

            if (!domain.TryGetValue(request.ItemName, out item))
            {
                item = new Dictionary<string, string>();
                domain.Add(request.ItemName, item);
            }

            foreach (var attribute in request.Attribute)
            {
                item[attribute.Name] = attribute.Value;
            }

            ++PutAttributesCount;
            return new PutAttributesResponse();
        }
    }
}
