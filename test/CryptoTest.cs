// Copyright (C) 2017 Dmitry Yakimenko (detunized@gmail.com).
// Licensed under the terms of the MIT license. See LICENCE for details.

using System.Security.Cryptography;
using NUnit.Framework;

namespace RoboForm.Test
{
    [TestFixture]
    class CryptoTest
    {
        [Test]
        public void RandomBytes_returns_array_of_requested_size()
        {
            foreach (var size in new[] { 0, 1, 2, 3, 4, 15, 255, 1024, 1337 })
                Assert.That(Crypto.RandomBytes(size).Length, Is.EqualTo(size));
        }

        [Test]
        public void RandomDeviceId_starts_with_B()
        {
            for (var i = 0; i < 10; ++i)
                Assert.That(Crypto.RandomDeviceId(), Is.StringStarting("B"));
        }

        [Test]
        public void RandomDeviceId_has_correct_length()
        {
            for (var i = 0; i < 10; ++i)
                Assert.That(Crypto.RandomDeviceId().Length, Is.EqualTo(33));
        }

        [Test]
        public void ComputeClientKey_returns_key()
        {
            // Generated with the original JavaScript code
            Assert.That(Crypto.ComputeClientKey(TestData.Password, TestData.AuthInfo),
                        Is.EqualTo("8sbDhSTLwbl0FhiHAxFxGUQvQwcr4JIbpExO64+Jj8o=".Decode64()));
        }

        [Test]
        public void Hmac_returns_hashed_message()
        {
            // Generated with OpenSSL (just a smoke test, we're not implementing HMAC here)
            // $ echo -n message | openssl dgst -sha256 -binary -hmac "salt" | openssl base64
            Assert.That(Crypto.Hmac("salt".ToBytes(), "message".ToBytes()),
                        Is.EqualTo("3b8WZhUCYErLcNYqWWvzwomOHB0vZS6seUq4xfkSSd0=".Decode64()));
        }

        [Test]
        public void Md5_returns_hashed_message()
        {
            // Generated with OpenSSL (just a smoke test, we're not implementing MD5 here)
            // $ echo -n message | openssl dgst -md5 -binary | openssl base64
            Assert.That(Crypto.Md5("message".ToBytes()),
                        Is.EqualTo("eOcxAn2P1Q7WQjQLfJpjsw==".Decode64()));
        }

        [Test]
        public void DecryptAes256_returns_plaintext_without_padding()
        {
            // Generated with Ruby/openssl
            Assert.That(Crypto.DecryptAes256("XOUQiNfzQHLMHYJzo8jvaw==".Decode64(),
                                             "this is a very secure password!!".ToBytes(),
                                             "iviviviviviviviv".ToBytes(),
                                             PaddingMode.None),
                        Is.EqualTo("decrypted data!!".ToBytes()));
        }

        [Test]
        public void DecryptAes256_returns_ciphertext_with_padding()
        {
            // Generated with Ruby/openssl
            Assert.That(Crypto.DecryptAes256("snfIB8VWKBn7p869FXAfrw==".Decode64(),
                                             "this is a very secure password!!".ToBytes(),
                                             "iviviviviviviviv".ToBytes(),
                                             PaddingMode.PKCS7),
                        Is.EqualTo("decrypted data!".ToBytes()));
        }

        [Test]
        public void HashPassword_returns_hashed_password()
        {
            // TODO: Generate a test case with MD5

            // Generated with the original JavaScript code
            Assert.That(Crypto.HashPassword(TestData.Password, TestData.AuthInfo),
                        Is.EqualTo("b+rd7TUt65+hdE7+lHCBPPWHjxbq6qs0y7zufYfqHto=".Decode64()));
        }

        [Test]
        public void Sha256_returns_hashed_message()
        {
            // Generated with OpenSSL (just a smoke test, we're not implementing SHA here)
            // $ echo -n message | openssl dgst -sha256 -binary | openssl base64
            Assert.That(Crypto.Sha256("message".ToBytes()),
                        Is.EqualTo("q1MKE+RZFJgrefm34/uplM/R8/si9xzqGvvwK0YMbR0=".Decode64()));
        }
    }
}
