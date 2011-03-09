using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using ResultSet = System.Collections.Generic.IEnumerable<System.Collections.Generic.IDictionary<string, object>>;

namespace Simple.Data.SimpleDB
{
    [Export("SimpleDb", typeof(Adapter))]
    public class SimpleDbAdapter : Adapter, IAdapterWithFunctions
    {
        private AmazonSimpleDB client;

        protected override void OnSetup()
        {
            var settings = (IDictionary<string, object>) Settings;

            object providedClient;

            if (settings.TryGetValue("Client", out providedClient))
            {
                this.client = (AmazonSimpleDB) providedClient;

                return;
            }
            
            object accessKey;
            object secret;

            if (!settings.TryGetValue("AccessKey", out accessKey) || 
                !settings.TryGetValue("Secret", out secret))
            {
                throw new InvalidOperationException(
                    "The SimpleDB adapter requires the AccessKey and Secret properties to be set.");
            }
            
            var config = new AmazonSimpleDBConfig();
            object serviceUrl;

            if (settings.TryGetValue("ServiceUrl", out serviceUrl))
            {
                config.WithServiceURL(serviceUrl.ToString());
            }

            client = Amazon.AWSClientFactory.CreateAmazonSimpleDBClient(accessKey.ToString(), secret.ToString(), config);
        }

        private static Dictionary<string, object> ConvertToDictionary(Item item)
        {
            var dictionary = item.Attribute.ToDictionary(
                    attribute => attribute.Name,
                    attribute => (object)attribute.Value);

            dictionary["Name"] = item.Name;

            return dictionary;
        }

        public override IEnumerable<IDictionary<string, object>> Find(string tableName, SimpleExpression criteria)
        {
            var request = new SelectRequest().WithSelectExpression("select * from " + tableName);

            while (request != null)
            {
                var response = client.Select(request);

                foreach (var item in response.SelectResult.Item)
                {
                    yield return ConvertToDictionary(item);
                }

                if (response.SelectResult.IsSetNextToken())
                {
                    request.NextToken = response.SelectResult.NextToken;
                }
                else
                {
                    request = null;
                }
            }
        }

        public override IDictionary<string, object> Insert(string tableName, IDictionary<string, object> data)
        {
            var request = new PutAttributesRequest()
                .WithDomainName(tableName)            
                .WithItemName(data["Name"].ToString())
                .WithAttribute(data
                    .Where(pair => pair.Key != "Name")
                    .Select(pair => new ReplaceableAttribute()
                        .WithName(pair.Key)
                        .WithValue(pair.Value.ToString())).ToArray());
            
            client.PutAttributes(request);

            return null;
        }

        public override int Update(string tableName, IDictionary<string, object> data, SimpleExpression criteria)
        {
            var request = new PutAttributesRequest()
                .WithDomainName(tableName)
                .WithItemName(data["Name"].ToString())
                .WithAttribute(data
                    .Where(pair => pair.Key != "Name")
                    .Select(pair => new ReplaceableAttribute()
                        .WithName(pair.Key)
                        .WithValue(pair.Value.ToString())
                        .WithReplace(true)).ToArray());

            client.PutAttributes(request);

            return 1;
        }

        public override int Delete(string tableName, SimpleExpression criteria)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<string> GetKeyFieldNames(string tableName)
        {
            throw new NotImplementedException();
        }

        public bool IsValidFunction(string functionName)
        {
            return new[] {"CreateDomain", "ListDomains"}.Contains(functionName);
        }

        public IEnumerable<IEnumerable<IEnumerable<KeyValuePair<string, object>>>> Execute(string functionName, IDictionary<string, object> parameters)
        {
            object name;

            if (!parameters.TryGetValue("name", out name))
            {
                if (!parameters.Any())
                {
                    throw new InvalidOperationException("This function requires the 'name' parameter.");
                }

                name = parameters.Values.First();
            }

            var request = new CreateDomainRequest().WithDomainName(name.ToString());
            var response = client.CreateDomain(request);

            return Enumerable.Empty<ResultSet>();
        }

        public void CreateDomain(string name)
        {
            var request = new CreateDomainRequest().WithDomainName(name);
            client.CreateDomain(request);
        }

        private IEnumerable<string> ListDomains()
        {
            var request = new ListDomainsRequest();

            while (request != null)
            {
                var response = client.ListDomains(request);

                foreach (var domain in response.ListDomainsResult.DomainName)
                {
                    yield return domain;
                }

                if (response.ListDomainsResult.IsSetNextToken())
                {
                    request.NextToken = response.ListDomainsResult.NextToken;                    
                }
                else
                {
                    request = null;
                }
            }
        }
    }
}
