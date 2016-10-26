//-----------------------------------------------------------------------
// <copyright file="FilterAttributeExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business
{
    using System.Collections.Generic;
    using Data.Entities;
    using Huellitas.Business.Exceptions;
    using Huellitas.Business.Extensions.Services;
    using Huellitas.Business.Services.Contents;
    using NUnit.Framework;

    /// <summary>
    /// Filter Attribute Extension Tests
    /// </summary>
    [TestFixture]
    public class FilterAttributeExtensionsTest
    {
        /// <summary>
        /// Adds the basic filter attribute not null.
        /// </summary>
        [Test]
        public void AddBasicFilterAttributeNotNull()
        {
            var attributes = new List<FilterAttribute>();
            attributes.Add(ContentAttributeType.Age, "123", FilterAttributeType.Equals, "2");
            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual(ContentAttributeType.Age, attributes[0].Attribute);
        }

        /// <summary>
        /// Adds the basic filter attribute null.
        /// </summary>
        [Test]
        public void AddBasicFilterAttributeNull()
        {
            var attributes = new List<FilterAttribute>();
            attributes.Add(ContentAttributeType.Age, null, FilterAttributeType.Equals, "2");
            Assert.AreEqual(0, attributes.Count);
        }

        /// <summary>
        /// Adds the invalid integer filter attribute.
        /// </summary>
        [Test]
        public void AddInvalidIntFilterAttribute()
        {
            var attributes = new List<FilterAttribute>();
            var ex = Assert.Throws<HuellitasException>(() => attributes.AddInt(Data.Entities.ContentAttributeType.Age, "1b"));
            Assert.IsTrue(ex.Message.Contains("El valor ingresado"));

            ex = Assert.Throws<HuellitasException>(() => attributes.AddInt(Data.Entities.ContentAttributeType.Age, "b"));
            Assert.IsTrue(ex.Message.Contains("El valor ingresado"));

            ex = Assert.Throws<HuellitasException>(() => attributes.AddInt(Data.Entities.ContentAttributeType.Age, "4546546545646546456544654565465465"));
            Assert.IsTrue(ex.Message.Contains("El valor ingresado"));
        }

        /// <summary>
        /// Adds the invalid integer list.
        /// </summary>
        [Test]
        public void AddInvalidIntList()
        {
            var attributes = new List<FilterAttribute>();
            var ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "1,2b,3"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);

            attributes = new List<FilterAttribute>();
            ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "1,b,3"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);

            attributes = new List<FilterAttribute>();
            ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "1,b"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);

            attributes = new List<FilterAttribute>();
            ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "b,1"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);

            attributes = new List<FilterAttribute>();
            ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "b"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);

            attributes = new List<FilterAttribute>();
            ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, ",1"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);

            attributes = new List<FilterAttribute>();
            ex = Assert.Throws<HuellitasException>(() => attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "8,"));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);
        }

        /// <summary>
        /// Adds the invalid range attribute bad range.
        /// </summary>
        [Test]
        public void AddInvalidRangeAttribute_BadRange()
        {
            var attributes = new List<FilterAttribute>();
            var ex = Assert.Throws<HuellitasException>(() => attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, "12 13", false));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);
            Assert.IsTrue(ex.Message.Contains("NumeroMenor-NumeroMayor"));
        }

        /// <summary>
        /// Adds the invalid range attribute no numbers.
        /// </summary>
        [Test]
        public void AddInvalidRangeAttribute_NoNumbers()
        {
            var attributes = new List<FilterAttribute>();

            var ex = Assert.Throws<HuellitasException>(() => attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, "1-b", false));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);
            Assert.IsTrue(ex.Message.Contains("no son numericos"));
        }

        /// <summary>
        /// Adds invalid the range attribute without maximum.
        /// </summary>
        [Test]
        public void AddInvalidRangeAttribute_WithoutMax()
        {
            var attributes = new List<FilterAttribute>();
            var ex = Assert.Throws<HuellitasException>(() => attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, "1-", false));
            Assert.AreEqual(HuellitasExceptionCode.BadArgument, ex.Code);
            Assert.IsTrue(ex.Message.Contains("no son numericos"));
        }

        /// <summary>
        /// Adds the null integer list.
        /// </summary>
        [Test]
        public void AddNullIntList()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddIntList(Data.Entities.ContentAttributeType.Age, null);
            Assert.AreEqual(0, attributes.Count);
        }

        /// <summary>
        /// Adds the null range.
        /// </summary>
        [Test]
        public void AddNullRange()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, null);
            Assert.AreEqual(0, attributes.Count);
        }

        /// <summary>
        /// Adds the valid integer filter attribute.
        /// </summary>
        [Test]
        public void AddValidIntFilterAttribute()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddInt(ContentAttributeType.Age, "1");
            Assert.AreEqual(1, attributes.Count);
        }

        /// <summary>
        /// Adds the valid integer list.
        /// </summary>
        [Test]
        public void AddValidIntList()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "1,2,3,4,5,6");
            Assert.AreEqual(1, attributes.Count);
            attributes.AddIntList(Data.Entities.ContentAttributeType.Age, "8");
            Assert.AreEqual(2, attributes.Count);
        }

        /// <summary>
        /// Adds the valid null integer filter attribute.
        /// </summary>
        [Test]
        public void AddValidNullIntFilterAttribute()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddInt(ContentAttributeType.Age, null);
            Assert.AreEqual(0, attributes.Count);
        }

        /// <summary>
        /// Adds the valid range attribute with maximum.
        /// </summary>
        [Test]
        public void AddValidRangeAttribute_WithMax()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, "1-5", false);
            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual("1", attributes[0].Value.ToString());
            Assert.AreEqual(FilterAttributeType.Range, attributes[0].FilterType);
            Assert.AreEqual("5", attributes[0].ValueTo.ToString());

            attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, "5-1", false);
            Assert.AreEqual(2, attributes.Count);
            Assert.AreEqual("5", attributes[1].Value.ToString());
            Assert.AreEqual("1", attributes[1].ValueTo.ToString());
        }

        /// <summary>
        /// Adds the valid range attribute without maximum.
        /// </summary>
        [Test]
        public void AddValidRangeAttribute_WithoutMax()
        {
            var attributes = new List<FilterAttribute>();
            attributes.AddRangeAttribute(Data.Entities.ContentAttributeType.Age, "1-", true);
            Assert.AreEqual(1, attributes.Count);
            Assert.AreEqual("1", attributes[0].Value.ToString());
            Assert.AreEqual(int.MaxValue.ToString(), attributes[0].ValueTo.ToString());
        }
    }
}