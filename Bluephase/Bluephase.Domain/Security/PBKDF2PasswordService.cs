#region Using directives

using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

#endregion

namespace Bluephase.Domain.Security
{
    public class PBKDF2PasswordService : IPasswordService
    {
        private const byte SaltSize = 16;
        private const int Iterations = 5000;

        #region IPasswordService Members

        public Password GeneratePassword(string plainPassword)
        {
            GCHandle gch = GCHandle.Alloc(plainPassword, GCHandleType.Pinned);

            var hashed = new Password();

            byte[] salt = GenerateSalt();

            hashed.Hash = ComputeHash(plainPassword, salt);
            hashed.Salt = Convert.ToBase64String(salt);

            ZeroMemory(gch.AddrOfPinnedObject(), plainPassword.Length*2);

            gch.Free();

            return hashed;
        }

        public bool AreEqual(string plainPassword, string hashedPassword, string salt)
        {
            GCHandle gch = GCHandle.Alloc(plainPassword, GCHandleType.Pinned);

            byte[] saltBytes = Convert.FromBase64String(salt);

            string newHash = ComputeHash(plainPassword, saltBytes);

            ZeroMemory(gch.AddrOfPinnedObject(), plainPassword.Length*2);

            gch.Free();

            return newHash == hashedPassword;
        }

        #endregion

        //  Call this function to remove the plaintext password from memory after use
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        private string ComputeHash(string text, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(text, salt, Iterations)) {
                byte[] key = pbkdf2.GetBytes(64);
                return Convert.ToBase64String(key);
            }
        }

        private byte[] GenerateSalt()
        {
            using (RandomNumberGenerator rand = RandomNumberGenerator.Create()) {
                var saltBytes = new byte[SaltSize];

                rand.GetBytes(saltBytes);

                return saltBytes;
            }
        }
    }
}