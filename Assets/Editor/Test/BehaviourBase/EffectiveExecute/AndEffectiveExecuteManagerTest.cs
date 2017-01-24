using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ai.BehaviourBase.EffectiveExecute {

	/// <summary>
	/// Test用にEffectiveExecuteを継承したクラス TestNumberが奇数なら実行可能となります
	/// </summary>
	public class TestEffectiveExecute : EffectiveExecute {
		private uint testNumber = 0;
		public uint TestNumber {
			set { testNumber = value; }
		}

		/// <summary>
		/// 有効かどうかを判定する処理を記載するメソッド
		/// Test用にtestNumberが奇数なら有効、偶数なら無効にする(0も無効)
		/// </summary>
		public override bool DecideIsEffecive(){
			if (testNumber % 2 == 1) {
				return true;
			} else {
				return false;
			}
		}
	}

	public class AndEffectiveExecuteManagerTest : AndEffectiveExecuteManager {
		
		[Test]
		public void TestAndEffectiveExecuteManager(){
			TestEffectiveExecute effectiveExecuteTest_1 = new TestEffectiveExecute();
			effectiveExecuteTest_1.TestNumber = 1;
			base.effectiveExecuteList.Add(effectiveExecuteTest_1);
			Assert.AreEqual(true, CanExecute());

			TestEffectiveExecute effectiveExecuteTest_3 = new TestEffectiveExecute();
			effectiveExecuteTest_3.TestNumber = 3;
			base.effectiveExecuteList.Add(effectiveExecuteTest_3);
			Assert.AreEqual(true, CanExecute());

			TestEffectiveExecute effectiveExecuteTest_5 = new TestEffectiveExecute();
			effectiveExecuteTest_5.TestNumber = 5;
			base.effectiveExecuteList.Add(effectiveExecuteTest_5);
			Assert.AreEqual(true, CanExecute());

			TestEffectiveExecute effectiveExecuteTest_2 = new TestEffectiveExecute();
			effectiveExecuteTest_2.TestNumber = 2;
			base.effectiveExecuteList.Add(effectiveExecuteTest_2);
			Assert.AreEqual(false, CanExecute());
		}
	}
}

