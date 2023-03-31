using System;
using UnityEngine;

public class Handler : BaseHandler
{
    public override bool Execute()
    {
        if (!GameModel.IsActiveInput)
        {
            return false;
        }

        try
        {
            var targetPlace = Strategy.GetActualTarget(ModifyItemType, ItemList);
            if (targetPlace == null)
            {
                Debug.Log("Don't Found actual target");
                return false;
            }

            var externalTargetPlace =
                Strategy.GetActualExternalTarget(ModifyItemType, targetPlace, ItemType.None, ExternalItemList);

            if (ModifyItemType == ModifyItemType.External && (targetPlace == null || externalTargetPlace == null))
            {
                Debug.Log("Don't Found actual target");
                return false;
            }

            MainController.Execute(ModifyItemType, targetPlace, externalTargetPlace);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }

        return true;
    }
}