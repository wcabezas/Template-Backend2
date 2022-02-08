namespace Template.Common.Providers
{
    /// <summary>
    /// Session provider to capture session information and make 
    /// it available to all classes by DI
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// Username
        /// </summary>
        string Username { get; }

        /// <summary>
        /// Setups the session
        /// </summary>
        void Setup(string username);
    }
}
