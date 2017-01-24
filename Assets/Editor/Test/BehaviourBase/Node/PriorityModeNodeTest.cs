using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using Ai.BehaviourBase.Node;
using Ai.BehaviourBase.EffectiveExecute;

public class PriorityModeNodeTest {

    public class PriorityTestActionNode : ActionNode{
        private int id = 0;
        public int Id {
            set { id = value; }
            get { return id; }
        }		

		/// <summary>
		/// 既存の設定されているものを削除しEffectiveExecuteMangerを設定します
		/// </summary>
		/// <param name="effectiveExecuteManager">Effective execute manager.</param>
		public void SetEffectiveExecuteManager(EffectiveExecuteManager effectiveExecuteManager){
			if( base.effectiveExecuteManager != null){
				base.effectiveExecuteManager = null;
			}
			base.effectiveExecuteManager = effectiveExecuteManager;
		}

        public override void DoExecute() {
            base.DoExecute();
            Debug.Log("これがこのノードのidだよ: " + id.ToString());
            Debug.Log("これがこのノードのプライオリティだよ: " + priority.ToString());
        }

		/// <summary>
		/// テスト用に実行可能ノードになる設定を行います
		/// </summary>
		public void ConfigureCanExecuteData(){
			effectiveExecuteManager.ClearEffectveExecute();
			TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
			testEffectiveExecute.TestNumber = 1;
			effectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
		}

		/// <summary>
		/// テスト用に実行可能条件Dataを設定 TestNumber 1 ~ 10の EffectiveExecuteを設定
		/// </summary>
		public void ConfigureTestEffectiveExecuteData(){
			effectiveExecuteManager.ClearEffectveExecute();
			for(uint i = 0; i < 10; i++) {
				TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
				testEffectiveExecute.TestNumber = i;
				effectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
			}
		}
    }

    /// <summary>
    /// テスト用に本来外部にはみせないexecuteNodeListを返す関数持ちのPriorityModeNode
    /// </summary>
    public class PriorityModeNodeForTest : PriorityModeNode{
        public List<BehaviourBaseNode> GetExecuteNodeList() {
            return base.executeNodeList;
        }
    }

	[Test]
	/// <summary>
	/// 有効なものだけをPickし優先度順に整列するかのTest
	/// </summary>
	public void AndExecuteEfectiveDecideTest(){
		PriorityModeNodeForTest priorityModeNodeForTest = new PriorityModeNodeForTest();
		int maxNodeCount = 10;
		//整列検証用データを用意
		for(int i = 0; i < maxNodeCount; i++) {
			PriorityTestActionNode priorityTestActionNode = new PriorityTestActionNode();
			AndEffectiveExecuteManager andEffectiveExecuteManager = new AndEffectiveExecuteManager();
			priorityTestActionNode.SetEffectiveExecuteManager(andEffectiveExecuteManager);
			priorityTestActionNode.ConfigureTestEffectiveExecuteData();	
		}
		//TODO 以下に実行可能が加味されて優先度順にPickされてるかの　テストを書く

	}
		

    [Test]
	/// <summary>
	/// 優先度順に整列するかのTest
	/// </summary>
    public void DecideEffectiveNodesTest() {
        //整列検証用データを用意
        PriorityModeNodeForTest priorityModeNodeForTest = new PriorityModeNodeForTest();
        int maxNodeCount = 10;
        for (int i = 0; i < maxNodeCount; i++) {
            PriorityTestActionNode priorityTestActionNode = new PriorityTestActionNode();
			AndEffectiveExecuteManager andEffectiveExecuteManager = new AndEffectiveExecuteManager();
			priorityTestActionNode.SetEffectiveExecuteManager(andEffectiveExecuteManager);
			priorityTestActionNode.ConfigureCanExecuteData();
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

        //優先度順に整列
        priorityModeNodeForTest.OnCatchChooseAlignExecutableNodesRequest();

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