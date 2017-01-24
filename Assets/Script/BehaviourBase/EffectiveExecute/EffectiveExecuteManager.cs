using System.Collections.Generic;
using UnityEngine;

namespace Ai.BehaviourBase.EffectiveExecute {
    public class EffectiveExecuteManager {
        protected List<EffectiveExecute> effectiveExecuteList = null;

		public EffectiveExecuteManager(){
			effectiveExecuteList = new List<EffectiveExecute>();
		}

		public void AddEffectiveExecute(EffectiveExecute effectiveExecute){
			if(effectiveExecute == null){
				Debug.Assert("effectiveExecute cannot add becouse that is null");
			}

			effectiveExecuteList.Add(effectiveExecute);
		}

        public virtual bool CanExecute() {
            bool ret = false;
            return ret;
        }
    }
}