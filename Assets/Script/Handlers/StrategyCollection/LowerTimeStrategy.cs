using System.Collections.Generic;
using System.Linq;
using ModestTree;


public class LowerTimeStrategy : FirstActualStrategy
    {
        private readonly GameModel _gameModel;

        public LowerTimeStrategy(GameModel gameModel): base(gameModel)
        {
            _gameModel = gameModel;
        }

        public override Place GetActualTarget(ModifyItemType modifyItemType, List<Place> targetList)
        {
            if (targetList == null || targetList.IsEmpty())
            {
                return null;
            }

            var list = Sort(targetList);
            return base.GetActualTarget(modifyItemType, list);
        }

        public override Place GetActualExternalTarget(ModifyItemType modifyItemType, Place targetPlace, ItemType itemType,
            List<Place> externalTargetList)
        {
            if (externalTargetList == null || externalTargetList.IsEmpty())
            {
                return null;
            }

            var list = Sort(externalTargetList);
            return base.GetActualExternalTarget(modifyItemType, targetPlace, itemType, list);
        }

        private List<Place> Sort(List<Place> list)
        {
            return list
                .Where(p => p != null && _gameModel.ContainsKey(p))
                .OrderByDescending(p => _gameModel.GetItemModelByPlace(p).ItemTimer.Current).ToList();
        }
    }
