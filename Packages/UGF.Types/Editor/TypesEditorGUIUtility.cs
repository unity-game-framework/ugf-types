using System;
using System.Collections.Generic;
using System.Linq;
using UGF.Types.Editor.IMGUI;
using UGF.Types.Runtime;

namespace UGF.Types.Editor
{
    /// <summary>
    /// Provides utilities to work with editor IMGUI and types.
    /// </summary>
    public static class TypesEditorGUIUtility
    {
        /// <summary>
        /// Gets dropdown used to display menu selection, with types that match specified func condition. if presents.
        /// <para>
        /// Returns menu with sorted types by their fullname.
        /// </para>
        /// </summary>
        /// <param name="validate">The function to validate type.</param>
        public static TypesDropdown GetTypesDropdown(Func<Type, bool> validate = null)
        {
            var types = new List<Type>();

            TypesUtility.CollectTypes(types, validate);

            types.Sort((type1, type2) =>
            {
                string name1 = type1.FullName ?? type1.Name;
                string name2 = type2.FullName ?? type2.Name;
                int order = name2.Count(c => c == '.') - name1.Count(c => c == '.');

                if (order == 0)
                {
                    order = string.Compare(name1, name2, StringComparison.Ordinal);
                }

                return order;
            });

            return new TypesDropdown(types);
        }
    }
}
