using System.Collections.Generic;

public interface IChoiceStrategy
{
    Place GetActualTarget(ModifyItemType modifyItemType, List<Place> targetList);
    Place GetActualExternalTarget(ModifyItemType modifyItemType,Place targetPlace, ItemType itemType, List<Place> externalTargetList);
}