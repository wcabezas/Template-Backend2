using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Common.Providers
{
    /// <inheritdoc/>  
    public class SessionProvider : ISessionProvider
    {
        /// <inheritdoc/>  
        public string Username { get; private set; }

        /// <inheritdoc/>  
        public void Setup(string username)
        {
            this.Username = username;
        }
    }
}
