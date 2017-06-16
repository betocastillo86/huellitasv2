﻿namespace Huellitas.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    public static class SeedingNotifications
    {
        public static void Seed(HuellitasContext context)
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

            list.Add(new Notification() { Id = Convert.ToInt32(NotificationType.OutDatedPet), Name = "Publicación de Mascota ha vencido", Active = true, IsEmail = true, IsSystem = true, SystemText = "La publicación de tu mascota %%Pet.Name%% se ha vencido", Tags = "[{ \"key\":\"%%NotifiedUser.Name%%\",\"value\":\"Nombre del usuario\" },{ \"key\":\"%%NotifiedUser.Email%%\",\"value\":\"Correo del usuario notificado\" },{\"key\":\"%%Url%%\" , \"value\": \"Link principal\"},{\"key\":\"%%Pet.Name%%\", \"value\":\"Nombre de la mascota\"},{\"key\":\"%%Pet.Url%%\", \"value\":\"Link de la mascota\"},{\"key\":\"%%Pet.CreationDate%%\", \"value\":\"Fecha de publicación mascota\"}]", EmailSubject = "La publicacion de tu mascota %%Pet.Name%% ha caducado", EmailHtml = "La publicacion de tu mascota %%Pet.Name%% ha caducado" });

            foreach (var item in list)
            {
                if (!context.Notifications.Any(c => c.Id.Equals(item.Id)))
                {
                    context.Notifications.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}