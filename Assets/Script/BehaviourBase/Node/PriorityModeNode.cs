using System.Collections.Generic;

namespace Ai.BehaviourBase.Node {
    public class PriorityModeNode : NonActionNode {

        /// <summary>
        /// childList内にあるpriorityの最大値
        /// nodeがnodeListにaddされるたびにpriorityを比較し、既存より大きかったら更新する
        /// </summary>
        private int maxPriorityNum = 0;

        public override void AddNode(BehaviourBaseNode node) {
            base.AddNode(node);
            if (node.Priority > maxPriorityNum) {
                maxPriorityNum = node.Priority;
            }
        }

        public override void DoExecute() {
            foreach (BehaviourBaseNode node in base.executeNodeList) {
                node.DoExecute();
            }
        }

        /// <summary>
        /// 実行可能なNodeをPriorityが高い順にexecuteList内に並べていく。
        /// 同じPriorytyが存在する場合はnodeList内のindex順に並べる
        /// </summary>
        protected override void ChooseAlignExecutableNodes() {
            for (int conparedPriority = 1; conparedPriority <= maxPriorityNum; conparedPriority++) {
                List<BehaviourBaseNode> tmpNodeList = base.nodeList.FindAll(node => node.Priority == conparedPriority);
                foreach (BehaviourBaseNode node in tmpNodeList) {
                    base.executeNodeList.Add(node);
                }
            }
        }

        public void OnCatchChooseAlignExecutableNodesRequest() {
            ChooseAlignExecutableNodes();
        }
    }
}