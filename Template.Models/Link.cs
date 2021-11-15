namespace Template.Models
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
        /// Relate
        /// </summary>
        public string Rel { get; private set; }
        
        /// <summary>
        /// Method
        /// </summary>
        public string Method { get; private set; }
        

        /// <summary>
        /// Constructor
        /// </summary>
        public ResponseLink(string href, string rel, string method)
        {
            this.Href = href;   
            this.Rel = rel;
            this.Method = method;
        }
    }
}
