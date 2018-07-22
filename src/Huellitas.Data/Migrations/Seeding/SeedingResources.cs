//-----------------------------------------------------------------------
// <copyright file="SeedingResources.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;
    using Huellitas.Data.Core;
    using Huellitas.Data.Entities;

    /// <summary>
    /// seeding resources
    /// </summary>
    public static class SeedingResources
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HuellitasContext context)
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
            list.Add(new TextResource() { Name = "Seo.Pets.Description", Value = "Adopta una mascota. Encuentra el perro o gato que se convertirá en el mejor amigo los próximos años de tu vida. Huellitas sin hogar cuenta con una búsqueda avanzada de mascotas, empieza ya.", Language = LanguageEnum.Spanish });
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
    }
}