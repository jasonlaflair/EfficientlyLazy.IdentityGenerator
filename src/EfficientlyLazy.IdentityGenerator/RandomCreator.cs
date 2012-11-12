using System;
using System.Security.Cryptography;

namespace EfficientlyLazy.IdentityGenerator
{
    internal class RandomEngine : Random
    {
        public RandomEngine()
            : base(GenerateCryptographicSeededRandom())
        {
            
        }

        private static int GenerateCryptographicSeededRandom()
        {
            // We will make up an integer seed from 4 bytes of this array.
            var randomBytes = new byte[4];

            // Generate 4 random bytes.
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            var seed = ((randomBytes[0] & 0x7f) << 24) | (randomBytes[1] << 16) | (randomBytes[2] << 8) | (randomBytes[3]);

            return seed;
        }
    }
}