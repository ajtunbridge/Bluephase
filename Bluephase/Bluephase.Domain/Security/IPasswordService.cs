namespace Bluephase.Domain.Security
{
    public interface IPasswordService
    {
        /// <summary>
        ///     Hashes the supplied plaintext password and returns the
        ///     hash and salt value that were generated
        /// </summary>
        /// <param name="plainPassword">The password to secure</param>
        /// <returns></returns>
        Password GeneratePassword(string plainPassword);

        /// <summary>
        ///     Checks if the plain password supplied matches the supplied hash
        ///     when hashed using the supplied salt value
        /// </summary>
        /// <param name="plainPassword">The password to verify</param>
        /// <param name="salt">The salt to use when hashing the plain password</param>
        /// <param name="hashedPassword">The hashed password to compare to</param>
        /// <returns></returns>
        bool AreEqual(string plainPassword, string hashedPassword, string salt);
    }
}