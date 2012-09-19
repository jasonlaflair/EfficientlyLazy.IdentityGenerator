using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EfficientlyLazy.IdentityGenerator.Entity;
using Xunit;
using Xunit.Extensions;

namespace EfficientlyLazy.IdentityGenerator.Tests
{
    public class GeneratorTests
    {
        private const int MAX_LOOP = 250;
        private readonly IGenerator _generator;

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

        [Theory]
        [InlineData(null, null)]
        [InlineData(10, 20)]
        [InlineData(5, 50)]
        [InlineData(60, 100)]
        public void GenerateDOB(int? min, int? max)
        {
            // Arrange
            var list = new List<DateTime>();

            // Act
            for (var i = 0; i < MAX_LOOP; i++)
            {
                if (min.HasValue && max.HasValue)
                {
                    list.Add(_generator.GenerateDOB(min.Value, max.Value));
                }
                else
                {
                    list.Add(_generator.GenerateDOB());
                }
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
