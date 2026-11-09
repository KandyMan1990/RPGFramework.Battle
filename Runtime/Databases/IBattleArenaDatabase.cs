namespace RPGFramework.Battle.Databases
{
    /// <summary>Every arena the game's battles can be fought in, by the hash of its name, which <c>BattleArgs.Arena</c> holds.</summary>
    public interface IBattleArenaDatabase
    {
        /// <summary>The arena named by <paramref name="nameHash" />.</summary>
        BattleArenaDefinition Get(ulong nameHash);
    }
}
