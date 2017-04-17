﻿//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Enums;
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
            EnsureSeedingExtension.SeedNotifications(context);
            EnsureSeedingExtension.SeedResources(context);
        }

        /// <summary>
        /// Seeds the notifications.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedNotifications(HuellitasContext context)
        {
            var list = new List<Notification>();
            list.Add(new Notification() { Id = 1, Name = "Registro de usuario", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"}]", EmailSubject = "Bienvenido a Huellitas sin hogar", EmailHtml = "Te has registrado correctamente" });
            list.Add(new Notification() { Id = 2, Name = "Mascota creada correctamente", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"}]", EmailSubject = "Mascota creada correctamente", EmailHtml = "Tu mascota ha sido creada correctamente" });
            list.Add(new Notification() { Id = 3, Name = "Mascota aprobada", Active = true, IsEmail = true, IsSystem = true, SystemText = "Tu mascota de nombre %%Pet.Name%% fue aprobada", Tags = string.Empty, EmailSubject = "Mascota creada correctamente", EmailHtml = "Tu mascota ha sido creada correctamente" });
            list.Add(new Notification() { Id = 4, Name = "Formulario de adopción enviado", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "Tu formulario está siendo evaluado", EmailHtml = "Tu formulario está siendo evaluado" });
            list.Add(new Notification() { Id = 5, Name = "Formulario de adopción recibido", Active = true, IsEmail = true, IsSystem = true, SystemText = "Has recibido un formulario de adopción", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "Has recibido un formulario de adopción", EmailHtml = "Has recibido un formulario de adopción" });

            list.Add(new Notification() { Id = 6, Name = "El formulario fue aprobado", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario fue aprobado", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}  ,{\"key\":\"%%Answer.AdditionalInfo%%\", \"value\":\"Información adicional de la respuesta\"}]", EmailSubject = "El formulario fue aprobado", EmailHtml = "El formulario fue aprobado" });
            list.Add(new Notification() { Id = 7, Name = "El formulario fue rechazado", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario fue rechazado", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"},{\"key\":\"%%Answer.AdditionalInfo%%\", \"value\":\"Información adicional de la respuesta\"}]", EmailSubject = "El formulario fue rechazado", EmailHtml = "El formulario fue rechazado" });
            list.Add(new Notification() { Id = 8, Name = "El formulario fue rechazado por adopción previa", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario fue rechazado por adopción previa", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"},{\"key\":\"%%Answer.AdditionalInfo%%\", \"value\":\"Información adicional de la respuesta\"}]", EmailSubject = "El formulario fue rechazado por adopción previa", EmailHtml = "El formulario fue rechazado por adopción previa" });

            list.Add(new Notification() { Id = 9, Name = "Confirmación solicitud de refugio recibida", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = null, EmailSubject = "Tu solicitud de refugio ha sido recibida", EmailHtml = "Tu solicitud de refugio ha sido recibida" });
            list.Add(new Notification() { Id = 10, Name = "Solicitud de shelter rechazada", Active = true, IsEmail = true, IsSystem = true, SystemText = "Se ha solicitado un nuevo shelter", Tags = null, EmailSubject = "Se ha solicitado un nuevo shelter", EmailHtml = "Se ha solicitado un nuevo shelter" });
            list.Add(new Notification() { Id = 11, Name = "Se ha solicitado un nuevo shelter", Active = true, IsEmail = true, IsSystem = true, SystemText = "Solicitud de shelter rechazada", Tags = null, EmailSubject = "Solicitud de shelter rechazada", EmailHtml = "Solicitud de shelter rechazada" });
            list.Add(new Notification() { Id = 12, Name = "Aprobación de shelter en la plataforma", Active = true, IsEmail = true, IsSystem = true, SystemText = "Aprobación de shelter en la plataforma", Tags = null, EmailSubject = "Aprobación de shelter en la plataforma", EmailHtml = "Aprobación de shelter en la plataforma" });
            list.Add(new Notification() { Id = 13, Name = "Formulario compartido con usuario del sistema", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario ha sido compartido", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "El formulario ha sido compartido", EmailHtml = "El formulario ha sido compartido" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.AdoptionFormSentToOtherUser), Name = "Formulario enviado al correo", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Url de la mascota\"}]", EmailSubject = "Te han enviado un formulario de adopción", EmailHtml = "Te han enviado un formulario de adopción por %%Pet.Name%%" });
            list.Add(new Notification() { Id = 15, Name = "Usuario agregado a un refugio", Active = true, IsEmail = true, IsSystem = true, SystemText = "Te han agregado al refugio %%Shelter.Name%%", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "Te han agregado al refugio %%Shelter.Name%%", EmailHtml = "Te han agregado a un refugio" });
            list.Add(new Notification() { Id = 16, Name = "Padrino agregado a mascota", Active = true, IsEmail = true, IsSystem = true, SystemText = "Ahora eres padrino de %%Pet.Name%%", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"}]", EmailSubject = "Ahora eres padrino de %%Pet.Name%%", EmailHtml = "Ahora eres padrino de %%Pet.Name%%" });

            foreach (var item in list)
            {
                if (!context.Notifications.Any(c => c.Id.Equals(item.Id)))
                {
                    context.Notifications.Add(item);
                }
            }

            context.SaveChanges();
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

            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthDetail", Value = "1000" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightDetail", Value = "666" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeWidthList", Value = "345" });
            list.Add(new SystemSetting() { Name = "ContentSettings.PictureSizeHeightList", Value = "230" });
            list.Add(new SystemSetting() { Name = "ContentSettings.DaysToAutoClosingPet", Value = "30" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.SiteUrl", Value = "http://localhost:23178/" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.BodyBaseHtml", Value = "<html><body><h1>Huellitas sin Hogar</h2>%%Body%%</body></html>" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.EmailSenderName", Value = "Huellitas sin hogar" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.DefaultPageSize", Value = "10" });

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

        #region REsources        
        /// <summary>
        /// Seeds the resources.
        /// </summary>
        /// <param name="context">The context.</param>
        private static void SeedResources(HuellitasContext context)
        {
            var list = new List<TextResource>();

            list.Add(new TextResource() { Name = "UserRole.Public", Value = "Público", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "UserRole.SuperAdmin", Value = "Super administrador", Language = LanguageEnum.Spanish });

            foreach (var item in list)
            {
                if (!context.TextResources.Any(c => c.Name.Equals(item.Name)))
                {
                    context.TextResources.Add(item);
                }
            }

            context.SaveChanges();
        }
        #endregion
    }
}