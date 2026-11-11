using System.Collections.Generic;
using RPGFramework.Battle.Databases;
using UnityEditor;
using UnityEngine.UIElements;

namespace RPGFramework.Battle.Editor
{
    /// <summary>
    /// A name picked from the project's databases, for an enemy or an enemy group. What is already written stays shown when
    /// nothing answers to it, so opening an asset cannot repoint it.
    /// </summary>
    [CustomPropertyDrawer(typeof(EnemyNameAttribute))]
    [CustomPropertyDrawer(typeof(EnemyGroupNameAttribute))]
    internal sealed class BattleContentNameDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            List<string> names = attribute is EnemyNameAttribute ? BattleContentNames.Enemies() : BattleContentNames.EnemyGroups();

            DropdownField field = new DropdownField(property.displayName, names, -1);

            field.SetValueWithoutNotify(property.stringValue);
            field.RegisterValueChangedCallback(e =>
                                               {
                                                   property.stringValue = e.newValue;
                                                   property.serializedObject.ApplyModifiedProperties();
                                               });

            return field;
        }
    }
}
