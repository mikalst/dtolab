using System;
using Xunit;
using System.Text.Json;
using System.IO;
using System.Text;
using dtolab.tests.sample1.DTOs;

namespace json2record.tests
{
    public class UnitTestSample1
    {
        private Sample1Value _sample;
        public UnitTestSample1 () {
            try {
                using (StreamReader file = File.OpenText(@"../../../samples/sample1.json"))
                {
                    _sample = JsonSerializer.Deserialize<Sample1Value>(
                        new UTF8Encoding().GetBytes(file.ReadToEnd()),
                        new JsonSerializerOptions() {
                            PropertyNameCaseInsensitive = true
                        }
                    );
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        [Fact]
        public void TestSimpleString()
        {
            Assert.Equal("Test", _sample.firstName);
        }

        [Fact]
        public void HandleStringWithSymbols()
        {
            Assert.True(_sample.surname == "!*@#&(#)(()string");
        }

        [Fact]
        public void HandleFloat()
        {
            Assert.Equal(1337.733, _sample.account.balance);
            Assert.Equal(10000.0, _sample.account.creditLimit);
        }

        [Fact]
        public void HandleInt()
        {
            Assert.Equal(3, _sample.account.tier);
        }

        [Fact]
        public void HandleDateTime()
        {
            Assert.Equal(DateTime.Parse("1990-01-01T08:00Z").ToUniversalTime(), _sample.Date);
        }
    }
}
