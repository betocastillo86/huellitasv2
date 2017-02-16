//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Huellitas sin hogar">
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
    /// Ensure the seeding of data base
    /// </summary>
    public static class EnsureSeedingExtension
    {
        /// <summary>
        /// Ensures the seeding.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void EnsureSeeding(this HuellitasContext context)
        {
            if (context.AllMigrationsApplied())
            {
                EnsureSeedingExtension.SeedingContents(context);
            }
        }

        /// <summary>
        /// Seeds the contents.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedingContents(HuellitasContext context)
        {
            EnsureSeedingExtension.SeedRoles(context);
            EnsureSeedingExtension.SeedUsers(context);
            EnsureSeedingExtension.SeedCustomTables(context);
            EnsureSeedingExtension.SeedCustomTablesRows(context);
            EnsureSeedingExtension.SeedContents(context);
            EnsureSeedingExtension.SeedFiles(context);
            EnsureSeedingExtension.SeedLocations(context);
            EnsureSeedingExtension.SeedSettings(context);
            EnsureSeedingExtension.SeedAdoptionForms(context);
        }

        #region AdoptionForms        
        /// <summary>
        /// Seeds the adoption forms.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedAdoptionForms(HuellitasContext context)
        {
            var list = new List<AdoptionForm>();

            var jobId = context.CustomTableRows.FirstOrDefault(c => c.CustomTableId == Convert.ToInt32(CustomTableType.Jobs)).Id;

            list.Add(new AdoptionForm() { ContentId = 1, Name = "Nombre formulario 1", Email = "public@public.com", CreationDate = DateTime.Now, Address = "Cr 10 10 10", BirthDate = DateTime.Now, FamilyMembers = 2, JobId = jobId, LocationId = 1, PhoneNumber = "123456", Town = "20 de julio", LastStatusEnum = AdoptionFormAnswerStatus.None, UserId = 2 });
            list.Add(new AdoptionForm() { ContentId = 2, Name = "Nombre formulario 2", Email = "public@public.com", CreationDate = DateTime.Now, Address = "Cr 10 10 10", BirthDate = DateTime.Now, FamilyMembers = 2, JobId = jobId, LocationId = 1, PhoneNumber = "123456", Town = "20 de julio", LastStatusEnum = AdoptionFormAnswerStatus.None, UserId = 2 });
            list.Add(new AdoptionForm() { ContentId = 3, Name = "Nombre formulario 3", Email = "public@public.com", CreationDate = DateTime.Now, Address = "Cr 10 10 10", BirthDate = DateTime.Now, FamilyMembers = 2, JobId = jobId, LocationId = 1, PhoneNumber = "123456", Town = "20 de julio", LastStatusEnum = AdoptionFormAnswerStatus.None, UserId = 2 });

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
        #endregion

        #region Roles

        /// <summary>
        /// Seeds the roles.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedRoles(HuellitasContext context)
        {
            var list = new List<Role>();

            list.Add(new Entities.Role() { Name = "SuperAdmin", Description = "Usuario con todos los privilegios" });
            list.Add(new Entities.Role() { Name = "Public", Description = "Usuario publico" });

            foreach (var item in list)
            {
                if (!context.Roles.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Roles.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Roles

        #region Users

        /// <summary>
        /// Seeds the users.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedUsers(HuellitasContext context)
        {
            var list = new List<User>();

            list.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "210c1680b957c6ed6df5d9afd17f205cd9a01dbb"/*123.admin@admin.com*/, RoleId = 1, CreatedDate = DateTime.Now });
            list.Add(new Entities.User() { Name = "Publico", Email = "public@public.com", Password = "55d81fa21753c11353fcb3a4721a5d8ab59e5813"/*123456.public@public.com*/, RoleId = 2, CreatedDate = DateTime.Now });
            list.Add(new Entities.User() { Name = "Fundación", Email = "fundacion@fundacion.com", Password = "614c39951f7372fcad450958b43e6fe9edd34923"/*123456.fundacion@fundacion.com*/, RoleId = 2, CreatedDate = DateTime.Now });

            foreach (var item in list)
            {
                if (!context.Users.Any(c => c.Email.Equals(item.Email)))
                {
                    context.Users.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Users

        #region CustomTables

        /// <summary>
        /// Seeds the custom tables.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedCustomTables(HuellitasContext context)
        {
            var list = new List<CustomTable>();

            list.Add(new CustomTable() { Id = 1, Name = "Subtipo Animales", Description = "Subtipo de contenido para animales" });
            list.Add(new CustomTable() { Id = 2, Name = "Tamaño", Description = "Tamaño de los animales existentes" });
            list.Add(new CustomTable() { Id = 3, Name = "Genero de los animales", Description = "Tamaño de los animales existentes" });
            list.Add(new CustomTable() { Id = 4, Name = "Preguntas Formularios", Description = "Preguntas hechas en los formularios de adopción" });
            list.Add(new CustomTable() { Id = Convert.ToInt32(CustomTableType.Jobs), Name = "Trabajos para formularios", Description = "Trabajos para formularios" });

            foreach (var item in list)
            {
                if (!context.CustomTables.Any(c => c.Id.Equals(item.Id)))
                {
                    context.CustomTables.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion CustomTables

        #region CustomTablesRows

        /// <summary>
        /// Seeds the custom tables rows.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedCustomTablesRows(HuellitasContext context)
        {
            var list = new List<CustomTableRow>();

            list.Add(new CustomTableRow() { CustomTableId = 1, Value = "Perro" });
            list.Add(new CustomTableRow() { CustomTableId = 1, Value = "Gato" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Miniatura" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Pequeño" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Mediano-Pequeño" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Mediano-Grande" });
            list.Add(new CustomTableRow() { CustomTableId = 2, Value = "Grande" });
            list.Add(new CustomTableRow() { CustomTableId = 3, Value = "Macho" });
            list.Add(new CustomTableRow() { CustomTableId = 3, Value = "Hembra" });

            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Porque razón quieres adoptar un animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True" });
            var morePets = new CustomTableRow() { CustomTableId = 4, Value = "¿Tienes otras mascotas actualmente?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True" };
            list.Add(morePets);
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Que tipo de animal y que cantidad de ellos?", ParentCustomTableRow = morePets, AdditionalInfo = $"{AdoptionFormQuestionType.ChecksWithText}|Perro,Gato,Otro|True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿En qué lugar de la casa dormirá el animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Quién es la persona que autorizará la adopción del animal?", AdditionalInfo = $"{AdoptionFormQuestionType.SingleWithOther}|Soy Responsable,Madre,Padre|True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Cuánto tiempo al día permanecerá solo el animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Nunca,2 a 4 horas,4 a 6 horas,Más de 6 horas|True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué tipo de vivienda tienes?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Apartamento,Casa,Finca,Bodega|True" });
            var previousPets = new CustomTableRow() { CustomTableId = 4, Value = "¿Has tenido animales de compañía anteriormente?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}|True" };
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "Cuentanos un poco de tu anterior mascota", ParentCustomTableRow = previousPets, AdditionalInfo = $"{AdoptionFormQuestionType.OptionsWithText}|Especie,Cuanto tiempo vivió contigo?,¿En dónde está el animal?|True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué sucedería si alguien en la familia resulta alérgico a los pelos de los animales o si alguien desea tener un hijo?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué sucedería si tuvieras que mudarte a otra casa o ciudad /país?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas los gastos que genera tener un animal de compañía?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas que adoptar un animal es una responsabilidad de 14 a 17 años?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas que el animal necesita un periodo de ajuste en el que aprenda dónde debe ir al baño y se adapte a la familia?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Vives en casa propia o arriendo?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Casa propia,Arriendo|True" });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Cuánto crees que son los gastos mensuales para mantener bien al animal que deseas adoptar?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Hasta $20.000,Entre $20.000 y $50.000,Entre $50.000 y $80.000,Más de 80.000 pesos|True" });

            var jobs = Convert.ToInt32(CustomTableType.Jobs);
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Empleado" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Desempleado" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Independiente" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Estudiante" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Hogar" });

            foreach (var item in list)
            {
                if (!context.CustomTableRows.Any(c => c.Value.Equals(item.Value) && c.CustomTableId.Equals(item.CustomTableId)))
                {
                    context.CustomTableRows.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion CustomTablesRows

        #region Contents

        /// <summary>
        /// Seeds the contents.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedContents(HuellitasContext context)
        {
            var list = new List<Content>();

            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Uno",
                Body = "Cuerpo de contenido de prueba Pet 1",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = 1,
                FileId = 1,
                LocationId = 1,
                FriendlyName = "pet-uno",
                ContentAttributes = new List<ContentAttribute>()
                {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "5" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "3" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "1" }
                    }
            });
            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Dos",
                Body = "Cuerpo de contenido de prueba Pet 2",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = 1,
                FileId = 2,
                LocationId = 1,
                FriendlyName = "pet-dos",
                ContentAttributes = new List<ContentAttribute>()
                {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "1" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "5" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "2" }
                    }
            });
            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Tres",
                Body = "Cuerpo de contenido de prueba Pet 3",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = 1,
                FileId = 1,
                LocationId = 1,
                FriendlyName = "pet-tres",
                ContentAttributes = new List<ContentAttribute>()
                {
                        new ContentAttribute() { AttributeType = ContentAttributeType.Age, Value = "1" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Size, Value = "3" },
                        new ContentAttribute() { AttributeType = ContentAttributeType.Subtype, Value = "2" }
                    }
            });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Uno", Body = "Cuerpo de contenido de prueba Shelter 1", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1, FileId = 2, LocationId = 1, FriendlyName = "shelter-uno" });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Dos", Body = "Cuerpo de contenido de prueba Shelter 2", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = 1, FileId = 1, LocationId = 1, FriendlyName = "shelter-dos" });

            foreach (var item in list)
            {
                if (!context.Contents.Any(c => c.FriendlyName.Equals(item.FriendlyName)))
                {
                    context.Contents.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Contents

        #region Files

        /// <summary>
        /// Seeds the files.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedFiles(HuellitasContext context)
        {
            var list = new List<File>();

            list.Add(new File()
            {
                Name = "Imagen Uno",
                MimeType = "image/jpg",
                FileName = "1_imagen1.jpg"
            });

            list.Add(new File()
            {
                Name = "Imagen Dos",
                MimeType = "image/jpg",
                FileName = "2_imagen2.jpg"
            });

            foreach (var item in list)
            {
                if (!context.Files.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Files.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Files

        #region Locations

        /// <summary>
        /// Seeds the locations.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedLocations(HuellitasContext context)
        {
            var list = new List<Location>();

            var parent = new Location() { Id = 1, Name = "Colombia" };
            list.Add(parent);
            list.Add(new Location() { Name = "Bogotá", ParentLocationId = 1 });
            var cundinamarca = new Location() { Name = "Cundinamarca", ParentLocationId = 1 };
            list.Add(cundinamarca);
            var valle = new Location() { Name = "Valle", ParentLocationId = 1 };
            list.Add(valle);
            var atlantico = new Location() { Name = "Atlantico", ParentLocationId = 1 };
            list.Add(atlantico);
            var antioquia = new Location() { Name = "Antioquia", ParentLocationId = 1 };
            list.Add(antioquia);

            list.Add(new Location() { Name = "Cali", ParentLocation = valle });
            list.Add(new Location() { Name = "Tulua", ParentLocation = valle });
            list.Add(new Location() { Name = "Palmira", ParentLocation = valle });

            list.Add(new Location() { Name = "Barranquilla", ParentLocation = atlantico });
            list.Add(new Location() { Name = "Soledad", ParentLocation = atlantico });
            list.Add(new Location() { Name = "Otro de Atlantico", ParentLocation = atlantico });

            list.Add(new Location() { Name = "Medellin", ParentLocation = antioquia });
            list.Add(new Location() { Name = "Turbaco", ParentLocation = antioquia });
            list.Add(new Location() { Name = "Envigado", ParentLocation = antioquia });

            foreach (var item in list)
            {
                if (!context.Locations.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Locations.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Locations

        #region Settings        
        /// <summary>
        /// Seeds the settings.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedSettings(HuellitasContext context)
        {
            var list = new List<SystemSetting>();

            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationAudience", Value = "AudienceAuthentication" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationIssuer", Value = "AuthenticationIssuer" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.AuthenticationSecretKey", Value = "TheSecretKey132456789" });
            list.Add(new SystemSetting() { Name = "SecuritySettings.ExpirationTokenMinutes", Value = "60" });

            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthDetail", Value = "800" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightDetail", Value = "800" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthList", Value = "500" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightList", Value = "500" });

            foreach (var item in list)
            {
                if (!context.SystemSettings.Any(c => c.Name.Equals(item.Name)))
                {
                    context.SystemSettings.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion
    }
}