﻿//-----------------------------------------------------------------------
// <copyright file="UsersFilterModelTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Users
{
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Api;
    using NUnit.Framework;

    /// <summary>
    /// Users Filter Model Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class UsersFilterModelTest : BaseTest
    {
        /// <summary>
        /// Test User the filter is valid true can see all true.
        /// </summary>
        [Test]
        public void UserFilter_IsValid_True_CanSeeAll_True()
        {
            var filter = new UsersFilterModel();
            Assert.IsTrue(filter.IsValid(true));
        }

        /// <summary>
        /// Test users
        /// </summary>
        [Test]
        public void UserFilter_IsValid_True_FullObject_False()
        {
            var filter = new UsersFilterModel();
            filter.FullObject = false;
            Assert.IsTrue(filter.IsValid(false));
        }

        /// <summary>
        /// Test users
        /// </summary>
        [Test]
        public void UserFilter_IsValid_False_FullObject_True()
        {
            var filter = new UsersFilterModel();
            filter.FullObject = true;
            Assert.IsFalse(filter.IsValid(false));
            Assert.AreEqual("FullObject", filter.Errors[0].Target);
        }

        /// <summary>
        /// Test users
        /// </summary>
        [Test]
        public void UserFilter_IsValid_True_Role_Public()
        {
            var filter = new UsersFilterModel();
            filter.Role = RoleEnum.Public;
            Assert.IsTrue(filter.IsValid(false));
        }

        /// <summary>
        /// Test users
        /// </summary>
        [Test]
        public void UserFilter_IsValid_True_Role_Null()
        {
            var filter = new UsersFilterModel();
            filter.Role = null;
            Assert.IsTrue(filter.IsValid(false));
        }

        /// <summary>
        /// Test users
        /// </summary>
        [Test]
        public void UserFilter_IsValid_False_Role_Admin()
        {
            var filter = new UsersFilterModel();
            filter.Role = RoleEnum.SuperAdmin;
            Assert.IsFalse(filter.IsValid(false));
            Assert.AreEqual("Role", filter.Errors[0].Target);
        }
    }
}