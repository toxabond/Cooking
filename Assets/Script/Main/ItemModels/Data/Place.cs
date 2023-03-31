public class Place
{
    public int IdGroup { get; }
    public int Id { get; }

    public Place(int idGroup, int id)
    {
        IdGroup = idGroup;
        Id = id;
    }

    public override int GetHashCode()
    {
        return IdGroup * 10000 + Id;
    }

    public override string ToString()
    {
        return $"Place IdGroup:{IdGroup}, Id:{Id}";
    }
}