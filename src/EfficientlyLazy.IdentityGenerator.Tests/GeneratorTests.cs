using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using EfficientlyLazy.IdentityGenerator.Entity;
using Xunit;
using Xunit.Extensions;

namespace EfficientlyLazy.IdentityGenerator.Tests
{
    public class GeneratorTests
    {
        private const int MAX_LOOP = 250;
        private IGenerator _generator;

        public GeneratorTests()
        {
            Debug.Listeners.Add(new DefaultTraceListener());

            _generator = Generator.Configure()
                .IncludeAddress()
                .IncludeDOB()
                .IncludeName()
                .IncludeSSN()
                .Build();
        }

        [Theory]
        [InlineData(GenderFilter.Both)]
        [InlineData(GenderFilter.Male)]
        [InlineData(GenderFilter.Female)]
        public void GenerateName(GenderFilter filter)
        {
            // Arrange
            var list = new List<IName>();

            // Act
            for (var i = 0; i < MAX_LOOP; i++)
            {
                list.Add(_generator.GenerateName(filter));
            }

            // Assert
            Assert.Equal(MAX_LOOP, list.Distinct().Count());
        }

        [Fact]
        public void GenerateSSN()
        {
            // Arrange
            var list = new List<string>();

            // Act
            for (var i = 0; i < MAX_LOOP; i++)
            {
                list.Add(_generator.GenerateSSN());
            }

            var bad = list.GroupBy(x => x).Select(x => new
            {
                SSN = x.Key,
                Records = x.Count()
            }).Where(x => x.Records > 1).ToList();

            foreach (var b in bad)
            {
                Debug.WriteLine("MAX: " + b.SSN + " - " + b.Records);
            }

            // Assert
            Assert.Equal(MAX_LOOP, list.Distinct().Count());
        }

        [Theory]
        [InlineData(true, @"\d{3}-\d{2}-\d{4}")]
        [InlineData(false, @"\d{9}")]
        public void GenerateSSN_Dashing(bool isDashed, string regexMatch)
        {
            // Arrange
            _generator = Generator.Configure()
                .IncludeAddress()
                .IncludeDOB()
                .IncludeName()
                .IncludeSSN(isDashed)
                .Build();

            // Act
            var ssn = _generator.GenerateSSN();

            // Assert
            Assert.True(Regex.IsMatch(ssn, regexMatch));
        }

        [Fact]
        public void GenerateDOB()
        {
            // Arrange
            var list = new List<DateTime>();

            // Act
            for (var i = 0; i < MAX_LOOP; i++)
            {
                list.Add(_generator.GenerateDOB());
            }

            var bad = list.GroupBy(x => x).Select(x => new
                {
                    Date = x.Key,
                    Records = x.Count()
                }).Where(x => x.Records > 1).ToList();

            foreach (var b in bad)
            {
                Debug.WriteLine("MAX: " + b.Date + " - " + b.Records);
            }

            // Assert
            Assert.Equal(MAX_LOOP, list.Distinct().Count());
        }
    }
}
