using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodTracker.API.Middleware;

namespace PeriodTracker.API.Tests
{
    [TestClass]
    public class AuthenticationHelperTests
    {
        [TestMethod]
        public void Encrypt_ShouldReturnBasicToken()
        {
            var header = AuthenticationHelper.Encrypt("john.doe", "VerySecret!");
            Assert.AreEqual("Basic am9obi5kb2U6VmVyeVNlY3JldCE=", header);
        }

        [TestMethod]
        public void Decrypt_ShouldReturnUsernameAndPassword()
        {
            AuthenticationHelper.Decrypt(
                "Basic am9obi5kb2U6VmVyeVNlY3JldCE=", 
                out var user, 
                out var pass
            );
            Assert.AreEqual("john.doe", user);
            Assert.AreEqual("VerySecret!", pass);
        }
    }
}
