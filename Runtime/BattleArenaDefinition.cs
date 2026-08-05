namespace RPGFramework.Battle
{
    public class BattleArenaDefinition
    {
        public string AssetName { get; }
        public string AssetPath { get; }

        public BattleArenaDefinition(string assetName, string assetPath)
        {
            AssetName = assetName;
            AssetPath = assetPath;
        }
    }
}