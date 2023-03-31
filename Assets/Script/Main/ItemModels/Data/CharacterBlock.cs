public class CharacterBlock
{
    private int _createdTime;
    public int Id;
    public Model Model;
    public int CreatedTime => _createdTime;
    

    public CharacterBlock(int sec, int id, Model model)
    {
        _createdTime = sec;
        Id = id;
        Model = model;
    }
}