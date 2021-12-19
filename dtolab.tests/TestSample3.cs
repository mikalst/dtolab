using System;
using Xunit;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using json2record.common.Exceptions;
using json2record.common;
using json2record.common.Services;

namespace json2record.tests
{
    public class UnitTestSample3
    {
        public UnitTestSample3 () {
        }

        [Fact]
        public void TestMultipleIdenticallyNamedSubJSON_DifferentSubJSON_ThrowsNonMatchingDuplicateSubrecordsException()
        {
            var files = new Dictionary<string, FileModel>();
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(@"../../../samples/sample3.json"))
            {
                var jsonParser = new JsonParserService();
                Assert.Throws<NonMatchingDuplicateSubrecordsException>(
                    () => jsonParser.Parse(sr, "sample3", ref files)
                );
            };
        }
    }
}
