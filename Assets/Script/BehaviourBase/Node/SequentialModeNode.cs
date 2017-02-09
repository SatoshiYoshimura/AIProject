
namespace Ai.BehaviourBase.Node {
    public class SequentialModeNode : NonActionNode {

        /// <summary>
        /// 直下nodeを全部実行後ループするかどうかを表す変数: true Loopする : false Loopしない
        /// </summary>
        private bool isLooping = false;
        public bool IsLooping {
            get { return isLooping; }
            set { isLooping = value; }
        }

        /// <summary>
        /// あらかじめ決められた順序にそって実行する
        /// </summary>
        public override void DoExecute() {
            foreach (BehaviourBaseNode node in base.executeNodeList) {
                node.DoExecute();
            }
            if (isLooping) {
                DoExecute();
            }
        }

        /// <summary>
        /// 順序を決めるのは上位の存在がSwapを用いて決めるため、ここでは有効なものだけを取り出す処理を行う
        /// </summary>
        protected override void ChooseAlignExecutableNodes() {
            foreach (BehaviourBaseNode node in base.nodeList) {
                if (node.CanExecute()) {
                    executeNodeList.Add(node);
                }
            }
        }

        /// <summary>
        /// changeIndex番目のnodeとchangedIondex番目のnodeをの並び順(実行順)をSwapする
        /// </summary>
        /// <param name="changeIndex"></param>
        /// <param name="changedIndex"></param>
        public void Swap(int changeIndex, int changedIndex) {
            BehaviourBaseNode tmpNode = nodeList[changedIndex];
            nodeList[changedIndex] = nodeList[changeIndex];
            nodeList[changeIndex] = tmpNode;
        }                
    }
}