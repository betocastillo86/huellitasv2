//-----------------------------------------------------------------------
// <copyright file="ReflectionHelpers.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyModel;

    /// <summary>
    /// Reflection Helpers
    /// </summary>
    public class ReflectionHelpers
    {
        /// <summary>
        /// Gets the types on project. <![CDATA[Retorna los tipos existentes en la solución que implementen determinada interfaz]]>
        /// </summary>
        /// <param name="typeSearched">The type searched.</param>
        /// <returns>the value</returns>
        public static IEnumerable<Type> GetTypesOnProject(Type typeSearched)
        {
            var deps = DependencyContext.Default;

            var assemblies = new List<Assembly>();

            foreach (var library in deps.RuntimeLibraries.Where(c => c.Name.ToLower().Contains("huellitas")))
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }

            var allTypes = assemblies.SelectMany(c => c.ExportedTypes);

            var foundTypes = new List<Type>();

            var typeSearchedInfo = typeSearched.GetTypeInfo();

            foreach (var t in allTypes)
            {
                var name = t.Name;
                var typeInfo = t.GetTypeInfo();

                if (t.GetTypeInfo().IsInterface)
                {
                    continue;
                }

                if (typeSearched.IsAssignableFrom(t) || (typeSearchedInfo.IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(typeInfo, typeSearched)))
                {
                    foundTypes.Add(t);
                }
            }

            return foundTypes;
        }

        /// <summary>
        /// Validates if does the type implement open generic.
        /// </summary>
        /// <param name="typeinfo">The type.</param>
        /// <param name="openGeneric">The open generic.</param>
        /// <returns>the value</returns>
        private static bool DoesTypeImplementOpenGeneric(TypeInfo typeinfo, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();

                foreach (var implementedInterface in typeinfo.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.GetTypeInfo().IsGenericType)
                    {
                        continue;
                    }

                    var isMatch = genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                    return isMatch;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}