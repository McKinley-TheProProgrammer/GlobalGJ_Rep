// UltEvents // https://kybernetik.com.au/ultevents // Copyright 2021-2024 Kybernetik //

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UltEvents.Editor
{
    /// <summary>[Editor-Only] An <see cref="AdvancedDropdown"/> with generic callback delegates.</summary>
    public class GenericDropdown : AdvancedDropdown
    {
        /************************************************************************************************************************/

        private class Item : AdvancedDropdownItem
        {
            public GenericMenu.MenuFunction Function { get; set; }

            public Item(string name)
                : base(name)
            { }
        }

        /************************************************************************************************************************/

        private static readonly Texture2D
            SelectedIcon = EditorGUIUtility.Load("d_Toggle Icon") as Texture2D;

        private readonly Item
            RootItem;

        private readonly GenericMenu
            ContextMenu;

        /************************************************************************************************************************/

        /// <summary>Creates a new <see cref="GenericDropdown"/>.</summary>
        public GenericDropdown(AdvancedDropdownState state, string title)
            : base(state)
        {
            if (BoolPref.ContextMenuStyle)
                ContextMenu = new();
            else
                RootItem = new(title);
        }

        /// <inheritdoc/>
        protected override AdvancedDropdownItem BuildRoot()
            => RootItem;

        /// <inheritdoc/>
        protected override void ItemSelected(AdvancedDropdownItem item)
            => (item as Item)?.Function?.Invoke();

        /************************************************************************************************************************/

        /// <summary>Adds an item which will run the `function` when selected.</summary>
        public void AddItem(string path, bool on, GenericMenu.MenuFunction function)
        {
            if (ContextMenu != null)
            {
                ContextMenu.AddItem(new(path), on, function);
                return;
            }

            var item = GetOrCreateItem(path);
            item.enabled = true;
            item.Function = function;
            item.icon = on ? SelectedIcon : null;
        }

        /************************************************************************************************************************/

        /// <summary>Adds an item which will run the `function` for each target of the `property`.</summary>
        public void AddItem(
            SerializedProperty property,
            string path,
            MenuFunctionState state,
            Action<SerializedProperty> function)
        {
            if (ContextMenu != null)
            {
                ContextMenu.AddPropertyModifierFunction(property, path, state, function);
                return;
            }

            var item = GetOrCreateItem(path);
            item.enabled = state != MenuFunctionState.Disabled;
            item.Function = () =>
            {
                Serialization.ForEachTarget(property, function);
                GUIUtility.keyboardControl = 0;
                GUIUtility.hotControl = 0;
                EditorGUIUtility.editingTextField = false;
            };
            item.icon = state == MenuFunctionState.Selected ? SelectedIcon : null;
        }

        /************************************************************************************************************************/

        /// <summary>Adds an item which can't be selected.</summary>
        public void AddDisabledItem(string path)
        {
            if (ContextMenu != null)
            {
                ContextMenu.AddDisabledItem(new(path));
                return;
            }

            var item = GetOrCreateItem(path);
            item.enabled = false;
        }

        /************************************************************************************************************************/

        /// <summary>Adds a separator line.</summary>
        public void AddSeparator(string path)
        {
            if (ContextMenu != null)
            {
                ContextMenu.AddSeparator(new(path));
                return;
            }

            var parent = GetOrCreateItem(path);
            parent.AddSeparator();
        }

        /************************************************************************************************************************/

        /// <summary>Shows this menu relative to the `area`.</summary>
        public void ShowContext(Rect area)
        {
            if (ContextMenu != null)
                ContextMenu.DropDown(area);
            else
                Show(area);
        }

        /************************************************************************************************************************/

        private Item GetOrCreateItem(string path)
        {
            var item = RootItem;

            var start = 0;
            var end = 0;
            while (end < path.Length)
            {
                end = path.IndexOf('/', start);
                if (end < 0)
                    end = path.Length;

                if (end <= start)
                    break;

                var name = path[start..end];

                item = GetOrCreateChild(item, name);

                start = end + 1;
            }

            return item;
        }

        private Item GetOrCreateChild(AdvancedDropdownItem parent, string name)
        {
            foreach (var child in parent.children)
                if (child.name == name)
                    return (Item)child;

            var item = new Item(name);
            parent.AddChild(item);
            return item;
        }

        /************************************************************************************************************************/

        private static readonly GUIContent
            TypeFieldContent = new();

        /// <summary>Draws a field which lets you pick a <see cref="Type"/> from a list.</summary>
        public static void DrawTypeField(
            Rect area,
            SerializedProperty property,
            string selectedTypeName,
            GUIStyle style,
            ref AdvancedDropdownState state,
            Func<List<Type>> getTypes,
            Action<SerializedProperty, Type> setValue,
            Action onOpenMenu = null)
        {
            var selectedType = Type.GetType(selectedTypeName);

            if (selectedType != null)
            {
                TypeFieldContent.text = selectedType.GetNameCS(BoolPref.ShowFullTypeNames);
                TypeFieldContent.tooltip = selectedType.GetNameCS(true);
            }
            else
            {
                TypeFieldContent.text = "No Type Selected";
                TypeFieldContent.tooltip = "";
            }

            if (GUI.Button(area, TypeFieldContent, style))
            {
                onOpenMenu?.Invoke();

                property = property.Copy();

                state ??= new();

                var menu = new GenericDropdown(
                    state,
                    selectedType != null ? TypeFieldContent.tooltip : "Pick a Type");

                foreach (var type in getTypes())
                {
                    string path, typeName;
                    if (type == null)
                    {
                        path = typeName = "Null";
                    }
                    else
                    {
                        path = type.FullName;
                        path = path.Replace('.', '/');
                        path = path.Replace('+', '/');

                        typeName = type.AssemblyQualifiedName;
                    }

                    var itemState = typeName == selectedTypeName
                        ? MenuFunctionState.Selected
                        : MenuFunctionState.Normal;

                    menu.AddItem(property, path, itemState, targetProperty =>
                    {
                        setValue(targetProperty, type);
                    });
                }

                menu.ShowContext(area);
            }

            CheckDragAndDrop(area, property, getTypes, setValue);
        }

        /************************************************************************************************************************/

        private static void CheckDragAndDrop(
            Rect area,
            SerializedProperty property,
            Func<List<Type>> getTypes,
            Action<SerializedProperty, Type> setValue)
        {
            var dragging = DragAndDrop.objectReferences;
            if (dragging.Length != 1)
                return;

            var currentEvent = Event.current;
            if (!area.Contains(currentEvent.mousePosition))
                return;

            var drop = dragging[0].GetType();

            // If the dragged object is a valid type, continue.
            if (!getTypes().Contains(drop))
                return;

            if (currentEvent.type == EventType.DragUpdated || currentEvent.type == EventType.MouseDrag)
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Link;
            }
            else if (currentEvent.type == EventType.DragPerform)
            {
                setValue(property, drop);
                DragAndDrop.AcceptDrag();
                GUI.changed = true;
            }
        }

        /************************************************************************************************************************/
    }
}

#endif
