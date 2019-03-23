using System;
using System.Collections.Generic;
using System.Reflection;
using UGF.Assemblies.Runtime;
using UnityEngine;

namespace UGF.Types.Runtime
{
    public static class TypesUtility
    {
        public static void GetTypes<T>(ITypeProvider<T> provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            
            var types = new List<Type>();
            
            AssemblyUtility.GetBrowsableTypes(types, typeof(TypeIdentifierAttributeBase));
            
            AddTypes(provider, types);
        }
        
        public static void GetTypes<T>(ITypeProvider<T> provider, Assembly assembly)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            
            var types = new List<Type>();
            
            AssemblyUtility.GetBrowsableTypes(types, assembly, typeof(TypeIdentifierAttributeBase));
            
            AddTypes(provider, types);
        }

        public static void AddTypes<T>(ITypeProvider<T> provider, List<Type> types)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));
            if (types == null) throw new ArgumentNullException(nameof(types));
            
            for (int i = 0; i < types.Count; i++)
            {
                Type type = types[i];

                if (type.GetCustomAttribute<TypeIdentifierAttributeBase>() is ITypeIdentifierAttribute<T> attribute)
                {
                    provider.Add(attribute.Identifier, type);
                }
                else
                {
                    Debug.LogWarning($"Can not add type to provider, cause '{typeof(ITypeIdentifierAttribute<T>)}' not found at specified type: '{type}'.");
                }
            }
        }
    }
}