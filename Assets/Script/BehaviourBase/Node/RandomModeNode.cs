using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.BehaviourBase.Node {
    public class RandomModeNode : NonActionNode , OnOffModeNodeInterface {

        /// <summary>
        /// trueにするとOnOffModeのノードとなる
        /// </summary>
        public bool IsEnableOnOffMode {
            get { return isEnableOnOffMode; }
        }
        private bool isEnableOnOffMode = false;

        public override void DoExecute() {
            foreach (BehaviourBaseNode node in base.executeNodeList) {
                node.DoExecute();
                if (isEnableOnOffMode) {
                    break;
                }
            }
        }

        /// <summary>
        /// 実行可能なNodeをランダムにPickしexecuteList内に並べていく。
        /// </summary>
        protected override void ChooseAlignExecutableNodes() {
            int initSeed = System.Environment.TickCount;
            int maxNodeCount = nodeList.Count;
            //executeNodeに追加されるnode数もランダムに決める
            int addNodeCount = Mathf.FloorToInt(Random.Range(0,maxNodeCount));
            //ランダムなNodeをかぶりなくPick
            List<int> selectedNodeIndexList = new List<int>();
            for (int i = 0; i < addNodeCount; i++) {
                int index = Mathf.FloorToInt(Random.Range(0,addNodeCount));
                if (selectedNodeIndexList.Exists( x => x == index )) {
                    //ループカウンタを進めると少ない数ばかり選ばれてしまう
                    i--;
                    continue;
                }
                else {
                    if (nodeList[index].CanExecute()) {
                        base.AddNode(nodeList[index]);
                        selectedNodeIndexList.Add(index);
                    }
                }
            }
        }

    }
}


