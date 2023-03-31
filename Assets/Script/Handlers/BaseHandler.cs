using System.Collections.Generic;

public abstract class BaseHandler : IHandler
{
    public ModifyItemType ModifyItemType;
    public List<Place> ItemList;
    public List<Place> ExternalItemList;
    public IChoiceStrategy Strategy;
    public GameModel GameModel;
    public MainController MainController;
    public abstract bool Execute();
}