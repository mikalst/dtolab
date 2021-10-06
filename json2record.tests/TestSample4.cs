using System;
using Xunit;
using System.Text.Json;
using System.IO;
using System.Globalization;
using System.Text;
using json2record.tests.sample4.DTOs;
using System.Linq;

namespace json4record.tests
{
    public class UnitTestSample4
    {
        private Sample4 _sample;
        public UnitTestSample4 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample4.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample4>(
                    new UTF8Encoding().GetBytes(file.ReadToEnd()),
                    new JsonSerializerOptions() {
                        PropertyNameCaseInsensitive = true
                    }
                );
            }
        }

        [Fact]
        public void TestMultipleIdenticallyNamedSubJSON()
        {
            Assert.Equal("Issue", _sample.lines.First().operation);
        }

                [Fact]
        public void TestMultipleIdenticallyNamedSubJSON_HandlesEmptyLists()
        {
            Assert.Equal("Y", _sample.lastCheck);
        }
    }
}
