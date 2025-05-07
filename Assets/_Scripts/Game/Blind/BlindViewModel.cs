public class BlindViewModel
{
    public BlindData Data;
    public IBlindLogic blindLogic;

    public BlindViewModel(BlindData data)
    {
        this.Data = data;
    }
}