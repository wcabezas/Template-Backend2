using Newtonsoft.Json;
using System.IO;

namespace Template.Common.Models
{
    /// <summary>
    /// Generic request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Request<T> where T : class
    {
        /// <summary>
        /// Serialized data
        /// </summary>
        public T Data { get; set; }


        /// <summary>
        /// Empty constructor (needed) for serialization
        /// </summary>
        public Request()
        {

        }

        /// <summary>
        /// T base constructor
        /// </summary>
        /// <param name="data"></param>
        public Request(T data)
        {
            this.Data = data;
        }


        /// <summary>
        /// Strem base constructor
        /// </summary>
        /// <param name="stream"></param>
        public Request(Stream stream)
        {
            if (stream == null)
            {
                this.Data = null;
            }
            else
            {
                var body = new StreamReader(stream).ReadToEnd();
                if (!string.IsNullOrEmpty(body))
                {
                    if (typeof(T) == typeof(string))
                    {
                        this.Data = body as T;
                    }
                    else
                    {
                        this.Data = JsonConvert.DeserializeObject<T>(body);
                    }
                }
            }
        }    
    }
}
