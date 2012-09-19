using System.Collections.Generic;
using System.Linq;
using EfficientlyLazy.IdentityGenerator.Entity;
using Xunit;
using Xunit.Extensions;

namespace EfficientlyLazy.IdentityGenerator.Tests
{
    public class GeneratorConfigurationTests
    {
        [Fact]
        public void Configure_IncludeName_No_Specify()
        {
            // Act
            var generator = Generator.Configure()
                .IncludeName()
                .Build();

            // Assert
            Assert.Equal(GenderFilter.Both, generator.Genders);
            Assert.True(generator.IncludeName);
            Assert.True(((Generator)generator).InternalNameData);
            Assert.True(((Generator)generator).NameData.FirstNameData.Any());
            Assert.Equal(GenderFilter.Both, ((Generator)generator).NameData.GenderFilter);
            Assert.True(((Generator)generator).NameData.LastNameData.Any());
        }

        [Theory]
        [InlineData(GenderFilter.Both)]
        [InlineData(GenderFilter.Male)]
        [InlineData(GenderFilter.Female)]
        public void Configure_IncludeName_Specify_GenderFilter(GenderFilter filter)
        {
            // Act
            var generator = Generator.Configure()
                .IncludeName(filter)
                .Build();

            // Assert
            Assert.Equal(filter, generator.Genders);
            Assert.True(generator.IncludeName);
            Assert.True(((Generator)generator).InternalNameData);
            Assert.True(((Generator)generator).NameData.FirstNameData.Any());
            Assert.Equal(filter, ((Generator)generator).NameData.GenderFilter);
            Assert.True(((Generator)generator).NameData.LastNameData.Any());
        }

        [Fact]
        public void Configure_IncludeName_Specify_Data()
        {
            // Arrange
            var nameData = new NameData
                {
                    FirstNameData = new List<IFirstNameData>
                        {
                            new FirstNameData
                                {
                                    Gender = Gender.Female,
                                    Name = "Amy"
                                }
                        },
                    GenderFilter = GenderFilter.Female,
                    LastNameData = new List<string>
                        {
                            "Jackson"
                        }
                };

            // Act
            var generator = Generator.Configure()
                .IncludeName(nameData)
                .Build();

            // Assertfilter
            Assert.Equal(GenderFilter.Female, generator.Genders);
            Assert.True(generator.IncludeName);
            Assert.False(((Generator)generator).InternalNameData);
            Assert.True(((Generator)generator).NameData.FirstNameData.Count() == 1);
            Assert.Equal(GenderFilter.Female, ((Generator)generator).NameData.GenderFilter);
            Assert.True(((Generator)generator).NameData.LastNameData.Count() == 1);
        }

        [Fact]
        public void Configure_IncludeSSN_No_Specify()
        {
            // Act
            var generator = Generator.Configure()
                .IncludeSSN()
                .Build();

            // Assert
            Assert.True(generator.IncludeSSN);
            Assert.True(((Generator)generator).InternalSSNAreaCodeData);
            Assert.True(((Generator)generator).SSNAreaCodeData.Any());
        }

        [Fact]
        public void Configure_IncludeSSN_Specify_Data()
        {
            // Arrange
            var data = new List<ISSNAreaCodeData>
                {
                    new SSNAreaCodeData
                        {
                            Maximum = 100,
                            Minimum = 050,
                            StateAbbreviation = "MN"
                        }
                };

            // Act
            var generator = Generator.Configure()
                .IncludeSSN(data)
                .Build();

            // Assert
            Assert.True(generator.IncludeSSN);
            Assert.False(((Generator)generator).InternalSSNAreaCodeData);
            Assert.True(((Generator)generator).SSNAreaCodeData.Count() == 1);
        }

        [Fact]
        public void Configure_IncludeDOB_No_Specify()
        {
            // Act
            var generator = Generator.Configure()
                .IncludeDOB()
                .Build();

            // Assert
            Assert.True(generator.IncludeDOB);
            Assert.Equal(1, generator.MinimumAge);
            Assert.Equal(100, generator.MaximumAge);
        }

        [Fact]
        public void Configure_IncludeDOB_Specify_Data()
        {
            // Act
            var generator = Generator.Configure()
                .IncludeDOB(25, 80)
                .Build();

            // Assert
            Assert.True(generator.IncludeDOB);
            Assert.Equal(25, generator.MinimumAge);
            Assert.Equal(80, generator.MaximumAge);
        }

        [Fact]
        public void Configure_IncludeAddress_No_Specify()
        {
            // Act
            var generator = Generator.Configure()
                .IncludeAddress()
                .Build();

            // Assert
            Assert.True(generator.IncludeAddress);
            Assert.True(((Generator)generator).InternalAddressData);
            Assert.True(((Generator)generator).AddressData.CityStateZips.Any());
            Assert.True(((Generator)generator).AddressData.Directions.Any());
            Assert.True(((Generator)generator).AddressData.StreetTypes.Any());
        }

        [Fact]
        public void Configure_IncludeAddress_Specify_Data()
        {
            // Arrange
            var data = new AddressData
                {
                    CityStateZips = new List<ICityStateZipData>
                        {
                            new CityStateZipData()
                        },
                    Directions = new List<string>
                        {
                            "N"
                        },
                    StreetTypes = new List<string>
                        {
                            "St"
                        }
                };

            // Act
            var generator = Generator.Configure()
                .IncludeAddress(data)
                .Build();

            // Assert
            Assert.True(generator.IncludeAddress);
            Assert.False(((Generator)generator).InternalAddressData);
            Assert.True(((Generator)generator).AddressData.CityStateZips.Count() == 1);
            Assert.True(((Generator)generator).AddressData.Directions.Count() == 1);
            Assert.True(((Generator)generator).AddressData.StreetTypes.Count() == 1);
        }

        [Fact]
        public void Configure_Build()
        {
            // Act
            var generator = Generator.Configure()
                .IncludeName()
                .IncludeAddress()
                .IncludeDOB()
                .IncludeSSN()
                .Build();

            // Assert
            Assert.True(generator.IncludeName);
            Assert.True(generator.IncludeAddress);
            Assert.True(generator.IncludeDOB);
            Assert.True(generator.IncludeSSN);
        }
    }
}
