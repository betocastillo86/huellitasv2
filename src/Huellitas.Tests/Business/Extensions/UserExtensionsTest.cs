namespace Huellitas.Tests.Business.Extensions
{
    using Huellitas.Business.Extensions;
    using Huellitas.Data.Entities;
    using NUnit.Framework;

    /// <summary>
    /// User Extensions Test
    /// </summary>
    [TestFixture]
    public class UserExtensionsTest
    {
        /// <summary>
        /// The user
        /// </summary>
        private User user;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            user = new User();
        }

        /// <summary>
        /// Gets the cellphone number by user no cell phone numbers null value.
        /// </summary>
        /// <param name="phone1">The phone1.</param>
        /// <param name="phone2">The phone2.</param>
        [Test]
        [TestCase(null, null)]
        [TestCase("300000000", null)]
        [TestCase("0300000000", "0300000000")]
        [TestCase("0300000000", null)]
        [TestCase(null, "0300000000")]
        public void GetCellphoneNumberByUser_NoCellPhoneNumbers_NullValue(string phone1, string phone2)
        {
            user.PhoneNumber = phone1;
            user.PhoneNumber2 = phone2;
            var result = user.GetCellphoneNumber();
            Assert.IsNull(result);
        }

        /// <summary>
        /// Gets the cellphone number by user with phone correct number.
        /// </summary>
        /// <param name="phone1">The phone1.</param>
        /// <param name="phone2">The phone2.</param>
        /// <param name="expected">The expected.</param>
        [Test]
        [TestCase("3000000000", null, "3000000000")]
        [TestCase("0300000000", "3000000000", "3000000000")]
        [TestCase("3000000000", "0300000000", "3000000000")]
        [TestCase(null, "3000000000", "3000000000")]
        [TestCase(null, " 3000000000 ", "3000000000")]
        [TestCase("3000000001", "3000000002", "3000000001")]
        public void GetCellphoneNumberByUser_WithPhone_CorrectNumber(string phone1, string phone2, string expected)
        {
            user.PhoneNumber = phone1;
            user.PhoneNumber2 = phone2;
            var result = user.GetCellphoneNumber();
            Assert.AreEqual(expected, result);
        }
    }
}