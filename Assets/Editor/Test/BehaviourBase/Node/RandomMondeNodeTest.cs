using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Ai.BehaviourBase.EffectiveExecute;

namespace Ai.BehaviourBase.Node{


	/// <summary>
	/// Random monde action node.
	/// </summary>
	public class RandomTestActionNode : ActionNode{
		private int id = 0;
		public int ID{
			get { return id; }
			set { id = value; }
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
	}

	/// <summary>
	/// テスト用に本来外部にはみせないexecuteNodeListを返す関数持ちのRandomModeNode
	/// </summary>
	public class RandomMondeNodeTest : RandomModeNode {
		public List<BehaviourBaseNode> GetExecuteNodeList() {
			return base.executeNodeList;
		}

#region TestMethod
		/// <summary>
		/// Tests the random choose.
		/// 何度か実行してランダムにノードが選択されているか人力で確認する
		/// </summary>
		[Test]
		public void TestRandomChoose(){
			RandomMondeNodeTest testRandomNode = new RandomMondeNodeTest();
			int maxNodeNum = 10;
			for(int i = 0; i < maxNodeNum; i++) {
				RandomTestActionNode testRandomActionNode = new RandomTestActionNode();
				testRandomActionNode.ID = i;

				//ランダムのテストなので、全部実行可能にする
				TestEffectiveExecute testEffectiveExecute = new TestEffectiveExecute();
				testEffectiveExecute.TestNumber = 1;
				OrEffectiveExecuteManager orEffectiveExecuteManager = new OrEffectiveExecuteManager();
				orEffectiveExecuteManager.AddEffectiveExecute(testEffectiveExecute);
				testRandomActionNode.SetEffectiveExecuteManager(orEffectiveExecuteManager);

				testRandomNode.AddNode(testRandomActionNode);
			}

			testRandomNode.ChooseAlignExecutableNodes();

			foreach(RandomTestActionNode node in testRandomNode.executeNodeList) {
				Debug.Log(node.ID);
			}
		}
	}
}
#endregion