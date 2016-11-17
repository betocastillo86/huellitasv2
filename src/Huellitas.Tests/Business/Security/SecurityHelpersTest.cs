//-----------------------------------------------------------------------
// <copyright file="SecurityHelpersTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Security
{
    using Huellitas.Business.Security;
    using NUnit.Framework;

    /// <summary>
    /// Security Helpers Test
    /// </summary>
    [TestFixture]
    public class SecurityHelpersTest
    {
        /// <summary>
        /// Validates the <c>md5</c>.
        /// </summary>
        [Test]
        public void ValidMd5()
        {
            var helper = new SecurityHelpers();
            var hash = helper.ToMd5("123");
            Assert.AreEqual("202cb962ac59075b964b07152d234b70", hash);
        }

        /// <summary>
        /// Validates the <c>sha1</c>.
        /// </summary>
        [Test]
        public void ValidSha1()
        {
            var helper = new SecurityHelpers();
            var hash = helper.ToSha1("123");
            Assert.AreEqual("40bd001563085fc35165329ea1ff5c5ecbdbbeef", hash);
        }

        /// <summary>
        /// Validates the <c>sha1</c> with <c>salt</c>.
        /// </summary>
        [Test]
        public void ValidSha1WithSalt()
        {
            var helper = new SecurityHelpers();
            var hash = helper.ToSha1("123", "123");
            Assert.AreEqual("3c602ef657ef2971d34f95640a750d17200902a4", hash);
        }
    }
}