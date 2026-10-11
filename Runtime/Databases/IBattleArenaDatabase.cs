namespace RPGFramework.Battle.Databases
{
    /// <summary>Every arena the game's battles can be fought in, by the number <c>BattleArgs.Arena</c> gives.</summary>
    public interface IBattleArenaDatabase
    {
        /// <summary>The arena with this number.</summary>
        BattleArenaDefinition Get(int index);
    }
}