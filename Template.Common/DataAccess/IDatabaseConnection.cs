namespace Template.Common.DataAccess
{
    /// <summary>
    /// Handles the database connection so it can be shared
    /// between dataaccess components
    /// </summary>
    public interface IDatabaseConnection<T>
    {

        /// <summary>
        /// DB Context
        /// </summary>
        T DatabaseContext { get; }


        /// <summary>
        /// Abre la conexion a la base de datos
        /// </summary>
        void OpenDatabase();


        /// <summary>
        /// Cierra la conexion a datos
        /// </summary>
        void CloseDatabase();
    }
}
