using UnityEngine;
using System.Collections.Generic;
using NUnit.Framework;
using Ai.BehaviourBase.EffectiveExecute;

namespace Ai.BehaviourBase.Node {

    public class SequentialTestActionNode : ActionNode {
        private int id = 0;
        public int ID {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 既存の設定されているものを削除しEffectiveExecuteMangerを設定します
        /// </summary>
        /// <param name="effectiveExecuteManager">Effective execute manager.</param>
        public void SetEffectiveExecuteManager(EffectiveExecuteManager effectiveExecuteManager) {
            if (base.effectiveExecuteManager != null) {
                base.effectiveExecuteManager = null;
            }
            base.effectiveExecuteManager = effectiveExecuteManager;
        }

        public override void DoExecute() {
            Debug.Log(id);
        }
    }

    public class SequentialModeNodeTest : SequentialModeNode{
        public List<BehaviourBaseNode> GetExecuteNodeList() {
            return base.executeNodeList;
        }

        int loopCount = 0;
        /// <summary>
        /// あらかじめ決められた順序にそって実行する テスト用にカウンタに応じてループをやめる
        /// </summary>
        public override void DoExecute() {
            if (IsLooping) {
                for (int i = 0; i < 10; i++) {
                    foreach (BehaviourBaseNode node in base.executeNodeList) {
                        node.DoExecute();
                    }
                }
            } else {
                foreach (BehaviourBaseNode node in base.executeNodeList) {
                    node.DoExecute();
                }
            }
        }


        #region TestMethod
        /// <summary>
        /// 実行順に実行可能なものが実行されているかのテスト
        /// </summary>
        [Test]
        public void TestSeaqential() {

            SequentialModeNodeTest sequentialModeNodeTest = new SequentialModeNodeTest();
            int maxNode = 10;
            //奇数番目のみ実行される設定
            for (int i = 0; i < maxNode; i++) {
                SequentialTestActionNode node = new SequentialTestActionNode();
                node.ID = i;
                TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
                testEffectiveExecute.TestNumber = (uint)i;
                AndEffectiveExecuteManager andEffectiveExecuteManager = new AndEffectiveExecuteManager();
                andEffectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
                node.SetEffectiveExecuteManager(andEffectiveExecuteManager);
                sequentialModeNodeTest.AddNode(node);
            }

            sequentialModeNodeTest.OnCatchChooseAlignExecutableNodesRequest();
            List<BehaviourBaseNode> testList = sequentialModeNodeTest.GetExecuteNodeList();
            foreach (BehaviourBaseNode node in testList) {
                SequentialTestActionNode testNode = (SequentialTestActionNode)node;
                Debug.Log(testNode.ID);
                Assert.AreEqual(1, testNode.ID % 2);
            }


            sequentialModeNodeTest.DoExecute();
        }

        /// <summary>
        /// 実行順に実行可能なものが実行し、最後まで行ったらループすることのテスト
        /// </summary>
        [Test]
        public void TestSeaqentialLooping() {

            SequentialModeNodeTest sequentialModeNodeTest = new SequentialModeNodeTest();
            sequentialModeNodeTest.IsLooping = true;

            int maxNode = 10;
            //奇数番目のみ実行される設定
            for (int i = 0; i < maxNode; i++) {
                SequentialTestActionNode node = new SequentialTestActionNode();
                node.ID = i;
                TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
                testEffectiveExecute.TestNumber = (uint)i;
                AndEffectiveExecuteManager andEffectiveExecuteManager = new AndEffectiveExecuteManager();
                andEffectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
                node.SetEffectiveExecuteManager(andEffectiveExecuteManager);
                sequentialModeNodeTest.AddNode(node);
            }

            sequentialModeNodeTest.OnCatchChooseAlignExecutableNodesRequest();
            List<BehaviourBaseNode> testList = sequentialModeNodeTest.GetExecuteNodeList();
            foreach (BehaviourBaseNode node in testList) {
                SequentialTestActionNode testNode = (SequentialTestActionNode)node;
 
                Assert.AreEqual(1, testNode.ID % 2);
            }

            sequentialModeNodeTest.DoExecute();
        }

        /// <summary>
        /// Swapで実行順の入れ替えのテスト
        /// </summary>
        [Test]
        public void SwapTest() {
            SequentialModeNodeTest sequentialModeNodeTest = new SequentialModeNodeTest();

            int maxNode = 10;
            //奇数番目のみ実行される設定
            for (int i = 0; i < maxNode; i++) {
                SequentialTestActionNode node = new SequentialTestActionNode();
                node.ID = i;
                TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
                testEffectiveExecute.TestNumber = (uint)i;
                AndEffectiveExecuteManager andEffectiveExecuteManager = new AndEffectiveExecuteManager();
                andEffectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
                node.SetEffectiveExecuteManager(andEffectiveExecuteManager);
                sequentialModeNodeTest.AddNode(node);
            }

            sequentialModeNodeTest.Swap(3,7);

            sequentialModeNodeTest.OnCatchChooseAlignExecutableNodesRequest();
            List<BehaviourBaseNode> testList = sequentialModeNodeTest.GetExecuteNodeList();
            int maxLoop = testList.Count;
            for (int i = 0; i < maxLoop; i++) {
                SequentialTestActionNode testNode = (SequentialTestActionNode)testList[i];
                Assert.AreEqual(1, testNode.ID % 2);
                //1 7 5 3 9 になっているはず 
                if (i == 1) {
                    Assert.AreEqual(7,testNode.ID);
                }
                if (i == 3) {
                    Assert.AreEqual(3,testNode.ID);
                }
            }

            sequentialModeNodeTest.DoExecute();
        }
        #endregion
    }
}
