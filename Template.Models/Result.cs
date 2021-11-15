namespace Template.Models
{
    using System;
   
    /// <summary>
    /// Business logic result
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Process success indicator
        /// </summary>
        public bool Success { get; protected set; }

        /// <summary>
        /// Message process
        /// </summary>
        public string Message { get; protected set; }


        /// <summary>
        /// Emtpy constructor
        /// </summary>
        public Result()
        {

        }


        /// <summary>
        /// Typed constructor
        /// </summary>
        public Result(Exception ex)
        {
            this.Success = false;
            this.Message = ex.Message;
        }


        /// <summary>
        /// Typed constructor
        /// </summary>
        public Result(bool success, string message = "")
        {
            this.Success = success;
            this.Message = message;
        }
    }



    /// <summary>
    /// Typed business logic result
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Result<T> : Result
    {
        /// <summary>
        /// Actual endoint response data
        /// </summary>
        public T Data { get; set; }


        /// <summary>
        /// Typed constructor
        /// </summary>
        public Result(T data)
        {
            this.Data = data;
            this.Success = true;
            this.Message = string.Empty;
        }



        /// <summary>
        /// Typed constructor
        /// </summary>
        public Result(T data, bool success, string message)
        {
            this.Data = data;
            this.Success = success;
            this.Message = message;
        }


        /// <summary>
        /// Typed constructor
        /// </summary>
        public Result(string message)
        {
            this.Data = default(T);
            this.Success = false;
            this.Message = message;
        }
    }

}