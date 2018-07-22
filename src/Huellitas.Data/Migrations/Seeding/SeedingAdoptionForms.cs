//-----------------------------------------------------------------------
// <copyright file="SeedingAdoptionForms.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    /// <summary>
    /// Seeding adoptions
    /// </summary>
    public static class SeedingAdoptionForms
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HuellitasContext context)
        {
            var contentId1 = context.Contents.FirstOrDefault().Id;
            var contentId2 = context.Contents.Skip(1).FirstOrDefault().Id;
            var contentId3 = context.Contents.Skip(2).FirstOrDefault().Id;
            var userId = context.Users.Skip(1).FirstOrDefault().Id;

            var list = new List<AdoptionForm>();

            var jobId = context.CustomTableRows.FirstOrDefault(c => c.CustomTableId == Convert.ToInt32(CustomTableType.Jobs)).Id;

            list.Add(new AdoptionForm() { ContentId = contentId1, Name = "Nombre formulario 1", Email = "public@public.com", CreationDate = DateTime.Now, Address = "Cr 10 10 10", BirthDate = DateTime.Now, FamilyMembers = 2, JobId = jobId, LocationId = 1, PhoneNumber = "123456", Town = "20 de julio", LastStatusEnum = AdoptionFormAnswerStatus.None, UserId = userId });
            list.Add(new AdoptionForm() { ContentId = contentId2, Name = "Nombre formulario 2", Email = "public@public.com", CreationDate = DateTime.Now, Address = "Cr 10 10 10", BirthDate = DateTime.Now, FamilyMembers = 2, JobId = jobId, LocationId = 1, PhoneNumber = "123456", Town = "20 de julio", LastStatusEnum = AdoptionFormAnswerStatus.None, UserId = userId });
            list.Add(new AdoptionForm() { ContentId = contentId3, Name = "Nombre formulario 3", Email = "public@public.com", CreationDate = DateTime.Now, Address = "Cr 10 10 10", BirthDate = DateTime.Now, FamilyMembers = 2, JobId = jobId, LocationId = 1, PhoneNumber = "123456", Town = "20 de julio", LastStatusEnum = AdoptionFormAnswerStatus.None, UserId = userId });

            var questions = context.CustomTableRows.Where(c => c.CustomTableId == Convert.ToInt32(CustomTableType.QuestionAdoptionForm)).ToList();

            foreach (var item in list)
            {
                if (!context.AdoptionForms.Any(c => c.Name.Equals(item.Name)))
                {
                    foreach (var question in questions)
                    {
                        var attribute = new AdoptionFormAttribute { AttributeId = question.Id };
                        if (question.AdditionalInfo.Split(new char[] { '|' }).Length > 1)
                        {
                            attribute.Value = question.AdditionalInfo.Split(new char[] { '|' })[1].Split(new char[] { ',' })[0];
                        }
                        else
                        {
                            attribute.Value = "Si";
                        }

                        item.Attributes.Add(attribute);
                    }

                    context.AdoptionForms.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}