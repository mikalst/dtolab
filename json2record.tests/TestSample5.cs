using System;
using Xunit;
using System.Text.Json;
using System.IO;
using System.Globalization;
using System.Text;
using json2record.tests.sample5.DTOs;
using System.Linq;

namespace json4record.tests
{
    public class UnitTestSample5
    {
        private Sample5 _sample;
        public UnitTestSample5 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample5.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample5>(
                    new UTF8Encoding().GetBytes(file.ReadToEnd()),
                    new JsonSerializerOptions() {
                        PropertyNameCaseInsensitive = true
                    }
                );
            }
        }

        [Fact]
        public void CanReadEscapedQuote()
        {
            Assert.Equal("\"", _sample.escapedQuote);
        }
    }
}
