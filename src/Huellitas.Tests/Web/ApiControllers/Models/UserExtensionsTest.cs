﻿//-----------------------------------------------------------------------
// <copyright file="UserExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Web.ApiControllers.Models
{
    using System;
    using Huellitas.Data.Entities;
    using Huellitas.Web.Models.Extensions;
    using NUnit.Framework;

    /// <summary>
    /// User Extensions Test
    /// </summary>
    [TestFixture]
    public class UserExtensionsTest
    {
        /// <summary>
        /// To the user model sensitive information true.
        /// </summary>
        [Test]
        public void ToUserModel_SensitiveInfo_True()
        {
            var user = this.GetEntity();
            var model = user.ToModel(true);

            Assert.AreEqual(user.Id, model.Id);
            Assert.AreEqual(user.Name, model.Name);
            Assert.AreEqual(user.Email, model.Email);
            Assert.AreEqual(user.PhoneNumber, model.Phone);
            Assert.AreEqual(user.PhoneNumber2, model.Phone2);
            Assert.AreEqual(user.RoleEnum, model.Role);
        }

        /// <summary>
        /// To the user model sensitive information false.
        /// </summary>
        [Test]
        public void ToUserModel_SensitiveInfo_False()
        {
            var user = this.GetEntity();
            var model = user.ToModel(false);

            Assert.AreEqual(user.Id, model.Id);
            Assert.AreEqual(user.Name, model.Name);
            Assert.AreEqual(user.Email, model.Email);
            Assert.AreEqual(user.PhoneNumber, model.Phone);
            Assert.AreEqual(user.PhoneNumber2, model.Phone2);
            Assert.IsNull(model.Role);
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <returns>the user</returns>
        private User GetEntity()
        {
            return new User
            {
                Id = 1,
                Email = "email@email.com",
                Name = "name",
                CreatedDate = DateTime.UtcNow,
                Deleted = false,
                Password = "123",
                PhoneNumber = "456",
                PhoneNumber2 = "789",
                RoleEnum = RoleEnum.Public
            };
        }
    }
}