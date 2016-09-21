using Huellitas.Web.Models.Api.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Huellitas.Web.Models.Api.Contents
{
    public class PetModel : ContentBaseModel
    {
        public PetModel()
        {
            TypeId = Data.Entities.ContentType.Pet;
        }

        public ContentAttributeModel<int> Subtype { get; set; }

        public ContentAttributeModel<int> Size { get; set; }

        public ContentAttributeModel<int> Genre { get; set; }

        public int Moths { get; set; }

        public bool Castrated { get; set; }

        public bool AutoReply { get; set; }

        public IList<PetModel> _relatedPets;
        public IList<PetModel> RelatedPets
        {
            get{ return _relatedPets ?? (_relatedPets = new List<PetModel>()); }

            set { _relatedPets = value; }
        }

        public ShelterModel Shelter { get; set; }

        public IList<FileModel> Images { get; set; }
    }
}
