using System;
using Xunit;
using System.Text.Json;
using System.IO;
using json2record.tests.sample2.DTOs;
using System.Text;

namespace json2record.tests
{
    public class UnitTestSample2
    {
        private Sample2 _sample;
        public UnitTestSample2 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample2.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample2>(
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
            Assert.Equal("NORGE", _sample.mainAddress.country.name);
            Assert.Equal("NORGE", _sample.invoiceAddress.country.name);
        }
    }
}
