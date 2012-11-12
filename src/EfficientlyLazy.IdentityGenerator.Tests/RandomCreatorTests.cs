using System;
using Xunit;

namespace EfficientlyLazy.IdentityGenerator.Tests
{
    public class RandomCreatorTests
    {
        [Fact]
        public void Verify_Different_Randoms_Are_Created()
        {
            // Arrange
            var rnd1 = new RandomEngine();
            var rnd2 = new RandomEngine();

            // Act
            var value1 = rnd1.Next();
            var value2 = rnd2.Next();

            // Assert
            Assert.NotEqual(value1, value2);
        }

        [Fact]
        public void Verify_Same_Randoms_Are_Created()
        {
            // Arrange
            var rnd1 = new Random();
            var rnd2 = new Random();

            // Act
            var value1 = rnd1.Next();
            var value2 = rnd2.Next();

            // Assert
            Assert.Equal(value1, value2);
        }
    }
}
