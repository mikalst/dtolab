using System;
using Xunit;
using System.Text.Json;
using System.IO;
using System.Globalization;
using System.Text;
using System.Linq;
using dtolab.tests.sample4.DTOs;

namespace json4record.tests
{
    public class UnitTestSample4
    {
        private Sample4Value _sample;
        public UnitTestSample4 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample4.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample4Value>(
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
