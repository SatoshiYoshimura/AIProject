using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NUnit.Framework;
using Ai.BehaviourBase.Node;
using Ai.BehaviourBase.EffectiveExecute;

public class PriorityModeNodeTest {

#region Test Declear
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
			base.effectiveExecuteManager.ClearEffectveExecute();
            for (int i = 1; i < 6; i++) {
                TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
                uint testNum = (uint)(i % 2);
                if (testNum != 0) {
                    testEffectiveExecute.TestNumber = testNum;
                    base.effectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
                }
            }
        }
        
        /// <summary>
        /// テスト用にAndの場合実行不可能ノードになり、Orの場合実行可能になる設定を行います
        /// </summary>
        public void ConfigureMixedExecuteData() {
            effectiveExecuteManager.ClearEffectveExecute();
            for (int i = 0; i < 5; i++) {
                TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
                testEffectiveExecute.TestNumber = (uint)(i);
                effectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
            }
        }

        /// <summary>
        /// テスト用に実行不可能ノードになる設定を行います
        /// </summary>
        public void ConfigureCannotExecuteData() {
            effectiveExecuteManager.ClearEffectveExecute();
            for (int i = 0; i < 5; i++) {
                TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
                testEffectiveExecute.TestNumber = (uint)(i * 2);
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

#endregion

#region TestMethod

    [Test]
    /// <summary>
    /// 単品のPriorityNodeに対しAndEffectiveExecuteが正しく動いているかのTest
    /// </summary>
    public void AndExecuteEfectiveDecideTest() {
        //全部true がtrueかどうか
        PriorityTestActionNode canExecutePriorityTestActionNode = new PriorityTestActionNode();
        AndEffectiveExecuteManager canAndEffectiveExecuteManager = new AndEffectiveExecuteManager();
        canExecutePriorityTestActionNode.SetEffectiveExecuteManager(canAndEffectiveExecuteManager);
        canExecutePriorityTestActionNode.ConfigureCanExecuteData();

        Assert.AreEqual(true, canExecutePriorityTestActionNode.CanExecute());

        //全部falseがfalseかどうか
        PriorityTestActionNode cannotExecutePriorityTestActionNode = new PriorityTestActionNode();
        AndEffectiveExecuteManager cannotAndEffectiveExecuteManager = new AndEffectiveExecuteManager();
        cannotExecutePriorityTestActionNode.SetEffectiveExecuteManager(cannotAndEffectiveExecuteManager);
        cannotExecutePriorityTestActionNode.ConfigureCannotExecuteData();

        Assert.AreEqual(false, cannotExecutePriorityTestActionNode.CanExecute());

        //一部 falseがfalseかどうか
        PriorityTestActionNode mixedExecutePriorityTestActionNode = new PriorityTestActionNode();
        AndEffectiveExecuteManager mixedAndEffectiveExecuteManager = new AndEffectiveExecuteManager();
        mixedExecutePriorityTestActionNode.SetEffectiveExecuteManager(mixedAndEffectiveExecuteManager);
        mixedExecutePriorityTestActionNode.ConfigureMixedExecuteData();

        Assert.AreEqual(false, cannotExecutePriorityTestActionNode.CanExecute());
    }

    [Test]
    /// <summary>
    /// 単品のPriorityNodeに対しOrEffectiveExecuteが正しく動いているかのTest
    /// </summary>
    public void OrExecuteEfectiveDecideTest() {
        //全部true がtrueかどうか
        PriorityTestActionNode canExecutePriorityTestActionNode = new PriorityTestActionNode();
        OrEffectiveExecuteManager canOrEffectiveExecuteManager = new OrEffectiveExecuteManager();
        canExecutePriorityTestActionNode.SetEffectiveExecuteManager(canOrEffectiveExecuteManager);
        canExecutePriorityTestActionNode.ConfigureCanExecuteData();

        Assert.AreEqual(true, canExecutePriorityTestActionNode.CanExecute());

        //全部falseがfalseかどうか
        PriorityTestActionNode cannotExecutePriorityTestActionNode = new PriorityTestActionNode();
        OrEffectiveExecuteManager cannotOrEffectiveExecuteManager = new OrEffectiveExecuteManager();
        cannotExecutePriorityTestActionNode.SetEffectiveExecuteManager(cannotOrEffectiveExecuteManager);
        cannotExecutePriorityTestActionNode.ConfigureCannotExecuteData();

        Assert.AreEqual(false, cannotExecutePriorityTestActionNode.CanExecute());

        //一部 trueがあるとtrueかどうか
        PriorityTestActionNode mixedExecutePriorityTestActionNode = new PriorityTestActionNode();
        OrEffectiveExecuteManager mixedOrEffectiveExecuteManager = new OrEffectiveExecuteManager();
        mixedExecutePriorityTestActionNode.SetEffectiveExecuteManager(mixedOrEffectiveExecuteManager);
        mixedExecutePriorityTestActionNode.ConfigureMixedExecuteData();

        Assert.AreEqual(true, mixedExecutePriorityTestActionNode.CanExecute());
    }


    [Test]
    /// <summary>
    /// trueとfalseをふくむAnd条件でで有効なものだけをPickし優先度順に整列するかのTest
    /// </summary>
    public void NodeListAndExecuteEfectiveDecideTest() {
        PriorityModeNodeForTest priorityModeNodeForTest = new PriorityModeNodeForTest();
        int maxNodeCount = 10;
        int descPriority = 10;
        //整列検証用データを用意
        // 0 ~ 2 をfalseのみ、　3 ~ 5をtrue のみ 6 以上 を Mixed
        //Priorityは　降順でつける
        // 6 5 4　Pickが期待される 
        for (int i = 0; i < maxNodeCount; i++, descPriority--) {
            PriorityTestActionNode priorityTestActionNode = new PriorityTestActionNode();
            AndEffectiveExecuteManager andEffectiveExecuteManager = new AndEffectiveExecuteManager();
            priorityTestActionNode.SetEffectiveExecuteManager(andEffectiveExecuteManager);
            priorityTestActionNode.Id = i;
            priorityModeNodeForTest.Priority = descPriority;

            if ( i < 3) {
                priorityTestActionNode.ConfigureCannotExecuteData();
            }else if ( i < 6 ) {
                priorityTestActionNode.ConfigureCanExecuteData();
            }else {
                priorityTestActionNode.ConfigureMixedExecuteData();
            }

            priorityModeNodeForTest.AddNode(priorityTestActionNode);
        }

        //実行可能が加味されて優先度順にPickされてるかTest
        priorityModeNodeForTest.OnCatchChooseAlignExecutableNodesRequest();

        List<BehaviourBaseNode> testList = priorityModeNodeForTest.GetExecuteNodeList();
        int count = 0;
        foreach (PriorityTestActionNode node in testList) {
            if (count == 0) {
                Assert.AreEqual(6, node.Id);
            } else if(count == 1) {
                Assert.AreEqual(5, node.Id);
            } else {
                Assert.AreEqual(4, node.Id);
            }
            count++;
        }
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
#endregion
}