//-----------------------------------------------------------------------
// <copyright file="SqlExceptionHelper.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Tests.Helpers
{
    using System;
    using System.Data.SqlClient;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// SQL Exception Helper to generate this kind of exceptions
    /// </summary>
    public static class SqlExceptionHelper
    {
        /// <summary>
        /// Gets the database update exception with <c>SQL</c> exception.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="message">The message.</param>
        /// <returns>the exception</returns>
        public static DbUpdateException GetDbUpdateExceptionWithSqlException(int number, string message)
        {
            return new DbUpdateException(message, SqlExceptionHelper.GetSqlException(number, message));
        }

        /// <summary>
        /// Constructs with the specified parameters.
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="parameters">The p.</param>
        /// <returns>the object</returns>
        private static T Construct<T>(params object[] parameters)
        {
            return (T)typeof(T).GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)[0].Invoke(parameters);
        }

        /// <summary>
        /// Gets the SQL exception.
        /// </summary>
        /// <param name="number">The number.</param>
        /// <param name="message">The message.</param>
        /// <returns>the exception</returns>
        private static SqlException GetSqlException(int number, string message)
        {
            SqlErrorCollection collection = Construct<SqlErrorCollection>();
            SqlError error = Construct<SqlError>(number, (byte)2, (byte)3, message, message, "proc", 100, (uint)1, new Exception());

            typeof(SqlErrorCollection)
                .GetMethod("Add", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(collection, new object[] { error });

            return typeof(SqlException).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)[0].Invoke(null, new object[] { collection, "11.0.0" }) as SqlException;
        }
    }
}