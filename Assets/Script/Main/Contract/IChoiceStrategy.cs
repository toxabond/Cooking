using System.Collections.Generic;

public interface IChoiceStrategy
{
    Place GetActualTarget(ModifyItemType modifyItemType, IReadOnlyList<Place> targetList);
    Place GetActualExternalTarget(ModifyItemType modifyItemType, Place targetPlace, ItemType itemType,
        IReadOnlyList<Place> externalTargetList);
}