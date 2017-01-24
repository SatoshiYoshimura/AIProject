using System.Collections.Generic;

namespace Ai.BehaviourBase.EffectiveExecute {
    public class OrEffectiveExecuteManger : EffectiveExecuteManager {

        /// <summary>
        /// child EffectiveExecuteに一つでもIsEffectiveがある場合trueを返す
        /// </summary>
        /// <returns>bool</returns>
        public override bool CanExecute() {
            if (effectiveExecuteList.Exists(x => x.DecideIsEffecive() == true)) {
                return true;
            }
            
            return false;
        }
    }
}


