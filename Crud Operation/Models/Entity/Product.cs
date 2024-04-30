using static Crud_Operation.Enums.SystemEnums;

namespace Crud_Operation.Models.Entity
{
    public class Product : Shared
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }

    }
}
