//-----------------------------------------------------------------------
// <copyright file="NotificationType.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Data.Entities.Enums
{
    /// <summary>
    /// Notification Type
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// The manual notification.
        /// </summary>
        Manual = 0,

        /// <summary>
        /// The sign up <![CDATA[Registro de usuario]]>
        /// </summary>
        SignUp = 1,

        /// <summary>
        /// The created pet confirmation
        /// </summary>
        CreatedPetConfirmation = 2,

        /// <summary>
        /// The pet was approved
        /// </summary>
        PetApproved = 3,

        /// <summary>
        /// The adoption form confirmation
        /// </summary>
        AdoptionFormConfirmation = 4,

        /// <summary>
        /// The adoption form received
        /// </summary>
        AdoptionFormReceived = 5,

        /// <summary>
        /// The adoption form approved
        /// </summary>
        AdoptionFormApproved = 6,

        /// <summary>
        /// The adoption form rejected
        /// </summary>
        AdoptionFormRejected = 7,

        /// <summary>
        /// The adoption form already adopted
        /// </summary>
        AdoptionFormAlreadyAdopted = 8,

        /// <summary>
        /// The shelter request received
        /// </summary>
        ShelterRequestReceived = 9,

        /// <summary>
        /// The shelter request rejected
        /// </summary>
        ShelterRequestRejected = 10,

        /// <summary>
        /// The new shelter request
        /// </summary>
        NewShelterRequest = 11,

        /// <summary>
        /// The shelter approved
        /// </summary>
        ShelterApproved = 12,

        /// <summary>
        /// When The adoption form is shared to another existent user
        /// </summary>
        AdoptionFormShared = 13,

        /// <summary>
        /// The adoption form send
        /// </summary>
        AdoptionFormSentToOtherUser = 14,

        /// <summary>
        /// The user added to shelter
        /// </summary>
        UserAddedToShelter = 15,

        /// <summary>
        /// The parent added to pet
        /// </summary>
        ParentAddedToPet = 16
    }
}