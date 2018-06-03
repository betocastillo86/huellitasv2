//-----------------------------------------------------------------------
// <copyright file="CustomTableRowServiceExtensionsTest.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Business.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Business.Extensions;
    using Huellitas.Business.Services;
    using Huellitas.Data.Entities;
    using Huellitas.Data.Infraestructure;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Custom Table Row Service Extensions Test
    /// </summary>
    /// <seealso cref="Huellitas.Tests.BaseTest" />
    [TestFixture]
    public class CustomTableRowServiceExtensionsTest : BaseTest
    {
        /// <summary>
        /// The custom table row service
        /// </summary>
        private Mock<ICustomTableService> customTableRowService;

        /// <summary>
        /// Gets the adoption form questions boolean ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormQuestions_Boolean_Ok()
        {
            this.Setup();

            var questions = this.GetQuestions();
            this.customTableRowService.Setup(c => c.GetRowsByTableId(4, null, OrderByTableRow.DisplayOrder, 0, int.MaxValue))
                .Returns(questions);
            var models = this.customTableRowService.Object.GetAdoptionFormQuestions(this.cacheManager.Object);
            Assert.AreEqual(models[2].Id, 3);
            Assert.IsNull(models[2].QuestionParentId);
            Assert.AreEqual(models[2].Question, "Question3");
            Assert.Zero(models[2].Options.Length);
            Assert.IsTrue(models[2].Required);
        }

        /// <summary>
        /// Gets the adoption form questions checks with text ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormQuestions_ChecksWithText_Ok()
        {
            this.Setup();

            var questions = this.GetQuestions();
            this.customTableRowService.Setup(c => c.GetRowsByTableId(4, null, OrderByTableRow.DisplayOrder, 0, int.MaxValue))
                .Returns(questions);
            var models = this.customTableRowService.Object.GetAdoptionFormQuestions(this.cacheManager.Object);
            Assert.AreEqual(models[5].Id, 6);
            Assert.AreEqual(models[5].Question, "Question6");
            Assert.AreEqual(models[5].Options.Length, 3);
            Assert.IsFalse(models[5].Required);
        }

        /// <summary>
        /// Gets the adoption form questions option with text ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormQuestions_OptionWithText_Ok()
        {
            this.Setup();

            var questions = this.GetQuestions();
            this.customTableRowService.Setup(c => c.GetRowsByTableId(4, null, OrderByTableRow.DisplayOrder, 0, int.MaxValue))
                .Returns(questions);
            var models = this.customTableRowService.Object.GetAdoptionFormQuestions(this.cacheManager.Object);
            Assert.AreEqual(models[3].Id, 4);
            Assert.AreEqual(models[3].Question, "Question4");
            Assert.AreEqual(models[3].Options.Length, 4);
            Assert.AreEqual(models[3].Options[0], "Question4Option1");
            Assert.AreEqual(models[3].Options[1], "Question4Option2");
            Assert.IsTrue(models[3].Required);
        }

        /// <summary>
        /// Gets the adoption form questions single options ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormQuestions_SingleOptions_Ok()
        {
            this.Setup();

            var questions = this.GetQuestions();
            this.customTableRowService.Setup(c => c.GetRowsByTableId(4, null, OrderByTableRow.DisplayOrder, 0, int.MaxValue))
                .Returns(questions);
            var models = this.customTableRowService.Object.GetAdoptionFormQuestions(this.cacheManager.Object);
            Assert.AreEqual(models[0].Id, 1);
            Assert.IsNull(models[0].QuestionParentId);
            Assert.AreEqual(models[0].Question, "Question1");
            Assert.AreEqual(models[0].Options.Length, 3);
            Assert.AreEqual(models[0].Options[0], "Question1Option1");
            Assert.AreEqual(models[0].Options[1], "Question1Option2");
            Assert.IsTrue(models[0].Required);
        }

        /// <summary>
        /// Gets the adoption form questions text ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormQuestions_Text_Ok()
        {
            this.Setup();

            var questions = this.GetQuestions();
            this.customTableRowService.Setup(c => c.GetRowsByTableId(4, null, OrderByTableRow.DisplayOrder, 0, int.MaxValue))
                .Returns(questions);
            var models = this.customTableRowService.Object.GetAdoptionFormQuestions(this.cacheManager.Object);
            Assert.AreEqual(models[4].Id, 5);
            Assert.AreEqual(models[4].Question, "Question5");
            Assert.AreEqual(models[4].Options.Length, 0);
            Assert.IsTrue(models[4].Required);
        }

        /// <summary>
        /// Gets the adoption form questions with parent ok.
        /// </summary>
        [Test]
        public void GetAdoptionFormQuestions_WithParent_Ok()
        {
            this.Setup();

            var questions = this.GetQuestions();
            this.customTableRowService.Setup(c => c.GetRowsByTableId(4, null, OrderByTableRow.DisplayOrder, 0, int.MaxValue))
                .Returns(questions);
            var models = this.customTableRowService.Object.GetAdoptionFormQuestions(this.cacheManager.Object);

            Assert.IsNotNull(models[3].QuestionParentId);
            Assert.AreEqual(3, models[3].QuestionParentId.Value);
        }

        /// <summary>
        /// Setups this instance.
        /// </summary>
        protected override void Setup()
        {
            base.Setup();
            this.customTableRowService = new Mock<ICustomTableService>();
        }

        /// <summary>
        /// Gets the questions.
        /// </summary>
        /// <returns>the list</returns>
        private IPagedList<CustomTableRow> GetQuestions()
        {
            var list = new List<CustomTableRow>();
            list.Add(new CustomTableRow() { Id = 1, CustomTableId = 4, Value = "Question1", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Question1Option1,Question1Option2,Question1Option3|True" });
            list.Add(new CustomTableRow() { Id = 2, CustomTableId = 4, Value = "Question2", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Question2Option1,Question2Option2,Question2Option3,Question2Option4|True" });
            var previousPets = new CustomTableRow() { Id = 3, CustomTableId = 4, Value = "Question3", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True" };
            list.Add(previousPets);
            list.Add(new CustomTableRow() { Id = 4, CustomTableId = 4, Value = "Question4", ParentCustomTableRow = previousPets, ParentCustomTableRowId = 3, AdditionalInfo = $"{AdoptionFormQuestionType.OptionsWithText}|Question4Option1,Question4Option2,Question4Option3,Question4Option4|True" });
            list.Add(new CustomTableRow() { Id = 5, CustomTableId = 4, Value = "Question5", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True" });
            list.Add(new CustomTableRow() { Id = 6, CustomTableId = 4, Value = "Question6", AdditionalInfo = $"{AdoptionFormQuestionType.ChecksWithText}|Question6Option1,Question6Option2,Question6Option3|False" });
            return new PagedList<CustomTableRow>(list.AsQueryable(), 0, int.MaxValue);
        }
    }
}