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
        /// ParentNodeはChildNodeListをDoexecuteを実行しながらRoop回す
        /// </summary>
        public abstract void DoExecute();
    }
}