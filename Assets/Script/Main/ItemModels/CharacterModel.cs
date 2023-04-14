public class CharacterModel
{
    public readonly int Amount;
    public int CurrentAmount;

    public CharacterModel(int amount, int currentAmount = 0)
    {
        Amount = amount;
        CurrentAmount = currentAmount;
    }
}