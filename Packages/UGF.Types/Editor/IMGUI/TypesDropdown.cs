using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UGF.Types.Editor.IMGUI
{
    public class TypesDropdown : AdvancedDropdown
    {
        public event Action<Type> Selected;

        private readonly Dictionary<int, Type> m_types = new Dictionary<int, Type>();

        public TypesDropdown(IEnumerable<Type> types) : base(new AdvancedDropdownState())
        {
            if (types == null) throw new ArgumentNullException(nameof(types));

            minimumSize = new Vector2(minimumSize.x, 500F);

            foreach (Type type in types)
            {
                m_types.Add(type.GetHashCode(), type);
            }
        }

        protected override void ItemSelected(AdvancedDropdownItem item)
        {
            base.ItemSelected(item);

            Selected?.Invoke(m_types[item.id]);
        }

        protected override AdvancedDropdownItem BuildRoot()
        {
            var root = new AdvancedDropdownItem("Types");

            foreach (KeyValuePair<int, Type> pair in m_types)
            {
                Type type = pair.Value;

                var child = new AdvancedDropdownItem(type.Name)
                {
                    id = pair.Key
                };

                IEnumerable<string> path = !string.IsNullOrEmpty(type.Namespace)
                    ? type.Namespace.Split('.')
                    : Enumerable.Empty<string>();

                AddMenuItem(root, child, path);
            }

            return root;
        }

        private void AddMenuItem(AdvancedDropdownItem parent, AdvancedDropdownItem child, IEnumerable<string> path)
        {
            int count = path.Count();

            if (count == 0)
            {
                parent.AddChild(child);
            }
            else
            {
                string directoryName = path.First();
                AdvancedDropdownItem directory = null;

                foreach (AdvancedDropdownItem item in parent.children)
                {
                    if (item.name == directoryName)
                    {
                        directory = item;
                        break;
                    }
                }

                if (directory == null)
                {
                    directory = new AdvancedDropdownItem(directoryName);

                    parent.AddChild(directory);
                }

                AddMenuItem(directory, child, path.Skip(1));
            }
        }
    }
}
