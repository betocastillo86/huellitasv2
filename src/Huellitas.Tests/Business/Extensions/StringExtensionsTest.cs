//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Extensions
{
    using System;
    using Huellitas.Business.Utilities.Extensions;
    using NUnit.Framework;

    /// <summary>
    /// String Extensions Test
    /// </summary>
    [TestFixture]
    public class StringExtensionsTest
    {
        /// <summary>
        /// Determines whether [is number true].
        /// </summary>
        [Test]
        public void IsNumber_True()
        {
            Assert.IsTrue("123".IsNumber());
            Assert.IsTrue("-123".IsNumber());
            Assert.IsTrue(int.MaxValue.ToString().IsNumber());
            Assert.IsTrue(int.MinValue.ToString().IsNumber());
        }

        /// <summary>
        /// Determines whether [is number false].
        /// </summary>
        [Test]
        public void IsNumber_False()
        {
            Assert.IsFalse("123a".IsNumber());
            Assert.IsFalse("a".IsNumber());
            Assert.IsFalse(double.MaxValue.ToString().IsNumber());
            Assert.IsFalse(double.MinValue.ToString().IsNumber());
        }

        /// <summary>
        /// Determines whether [is valid integer list false].
        /// </summary>
        [Test]
        public void IsValidIntList_False()
        {
            Assert.IsFalse("1a,2,3".IsValidIntList());
            Assert.IsFalse("1,a,3".IsValidIntList());
            Assert.IsFalse("1,".IsValidIntList());
            Assert.IsFalse(",1".IsValidIntList());
            Assert.IsFalse(string.Empty.IsValidIntList());
            Assert.IsFalse(" ".IsValidIntList());
            Assert.IsFalse("a".IsValidIntList());
            Assert.IsFalse("a,2,3".IsValidIntList());
            Assert.IsFalse("a,2,3".IsValidIntList());
            Assert.IsFalse("1,2,a ".IsValidIntList());
        }

        /// <summary>
        /// Determines whether [is valid integer list true].
        /// </summary>
        [Test]
        public void IsValidIntList_True()
        {
            Assert.IsTrue("1,2,3".IsValidIntList());
            Assert.IsTrue("1".IsValidIntList());
        }

        /// <summary>
        /// To the integer list invalid.
        /// </summary>
        [Test]
        public void ToIntList_Invalid()
        {
            Assert.Catch<FormatException>(() => "1a,2,3".ToIntList());
            Assert.Catch<FormatException>(() => "1,a,3".ToIntList());
            Assert.Catch<FormatException>(() => "a".ToIntList());
            Assert.Catch<FormatException>(() => ",2,3".ToIntList());
            Assert.Catch<FormatException>(() => "1,2,3,".ToIntList());
        }

        /// <summary>
        /// To the integer list valid default not null.
        /// </summary>
        [Test]
        public void ToIntList_Valid_DefaultNotNull()
        {
            Assert.AreEqual(new int[] { }, string.Empty.ToIntList(false));
        }

        /// <summary>
        /// To the integer list valid default null.
        /// </summary>
        [Test]
        public void ToIntList_Valid_DefaultNull()
        {
            Assert.AreEqual(new int[] { 1, 2, 3 }, "1,2,3".ToIntList());
            Assert.AreEqual(null, string.Empty.ToIntList());
        }

        /// <summary>
        /// To the string list default not null.
        /// </summary>
        [Test]
        public void ToStringList_DefaultNotNull()
        {
            Assert.AreEqual(new string[] { "a", "b", "c" }, "a,b,c".ToStringList(false));
            Assert.AreEqual(new string[] { }, string.Empty.ToStringList(false));
        }

        /// <summary>
        /// To the string list default null.
        /// </summary>
        [Test]
        public void ToStringList_DefaultNull()
        {
            Assert.AreEqual(null, string.Empty.ToStringList());
        }
    }
}