//-----------------------------------------------------------------------
// <copyright file="EnsureSeedingExtension.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
            EnsureSeedingExtension.SeedFiles(context);
            EnsureSeedingExtension.SeedLocations(context);
            EnsureSeedingExtension.SeedContents(context);
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
            list.Add(new Notification() { Id = 3, Name = "Mascota aprobada", Active = true, IsEmail = true, IsSystem = true, SystemText = "Tu mascota de nombre %%Pet.Name%% fue aprobada", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"}]", EmailSubject = "La publicacion de %%Pet.Name%% ha sido aprobada", EmailHtml = "Tu mascota ha sido creada correctamente" });
            list.Add(new Notification() { Id = 4, Name = "Formulario de adopción enviado", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "Tu formulario está siendo evaluado", EmailHtml = "Tu formulario está siendo evaluado" });
            list.Add(new Notification() { Id = 5, Name = "Formulario de adopción recibido", Active = true, IsEmail = true, IsSystem = true, SystemText = "Has recibido un formulario de adopción", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "Has recibido un formulario de adopción", EmailHtml = "Has recibido un formulario de adopción" });

            list.Add(new Notification() { Id = 6, Name = "El formulario fue aprobado", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario fue aprobado", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}  ,{\"key\":\"%%Answer.AdditionalInfo%%\", \"value\":\"Información adicional de la respuesta\"}]", EmailSubject = "El formulario fue aprobado", EmailHtml = "El formulario fue aprobado" });
            list.Add(new Notification() { Id = 7, Name = "El formulario fue rechazado", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario fue rechazado", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"},{\"key\":\"%%Answer.AdditionalInfo%%\", \"value\":\"Información adicional de la respuesta\"}]", EmailSubject = "El formulario fue rechazado", EmailHtml = "El formulario fue rechazado" });
            list.Add(new Notification() { Id = 8, Name = "El formulario fue rechazado por adopción previa", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario fue rechazado por adopción previa", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"},{\"key\":\"%%Answer.AdditionalInfo%%\", \"value\":\"Información adicional de la respuesta\"}]", EmailSubject = "El formulario fue rechazado por adopción previa", EmailHtml = "El formulario fue rechazado por adopción previa" });

            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.ShelterRequestReceived), Name = "Confirmación solicitud de refugio recibida", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre de la fundación\"},{\"key\":\"%%Shelter.Url%%\", \"value\":\"Link de la fundación\"}]", EmailSubject = "Tu solicitud de refugio ha sido recibida", EmailHtml = "Tu solicitud de refugio ha sido recibida" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.ShelterRequestRejected), Name = "Solicitud de shelter rechazada", Active = true, IsEmail = true, IsSystem = true, SystemText = "Solicitud de shelter rechazada", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre de la fundación\"},{\"key\":\"%%Shelter.Url%%\", \"value\":\"Link de la fundación\"}]", EmailSubject = "Solicitud de shelter rechazada", EmailHtml = "Solicitud de shelter rechazada" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.NewShelterRequest), Name = "Se ha solicitado un nuevo shelter", Active = true, IsEmail = true, IsSystem = true, SystemText = "Solicitud de shelter rechazada", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre de la fundación\"},{\"key\":\"%%Shelter.Url%%\", \"value\":\"Link de la fundación\"}]", EmailSubject = "Tienes una nueva solicitud de shelter para aprobar", EmailHtml = "Tienes una nueva solicitud de shelter para aprobar: <b><a href=\"%%Url%%\">%%Shelter.Name%%</a></b>" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.ShelterApproved), Name = "Aprobación de shelter en la plataforma", Active = true, IsEmail = true, IsSystem = true, SystemText = "Aprobación de shelter en la plataforma", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre de la fundación\"},{\"key\":\"%%Shelter.Url%%\", \"value\":\"Link de la fundación\"}]", EmailSubject = "Aprobación de shelter en la plataforma", EmailHtml = "Aprobación de shelter en la plataforma" });
            list.Add(new Notification() { Id = 13, Name = "Formulario compartido con usuario del sistema", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario ha sido compartido", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "El formulario ha sido compartido", EmailHtml = "El formulario ha sido compartido" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.AdoptionFormSentToOtherUser), Name = "Formulario enviado al correo", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Url de la mascota\"}]", EmailSubject = "Te han enviado un formulario de adopción", EmailHtml = "Te han enviado un formulario de adopción por %%Pet.Name%%" });
            list.Add(new Notification() { Id = 15, Name = "Usuario agregado a un refugio", Active = true, IsEmail = true, IsSystem = true, SystemText = "Te han agregado al refugio %%Shelter.Name%%", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"}]", EmailSubject = "Te han agregado al refugio %%Shelter.Name%%", EmailHtml = "Te han agregado a un refugio" });
            list.Add(new Notification() { Id = 16, Name = "Padrino agregado a mascota", Active = true, IsEmail = true, IsSystem = true, SystemText = "Ahora eres padrino de %%Pet.Name%%", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"}]", EmailSubject = "Ahora eres padrino de %%Pet.Name%%", EmailHtml = "Ahora eres padrino de %%Pet.Name%%" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.CreatedLostPetConfirmation), Name = "Animal perdido creado correctamente", Active = true, IsEmail = true, IsSystem = false, SystemText = null, Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"}]", EmailSubject = "La mascota que registraste como perdido fue creado correctamente", EmailHtml = "El animal perdido ha sido creado correctamente" });
            
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.NewCommentOnContent), Name = "Comentario en contenido creado", Active = true, IsEmail = true, IsSystem = true, SystemText = "Han comentado a %%Content.Name%%", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{ \"key\":\"%%TriggerUser.Name%%\",\"value\":\"Nombre del usuario que realiza acción\" },{ \"key\":\"%%TriggerUser.Email%%\",\"value\":\"Correo del usuario que realiza acción\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Content.Name%%\", \"value\":\"Nombre del contenido\"},{\"key\":\"%%Content.Url%%\", \"value\":\"Link del contenido\"}]", EmailSubject = "Han comentado algo que te puede interesar", EmailHtml = "Recibiste un comentario en el contenido %%Content.Name%%" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.NewSubcommentOnMyComment), Name = "Subcomentario en mi comentario", Active = true, IsEmail = true, IsSystem = true, SystemText = "Han respondido tu comentario", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{ \"key\":\"%%TriggerUser.Name%%\",\"value\":\"Nombre del usuario que realiza acción\" },{ \"key\":\"%%TriggerUser.Email%%\",\"value\":\"Correo del usuario que realiza acción\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Content.Name%%\", \"value\":\"Nombre del contenido\"},{\"key\":\"%%Content.Url%%\", \"value\":\"Link del contenido\"}]", EmailSubject = "Han respondido tu comentario", EmailHtml = "Has recibido una respuesta del comentario de  %%Content.Name%%" });
            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.NewSubcommentOnSomeoneElseComment), Name = "Subcomentario en el comentario de otro", Active = true, IsEmail = true, IsSystem = true, SystemText = "Han respondido tu comentario", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{ \"key\":\"%%TriggerUser.Name%%\",\"value\":\"Nombre del usuario que realiza acción\" },{ \"key\":\"%%TriggerUser.Email%%\",\"value\":\"Correo del usuario que realiza acción\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Content.Name%%\", \"value\":\"Nombre del contenido\"},{\"key\":\"%%Content.Url%%\", \"value\":\"Link del contenido\"}]", EmailSubject = "Han respondido tu comentario", EmailHtml = "Has recibido una respuesta del comentario de  %%Content.Name%%" });

            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.AdoptionFormNotAnswered), Name = "Formulario no ha sido respondido", Active = true, IsEmail = true, IsSystem = true, SystemText = "El formulario no ha sido respondido desde %%AdoptionForm.Days%% días", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Shelter.Name%%\", \"value\":\"Nombre del refugio o padrino\"},{\"key\":\"%%Shelter.Url%%\",\"value\":\"URL del refugio o padrino\"},{\"key\":\"%%Shelter.Phone%%\", \"value\":\"Teléfono del refugio o padrino\"},{\"key\":\"%%Shelter.Address%%\", \"value\":\"Dirección del refugio o padrino\"},{\"key\":\"%%Shelter.Email%%\", \"value\":\"Correo del refugio o padrino\"},{\"key\":\"%%AdoptionForm.CreationDate%%\", \"value\":\"Fecha de solicitud de formulario\"},{\"key\":\"%%AdoptionForm.Days%%\", \"value\":\"Dias sin responder el formulario\"}]", EmailSubject = "Tienes un formulario por responder hace %%AdoptionForm.Days%%", EmailHtml = "Tienes un formulario por responder hace %%AdoptionForm.Days%%" });
            
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

        #endregion AdoptionForms

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

            list.Add(new Entities.User() { Name = "Administrador", Email = "admin@admin.com", Password = "12a80b33b30a82e27e46634b44679a7946e0c6e9"/*123.123456*/, RoleId = 1, CreatedDate = DateTime.Now, Salt = "123" });
            list.Add(new Entities.User() { Name = "Publico", Email = "public@public.com", Password = "55d81fa21753c11353fcb3a4721a5d8ab59e5813"/*123456.public@public.com*/, RoleId = 2, CreatedDate = DateTime.Now, Salt = "123" });
            list.Add(new Entities.User() { Name = "Fundación", Email = "fundacion@fundacion.com", Password = "614c39951f7372fcad450958b43e6fe9edd34923"/*123456.fundacion@fundacion.com*/, RoleId = 2, CreatedDate = DateTime.Now, Salt = "123" });

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
            list.Add(new CustomTable() { Id = Convert.ToInt32(CustomTableType.Breed), Name = "Razas", Description = "Razas de animales" });

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

            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Porque razón quieres adoptar un animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True", DisplayOrder = 1 });
            var morePets = new CustomTableRow() { CustomTableId = 4, Value = "¿Tienes otras mascotas actualmente?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True", DisplayOrder = 2 };
            list.Add(morePets);
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Que tipo de animal y que cantidad de ellos?", ParentCustomTableRow = morePets, AdditionalInfo = $"{AdoptionFormQuestionType.ChecksWithText}|Perro,Gato,Otro|True", DisplayOrder = 3 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿En qué lugar de la casa dormirá el animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True", DisplayOrder = 4 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Quién es la persona que autorizará la adopción del animal?", AdditionalInfo = $"{AdoptionFormQuestionType.SingleWithOther}|Soy Responsable,Madre,Padre|True", DisplayOrder = 5 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Cuánto tiempo al día permanecerá solo el animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Nunca,2 a 4 horas,4 a 6 horas,Más de 6 horas|True", DisplayOrder = 6 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué tipo de vivienda tienes?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Apartamento,Casa,Finca,Bodega|True", DisplayOrder = 7 });
            var previousPets = new CustomTableRow() { CustomTableId = 4, Value = "¿Has tenido animales de compañía anteriormente?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True", DisplayOrder = 8 };
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "Cuentanos un poco de tu anterior mascota", ParentCustomTableRow = previousPets, AdditionalInfo = $"{AdoptionFormQuestionType.OptionsWithText}|Especie,Cuanto tiempo vivió contigo?,¿En dónde está el animal?|True", DisplayOrder = 9 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué sucedería si alguien en la familia resulta alérgico a los pelos de los animales o si alguien desea tener un hijo?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True", DisplayOrder = 10 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué sucedería si tuvieras que mudarte a otra casa o ciudad /país?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True", DisplayOrder = 11 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas los gastos que genera tener un animal de compañía?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True", DisplayOrder = 12 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas que adoptar un animal es una responsabilidad de 14 a 17 años?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True", DisplayOrder = 13 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas que el animal necesita un periodo de ajuste en el que aprenda dónde debe ir al baño y se adapte a la familia?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True", DisplayOrder = 14 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Vives en casa propia o arriendo?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Casa propia,Arriendo|True", DisplayOrder = 15 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Cuánto crees que son los gastos mensuales para mantener bien al animal que deseas adoptar?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Hasta $20.000,Entre $20.000 y $50.000,Entre $50.000 y $80.000,Más de 80.000 pesos|True", DisplayOrder = 16 });

            var jobs = Convert.ToInt32(CustomTableType.Jobs);
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Empleado" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Desempleado" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Independiente" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Estudiante" });
            list.Add(new CustomTableRow() { CustomTableId = jobs, Value = "Hogar" });

            var breed = Convert.ToInt32(CustomTableType.Breed);
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = " Akita " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = " Akita Americano " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Afgano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Airedale Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Alaskan Malamute " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "American Pitt Bull Terrier " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "American Staffordshire Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "American Water Spaniel" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Antiguo Pastor Inglés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Australian Kelpie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Australian Shepherd " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Barzoi " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Basset Azul de Gaseogne" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Basset Hound" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Basset leonado de Bretaña" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Beagle" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bearded Collie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Beauceron " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Berger Blanc Suisse " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bichón Frisé" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bichón Habanero " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bichón Maltés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bloodhound" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bobtail" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Border Collie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Border Collie " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Borzoi " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Boston Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Boxer" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Boyero Australiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Boyero de Flandes" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Boyero de Montaña Bernés " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Braco Alemán" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Braco de Weimar " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Braco Húngaro" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Briard " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bull Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bulldog Americano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bulldog Francés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bulldog Inglés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bullmastiff" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ca de Bou " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ca mè mallorquí " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Cane Corso" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Caniche" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Carlino" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Chien de Saint Hubert " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Chihuahua" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Chino Crestado" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Chow Chow" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Cirneco del Etna" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Cocker Spaniel Americano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Cocker Spaniel Inglés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Collie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Coton de Tuléar " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dachshunds " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dálmata" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Deutsch Drahthaar" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Deutsch Kurzhaar " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dobermann" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dogo Alemán" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dogo Argentino" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dogo Canario " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dogo de Burdeos" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Drahthaar" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "English Springer Spaniel " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Epagneul Bretón" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Eurasier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Fila Brasileiro " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Fox Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Foxhound Americano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Foxhound Inglés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Galgo Afgano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Galgo Español" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Galgo Italiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Galgo Ruso" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Gigante de los Pirineos" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Golden Retriever" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Gos d'Atura" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Gran Danés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Gran Perro Japonés " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Husky Siberiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Irish Wolfhound" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Jack Russel" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Japanes Chin" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Kelpie " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Kerry Blue" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Komondor" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Kuvasz " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Labrador Retriever" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Laika de Siberia Occidental" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Laika Ruso-europeo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Lebrel ruso " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Leonberger " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Lhasa Apso" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Magyar Vizsla" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Maltés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Mastín del Alentejo " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Mastín del Pirineo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Mastin del Tibet" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Mastín Español" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Mastín Napolitano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Norfolk Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ovtcharka " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pachón Navarro " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor Alemán" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor Australiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor Belga" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor Blanco Suizo " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor de Beauce" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor de Brie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor de los Pirineos de Cara Rosa" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor de Shetland " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pastor del Cáucaso " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pekinés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perdiguero Burgos " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perdiguero Chesapeake Bay" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perdiguero de Drentse" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perdiguero de Pelo lido" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perdiguero de pelo rizado" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perdiguero Portugués" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perro Crestado Chino" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perro de Aguas" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perro sin pelo de Mexico " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Perro sin pelo del Perú" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pinscher miniatura " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pitbull" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Podenco Andaluz" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Podenco Ibicenco" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Podenco Portugués" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "presa canario" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Presa Mallorquin" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Rafeiro do Alentejo " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Rottweiler" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Rough Collie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sabueso Español" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sabueso Hélenico" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sabueso Italiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sabueso Suizo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Saint Hubert " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Saluki" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Samoyedo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "San Bernardo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Schnaucer" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Scottish Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sealyhalm Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Setter Gordon" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Setter Irlandés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Shar Pei" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Shiba " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Shiba Inu" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Shih Tzu " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Siberian Husky" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Smooth Collie" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Spaniel Japonés " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Spinone Italiano " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Spitz Alemán " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Springer Spaniel Inglés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Staffordshire Bull Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Teckel" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terranova" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Australiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Escocés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Irlandés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Japonés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Negro Ruso" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Norfolk" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Terrier Ruso" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Tibetan Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Vizsla " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Welhs Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "West Highland T." });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Wolfspitz" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Xoloitzquintle " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Yorkshire Terrier" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Criollo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Otro" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Poodle" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Aphrodite's Giants" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Australian Mist" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "American Curl" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Azul ruso" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "American shorthair" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "American wirehair" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Angora turco" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Africano doméstico" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bengala" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bobtail japonés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bombay" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Bosque de Noruega" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Brazilian Shorthair" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Brivon de pelo corto" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Brivon de pelo largo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "British Shorthair" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Burmés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Burmilla" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Cornish rexx" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "California Spangled" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ceylon" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Cymric" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Chartreux" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Deutsch Langhaar" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Devon rex" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dorado africano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Don Sphynx" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Dragon Li" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Europeo Común" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Exótico de Pelo Corto " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "FoldEx" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "German Rex" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Habana brown" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Himalayo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Korat" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Khao Manee" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Maine Coon" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Manx" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Mau egipcio" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Munchkin" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ocicat" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Oriental" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Oriental de pelo largo" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ojos azules" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "PerFold " });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Persa Americano o Moderno ( Modern Persian Cat2 )" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Persa Clásico o Tradicional ( Traditional Persian Cat3 )" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Peterbald" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Pixie Bob" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Ragdoll" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sagrado de Birmania" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Scottish Fold" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Selkirk rex" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Serengeti" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Seychellois" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Siamés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Siamés Moderno" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Siamés Tradicional" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Siberiano" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Snowshoe" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Sphynx" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Tonkinés" });
            list.Add(new CustomTableRow() { CustomTableId = breed, Value = "Van Turco" });

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
            var userid = context.Users.FirstOrDefault().Id;

            var list = new List<Content>();

            list.Add(new Entities.Content()
            {
                Name = "Contenido de prueba Pet Uno",
                Body = "Cuerpo de contenido de prueba Pet 1",
                Type = Entities.ContentType.Pet,
                StatusType = Entities.StatusType.Published,
                CreatedDate = DateTime.Now,
                UserId = userid,
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
                UserId = userid,
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
                UserId = userid,
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
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Uno", Body = "Cuerpo de contenido de prueba Shelter 1", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = userid, FileId = 2, LocationId = 1, FriendlyName = "shelter-uno" });
            list.Add(new Entities.Content() { Name = "Contenido de prueba Shelter Dos", Body = "Cuerpo de contenido de prueba Shelter 2", Type = Entities.ContentType.Shelter, StatusType = Entities.StatusType.Published, CreatedDate = DateTime.Now, UserId = userid, FileId = 1, LocationId = 1, FriendlyName = "shelter-dos" });

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

            var locations = new Location[]
            {
                new Location() { Name = "Colombia", ChildrenLocations = new Location[]
                {
                    new Location() { Name = "Antioquia", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Medellín" },
                        new Location() { Name = "Abejorral" },
                        new Location() { Name = "Abriaquí" },
                        new Location() { Name = "Alejandría" },
                        new Location() { Name = "Amaga" },
                        new Location() { Name = "Amalfi" },
                        new Location() { Name = "Andes" },
                        new Location() { Name = "Angelopolis" },
                        new Location() { Name = "Angostura" },
                        new Location() { Name = "Anorí" },
                        new Location() { Name = "Santafé De Antioquia" },
                        new Location() { Name = "Anza" },
                        new Location() { Name = "Apartadó" },
                        new Location() { Name = "Arboletes" },
                        new Location() { Name = "Argelia" },
                        new Location() { Name = "Armenia" },
                        new Location() { Name = "Barbosa" },
                        new Location() { Name = "Belmira" },
                        new Location() { Name = "Bello" },
                        new Location() { Name = "Betania" },
                        new Location() { Name = "Betulia" },
                        new Location() { Name = "Ciudad Bolívar" },
                        new Location() { Name = "Briceño" },
                        new Location() { Name = "Buriticá" },
                        new Location() { Name = "Cáceres" },
                        new Location() { Name = "Caicedo" },
                        new Location() { Name = "Caldas" },
                        new Location() { Name = "Campamento" },
                        new Location() { Name = "Cañasgordas" },
                        new Location() { Name = "Caracolí" },
                        new Location() { Name = "Caramanta" },
                        new Location() { Name = "Carepa" },
                        new Location() { Name = "El Carmen De Viboral" },
                        new Location() { Name = "Carolina" },
                        new Location() { Name = "Caucasia" },
                        new Location() { Name = "Chigorodó" },
                        new Location() { Name = "Cisneros" },
                        new Location() { Name = "Cocorná" },
                        new Location() { Name = "Concepción" },
                        new Location() { Name = "Concordia" },
                        new Location() { Name = "Copacabana" },
                        new Location() { Name = "Dabeiba" },
                        new Location() { Name = "Don Matias" },
                        new Location() { Name = "Ebéjico" },
                        new Location() { Name = "El Bagre" },
                        new Location() { Name = "Entrerrios" },
                        new Location() { Name = "Envigado" },
                        new Location() { Name = "Fredonia" },
                        new Location() { Name = "Frontino" },
                        new Location() { Name = "Giraldo" },
                        new Location() { Name = "Girardota" },
                        new Location() { Name = "Gómez Plata" },
                        new Location() { Name = "Granada" },
                        new Location() { Name = "Guadalupe" },
                        new Location() { Name = "Guarne" },
                        new Location() { Name = "Guatape" },
                        new Location() { Name = "Heliconia" },
                        new Location() { Name = "Hispania" },
                        new Location() { Name = "Itagui" },
                        new Location() { Name = "Ituango" },
                        new Location() { Name = "Jardin" },
                        new Location() { Name = "Jericó" },
                        new Location() { Name = "La Ceja" },
                        new Location() { Name = "La Estrella" },
                        new Location() { Name = "La Pintada" },
                        new Location() { Name = "La Unión" },
                        new Location() { Name = "Liborina" },
                        new Location() { Name = "Maceo" },
                        new Location() { Name = "Marinilla" },
                        new Location() { Name = "Montebello" },
                        new Location() { Name = "Murindó" },
                        new Location() { Name = "Mutata" },
                        new Location() { Name = "Nariño" },
                        new Location() { Name = "Necoclí" },
                        new Location() { Name = "Nechí" },
                        new Location() { Name = "Olaya" },
                        new Location() { Name = "Peñol" },
                        new Location() { Name = "Peque" },
                        new Location() { Name = "Pueblorrico" },
                        new Location() { Name = "Puerto Berrio" },
                        new Location() { Name = "Puerto Nare" },
                        new Location() { Name = "Puerto Triunfo" },
                        new Location() { Name = "Remedios" },
                        new Location() { Name = "Retiro" },
                        new Location() { Name = "Rionegro" },
                        new Location() { Name = "Sabanalarga" },
                        new Location() { Name = "Sabaneta" },
                        new Location() { Name = "Salgar" },
                        new Location() { Name = "San Andrés De Cuerquia" },
                        new Location() { Name = "San Carlos" },
                        new Location() { Name = "San Francisco" },
                        new Location() { Name = "San Jerónimo" },
                        new Location() { Name = "San José De La Montaña" },
                        new Location() { Name = "San Juan De Uraba" },
                        new Location() { Name = "San Luis" },
                        new Location() { Name = "San Pedro" },
                        new Location() { Name = "San Pedro De Uraba" },
                        new Location() { Name = "San Rafael" },
                        new Location() { Name = "San Roque" },
                        new Location() { Name = "San Vicente" },
                        new Location() { Name = "Santa Barbara" },
                        new Location() { Name = "Santa Rosa De Osos" },
                        new Location() { Name = "Santo Domingo" },
                        new Location() { Name = "El Santuario" },
                        new Location() { Name = "Segovia" },
                        new Location() { Name = "Sonson" },
                        new Location() { Name = "Sopetran" },
                        new Location() { Name = "Támesis" },
                        new Location() { Name = "Tarazá" },
                        new Location() { Name = "Tarso" },
                        new Location() { Name = "Titiribí" },
                        new Location() { Name = "Toledo" },
                        new Location() { Name = "Turbo" },
                        new Location() { Name = "Uramita" },
                        new Location() { Name = "Urrao" },
                        new Location() { Name = "Valdivia" },
                        new Location() { Name = "Valparaiso" },
                        new Location() { Name = "Vegachí" },
                        new Location() { Name = "Venecia" },
                        new Location() { Name = "Vigía Del Fuerte" },
                        new Location() { Name = "Yalí" },
                        new Location() { Name = "Yarumal" },
                        new Location() { Name = "Yolombó" },
                        new Location() { Name = "Yondó" },
                        new Location() { Name = "Zaragoza" }
                    } },
                    new Location() { Name = "Atlántico", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Barranquilla" },
                        new Location() { Name = "Baranoa" },
                        new Location() { Name = "Campo De La Cruz" },
                        new Location() { Name = "Candelaria" },
                        new Location() { Name = "Galapa" },
                        new Location() { Name = "Juan De Acosta" },
                        new Location() { Name = "Luruaco" },
                        new Location() { Name = "Malambo" },
                        new Location() { Name = "Manati" },
                        new Location() { Name = "Palmar De Varela" },
                        new Location() { Name = "Piojó" },
                        new Location() { Name = "Polonuevo" },
                        new Location() { Name = "Ponedera" },
                        new Location() { Name = "Puerto Colombia" },
                        new Location() { Name = "Repelon" },
                        new Location() { Name = "Sabanagrande" },
                        new Location() { Name = "Sabanalarga" },
                        new Location() { Name = "Santa Lucia" },
                        new Location() { Name = "Santo Tomas" },
                        new Location() { Name = "Soledad" },
                        new Location() { Name = "Suan" },
                        new Location() { Name = "Tubara" },
                        new Location() { Name = "Usiacuri" }
                    } },
                    new Location() { Name = "Bogotá", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Bogotá D.C." }
                    } },
                    new Location() { Name = "Bolivar", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Cartagena" },
                        new Location() { Name = "Achí" },
                        new Location() { Name = "Altos Del Rosario" },
                        new Location() { Name = "Arenal" },
                        new Location() { Name = "Arjona" },
                        new Location() { Name = "Arroyohondo" },
                        new Location() { Name = "Barranco De Loba" },
                        new Location() { Name = "Calamar" },
                        new Location() { Name = "Cantagallo" },
                        new Location() { Name = "Cicuco" },
                        new Location() { Name = "Cordoba" },
                        new Location() { Name = "Clemencia" },
                        new Location() { Name = "El Carmen De Bolivar" },
                        new Location() { Name = "El Guamo" },
                        new Location() { Name = "El Peñon" },
                        new Location() { Name = "Hatillo De Loba" },
                        new Location() { Name = "Magangué" },
                        new Location() { Name = "Mahates" },
                        new Location() { Name = "Margarita" },
                        new Location() { Name = "Maria La Baja" },
                        new Location() { Name = "Montecristo" },
                        new Location() { Name = "Mompós" },
                        new Location() { Name = "Norosi" },
                        new Location() { Name = "Morales" },
                        new Location() { Name = "Pinillos" },
                        new Location() { Name = "Regidor" },
                        new Location() { Name = "Río Viejo" },
                        new Location() { Name = "San Cristobal" },
                        new Location() { Name = "San Estanislao" },
                        new Location() { Name = "San Fernando" },
                        new Location() { Name = "San Jacinto" },
                        new Location() { Name = "San Jacinto Del Cauca" },
                        new Location() { Name = "San Juan Nepomuceno" },
                        new Location() { Name = "San Martin De Loba" },
                        new Location() { Name = "San Pablo" },
                        new Location() { Name = "Santa Catalina" },
                        new Location() { Name = "Santa Rosa" },
                        new Location() { Name = "Santa Rosa Del Sur" },
                        new Location() { Name = "Simití" },
                        new Location() { Name = "Soplaviento" },
                        new Location() { Name = "Talaigua Nuevo" },
                        new Location() { Name = "Tiquisio" },
                        new Location() { Name = "Turbaco" },
                        new Location() { Name = "Turbana" },
                        new Location() { Name = "Villanueva" },
                        new Location() { Name = "Zambrano" }
                    } },
                    new Location() { Name = "Boyacá", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Tunja" },
                        new Location() { Name = "Almeida" },
                        new Location() { Name = "Aquitania" },
                        new Location() { Name = "Arcabuco" },
                        new Location() { Name = "Belén" },
                        new Location() { Name = "Berbeo" },
                        new Location() { Name = "Betéitiva" },
                        new Location() { Name = "Boavita" },
                        new Location() { Name = "Boyacá" },
                        new Location() { Name = "Briceño" },
                        new Location() { Name = "Buenavista" },
                        new Location() { Name = "Busbanzá" },
                        new Location() { Name = "Caldas" },
                        new Location() { Name = "Campohermoso" },
                        new Location() { Name = "Cerinza" },
                        new Location() { Name = "Chinavita" },
                        new Location() { Name = "Chiquinquirá" },
                        new Location() { Name = "Chiscas" },
                        new Location() { Name = "Chita" },
                        new Location() { Name = "Chitaraque" },
                        new Location() { Name = "Chivatá" },
                        new Location() { Name = "Ciénega" },
                        new Location() { Name = "Cómbita" },
                        new Location() { Name = "Coper" },
                        new Location() { Name = "Corrales" },
                        new Location() { Name = "Covarachía" },
                        new Location() { Name = "Cubará" },
                        new Location() { Name = "Cucaita" },
                        new Location() { Name = "Cuítiva" },
                        new Location() { Name = "Chíquiza" },
                        new Location() { Name = "Chivor" },
                        new Location() { Name = "Duitama" },
                        new Location() { Name = "El Cocuy" },
                        new Location() { Name = "El Espino" },
                        new Location() { Name = "Firavitoba" },
                        new Location() { Name = "Floresta" },
                        new Location() { Name = "Gachantivá" },
                        new Location() { Name = "Gameza" },
                        new Location() { Name = "Garagoa" },
                        new Location() { Name = "Guacamayas" },
                        new Location() { Name = "Guateque" },
                        new Location() { Name = "Guayatá" },
                        new Location() { Name = "Güicán" },
                        new Location() { Name = "Iza" },
                        new Location() { Name = "Jenesano" },
                        new Location() { Name = "Jericó" },
                        new Location() { Name = "Labranzagrande" },
                        new Location() { Name = "La Capilla" },
                        new Location() { Name = "La Victoria" },
                        new Location() { Name = "La Uvita" },
                        new Location() { Name = "Villa De Leyva" },
                        new Location() { Name = "Macanal" },
                        new Location() { Name = "Maripí" },
                        new Location() { Name = "Miraflores" },
                        new Location() { Name = "Mongua" },
                        new Location() { Name = "Monguí" },
                        new Location() { Name = "Moniquirá" },
                        new Location() { Name = "Motavita" },
                        new Location() { Name = "Muzo" },
                        new Location() { Name = "Nobsa" },
                        new Location() { Name = "Nuevo Colón" },
                        new Location() { Name = "Oicatá" },
                        new Location() { Name = "Otanche" },
                        new Location() { Name = "Pachavita" },
                        new Location() { Name = "Páez" },
                        new Location() { Name = "Paipa" },
                        new Location() { Name = "Pajarito" },
                        new Location() { Name = "Panqueba" },
                        new Location() { Name = "Pauna" },
                        new Location() { Name = "Paya" },
                        new Location() { Name = "Paz De Rio" },
                        new Location() { Name = "Pesca" },
                        new Location() { Name = "Pisba" },
                        new Location() { Name = "Puerto Boyaca" },
                        new Location() { Name = "Quípama" },
                        new Location() { Name = "Ramiriquí" },
                        new Location() { Name = "Ráquira" },
                        new Location() { Name = "Rondón" },
                        new Location() { Name = "Saboyá" },
                        new Location() { Name = "Sáchica" },
                        new Location() { Name = "Samacá" },
                        new Location() { Name = "San Eduardo" },
                        new Location() { Name = "San José De Pare" },
                        new Location() { Name = "San Luis De Gaceno" },
                        new Location() { Name = "San Mateo" },
                        new Location() { Name = "San Miguel De Sema" },
                        new Location() { Name = "San Pablo De Borbur" },
                        new Location() { Name = "Santana" },
                        new Location() { Name = "Santa María" },
                        new Location() { Name = "Santa Rosa De Viterbo" },
                        new Location() { Name = "Santa Sofía" },
                        new Location() { Name = "Sativanorte" },
                        new Location() { Name = "Sativasur" },
                        new Location() { Name = "Siachoque" },
                        new Location() { Name = "Soatá" },
                        new Location() { Name = "Socotá" },
                        new Location() { Name = "Socha" },
                        new Location() { Name = "Sogamoso" },
                        new Location() { Name = "Somondoco" },
                        new Location() { Name = "Sora" },
                        new Location() { Name = "Sotaquirá" },
                        new Location() { Name = "Soracá" },
                        new Location() { Name = "Susacón" },
                        new Location() { Name = "Sutamarchán" },
                        new Location() { Name = "Sutatenza" },
                        new Location() { Name = "Tasco" },
                        new Location() { Name = "Tenza" },
                        new Location() { Name = "Tibaná" },
                        new Location() { Name = "Tibasosa" },
                        new Location() { Name = "Tinjacá" },
                        new Location() { Name = "Tipacoque" },
                        new Location() { Name = "Toca" },
                        new Location() { Name = "Togüí" },
                        new Location() { Name = "Tópaga" },
                        new Location() { Name = "Tota" },
                        new Location() { Name = "Tununguá" },
                        new Location() { Name = "Turmequé" },
                        new Location() { Name = "Tuta" },
                        new Location() { Name = "Tutazá" },
                        new Location() { Name = "Umbita" },
                        new Location() { Name = "Ventaquemada" },
                        new Location() { Name = "Viracachá" },
                        new Location() { Name = "Zetaquira" }
                    } },
                    new Location() { Name = "Caldas", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Manizales" },
                        new Location() { Name = "Aguadas" },
                        new Location() { Name = "Anserma" },
                        new Location() { Name = "Aranzazu" },
                        new Location() { Name = "Belalcázar" },
                        new Location() { Name = "Chinchina" },
                        new Location() { Name = "Filadelfia" },
                        new Location() { Name = "La Dorada" },
                        new Location() { Name = "La Merced" },
                        new Location() { Name = "Manzanares" },
                        new Location() { Name = "Marmato" },
                        new Location() { Name = "Marquetalia" },
                        new Location() { Name = "Marulanda" },
                        new Location() { Name = "Neira" },
                        new Location() { Name = "Norcasia" },
                        new Location() { Name = "Pácora" },
                        new Location() { Name = "Palestina" },
                        new Location() { Name = "Pensilvania" },
                        new Location() { Name = "Riosucio" },
                        new Location() { Name = "Risaralda" },
                        new Location() { Name = "Salamina" },
                        new Location() { Name = "Samaná" },
                        new Location() { Name = "San José" },
                        new Location() { Name = "Supía" },
                        new Location() { Name = "Victoria" },
                        new Location() { Name = "Villamaria" },
                        new Location() { Name = "Viterbo" }
                    } },
                    new Location() { Name = "Caqueta", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Florencia" },
                        new Location() { Name = "Albania" },
                        new Location() { Name = "Belén De Los Andaquies" },
                        new Location() { Name = "Cartagena Del Chairá" },
                        new Location() { Name = "Curillo" },
                        new Location() { Name = "El Doncello" },
                        new Location() { Name = "El Paujil" },
                        new Location() { Name = "La Montañita" },
                        new Location() { Name = "Milan" },
                        new Location() { Name = "Morelia" },
                        new Location() { Name = "Puerto Rico" },
                        new Location() { Name = "San Jose Del Fragua" },
                        new Location() { Name = "San Vicente Del Caguán" },
                        new Location() { Name = "Solano" },
                        new Location() { Name = "Solita" },
                        new Location() { Name = "Valparaiso" }
                    } },
                    new Location() { Name = "Cauca", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Popayán" },
                        new Location() { Name = "Almaguer" },
                        new Location() { Name = "Argelia" },
                        new Location() { Name = "Balboa" },
                        new Location() { Name = "Bolívar" },
                        new Location() { Name = "Buenos Aires" },
                        new Location() { Name = "Cajibío" },
                        new Location() { Name = "Caldono" },
                        new Location() { Name = "Caloto" },
                        new Location() { Name = "Corinto" },
                        new Location() { Name = "El Tambo" },
                        new Location() { Name = "Florencia" },
                        new Location() { Name = "Guachene" },
                        new Location() { Name = "Guapi" },
                        new Location() { Name = "Inzá" },
                        new Location() { Name = "Jambalo" },
                        new Location() { Name = "La Sierra" },
                        new Location() { Name = "La Vega" },
                        new Location() { Name = "Lopez" },
                        new Location() { Name = "Mercaderes" },
                        new Location() { Name = "Miranda" },
                        new Location() { Name = "Morales" },
                        new Location() { Name = "Padilla" },
                        new Location() { Name = "Paez" },
                        new Location() { Name = "Patia" },
                        new Location() { Name = "Piamonte" },
                        new Location() { Name = "Piendamo" },
                        new Location() { Name = "Puerto Tejada" },
                        new Location() { Name = "Purace" },
                        new Location() { Name = "Rosas" },
                        new Location() { Name = "San Sebastian" },
                        new Location() { Name = "Santander De Quilichao" },
                        new Location() { Name = "Santa Rosa" },
                        new Location() { Name = "Silvia" },
                        new Location() { Name = "Sotara" },
                        new Location() { Name = "Suarez" },
                        new Location() { Name = "Sucre" },
                        new Location() { Name = "Timbio" },
                        new Location() { Name = "Timbiqui" },
                        new Location() { Name = "Toribio" },
                        new Location() { Name = "Totoro" },
                        new Location() { Name = "Villa Rica" }
                    } },
                    new Location() { Name = "Cesar", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Valledupar" },
                        new Location() { Name = "Aguachica" },
                        new Location() { Name = "Agustín Codazzi" },
                        new Location() { Name = "Astrea" },
                        new Location() { Name = "Becerril" },
                        new Location() { Name = "Bosconia" },
                        new Location() { Name = "Chimichagua" },
                        new Location() { Name = "Chiriguana" },
                        new Location() { Name = "Curumaní" },
                        new Location() { Name = "El Copey" },
                        new Location() { Name = "El Paso" },
                        new Location() { Name = "Gamarra" },
                        new Location() { Name = "González" },
                        new Location() { Name = "La Gloria" },
                        new Location() { Name = "La Jagua De Ibirico" },
                        new Location() { Name = "Manaure" },
                        new Location() { Name = "Pailitas" },
                        new Location() { Name = "Pelaya" },
                        new Location() { Name = "Pueblo Bello" },
                        new Location() { Name = "Río De Oro" },
                        new Location() { Name = "La Paz" },
                        new Location() { Name = "San Alberto" },
                        new Location() { Name = "San Diego" },
                        new Location() { Name = "San Martin" },
                        new Location() { Name = "Tamalameque" }
                    } },
                    new Location() { Name = "Cordoba", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Montería" },
                        new Location() { Name = "Ayapel" },
                        new Location() { Name = "Buenavista" },
                        new Location() { Name = "Canalete" },
                        new Location() { Name = "Cereté" },
                        new Location() { Name = "Chimá" },
                        new Location() { Name = "Chinú" },
                        new Location() { Name = "Ciénaga De Oro" },
                        new Location() { Name = "Cotorra" },
                        new Location() { Name = "La Apartada" },
                        new Location() { Name = "Lorica" },
                        new Location() { Name = "Los Córdobas" },
                        new Location() { Name = "Momil" },
                        new Location() { Name = "Montelíbano" },
                        new Location() { Name = "Moñitos" },
                        new Location() { Name = "Planeta Rica" },
                        new Location() { Name = "Pueblo Nuevo" },
                        new Location() { Name = "Puerto Escondido" },
                        new Location() { Name = "Puerto Libertador" },
                        new Location() { Name = "Purísima" },
                        new Location() { Name = "Sahagún" },
                        new Location() { Name = "San Andres Sotavento" },
                        new Location() { Name = "San Antero" },
                        new Location() { Name = "San Bernardo Del Viento" },
                        new Location() { Name = "San Carlos" },
                        // TODO: agregar a la estructura del update (instalador)
                        new Location() { Name = "San Jose de Ure" },
                        new Location() { Name = "San Pelayo" },
                        new Location() { Name = "Tierralta" },
                        // TODO: agregar a la estructura del update (instalador)
                        new Location() { Name = "Tuchín" },
                        new Location() { Name = "Valencia" }
                    } },
                    new Location() { Name = "Cundinamarca", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Agua De Dios" },
                        new Location() { Name = "Albán" },
                        new Location() { Name = "Anapoima" },
                        new Location() { Name = "Anolaima" },
                        new Location() { Name = "Arbeláez" },
                        new Location() { Name = "Beltrán" },
                        new Location() { Name = "Bituima" },
                        new Location() { Name = "Bojacá" },
                        new Location() { Name = "Cabrera" },
                        new Location() { Name = "Cachipay" },
                        new Location() { Name = "Cajicá" },
                        new Location() { Name = "Caparrapí" },
                        new Location() { Name = "Caqueza" },
                        new Location() { Name = "Carmen De Carupa" },
                        new Location() { Name = "Chaguaní" },
                        new Location() { Name = "Chía" },
                        new Location() { Name = "Chipaque" },
                        new Location() { Name = "Choachí" },
                        new Location() { Name = "Chocontá" },
                        new Location() { Name = "Cogua" },
                        new Location() { Name = "Cota" },
                        new Location() { Name = "Cucunubá" },
                        new Location() { Name = "El Colegio" },
                        new Location() { Name = "El Peñón" },
                        new Location() { Name = "El Rosal" },
                        new Location() { Name = "Facatativá" },
                        new Location() { Name = "Fomeque" },
                        new Location() { Name = "Fosca" },
                        new Location() { Name = "Funza" },
                        new Location() { Name = "Fúquene" },
                        new Location() { Name = "Fusagasugá" },
                        new Location() { Name = "Gachala" },
                        new Location() { Name = "Gachancipá" },
                        new Location() { Name = "Gacheta" },
                        new Location() { Name = "Gama" },
                        new Location() { Name = "Girardot" },
                        new Location() { Name = "Granada" },
                        new Location() { Name = "Guachetá" },
                        new Location() { Name = "Guaduas" },
                        new Location() { Name = "Guasca" },
                        new Location() { Name = "Guataquí" },
                        new Location() { Name = "Guatavita" },
                        new Location() { Name = "Guayabal De Siquima" },
                        new Location() { Name = "Guayabetal" },
                        new Location() { Name = "Gutiérrez" },
                        new Location() { Name = "Jerusalén" },
                        new Location() { Name = "Junín" },
                        new Location() { Name = "La Calera" },
                        new Location() { Name = "La Mesa" },
                        new Location() { Name = "La Palma" },
                        new Location() { Name = "La Peña" },
                        new Location() { Name = "La Vega" },
                        new Location() { Name = "Lenguazaque" },
                        new Location() { Name = "Macheta" },
                        new Location() { Name = "Madrid" },
                        new Location() { Name = "Manta" },
                        new Location() { Name = "Medina" },
                        new Location() { Name = "Mosquera" },
                        new Location() { Name = "Nariño" },
                        new Location() { Name = "Nemocon" },
                        new Location() { Name = "Nilo" },
                        new Location() { Name = "Nimaima" },
                        new Location() { Name = "Nocaima" },
                        new Location() { Name = "Venecia" },
                        new Location() { Name = "Pacho" },
                        new Location() { Name = "Paime" },
                        new Location() { Name = "Pandi" },
                        new Location() { Name = "Paratebueno" },
                        new Location() { Name = "Pasca" },
                        new Location() { Name = "Puerto Salgar" },
                        new Location() { Name = "Puli" },
                        new Location() { Name = "Quebradanegra" },
                        new Location() { Name = "Quetame" },
                        new Location() { Name = "Quipile" },
                        new Location() { Name = "Apulo" },
                        new Location() { Name = "Ricaurte" },
                        new Location() { Name = "San Antonio Del Tequendama" },
                        new Location() { Name = "San Bernardo" },
                        new Location() { Name = "San Cayetano" },
                        new Location() { Name = "San Francisco" },
                        new Location() { Name = "San Juan De Rio Seco" },
                        new Location() { Name = "Sasaima" },
                        new Location() { Name = "Sesquilé" },
                        new Location() { Name = "Sibaté" },
                        new Location() { Name = "Silvania" },
                        new Location() { Name = "Simijaca" },
                        new Location() { Name = "Soacha" },
                        new Location() { Name = "Sopó" },
                        new Location() { Name = "Subachoque" },
                        new Location() { Name = "Suesca" },
                        new Location() { Name = "Supata" },
                        new Location() { Name = "Susa" },
                        new Location() { Name = "Sutatausa" },
                        new Location() { Name = "Tabio" },
                        new Location() { Name = "Tausa" },
                        new Location() { Name = "Tena" },
                        new Location() { Name = "Tenjo" },
                        new Location() { Name = "Tibacuy" },
                        new Location() { Name = "Tibirita" },
                        new Location() { Name = "Tocaima" },
                        new Location() { Name = "Tocancipá" },
                        new Location() { Name = "Topaipi" },
                        new Location() { Name = "Ubalá" },
                        new Location() { Name = "Ubaque" },
                        new Location() { Name = "Villa De San Diego De Ubate" },
                        new Location() { Name = "Une" },
                        new Location() { Name = "Útica" },
                        new Location() { Name = "Vergara" },
                        new Location() { Name = "Vianí" },
                        new Location() { Name = "Villagomez" },
                        new Location() { Name = "Villapinzón" },
                        new Location() { Name = "Villeta" },
                        new Location() { Name = "Viotá" },
                        new Location() { Name = "Yacopí" },
                        new Location() { Name = "Zipacon" },
                        new Location() { Name = "Zipaquirá" }
                    } },
                    new Location() { Name = "Choco", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Quibdó" },
                        new Location() { Name = "Acandí" },
                        new Location() { Name = "Alto Baudó" },
                        new Location() { Name = "Atrato" },
                        new Location() { Name = "Bagadó" },
                        new Location() { Name = "Bahía Solano" },
                        new Location() { Name = "Bajo Baudó" },
                        // TODO: agregar a la estructura del update (instalador)
                        new Location() { Name = "Belén De Bajirá" },
                        new Location() { Name = "Bojaya" },
                        new Location() { Name = "El Canton Del San Pablo" },
                        new Location() { Name = "Carmén Del Darién" },
                        new Location() { Name = "Certegui" },
                        new Location() { Name = "Condoto" },
                        new Location() { Name = "El Carmen De Atrato" },
                        new Location() { Name = "El Litoral Del San Juan" },
                        new Location() { Name = "Istmina" },
                        new Location() { Name = "Jurado" },
                        new Location() { Name = "Lloró" },
                        new Location() { Name = "Medio Atrato" },
                        new Location() { Name = "Medio Baudó" },
                        new Location() { Name = "Medio San Juan" },
                        new Location() { Name = "Nóvita" },
                        new Location() { Name = "Nuquí" },
                        new Location() { Name = "Río Fíro" },
                        new Location() { Name = "Rio Quito" },
                        new Location() { Name = "Riosucio" },
                        new Location() { Name = "San José Del Palmar" },
                        new Location() { Name = "Sipí" },
                        new Location() { Name = "Tadó" },
                        new Location() { Name = "Unguia" },
                        new Location() { Name = "Union Panamericana" }
                    } },
                    new Location() { Name = "Huila", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Neiva" },
                        new Location() { Name = "Acevedo" },
                        new Location() { Name = "Agrado" },
                        new Location() { Name = "Aipe" },
                        new Location() { Name = "Algeciras" },
                        new Location() { Name = "Altamira" },
                        new Location() { Name = "Baraya" },
                        new Location() { Name = "Campoalegre" },
                        new Location() { Name = "Colombia" },
                        new Location() { Name = "Elías" },
                        new Location() { Name = "Garzón" },
                        new Location() { Name = "Gigante" },
                        new Location() { Name = "Guadalupe" },
                        new Location() { Name = "Hobo" },
                        new Location() { Name = "Iquira" },
                        new Location() { Name = "Isnos" },
                        new Location() { Name = "La Argentina" },
                        new Location() { Name = "La Plata" },
                        new Location() { Name = "Nátaga" },
                        new Location() { Name = "Oporapa" },
                        new Location() { Name = "Paicol" },
                        new Location() { Name = "Palermo" },
                        new Location() { Name = "Palestina" },
                        new Location() { Name = "Pital" },
                        new Location() { Name = "Pitalito" },
                        new Location() { Name = "Rivera" },
                        new Location() { Name = "Saladoblanco" },
                        new Location() { Name = "San Agustín" },
                        new Location() { Name = "Santa María" },
                        new Location() { Name = "Suaza" },
                        new Location() { Name = "Tarqui" },
                        new Location() { Name = "Tesalia" },
                        new Location() { Name = "Tello" },
                        new Location() { Name = "Teruel" },
                        new Location() { Name = "Timaná" },
                        new Location() { Name = "Villavieja" },
                        new Location() { Name = "Yaguará" }
                    } },
                    new Location() { Name = "La Guajira", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Riohacha" },
                        new Location() { Name = "Albania" },
                        new Location() { Name = "Barrancas" },
                        new Location() { Name = "Dibulla" },
                        new Location() { Name = "Distraccion" },
                        new Location() { Name = "El Molino" },
                        new Location() { Name = "Fonseca" },
                        new Location() { Name = "Hatonuevo" },
                        new Location() { Name = "La Jagua Del Pilar" },
                        new Location() { Name = "Maicao" },
                        new Location() { Name = "Manaure" },
                        new Location() { Name = "San Juan Del Cesar" },
                        new Location() { Name = "Uribia" },
                        new Location() { Name = "Urumita" },
                        new Location() { Name = "Villanueva" }
                    } },
                    new Location() { Name = "Magdalena", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Santa Marta" },
                        new Location() { Name = "Algarrobo" },
                        new Location() { Name = "Aracataca" },
                        new Location() { Name = "Ariguaní" },
                        new Location() { Name = "Cerro San Antonio" },
                        new Location() { Name = "Chibolo" },
                        new Location() { Name = "Ciénaga" },
                        new Location() { Name = "Concordia" },
                        new Location() { Name = "El Banco" },
                        new Location() { Name = "El Piñon" },
                        new Location() { Name = "El Reten" },
                        new Location() { Name = "Fundacion" },
                        new Location() { Name = "Guamal" },
                        new Location() { Name = "Nueva Granada" },
                        new Location() { Name = "Pedraza" },
                        new Location() { Name = "Pijiño Del Carmen" },
                        new Location() { Name = "Pivijay" },
                        new Location() { Name = "Plato" },
                        new Location() { Name = "Puebloviejo" },
                        new Location() { Name = "Remolino" },
                        new Location() { Name = "Sabanas De San Angel" },
                        new Location() { Name = "Salamina" },
                        new Location() { Name = "San Sebastian De Buenavista" },
                        new Location() { Name = "San Zenon" },
                        new Location() { Name = "Santa Ana" },
                        new Location() { Name = "Santa Barbara De Pinto" },
                        new Location() { Name = "Sitionuevo" },
                        new Location() { Name = "Tenerife" },
                        new Location() { Name = "Zapayan" },
                        new Location() { Name = "Zona Bananera" }
                    } },
                    new Location() { Name = "Meta", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Villavicencio" },
                        new Location() { Name = "Acacias" },
                        new Location() { Name = "Barranca De Upia" },
                        new Location() { Name = "Cabuyaro" },
                        new Location() { Name = "Castilla La Nueva" },
                        new Location() { Name = "Cubarral" },
                        new Location() { Name = "Cumaral" },
                        new Location() { Name = "El Calvario" },
                        new Location() { Name = "El Castillo" },
                        new Location() { Name = "El Dorado" },
                        new Location() { Name = "Fuente De Oro" },
                        new Location() { Name = "Granada" },
                        new Location() { Name = "Guamal" },
                        new Location() { Name = "Mapiripan" },
                        new Location() { Name = "Mesetas" },
                        new Location() { Name = "La Macarena" },
                        new Location() { Name = "Uribe" },
                        new Location() { Name = "Lejanías" },
                        new Location() { Name = "Puerto Concordia" },
                        new Location() { Name = "Puerto Gaitán" },
                        new Location() { Name = "Puerto Lopez" },
                        new Location() { Name = "Puerto Lleras" },
                        new Location() { Name = "Puerto Rico" },
                        new Location() { Name = "Restrepo" },
                        new Location() { Name = "San Carlos De Guaroa" },
                        new Location() { Name = "San Juan De Arama" },
                        new Location() { Name = "San Juanito" },
                        new Location() { Name = "San Martín" },
                        new Location() { Name = "Vistahermosa" }
                    } },
                    new Location() { Name = "Nariño", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Pasto" },
                        new Location() { Name = "Alban" },
                        new Location() { Name = "Aldana" },
                        new Location() { Name = "Ancuya" },
                        new Location() { Name = "Arboleda" },
                        new Location() { Name = "Barbacoas" },
                        new Location() { Name = "Belen" },
                        new Location() { Name = "Buesaco" },
                        new Location() { Name = "Colon" },
                        new Location() { Name = "Consaca" },
                        new Location() { Name = "Contadero" },
                        new Location() { Name = "Córdoba" },
                        new Location() { Name = "Cuaspud" },
                        new Location() { Name = "Cumbal" },
                        new Location() { Name = "Cumbitara" },
                        new Location() { Name = "Chachagsi" },
                        new Location() { Name = "El Charco" },
                        new Location() { Name = "El Peñol" },
                        new Location() { Name = "El Rosario" },
                        new Location() { Name = "El Tablon De Gomez" },
                        new Location() { Name = "El Tambo" },
                        new Location() { Name = "Funes" },
                        new Location() { Name = "Guachucal" },
                        new Location() { Name = "Guaitarilla" },
                        new Location() { Name = "Gualmatan" },
                        new Location() { Name = "Iles" },
                        new Location() { Name = "Imues" },
                        new Location() { Name = "Ipiales" },
                        new Location() { Name = "La Cruz" },
                        new Location() { Name = "La Florida" },
                        new Location() { Name = "La Llanada" },
                        new Location() { Name = "La Tola" },
                        new Location() { Name = "La Union" },
                        new Location() { Name = "Leiva" },
                        new Location() { Name = "Linares" },
                        new Location() { Name = "Los Andes" },
                        new Location() { Name = "Magsi" },
                        new Location() { Name = "Mallama" },
                        new Location() { Name = "Mosquera" },
                        new Location() { Name = "Nariño" },
                        new Location() { Name = "Olaya Herrera" },
                        new Location() { Name = "Ospina" },
                        new Location() { Name = "Francisco Pizarro" },
                        new Location() { Name = "Policarpa" },
                        new Location() { Name = "Potosí" },
                        new Location() { Name = "Providencia" },
                        new Location() { Name = "Puerres" },
                        new Location() { Name = "Pupiales" },
                        new Location() { Name = "Ricaurte" },
                        new Location() { Name = "Roberto Payan" },
                        new Location() { Name = "Samaniego" },
                        new Location() { Name = "Sandoná" },
                        new Location() { Name = "San Bernardo" },
                        new Location() { Name = "San Lorenzo" },
                        new Location() { Name = "San Pablo" },
                        new Location() { Name = "San Pedro De Cartago" },
                        new Location() { Name = "Santa Barbara" },
                        new Location() { Name = "Santacruz" },
                        new Location() { Name = "Sapuyes" },
                        new Location() { Name = "Taminango" },
                        new Location() { Name = "Tangua" },
                        new Location() { Name = "San Andres De Tumaco" },
                        new Location() { Name = "Tuquerres" },
                        new Location() { Name = "Yacuanquer" }
                    } },
                    new Location() { Name = "N. De Santander", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Cúcuta" },
                        new Location() { Name = "Abrego" },
                        new Location() { Name = "Arboledas" },
                        new Location() { Name = "Bochalema" },
                        new Location() { Name = "Bucarasica" },
                        new Location() { Name = "Cácota" },
                        new Location() { Name = "Cachirá" },
                        new Location() { Name = "Chinácota" },
                        new Location() { Name = "Chitagá" },
                        new Location() { Name = "Convención" },
                        new Location() { Name = "Cucutilla" },
                        new Location() { Name = "Durania" },
                        new Location() { Name = "El Carmen" },
                        new Location() { Name = "El Tarra" },
                        new Location() { Name = "El Zulia" },
                        new Location() { Name = "Gramalote" },
                        new Location() { Name = "Hacarí" },
                        new Location() { Name = "Herrán" },
                        new Location() { Name = "Labateca" },
                        new Location() { Name = "La Esperanza" },
                        new Location() { Name = "La Playa" },
                        new Location() { Name = "Los Patios" },
                        new Location() { Name = "Lourdes" },
                        new Location() { Name = "Mutiscua" },
                        new Location() { Name = "Ocaña" },
                        new Location() { Name = "Pamplona" },
                        new Location() { Name = "Pamplonita" },
                        new Location() { Name = "Puerto Santander" },
                        new Location() { Name = "Ragonvalia" },
                        new Location() { Name = "Salazar" },
                        new Location() { Name = "San Calixto" },
                        new Location() { Name = "San Cayetano" },
                        new Location() { Name = "Santiago" },
                        new Location() { Name = "Sardinata" },
                        new Location() { Name = "Silos" },
                        new Location() { Name = "Teorama" },
                        new Location() { Name = "Tibú" },
                        new Location() { Name = "Toledo" },
                        new Location() { Name = "Villa Caro" },
                        new Location() { Name = "Villa Del Rosario" }
                    } },
                    new Location() { Name = "Quindio", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Armenia" },
                        new Location() { Name = "Buenavista" },
                        new Location() { Name = "Calarca" },
                        new Location() { Name = "Circasia" },
                        new Location() { Name = "Cordoba" },
                        new Location() { Name = "Filandia" },
                        new Location() { Name = "Genova" },
                        new Location() { Name = "La Tebaida" },
                        new Location() { Name = "Montenegro" },
                        new Location() { Name = "Pijao" },
                        new Location() { Name = "Quimbaya" },
                        new Location() { Name = "Salento" }
                    } },
                    new Location() { Name = "Risaralda", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Pereira" },
                        new Location() { Name = "Apía" },
                        new Location() { Name = "Balboa" },
                        new Location() { Name = "Belén De Umbría" },
                        new Location() { Name = "Dosquebradas" },
                        new Location() { Name = "Guática" },
                        new Location() { Name = "La Celia" },
                        new Location() { Name = "La Virginia" },
                        new Location() { Name = "Marsella" },
                        new Location() { Name = "Mistrató" },
                        new Location() { Name = "Pueblo Rico" },
                        new Location() { Name = "Quinchia" },
                        new Location() { Name = "Santa Rosa De Cabal" },
                        new Location() { Name = "Santuario" }
                    } },
                    new Location() { Name = "Santander", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Bucaramanga" },
                        new Location() { Name = "Aguada" },
                        new Location() { Name = "Albania" },
                        new Location() { Name = "Aratoca" },
                        new Location() { Name = "Barbosa" },
                        new Location() { Name = "Barichara" },
                        new Location() { Name = "Barrancabermeja" },
                        new Location() { Name = "Betulia" },
                        new Location() { Name = "Bolívar" },
                        new Location() { Name = "Cabrera" },
                        new Location() { Name = "California" },
                        new Location() { Name = "Capitanejo" },
                        new Location() { Name = "Carcasí" },
                        new Location() { Name = "Cepitá" },
                        new Location() { Name = "Cerrito" },
                        new Location() { Name = "Charalá" },
                        new Location() { Name = "Charta" },
                        new Location() { Name = "Chima" },
                        new Location() { Name = "Chipatá" },
                        new Location() { Name = "Cimitarra" },
                        new Location() { Name = "Concepción" },
                        new Location() { Name = "Confines" },
                        new Location() { Name = "Contratación" },
                        new Location() { Name = "Coromoro" },
                        new Location() { Name = "Curití" },
                        new Location() { Name = "El Carmen De Chucurí" },
                        new Location() { Name = "El Guacamayo" },
                        new Location() { Name = "El Peñón" },
                        new Location() { Name = "El Playón" },
                        new Location() { Name = "Encino" },
                        new Location() { Name = "Enciso" },
                        new Location() { Name = "Florián" },
                        new Location() { Name = "Floridablanca" },
                        new Location() { Name = "Galán" },
                        new Location() { Name = "Gambita" },
                        new Location() { Name = "Girón" },
                        new Location() { Name = "Guaca" },
                        new Location() { Name = "Guadalupe" },
                        new Location() { Name = "Guapotá" },
                        new Location() { Name = "Guavatá" },
                        new Location() { Name = "Guepsa" },
                        new Location() { Name = "Hato" },
                        new Location() { Name = "Jesús María" },
                        new Location() { Name = "Jordán" },
                        new Location() { Name = "La Belleza" },
                        new Location() { Name = "Landázuri" },
                        new Location() { Name = "La Paz" },
                        new Location() { Name = "Lebríja" },
                        new Location() { Name = "Los Santos" },
                        new Location() { Name = "Macaravita" },
                        new Location() { Name = "Málaga" },
                        new Location() { Name = "Matanza" },
                        new Location() { Name = "Mogotes" },
                        new Location() { Name = "Molagavita" },
                        new Location() { Name = "Ocamonte" },
                        new Location() { Name = "Oiba" },
                        new Location() { Name = "Onzaga" },
                        new Location() { Name = "Palmar" },
                        new Location() { Name = "Palmas Del Socorro" },
                        new Location() { Name = "Páramo" },
                        new Location() { Name = "Piedecuesta" },
                        new Location() { Name = "Pinchote" },
                        new Location() { Name = "Puente Nacional" },
                        new Location() { Name = "Puerto Parra" },
                        new Location() { Name = "Puerto Wilches" },
                        new Location() { Name = "Rionegro" },
                        new Location() { Name = "Sabana De Torres" },
                        new Location() { Name = "San Andrés" },
                        new Location() { Name = "San Benito" },
                        new Location() { Name = "San Gil" },
                        new Location() { Name = "San Joaquín" },
                        new Location() { Name = "San José De Miranda" },
                        new Location() { Name = "San Miguel" },
                        new Location() { Name = "San Vicente De Chucurí" },
                        new Location() { Name = "Santa Bárbara" },
                        new Location() { Name = "Santa Helena Del Opón" },
                        new Location() { Name = "Simacota" },
                        new Location() { Name = "Socorro" },
                        new Location() { Name = "Suaita" },
                        new Location() { Name = "Sucre" },
                        new Location() { Name = "Surata" },
                        new Location() { Name = "Tona" },
                        new Location() { Name = "Valle De San José" },
                        new Location() { Name = "Vélez" },
                        new Location() { Name = "Vetas" },
                        new Location() { Name = "Villanueva" },
                        new Location() { Name = "Zapatoca" }
                    } },
                    new Location() { Name = "Sucre", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Sincelejo" },
                        new Location() { Name = "Buenavista" },
                        new Location() { Name = "Caimito" },
                        new Location() { Name = "Coloso" },
                        new Location() { Name = "Corozal" },
                        new Location() { Name = "Coveñas" },
                        new Location() { Name = "Chalán" },
                        new Location() { Name = "El Roble" },
                        new Location() { Name = "Galeras" },
                        new Location() { Name = "Guaranda" },
                        new Location() { Name = "La Unión" },
                        new Location() { Name = "Los Palmitos" },
                        new Location() { Name = "Majagual" },
                        new Location() { Name = "Morroa" },
                        new Location() { Name = "Ovejas" },
                        new Location() { Name = "Palmito" },
                        new Location() { Name = "Sampués" },
                        new Location() { Name = "San Benito Abad" },
                        new Location() { Name = "San Juan De Betulia" },
                        new Location() { Name = "San Marcos" },
                        new Location() { Name = "San Onofre" },
                        new Location() { Name = "San Pedro" },
                        new Location() { Name = "San Luis De Sincé" },
                        new Location() { Name = "Sucre" },
                        new Location() { Name = "Santiago De Tolú" },
                        new Location() { Name = "Tolú Viejo" }
                    } },
                    new Location() { Name = "Tolima", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Ibague" },
                        new Location() { Name = "Alpujarra" },
                        new Location() { Name = "Alvarado" },
                        new Location() { Name = "Ambalema" },
                        new Location() { Name = "Anzoátegui" },
                        new Location() { Name = "Armero" },
                        new Location() { Name = "Ataco" },
                        new Location() { Name = "Cajamarca" },
                        new Location() { Name = "Carmen De Apicalá" },
                        new Location() { Name = "Casabianca" },
                        new Location() { Name = "Chaparral" },
                        new Location() { Name = "Coello" },
                        new Location() { Name = "Coyaima" },
                        new Location() { Name = "Cunday" },
                        new Location() { Name = "Dolores" },
                        new Location() { Name = "Espinal" },
                        new Location() { Name = "Falan" },
                        new Location() { Name = "Flandes" },
                        new Location() { Name = "Fresno" },
                        new Location() { Name = "Guamo" },
                        new Location() { Name = "Herveo" },
                        new Location() { Name = "Honda" },
                        new Location() { Name = "Icononzo" },
                        new Location() { Name = "Lerida" },
                        new Location() { Name = "Libano" },
                        new Location() { Name = "Mariquita" },
                        new Location() { Name = "Melgar" },
                        new Location() { Name = "Murillo" },
                        new Location() { Name = "Natagaima" },
                        new Location() { Name = "Ortega" },
                        new Location() { Name = "Palocabildo" },
                        new Location() { Name = "Piedras" },
                        new Location() { Name = "Planadas" },
                        new Location() { Name = "Prado" },
                        new Location() { Name = "Purificación" },
                        new Location() { Name = "Rioblanco" },
                        new Location() { Name = "Roncesvalles" },
                        new Location() { Name = "Rovira" },
                        new Location() { Name = "Saldaña" },
                        new Location() { Name = "San Antonio" },
                        new Location() { Name = "San Luis" },
                        new Location() { Name = "Santa Isabel" },
                        new Location() { Name = "Suárez" },
                        new Location() { Name = "Valle De San Juan" },
                        new Location() { Name = "Venadillo" },
                        new Location() { Name = "Villahermosa" },
                        new Location() { Name = "Villarrica" }
                    } },
                    new Location() { Name = "Valle Del Cauca", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Cali" },
                        new Location() { Name = "Alcala" },
                        new Location() { Name = "Andalucía" },
                        new Location() { Name = "Ansermanuevo" },
                        new Location() { Name = "Argelia" },
                        new Location() { Name = "Bolívar" },
                        new Location() { Name = "Buenaventura" },
                        new Location() { Name = "Guadalajara De Buga" },
                        new Location() { Name = "Bugalagrande" },
                        new Location() { Name = "Caicedonia" },
                        new Location() { Name = "Calima" },
                        new Location() { Name = "Candelaria" },
                        new Location() { Name = "Cartago" },
                        new Location() { Name = "Dagua" },
                        new Location() { Name = "El Águila" },
                        new Location() { Name = "El Cairo" },
                        new Location() { Name = "El Cerrito" },
                        new Location() { Name = "El Dovio" },
                        new Location() { Name = "Florida" },
                        new Location() { Name = "Ginebra" },
                        new Location() { Name = "Guacarí" },
                        new Location() { Name = "Jamundí" },
                        new Location() { Name = "La Cumbre" },
                        new Location() { Name = "La Union" },
                        new Location() { Name = "La Victoria" },
                        new Location() { Name = "Obando" },
                        new Location() { Name = "Palmira" },
                        new Location() { Name = "Pradera" },
                        new Location() { Name = "Restrepo" },
                        new Location() { Name = "Riofrio" },
                        new Location() { Name = "Roldanillo" },
                        new Location() { Name = "San Pedro" },
                        new Location() { Name = "Sevilla" },
                        new Location() { Name = "Toro" },
                        new Location() { Name = "Trujillo" },
                        new Location() { Name = "Tuluá" },
                        new Location() { Name = "Ulloa" },
                        new Location() { Name = "Versalles" },
                        new Location() { Name = "Vijes" },
                        new Location() { Name = "Yotoco" },
                        new Location() { Name = "Yumbo" },
                        new Location() { Name = "Zarzal" }
                    } },
                    new Location() { Name = "Arauca", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Arauca" },
                        new Location() { Name = "Arauquita" },
                        new Location() { Name = "Cravo Norte" },
                        new Location() { Name = "Fortul" },
                        new Location() { Name = "Puerto Rondón" },
                        new Location() { Name = "Saravena" },
                        new Location() { Name = "Tame" }
                    } },
                    new Location() { Name = "Casanare", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Yopal" },
                        new Location() { Name = "Aguazul" },
                        new Location() { Name = "Chameza" },
                        new Location() { Name = "Hato Corozal" },
                        new Location() { Name = "La Salina" },
                        new Location() { Name = "Maní" },
                        new Location() { Name = "Monterrey" },
                        new Location() { Name = "Nunchia" },
                        new Location() { Name = "Orocué" },
                        new Location() { Name = "Paz De Ariporo" },
                        new Location() { Name = "Pore" },
                        new Location() { Name = "Recetor" },
                        new Location() { Name = "Sabanalarga" },
                        new Location() { Name = "Sácama" },
                        new Location() { Name = "San Luis De Palenque" },
                        new Location() { Name = "Támara" },
                        new Location() { Name = "Tauramena" },
                        new Location() { Name = "Trinidad" },
                        new Location() { Name = "Villanueva" }
                    } },
                    new Location() { Name = "Putumayo", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Mocoa" },
                        new Location() { Name = "Colón" },
                        new Location() { Name = "Orito" },
                        new Location() { Name = "Puerto Asis" },
                        new Location() { Name = "Puerto Caicedo" },
                        new Location() { Name = "Puerto Guzman" },
                        new Location() { Name = "Leguizamo" },
                        new Location() { Name = "Sibundoy" },
                        new Location() { Name = "San Francisco" },
                        new Location() { Name = "San Miguel" },
                        new Location() { Name = "Santiago" },
                        new Location() { Name = "Valle Del Guamuez" },
                        new Location() { Name = "Villagarzon" }
                    } },
                    new Location() { Name = "San Andres", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "San Andres" },
                        new Location() { Name = "Providencia" }
                    } },
                    new Location() { Name = "Amazonas", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Leticia" },
                        new Location() { Name = "El Encanto" },
                        new Location() { Name = "La Chorrera" },
                        new Location() { Name = "La Pedrera" },
                        new Location() { Name = "La Victoria" },
                        new Location() { Name = "Miriti - Paraná" },
                        new Location() { Name = "Puerto Alegria" },
                        new Location() { Name = "Puerto Arica" },
                        new Location() { Name = "Puerto Nariño" },
                        new Location() { Name = "Puerto Santander" },
                        new Location() { Name = "Tarapacá" }
                    } },
                    new Location() { Name = "Guainia", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Inírida" },
                        new Location() { Name = "Barranco Minas" },
                        new Location() { Name = "Mapiripana" },
                        new Location() { Name = "San Felipe" },
                        new Location() { Name = "Puerto Colombia" },
                        new Location() { Name = "La Guadalupe" },
                        new Location() { Name = "Cacahual" },
                        new Location() { Name = "Pana Pana" },
                        new Location() { Name = "Morichal" }
                    } },
                    new Location() { Name = "Guaviare", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "San José Del Guaviare" },
                        new Location() { Name = "Calamar" },
                        new Location() { Name = "El Retorno" },
                        new Location() { Name = "Miraflores" }
                    } },
                    new Location() { Name = "Vaupes", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Mitú" },
                        new Location() { Name = "Caruru" },
                        new Location() { Name = "Pacoa" },
                        new Location() { Name = "Taraira" },
                        new Location() { Name = "Papunaua" },
                        new Location() { Name = "Yavaraté" }
                    } },
                    new Location() { Name = "Vichada", ChildrenLocations = new Location[]
                    {
                        new Location() { Name = "Puerto Carreño" },
                        new Location() { Name = "La Primavera" },
                        new Location() { Name = "Santa Rosalia" },
                        new Location() { Name = "Cumaribo" }
                    } }
                } }
            };

            foreach (var item in locations)
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

            list.Add(new SystemSetting() { Name = "GeneralSettings.BannerPictureSizeWidth", Value = "1500" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.BannerPictureSizeHeight", Value = "1159" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.FacebookPublicToken", Value = "" });
            list.Add(new SystemSetting() { Name = "GeneralSettings.FacebookSecretToken", Value = "" });

            list.Add(new SystemSetting() { Name = "GeneralSettings.SeoImage", Value = "/img/front/porque-adoptar-una-mascota.png" });

            list.Add(new SystemSetting() { Name = "SecuritySettings.MaxRequestFileUploadMB", Value = "5" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.TakeEmailsToSend", Value = "30" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.MaxAttemtpsToSendEmail", Value = "5" });

            list.Add(new SystemSetting() { Name = "NotificationSettings.EmailSenderEmail", Value = "elemail@elemail.com" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpUser", Value = "user@smtp.com" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpPassword", Value = "password@smtp.com" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpPort", Value = "587" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpUseSsl", Value = "True" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SendEmailEnabled", Value = "False" });
            list.Add(new SystemSetting() { Name = "TaskSettings.SendEmailsInterval", Value = "30" });
            list.Add(new SystemSetting() { Name = "NotificationSettings.SmtpHost", Value = "elhostemail" });

            foreach (var item in list)
            {
                if (!context.SystemSettings.Any(c => c.Name.Equals(item.Name)))
                {
                    context.SystemSettings.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion Settings

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

            list.Add(new TextResource() { Name = "Home.HowTo.Help.Title", Value = "COMO AYUDAR", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Home.HowTo.Help.Content", Value = "Cada dia se ven miles de animales abandonados en la calle que no tienen quién se responsabilice por ellos. Maneras de ayudar hay muchas, entre ellas: Puedes ser voluntario, ser hogar temporar o apadrinar uno de estos animalitos en algún albergue.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Home.HowTo.Parent.Title", Value = "APADRINAR", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Home.HowTo.Parent.Content", Value = "Si no puedes adoptar y deseas ayudar a un animal lo puedes apadrinar. Consiste en ofrecer un aporte voluntario mensual para su sustento hasta el día en que sea adoptado. Con esto, el animal que escojas, ya sea de la calle o de alguna fundación existente tendrá una buena calidad de vida mientras encuentra un verdadero hogar.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Home.HowTo.Adopt.Title", Value = "ADOPTAR", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Home.HowTo.Adopt.Content", Value = "No compres, ADOPTA. Adoptando salvas la vida de dos animales, la de tu nuevo amigo y la del que ocupará su lugar, tan solo escoge la mascota de la cual quieres recibir todo su amor y compañia, diligencia el formulario y en poco tiempo nos comunicaremos contigo.", Language = LanguageEnum.Spanish });

            list.Add(new TextResource() { Name = "Home.Who.Title", Value = "QUIENES SOMOS", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Home.Who.Content", Value = "<p>Huellitas sin hogar es el esfuerzo de un grupo de amigos por ayudar a los miles de animales que se encuentran desprotegidos y en las calles colombianas. Uno de los principales objetivos es por medio de la pagina promover la adopción y apadrinamiento de mascotas en las fundaciones que se dedican al cuidado de estos animales.</p><p>No contamos con sede física ya que solo somos un medio de conexión entre las personas que buscan una mascota y los hogares de paso. Además de esto no recibimos , ni pedimos ningún tipo de ayuda económica, ya que es ayudar a los animales lo que nos mueve.</p><p>Está es la primera versión de la pagina y esperamos poder seguir evolucionando para mejorar en algo esta problematica tan grande de nuestro pais.</p><p>Por último, el equipo de huellitas sin hogar el cual ha donado su tiempo y conocimiento para la realización de este proyecto es el siguiente:</p>", Language = LanguageEnum.Spanish });

            list.Add(new TextResource() { Name = "Seo.Home.Title", Value = "Huellitas sin hogar: Adopta una mascota y dale amor toda la vida en Bogotá y Colombia. Perros y gatos.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Home.Description", Value = "Huellitas sin Hogar es un sitio web sin animo de lucro en el cual puedes buscar una mascota y adoptarla en Bogotá y el resto de Colombia completamente gratis. Hay desde cachorros hasta perros y gatos adultos", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.EditPet.Title", Value = "Ingresa los datos de la mascota que deseas dar en adopción", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.EditPet.Description", Value = "En Huellitas sin hogar deseamos que todos los perros y gatos tengan un hogar donde séan valorados. Ingresa los datos de la mascota que deseas dar en adopción. Es importante que estés en Colombia.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Adopt.Title", Value = "Empieza el proceso de adopción de {petName} en {petLocation}", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Adopt.Description", Value = "Empieza el proceso de adopción de {petName} en {petLocation} llenando el formulario de adopción. Despues que lo llenes debes esperar nuestra respuesta.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.AdoptTerms.Title", Value = "Empezar proceso de adopción de mascota en Colombia con Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.AdoptTerms.Description", Value = "Para empezar el proceso de adopción de un mascota con Huellitas sin hogar te pedimos que sigas estas recomendaciones muy importantes.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.PetDetail.Title", Value = "{petName} en adopción. Adopta {petSubtype} con Huellitas sin hogar en {petLocation}", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Pets.Title", Value = "Buscar mascotas y empezar proceso de adopción de mascota en Colombia con Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Pets.Description", Value = "Adopta una mascota. Encuentra el perro o gato que se convertirá en el mejor amigo los próximos años de tu vida. Huellitas sin hogar cuenta con una busqueda avanzada de mascotas, empieza ya.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.EditShelter.Title", Value = "Registra tu fundación o refugio de adopción de perros y gatos en Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.EditShelter.Description", Value = "Si no tienes página web de tu fundación de adopción de perros y gatos puedes realizar difusión de tus mascotas en Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.ShelterDetail.Title", Value = "Anímate y contacta a la fundación {shelterName} en {shelterLocation} y adopta un perro o gato", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.LostPets.Title", Value = "Publica y encuentra perros y gatos perdidos en Bogotá y el resto de Colombia", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.LostPets.Description", Value = "Si tu perro o gato está perdido o encontraste alguno por la calle publicalo en Huellitas sin hogar y encuentra a su dueño para que vuelvan a ser felices.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.EditLostPet.Title", Value = "Publica perros y gatos perdidos, en Huellitas sin hogar te ayudamos a encontrarlos", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.EditLostPet.Description", Value = "Si tu perro o gato está perdido publicalo en Huellitas sin hogar y entre la comunidad podemos llegar a encontrarlo. Principalmente en Bogotá y el resto de Colombia.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.LostPetDetail.Title", Value = "{petSubtype} perdido en {petLocation}, si conoces a {petName} ayudanos a conseguirle su hogar.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Shelters.Title", Value = "Fundaciones y refugios para adopción de perros y gatos en Bogotá y Colombia", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Shelters.Description", Value = "En huellitas sin hogar puedes encontrar fundaciones y refugios para adopción de perros y gatos en Bogotá y Colombia. Acercate a la que te quede mejor.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.NewPet.Title", Value = "Publicar mascota para dar en adopción, en Huellitas sin hogar hacemos la difusión", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.NewPet.Description", Value = "Si tienes una mascota para dar en adopción ya sea perro o gato, llena el formulario y espera nuestra confirmación. Si tienes una fundación también la puedes publicar en Huellitas sin hogar.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.AdoptionFormDetail.Title", Value = "Información del formulario de adopción", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.AdoptionFormDetail.Description", Value = "Información relacionada con el formulario de adopción recibido de tu mascota", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.AdoptionForms.Title", Value = "Formularios de adopción recibidos de mis mascotas", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.AdoptionForms.Description", Value = "Listado de formularios de adopción recibidos de mis mascotas. Mira el detalle de cada uno.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.MyPets.Title", Value = "Mis mascotas - Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.MyPets.Description", Value = "Listado de las mascotas creadas por mi.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.MyNotifications.Title", Value = "Mis notificaciones - Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.MyNotifications.Description", Value = "Listado de mis notificaciones.", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Faq.Title", Value = "¿Por qué adoptar una mascota es mejor que comprarla? - Huellitas sin hogar", Language = LanguageEnum.Spanish });
            list.Add(new TextResource() { Name = "Seo.Faq.Description", Value = "Adoptar una mascota es mejor que comprarla ya que te permite darle hogar a un perro o gato que sufrirá de las inclemencias de la calle, el clima y demás. Conoce más.", Language = LanguageEnum.Spanish });
            

            foreach (var item in list)
            {
                if (!context.TextResources.Any(c => c.Name.Equals(item.Name)))
                {
                    context.TextResources.Add(item);
                }
            }

            context.SaveChanges();
        }

        #endregion REsources
    }
}