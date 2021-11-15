namespace Template.Common.DataAccess
{
    /// <summary>
    /// Interfaz de acceso a datos
    /// </summary>
    public interface IDataAccess
    {
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
