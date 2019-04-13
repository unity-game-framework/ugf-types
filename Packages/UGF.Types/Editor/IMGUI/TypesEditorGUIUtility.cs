using System;
using System.Collections.Generic;
using System.Linq;
using UGF.Types.Runtime;

namespace UGF.Types.Editor.IMGUI
{
    public static class TypesEditorGUIUtility
    {
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
