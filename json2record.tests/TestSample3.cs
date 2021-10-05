using System;
using Xunit;
using System.Text.Json;
using System.IO;
using json2record.tests.sample2.DTOs;
using json2record.Services;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using json2record.Exceptions;

namespace json2record.tests
{
    public class UnitTestSample3
    {
        public UnitTestSample3 () {
        }

        [Fact]
        public void TestMultipleIdenticallyNamedSubJSON_DifferentSubJSON_ThrowsNonMatchingDuplicateSubrecordsException()
        {
            var structure = new Dictionary<string, HashSet<string>>();
            var packages = new Dictionary<string, SortedSet<string>>();
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(@"../../../samples/sample3.json"))
            {
                var jsonParser = new JsonParserService();
                Assert.Throws<NonMatchingDuplicateSubrecordsException>(
                    () => jsonParser.Parse(sr, "sample3", ref packages, ref structure)
                );
            };
        }
    }
}
