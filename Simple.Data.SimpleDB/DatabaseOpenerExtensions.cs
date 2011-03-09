using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon.SimpleDB;

namespace Simple.Data.SimpleDB
{
    public static class DatabaseOpenerExtensions
    {
        public static Database OpenSimpleDb(this IDatabaseOpener opener, string accessKey, string secret)
        {
            return opener.Open("SimpleDb", new {AccessKey = accessKey, Secret = secret});
        }

        public static Database OpenSimpleDb(this IDatabaseOpener opener, AmazonSimpleDB client)
        {
            return opener.Open("SimpleDb", new {Client = client});
        }
    }
}
