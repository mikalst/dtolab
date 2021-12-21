using Xunit;
using System.Text.Json;
using System.IO;
using System.Text;
using dtolab.tests.sample3.DTOs;

namespace dtolab.tests
{
    public class UnitTestSample3
    {
        private Sample3Value _sample;

        public UnitTestSample3 () {
            using (StreamReader file = File.OpenText(@"../../../samples/sample3.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample3Value>(
                    new UTF8Encoding().GetBytes(file.ReadToEnd()),
                    new JsonSerializerOptions() {
                        PropertyNameCaseInsensitive = true
                    }
                );
            }
        }

        [Fact]
        public void TestMultipleIdenticallyNamedSubJSON_DifferentSubJSON_RenamesSubJson()
        {
            Assert.NotNull(
                _sample.deliveryAddress.deliveryAddressCountry
            );
            Assert.NotNull(
                _sample.mainAddress.country
            );
        }
    }
}
