using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Data.Entities
{
    public enum ContentAttributeType
    {
        [Description("Subtipo al que pertenece el contenido")]
        Subtype,
        [Description("Genero del animal")]
        Genre,
        [Description("Edad del animal")]
        Age,
        [Description("Tamaño del animal")]
        Size,
        [Description("Fundaciones asociadas")]
        Shelter
    }
}
