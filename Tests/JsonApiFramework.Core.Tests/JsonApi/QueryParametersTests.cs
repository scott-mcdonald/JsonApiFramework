// Copyright (c) 2015–Present Scott McDonald. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;

using JsonApiFramework.JsonApi;
using JsonApiFramework.XUnit;

using Xunit;
using Xunit.Abstractions;

namespace JsonApiFramework.Tests.JsonApi
{
    public class QueryParametersTests : XUnitTest
    {
        // PUBLIC CONSTRUCTORS //////////////////////////////////////////////
        #region Constructors
        public QueryParametersTests(ITestOutputHelper output)
            : base(output)
        {
        }
        #endregion

        // PUBLIC METHODS ///////////////////////////////////////////////////
        #region Test Methods
        [Theory]
        [MemberData(nameof(TestQueryParametersCreateTestData))]
        public void TestQueryParametersCreate(string name, string urlString, object expectedQueryParameters)
        {
            // Arrange
            var expected = (QueryParameters)expectedQueryParameters;
            var url      = String.IsNullOrWhiteSpace(urlString) == false ? new Uri(urlString) : default(Uri);

            this.Output.WriteLine("Test Name: {0}", name);
            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("URL: {0}", urlString ?? "null");
            this.Output.WriteLine(String.Empty);

            this.Output.WriteLine("Expected Query Parameters:");
            this.WriteQueryParameters(expected);
            this.Output.WriteLine(String.Empty);

            // Act
            var actual = QueryParameters.Create(url);
            this.Output.WriteLine("Actual Query Parameters:");
            this.WriteQueryParameters(actual);

            // Assert
            Assert.Equal(expected.Filter,                 actual.Filter);
            Assert.Equal(expected.Fields.AsEnumerable(),  actual.Fields.AsEnumerable());
            Assert.Equal(expected.Page.AsEnumerable(),    actual.Page.AsEnumerable());
            Assert.Equal(expected.Include.AsEnumerable(), actual.Include.AsEnumerable());
        }
        #endregion

        // PRIVATE METHODS //////////////////////////////////////////////////
        #region Methods
        private void WriteQueryParameters(QueryParameters queryParameters)
        {
            this.Output.WriteLine($"Filter = {queryParameters.Filter ?? "null"}");

            this.Output.WriteLine("Sort");
            if (queryParameters.Sort.Any())
            {
                foreach (var sortItem in queryParameters.Sort)
                {
                    this.Output.WriteLine($"  {sortItem}");
                }
            }
            else
            {
                this.Output.WriteLine($"  <no items>");
            }

            foreach (var field in queryParameters.Fields)
            {
                var apiFieldType = field.Key;
                this.Output.WriteLine($"Field [type={apiFieldType}]");

                var apiFieldItems = field.Value;
                if (apiFieldItems.Any())
                {
                    foreach (var apiFieldItem in apiFieldItems)
                    {
                        this.Output.WriteLine($"  {apiFieldItem}");
                    }
                }
                else
                {
                    this.Output.WriteLine($"  <no items>");
                }
            }

            foreach (var page in queryParameters.Page)
            {
                var apiPageKey = page.Key;
                this.Output.WriteLine($"Page [key={apiPageKey}]");

                var apiPageItems = page.Value;
                if (apiPageItems.Any())
                {
                    foreach (var apiPageItem in apiPageItems)
                    {
                        this.Output.WriteLine($"  {apiPageItem}");
                    }
                }
                else
                {
                    this.Output.WriteLine($"  <no items>");
                }
            }

            this.Output.WriteLine("Include");
            if (queryParameters.Include.Any())
            {
                foreach (var includeItem in queryParameters.Include)
                {
                    this.Output.WriteLine($"  {includeItem}");
                }
            }
            else
            {
                this.Output.WriteLine($"  <no items>");
            }
        }
        #endregion

