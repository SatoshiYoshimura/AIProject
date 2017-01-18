using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Ai.BehaviourBase.Node;
using System.Collections.Generic;

public class PriorityModeNodeTest {

    public class PriorityTestActionNode : ActionNode{
        private int id = 0;
        public int Id {
            set { id = value; }
            get { return id; }
        }

        public override void DoExecute() {
            base.DoExecute();
            Debug.Log("これがこのノードのidだよ: " + id.ToString());
            Debug.Log("これがこのノードのプライオリティだよ: " + priority.ToString());
        }
    }

    /// <summary>
    /// テストように本来外部にはみせないexecuteNodeListを返す関数持ちのPriorityModeNode
    /// </summary>
    public class PriorityModeNodeForTest : PriorityModeNode{
        public List<BehaviourBaseNode> GetExecuteNodeList() {
            return base.executeNodeList;
        }
    }

    [Test]
    public void DecideEffectiveNodesTest() {
        //整列検証用データを用意
        PriorityModeNodeForTest priorityModeNodeForTest = new PriorityModeNodeForTest();
        int maxNodeCount = 10;
        for (int i = 0; i < maxNodeCount; i++) {
            PriorityTestActionNode priorityTestActionNode = new PriorityTestActionNode();
            priorityTestActionNode.Id = i;
            if ( i < 3) {
                priorityTestActionNode.Priority = 3;
            }
            else if (i < 6) {
                priorityTestActionNode.Priority = 2;
            }
            else {
                priorityTestActionNode.Priority = 1;
            }

            priorityModeNodeForTest.AddNode(priorityTestActionNode);    
        }

        //優先度順に整列 TODO 実行可能判定
        priorityModeNodeForTest.OnCatchDecideEffeciveNodesRequest();

        //優先度順に並んでいるか確認
        List<BehaviourBaseNode> testList = priorityModeNodeForTest.GetExecuteNodeList();
        for (int i = 0; i < maxNodeCount; i++) {
            PriorityTestActionNode tmpNode = (PriorityTestActionNode)testList[i];

            if (i < 4) {
                Assert.AreEqual(1, tmpNode.Priority);
            } else if (i < 7) {
                Assert.AreEqual(2, tmpNode.Priority);
            } else {
                Assert.AreEqual(3, tmpNode.Priority);
            }

            if ( tmpNode.Id < 3) {
                Assert.AreEqual(3, tmpNode.Priority);
            }
            else if ( tmpNode.Id < 6) {
                Assert.AreEqual(2, tmpNode.Priority);
            }
            else {
                Assert.AreEqual(1, tmpNode.Priority);
            }
        }
    }

    [Test]
	public void EditorTest() {
		//Arrange
		var gameObject = new GameObject();

		//Act
		//Try to rename the GameObject
		var newGameObjectName = "My game object";
		gameObject.name = newGameObjectName;

		//Assert
		//The object has a new name
		Assert.AreEqual(newGameObjectName, gameObject.name);
	}
}