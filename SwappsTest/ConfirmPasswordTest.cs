using Microsoft.VisualStudio.TestTools.UnitTesting;
using SWApps2.Converters;
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
            public const string salt = "AlwaysRamdomlyGeneratedPerPassword";
        }

        [TestMethod]
        public void TestOKPassword1()
        {
            byte[] salt = Encoding.UTF8.GetBytes(TestValues.salt);
            byte[] hash = SecurePassword.Hash(TestValues.password1, salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, salt, TestValues.password1);
            Assert.IsTrue(passwordConfirmed);
        }

        [TestMethod]
        public void TestOKPassword2()
        {
            byte[] salt = Encoding.UTF8.GetBytes(TestValues.salt);
            byte[] hash = SecurePassword.Hash(TestValues.password2, salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, salt, TestValues.password2);
            Assert.IsTrue(passwordConfirmed);
        }

        [TestMethod]
        public void TestOKPassword3()
        {
            byte[] salt = Encoding.UTF8.GetBytes(TestValues.salt);
            byte[] hash = SecurePassword.Hash(TestValues.password3, salt);
            bool passwordConfirmed = SecurePassword.ConfirmPassword(hash, salt, TestValues.password3);
            Assert.IsTrue(passwordConfirmed);
        }
    }
}
