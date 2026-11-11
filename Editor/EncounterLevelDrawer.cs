using RPGFramework.Battle.Databases;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace RPGFramework.Battle.Editor
{
    /// <summary>An encounter's level rule, showing only what the rule reads, each value named for what it means under it.</summary>
    [CustomPropertyDrawer(typeof(EncounterLevel))]
    internal sealed class EncounterLevelDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            SerializedProperty rule = property.FindPropertyRelative("m_Rule");

            Foldout      root       = new Foldout { text = property.displayName, value = true };
            EnumField    ruleField  = new EnumField("Rule", (EncounterLevelRule)rule.enumValueIndex);
            EnumField    partyLevel = new EnumField("Party Level", EncounterPartyLevel.Average);
            IntegerField low        = new IntegerField();
            IntegerField high       = new IntegerField();

            ruleField.BindProperty(rule);
            partyLevel.BindProperty(property.FindPropertyRelative("m_PartyLevel"));
            low.BindProperty(property.FindPropertyRelative("m_Low"));
            high.BindProperty(property.FindPropertyRelative("m_High"));

            ruleField.RegisterValueChangedCallback(e => Show((EncounterLevelRule)e.newValue, partyLevel, low, high));

            Show((EncounterLevelRule)rule.enumValueIndex, partyLevel, low, high);

            root.Add(ruleField);
            root.Add(partyLevel);
            root.Add(low);
            root.Add(high);

            return root;
        }

        private static void Show(EncounterLevelRule rule, EnumField partyLevel, IntegerField low, IntegerField high)
        {
            bool party = rule == EncounterLevelRule.PartyLevel || rule == EncounterLevelRule.PartyLevelBetween;

            partyLevel.style.display = party ? DisplayStyle.Flex : DisplayStyle.None;
            low.style.display        = rule == EncounterLevelRule.PartyLevel ? DisplayStyle.None : DisplayStyle.Flex;
            high.style.display       = rule == EncounterLevelRule.Between || rule == EncounterLevelRule.PartyLevelBetween ? DisplayStyle.Flex : DisplayStyle.None;

            low.label  = rule == EncounterLevelRule.Fixed ? "Level" : rule == EncounterLevelRule.Between ? "Lowest" : "Levels Below";
            high.label = rule == EncounterLevelRule.Between ? "Highest" : "Levels Above";
        }
    }
}
