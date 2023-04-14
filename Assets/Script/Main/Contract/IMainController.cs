public interface IMainController
{
    void Apply(ModifyItemType modifyItemType, ItemType itemType, Place externalPlace);
    void Execute(ModifyItemType modifyItemType, Place place, Place externalPlace);
}