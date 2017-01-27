//-----------------------------------------------------------------------
// <copyright file="AdoptionFormQuestionType.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities
{
    /// <summary>
    /// Adoption Form Question Type
    /// </summary>
    public enum AdoptionFormQuestionType
    {
        /// <summary>
        /// The single <![CDATA[Selección unica con multiples opciones]]>
        /// </summary>
        Single,

        /// <summary>
        /// The multiple <![CDATA[Multiples opciones]]>
        /// </summary>
        Multiple,

        /// <summary>
        /// The multiple with text <![CDATA[Multiple opción con texto para OTRO]]>
        /// </summary>
        MultipleWithOther,

        /// <summary>
        /// The text <![CDATA[Texto abierto]]>
        /// </summary>
        Text,

        /// <summary>
        /// The boolean <![CDATA[Si o no]]>
        /// </summary>
        Boolean,

        /// <summary>
        /// The options with text <![CDATA[Varias opciones para ingresar texto]]>
        /// </summary>
        OptionsWithText,

        /// <summary>
        /// The checks with text <![CDATA[Varias opciones con checkbox cada una con texto]]>
        /// </summary>
        ChecksWithText,

        /// <summary>
        /// The single with other
        /// </summary>
        SingleWithOther
    }
}