using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWApps2.Converters;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SwappsTest
{
    [TestClass]
    public class ConfirmPasswordTest
    {
        
        private static class TestValues
        {
            public const string password1 = "MyFavouriteLyrics";
            public const string password2 = "NameOfPet+BirthYear";
            public const string password3 = "123EenTweeDrieMaarDanInWoorden";
            //public const string salt = "AlwaysRamdomlyGeneratedPerPassword";
        }

        [TestMethod]
        public void TestOKPassword1()
        {
            byte[] salt = SecurePassword.GetSalt();
            string hash = SecurePassword.Hash(TestValues.password1, salt);
            string saltString = Convert.ToBase64String(salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, saltString, TestValues.password1);
            Assert.IsTrue(passwordConfirmed);
        }

        [TestMethod]
        public void TestOKPassword2()
        {
            byte[] salt = SecurePassword.GetSalt();
            string hash = SecurePassword.Hash(TestValues.password2, salt);
            string saltString = Convert.ToBase64String(salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, saltString, TestValues.password2);
            Assert.IsTrue(passwordConfirmed);
        }

        [TestMethod]
        public void TestOKPassword3()
        {
            byte[] salt = SecurePassword.GetSalt();
            string hash = SecurePassword.Hash(TestValues.password3, salt);
            string saltString = Convert.ToBase64String(salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, saltString, TestValues.password3);
            Assert.IsTrue(passwordConfirmed);
        }

        [TestMethod]
        public void TestOKPasswordWithEncoding()
        {
            byte[] salt = SecurePassword.GetSalt();
            string hash = SecurePassword.Hash(TestValues.password3, salt);
            string saltString = Convert.ToBase64String(salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, saltString, TestValues.password3);
            Assert.IsTrue(passwordConfirmed);
        }
    }
}
