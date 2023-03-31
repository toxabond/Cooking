using System.Linq;

public class ItemModel : ITimerContainer, ITickableUpdate
{
    public ItemType FromItemType;
    public ItemType ToItemType;
    public Timer ItemTimer { get; }
    public InventoryModel Inventory;

    public ItemType Type => FromItemType == ToItemType ? ToItemType : ItemType.Playing;

    public ItemModel(ItemType itemType, InventoryModel inventory)
    {
        SetItemType(itemType);
        ItemTimer = new Timer(0);
        Inventory = inventory;
    }

    public bool IsReady()
    {
        return Type != ItemType.Playing
               && !GameUtil.DestroyedItemType.Contains(Type)
               && Inventory.IsReady();
    }

    public virtual bool CanApply(ItemType itemType)
    {
        return Type != ItemType.Playing
               && !GameUtil.DestroyedItemType.Contains(Type)
               && Inventory.CanApply(itemType);
    }

    public virtual void Apply(ItemType itemType)
    {
        Inventory.Apply(itemType);
    }

    public virtual void Reset()
    {
        Inventory.Reset();
    }

    public virtual void UpdateByDeltaTimer(float delta)
    {
    }

    public void SetItemType(ItemType itemType)
    {
        FromItemType = itemType;
        ToItemType = itemType;
    }
}