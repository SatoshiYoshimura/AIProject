using Ai.BehaviourBase.EffectiveExecute;

namespace Ai.BehaviourBase.Node {
    public abstract class BehaviourBaseNode {
        /// <summary>
        /// parrentNodeがpriorityMode時に参照する
        /// モードの入れ替え発生を想定してBaseに持たす
        /// priority == 1 が最も優先度高く実行され,値が大きくなるにつれ、優先度が下がっていく
        /// </summary>
        protected int priority;
        public int Priority {
            get { return priority; }
            set { priority = value; }
        }

		/// <summary>
		/// CanExecute()によって当ノードが実行可能かどうかを判断してくれる存在
		/// </summary>
		protected EffectiveExecuteManager effectiveExecuteManager = null;

		public BehaviourBaseNode(){
			effectiveExecuteManager = new EffectiveExecuteManager();
		}

        /// <summary>
        /// ParentNodeはChildNodeListをDoexecuteを実行しながらRoop回す
        /// </summary>
        public abstract void DoExecute();

		/// <summary>
		/// 当ノードが実行可能かどうか　
		/// </summary>
		/// <returns><c>true</c> if this instance can execute; otherwise, <c>false</c>.</returns>
		public bool CanExecute(){
			return effectiveExecuteManager.CanExecute();
		}

    }
}