namespace RPGFramework.Battle.Databases
{
    public interface IBattleArenaDatabase
    {
        BattleArenaDefinition Get(int index);
    }
}