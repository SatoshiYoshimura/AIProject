using System.Collections.Generic;

namespace Ai.BehaviourBase.EffectiveExecute {
    public class AndEffectiveExecuteManager : EffectiveExecuteManager {

        /// <summary>
        /// childEffectiveExecuteが一つでもisNotEffectiveだった場合falseを返す
        /// </summary>
        /// <returns>bool</returns>
        public override bool CanExecute() {
            if (effectiveExecuteList.Exists(x => x.DecideIsEffecive() == false)) {
                return false;
            }

            return true;
        }
    }
}