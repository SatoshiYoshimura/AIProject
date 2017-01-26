using System.Collections.Generic;

namespace Ai.BehaviourBase.Node {
    public abstract class NonActionNode : BehaviourBaseNode {
        /// <summary>
        /// 全ChildNodeが状態関係なく雑に積まれているList
        /// </summary>
        protected List<BehaviourBaseNode> nodeList = null;

        /// <summary>
        /// 各モードのNodeがChildNodeの実行順をDecideEffectiveNodesにより判定する
        /// executeNodeList内には実行可能なnodeのみが実行順に入る
        /// </summary>
        protected List<BehaviourBaseNode> executeNodeList = null;

        public NonActionNode() {
            nodeList = new List<BehaviourBaseNode>();
            executeNodeList = new List<BehaviourBaseNode>();
        }

        public virtual void AddNode(BehaviourBaseNode node) {
            nodeList.Add(node);
        }

        private bool isRoot = false;

        protected abstract void ChooseAlignExecutableNodes();

        public void OnCatchChooseAlignExecutableNodesRequest() {
            ChooseAlignExecutableNodes();
        }
    }
}
