using Xunit;
using System.Text.Json;
using System.IO;
using System.Text;
using dtolab.tests.sample5.DTOs;

namespace json4record.tests
{
    public class UnitTestSample5
    {
        private Sample5Value _sample;
        public UnitTestSample5 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample5.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample5Value>(
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
