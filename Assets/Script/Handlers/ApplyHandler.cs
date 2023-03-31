using System;
using UnityEngine;

namespace Script.Core.Behaviors
{
    public class ApplyHandler : BaseHandler
    {
        public ItemType itemType;

        public override bool Execute()
        {
            if (!GameModel.IsActiveInput)
            {
                return false;
            }
            try
            {
                var externalTargetPlace =
                    Strategy.GetActualExternalTarget(ModifyItemType, null, itemType, ExternalItemList);
                if ( externalTargetPlace == null)
                {
                    Debug.Log("Don't Found actual target");
                    return false;
                }
                
                MainController.ApplyExecute(ModifyItemType, itemType, externalTargetPlace);
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }
    }
}