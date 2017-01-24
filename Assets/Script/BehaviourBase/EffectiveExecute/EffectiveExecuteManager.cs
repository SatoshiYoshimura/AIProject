using System.Collections.Generic;
using UnityEngine;

namespace Ai.BehaviourBase.EffectiveExecute {
    public class EffectiveExecuteManager {
        protected List<EffectiveExecute> effectiveExecuteList = null;

		public EffectiveExecuteManager(){
			effectiveExecuteList = new List<EffectiveExecute>();
		}

		/// <summary>
		/// EffectiveExecuteをListに追加します
		/// </summary>
		/// <param name="effectiveExecute">Effective execute.</param>
		public void AddEffectiveExecute(EffectiveExecute effectiveExecute){
			effectiveExecuteList.Add(effectiveExecute);
		}

		/// <summary>
		/// EffectiveexecuteListをClearします
		/// </summary>
		public void ClearEffectveExecute(){
			effectiveExecuteList.Clear();
		}

        public virtual bool CanExecute() {
            bool ret = false;
            return ret;
        }
    }
}