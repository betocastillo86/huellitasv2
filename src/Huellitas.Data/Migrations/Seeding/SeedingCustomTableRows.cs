﻿//-----------------------------------------------------------------------
// <copyright file="SeedingCustomTableRows.cs" company="Gabriel Castillo">
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
    /// Seeding Custom Table Rows
    /// </summary>
    public static class SeedingCustomTableRows
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HuellitasContext context)
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

            foreach (var item in list)
            {
                if (!context.CustomTableRows.Any(c => c.Value.Equals(item.Value) && c.CustomTableId.Equals(item.CustomTableId)))
                {
                    context.CustomTableRows.Add(item);
                }
            }

            context.SaveChanges();

            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Porque razón quieres adoptar un animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True|No entregues animales a personas que lo quieren cómo regalos para niños, o que sean para otra persona diferente a la que lo solicita en adopción", DisplayOrder = 1 });
            var morePets = new CustomTableRow() { CustomTableId = 4, Value = "¿Tienes otras mascotas actualmente?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True|Si tu perro o gato no se lleva bien con otros animales, ten en cuenta esto.", DisplayOrder = 2 };
            list.Add(morePets);
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Que tipo de animal y que cantidad de ellos?", ParentCustomTableRow = morePets, AdditionalInfo = $"{AdoptionFormQuestionType.ChecksWithText}|Perro,Gato,Otro|True|", DisplayOrder = 3 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿En qué lugar de la casa dormirá el animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True|No entregues animales que duerman en terrazas o patios.", DisplayOrder = 4 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Quién es la persona que autorizará la adopción del animal?", AdditionalInfo = $"{AdoptionFormQuestionType.SingleWithOther}|Soy Responsable,Madre,Padre|True|No entregues a personas menores de 23 años que llenen el formulario y se hagan ellos mismos responsables.", DisplayOrder = 5 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Cuánto tiempo al día permanecerá solo el animal?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Nunca,2 a 4 horas,4 a 6 horas,Más de 6 horas|True|No es recomendable que el perro se quede más de 6 horas solo", DisplayOrder = 6 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué tipo de vivienda tienes?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Apartamento,Casa,Finca,Bodega|True|Si tu perro es muy grande no es recomendable entregarlo a un apartamento", DisplayOrder = 7 });
            var previousPets = new CustomTableRow() { CustomTableId = 4, Value = "¿Has tenido animales de compañía anteriormente?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True|Si ha tenido mascotas por mucho tiempo es un gran punto a favor", DisplayOrder = 8 };
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "Cuentanos un poco de tu anterior mascota", ParentCustomTableRow = previousPets, AdditionalInfo = $"{AdoptionFormQuestionType.OptionsWithText}|Especie,Cuanto tiempo vivió contigo?,¿En dónde está el animal?|True|Si la anterior mascota fue regalada a otra familia NO apruebes el formulario.", DisplayOrder = 9 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué sucedería si alguien en la familia resulta alérgico a los pelos de los animales o si alguien desea tener un hijo?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True|", DisplayOrder = 10 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Qué sucedería si tuvieras que mudarte a otra casa o ciudad /país?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True|", DisplayOrder = 11 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas los gastos que genera tener un animal de compañía?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True|", DisplayOrder = 12 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas que adoptar un animal es una responsabilidad de 14 a 17 años?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True|", DisplayOrder = 13 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Estás consciente y aceptas que el animal necesita un periodo de ajuste en el que aprenda dónde debe ir al baño y se adapte a la familia?", AdditionalInfo = $"{AdoptionFormQuestionType.Boolean}||True|", DisplayOrder = 14 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Vives en casa propia o arriendo?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Casa propia,Arriendo|True|Preguntale si el arrendador ya le autorizó tener mascotas en su casa. Ten en cuenta que la gente que vive en arriendo y se cambia de lugar en muchas ocasiones el arrendador no le permite tener el animal. Esto genera abandono.", DisplayOrder = 15 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿Cuánto crees que son los gastos mensuales para mantener bien al animal que deseas adoptar?", AdditionalInfo = $"{AdoptionFormQuestionType.Single}|Hasta $20.000,Entre $20.000 y $50.000,Entre $50.000 y $80.000,Más de 80.000 pesos|True|Con menos de $50.000 mensuales es muy complicado mantener una mascota. Esto puede denotar desconocimiento en la tenencia de los animales.", DisplayOrder = 16 });
            list.Add(new CustomTableRow() { CustomTableId = 4, Value = "¿A qué te dedicas?", AdditionalInfo = $"{AdoptionFormQuestionType.Text}||True|Valida que la persona puede encargarse económicamente del animalito", DisplayOrder = 15 });

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
    }
}