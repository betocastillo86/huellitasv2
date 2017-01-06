//-----------------------------------------------------------------------
// <copyright file="ContentAttributeExtensionsTests.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Extensions;
    using NUnit.Framework;

    /// <summary>
    /// Content Attribute Extensions Test
    /// </summary>
    [TestFixture]
    public class ContentAttributeExtensionsTests
    {
        /// <summary>
        /// Adds the content attribute replace false.
        /// </summary>
        [Test]
        public void AddContentAttribute_Replace_False()
        {
            var list = new List<ContentAttribute>();
            list.Add(ContentAttributeType.Address, "a");
            list.Add(ContentAttributeType.Address, "b");

            Assert.AreEqual(2, list.Count(c => c.AttributeType == ContentAttributeType.Address));
        }

        /// <summary>
        /// Adds the content attribute replace false null new.
        /// </summary>
        [Test]
        public void AddContentAttribute_Replace_False_Null_New()
        {
            var list = new List<ContentAttribute>();
            list.Add(ContentAttributeType.Address, null);

            Assert.Zero(list.Count);
        }

        /// <summary>
        /// Adds the content attribute replace true existent.
        /// </summary>
        [Test]
        public void AddContentAttribute_Replace_True_Existent()
        {
            var list = new List<ContentAttribute>() { new ContentAttribute { AttributeType = ContentAttributeType.Address, Value = "a" } };
            list.Add(ContentAttributeType.Address, "b", true);

            Assert.AreEqual(1, list.Count(c => c.AttributeType == ContentAttributeType.Address));
            Assert.AreEqual("b", list.FirstOrDefault().Value);
        }

        /// <summary>
        /// Adds the content attribute replace true null remove.
        /// </summary>
        [Test]
        public void AddContentAttribute_Replace_True_Null_Remove()
        {
            var list = new List<ContentAttribute>() { new ContentAttribute { AttributeType = ContentAttributeType.Address, Value = "a" } };
            list.Add(ContentAttributeType.Address, null, true);

            Assert.Zero(list.Count);
        }

        /// <summary>
        /// Adds the content attribute replace true null new.
        /// </summary>
        [Test]
        public void AddContentAttribute_Replace_True_Null_New()
        {
            var list = new List<ContentAttribute>();
            list.Add(ContentAttributeType.Address, null, true);

            Assert.Zero(list.Count);
        }

        /// <summary>
        /// Gets the content attribute success.
        /// </summary>
        [Test]
        public void GetContentAttribute_Success()
        {
            var list = new List<ContentAttribute>();
            list.Add(ContentAttributeType.Address, "a", true);
            list.Add(ContentAttributeType.Age, "1", true);
            list.Add(ContentAttributeType.Castrated, "true", true);
            var content = new Content() { ContentAttributes = list };

            Assert.AreEqual("a", content.GetAttribute<string>(ContentAttributeType.Address));
            Assert.AreEqual(1, content.GetAttribute<int>(ContentAttributeType.Age));
            Assert.IsTrue(content.GetAttribute<bool>(ContentAttributeType.Castrated));
            Assert.IsFalse(content.GetAttribute<bool>(ContentAttributeType.Lat));
            Assert.Zero(content.GetAttribute<int>(ContentAttributeType.Lat));
            Assert.IsNull(content.GetAttribute<string>(ContentAttributeType.Lat));
        }

        /// <summary>
        /// Removes the content attribute existent.
        /// </summary>
        [Test]
        public void RemoveContentAttribute_Existent()
        {
            var list = new List<ContentAttribute>();
            list.Add(ContentAttributeType.Address, "a", true);
            list.Add(ContentAttributeType.Age, "1", true);
            list.Add(ContentAttributeType.Castrated, "true", true);
            var content = new Content() { ContentAttributes = list };

            list.Remove(ContentAttributeType.Age);
            Assert.IsNull(content.GetAttribute<string>(ContentAttributeType.Age));
        }

        /// <summary>
        /// Removes the content attribute not existent.
        /// </summary>
        [Test]
        public void RemoveContentAttribute_NotExistent()
        {
            var list = new List<ContentAttribute>();
            list.Add(ContentAttributeType.Address, "a", true);
            list.Add(ContentAttributeType.Age, "1", true);
            list.Add(ContentAttributeType.Castrated, "true", true);

            list.Remove(ContentAttributeType.Facebook);
            Assert.AreEqual(3, list.Count);
        }

        /// <summary>
        /// Removes the content attribute null list.
        /// </summary>
        [Test]
        public void RemoveContentAttribute_NullList()
        {
            IList<ContentAttribute> list = null;
            Assert.Throws<ArgumentNullException>(() => list.Remove(ContentAttributeType.Facebook));
        }
    }
}