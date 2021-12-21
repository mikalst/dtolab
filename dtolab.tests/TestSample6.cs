using Xunit;
using System.Text.Json;
using System.Text;
using dtolab.tests.sample6.DTOs;
using System.IO;

namespace json4record.tests
{
    public class UnitTestSample6
    {
        private Sample6Value _sample;
        public UnitTestSample6 () {
            using (StreamReader file = System.IO.File.OpenText(@"../../../samples/sample6.json"))
            {
                _sample = JsonSerializer.Deserialize<Sample6Value>(
                    new UTF8Encoding().GetBytes(file.ReadToEnd()),
                    new JsonSerializerOptions() {
                        PropertyNameCaseInsensitive = true
                    }
                );
            }
        }

        [Fact]
        public void HandlesNestedObjects_EquallyNamed()
        {
            Assert.Equal(
                "<p>asd</p>",
                _sample
                    .testData
                    .testalCompanyMainForm
                    .educationAdmissionRequirements
                    .educationAdmissionRequirementsValue
                    .achieveLearningOutcome
            );
        }
    }
}
