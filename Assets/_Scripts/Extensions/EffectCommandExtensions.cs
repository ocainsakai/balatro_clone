public static class EffectCommandExtensions
{
    public static IPlayEffectCommand CreateCommand(this EffectType effectType)
    {
        switch (effectType)
        {
            case EffectType.AddChip:
                return new AddChipCommand();
            default:
                return null;
        }
    }
}