namespace Template.SqlDataAccess.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Template.Models;
    

    /// <summary>
    /// Sample item 
    /// </summary>
    public class ItemEntity : BaseEntity
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid ItemId { get; set; }

        [Column(TypeName = "nvarchar(25)")]
        public string Text { get; set; }


        /// <summary>
        /// Returns the model using this entity info
        /// </summary>        
        public Item ToModel()
        {
            return new Item
            {
                ItemId = this.ItemId,
                Text = this.Text,
            };
        }


        /// <summary>
        /// Cretea a new entity based on a item
        /// </summary>        
        public static ItemEntity FromModel(Item item)
        {
            return new ItemEntity 
            {             
                ItemId = item.ItemId,
                Text = item.Text,
            };
        }

    }
}
