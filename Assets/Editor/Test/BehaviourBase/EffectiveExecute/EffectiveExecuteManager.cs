using System.Collections.Generic;

namespace Ai.BehaviourBase.EffectiveExecute {
    public class EffectiveExecuteManager {
        protected List<EffectiveExecute> effectiveExecuteList = null;
        
        public virtual bool CanExecute() {
            bool ret = false;
            return ret;
        }
    }
}