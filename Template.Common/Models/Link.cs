namespace Template.Common.Models
{
    /// <summary>
    /// Hateoas links for the response
    /// </summary>
    public class ResponseLink
    {
        /// <summary>
        /// Link
        /// </summary>
        public string Href { get; private set; }
        
     
        /// <summary>
        /// Action or method
        /// </summary>
        public string Action { get; private set; }
        

        /// <summary>
        /// Constructor
        /// </summary>
        public ResponseLink(string href,  string action)
        {
            this.Href = href;   
            this.Action = action;
        }
    }
}