        // PUBLIC FIELDS ////////////////////////////////////////////////////
        #region Test Data
        public static readonly IEnumerable<object[]> TestQueryParametersCreateTestData = new[]
                                                                                         {
                                                                                             new object[] {"WithNull", null, QueryParameters.Empty},

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndNoQuery",
                                                                                                 "http://api.example.com/articles",
                                                                                                 QueryParameters.Empty
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFilter",
                                                                                                 "http://api.example.com/articles?filter=title%20eq%20%27JSON%20API%27",
                                                                                                 new QueryParameters(
                                                                                                     "title eq 'JSON API'",
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFilterAsEmptyString",
                                                                                                 "http://api.example.com/articles?filter=",
                                                                                                 new QueryParameters(
                                                                                                     "",
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFilterWithSpaces",
                                                                                                 "http://api.example.com/articles?%20filter%20=%20title%20eq%20%27JSON%20API%27%20",
                                                                                                 new QueryParameters(
                                                                                                     "title eq 'JSON API'",
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndSort",
                                                                                                 "http://api.example.com/articles?sort=title,-id",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     "title,-id",
                                                                                                     null,
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndSortAsEmptyString",
                                                                                                 "http://api.example.com/articles?sort=",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     "",
                                                                                                     null,
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndSortWithSpaces",
                                                                                                 "http://api.example.com/articles?%20sort%20=%20title%20,%20-id ",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     "title,-id",
                                                                                                     null,
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFieldsOfCount1",
                                                                                                 "http://api.example.com/articles?fields[articles]=title,body",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", "title,body"},
                                                                                                     },
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFieldsOfCount1AsEmptyString",
                                                                                                 "http://api.example.com/articles?fields[articles]=",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", ""},
                                                                                                     },
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFieldsOfCount1WithSpaces",
                                                                                                 "http://api.example.com/articles?%20fields%20[%20articles%20]%20=%20title%20,%20body%20",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", "title,body"},
                                                                                                     },
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFieldsOfCount2",
                                                                                                 "http://api.example.com/articles?fields[articles]=title,body&fields[comments]=text",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", "title,body"},
                                                                                                         {"comments", "text"},
                                                                                                     },
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFieldsOfCount2AsEmptyStrings",
                                                                                                 "http://api.example.com/articles?fields[articles]=&fields[comments]=",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", ""},
                                                                                                         {"comments", ""},
                                                                                                     },
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndFieldsOfCount2WithSpaces",
                                                                                                 "http://api.example.com/articles?%20fields%20[%20articles%20]%20=%20title%20,%20body%20&%20fields%20[%20comments%20]%20=%20text%20",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", "title,body"},
                                                                                                         {"comments", "text"},
                                                                                                     },
                                                                                                     null,
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndPageOfCount1",
                                                                                                 "http://api.example.com/articles?page[cursor]=42",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"cursor", "42"},
                                                                                                     },
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndPageOfCount1AsEmptyString",
                                                                                                 "http://api.example.com/articles?page[cursor]=",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"cursor", ""},
                                                                                                     },
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndPageOfCount1WithSpaces",
                                                                                                 "http://api.example.com/articles?%20page%20[%20cursor%20]%20=%2042%20",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"cursor", "42"},
                                                                                                     },
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndPageOfCount2",
                                                                                                 "http://api.example.com/articles?page[number]=42&page[size]=24",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"number", "42"},
                                                                                                         {"size", "24"},
                                                                                                     },
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndPageOfCount2AsEmptyString",
                                                                                                 "http://api.example.com/articles?page[number]=&page[size]=",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"number", ""},
                                                                                                         {"size", ""},
                                                                                                     },
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndPageOfCount2WithSpaces",
                                                                                                 "http://api.example.com/articles?%20page%20[%20number%20]%20=%2042%20&%20page%20[%20size%20]%20=%2024%20",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"number", "42"},
                                                                                                         {"size", "24"},
                                                                                                     },
                                                                                                     null),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndInclude",
                                                                                                 "http://api.example.com/articles?include=author,comments",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     "author,comments"),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndIncludeAsEmptyString",
                                                                                                 "http://api.example.com/articles?include=",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     ""),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndQueryAndIncludeWithSpaces",
                                                                                                 "http://api.example.com/articles?%20include%20=%20author%20,%20comments%20",
                                                                                                 new QueryParameters(
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     null,
                                                                                                     "author,comments"),
                                                                                             },

                                                                                             new object[]
                                                                                             {
                                                                                                 "WithUrlAndAllQueryParameters",
                                                                                                 "http://api.example.com/articles?filter=title%20eq%20%27JSON%20API%27&sort=title,-id&fields[articles]=title,body&fields[comments]=text&page[number]=42&page[size]=24&include=author,comments",
                                                                                                 new QueryParameters(
                                                                                                     "title eq 'JSON API'",
                                                                                                     "title,-id",
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"articles", "title,body"},
                                                                                                         {"comments", "text"},
                                                                                                     },
                                                                                                     new Dictionary<string, string>
                                                                                                     {
                                                                                                         {"number", "42"},
                                                                                                         {"size", "24"},
                                                                                                     },
                                                                                                     "author,comments"),
                                                                                             },
                                                                                         };
        #endregion
    }
}
