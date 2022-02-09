using System;

namespace Template.Models
{
    /// <summary>
    /// Sample item
    /// </summary>
    public class Item  : BaseModel
    {
        /// <summary>
        /// Item Id
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// A simple text
        /// </summary>
        public string Text { get; set; }
    }
}
