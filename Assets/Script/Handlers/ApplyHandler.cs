using System;
using UnityEngine;

namespace Script.Core.Behaviors
{
    public class ApplyHandler : BaseHandler
    {
        public ItemType ItemType;

        public override bool Execute()
        {
            if (!GameModel.IsActiveInput)
            {
                return false;
            }
            try
            {
                var externalTargetPlace =
                    Strategy.GetActualExternalTarget(ModifyItemType, null, ItemType, ExternalItemList);
                if ( externalTargetPlace == null)
                {
                    Debug.Log("Don't Found actual target");
                    return false;
                }

                MainController.Apply(ModifyItemType, ItemType, externalTargetPlace);
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