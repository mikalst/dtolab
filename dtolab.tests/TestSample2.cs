using System;
using Xunit;
using System.Text.Json;
using System.IO;
using System.Text;
using dtolab.tests.sample2.DTOs;

namespace json2record.tests
{
    public class UnitTestSample2
    {
        private Sample2Value _sample;
        public UnitTestSample2 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample2.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample2Value>(
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
