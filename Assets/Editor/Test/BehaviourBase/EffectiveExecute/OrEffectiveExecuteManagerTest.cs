using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

namespace Ai.BehaviourBase.EffectiveExecute {

    public class OrEffectiveExecuteManagerTest : OrEffectiveExecuteManger {

        [Test]
        public void TestAndEffectiveExecuteManager() {

            TestEffectiveExecute effectiveExecuteTest_2 = new TestEffectiveExecute();
            effectiveExecuteTest_2.TestNumber = 2;
            base.effectiveExecuteList.Add(effectiveExecuteTest_2);
            Assert.AreEqual(false, CanExecute());

            TestEffectiveExecute effectiveExecuteTest_1 = new TestEffectiveExecute();
            effectiveExecuteTest_1.TestNumber = 1;
            base.effectiveExecuteList.Add(effectiveExecuteTest_1);
            Assert.AreEqual(true, CanExecute());

            TestEffectiveExecute effectiveExecuteTest_4 = new TestEffectiveExecute();
            effectiveExecuteTest_4.TestNumber = 4;
            base.effectiveExecuteList.Add(effectiveExecuteTest_4);
            Assert.AreEqual(true, CanExecute());

            TestEffectiveExecute effectiveExecuteTest_3 = new TestEffectiveExecute();
            effectiveExecuteTest_3.TestNumber = 3;
            base.effectiveExecuteList.Add(effectiveExecuteTest_3);
            Assert.AreEqual(true, CanExecute());

            TestEffectiveExecute effectiveExecuteTest_5 = new TestEffectiveExecute();
            effectiveExecuteTest_5.TestNumber = 5;
            base.effectiveExecuteList.Add(effectiveExecuteTest_5);
            Assert.AreEqual(true, CanExecute());

            TestEffectiveExecute effectiveExecuteTest_6 = new TestEffectiveExecute();
            effectiveExecuteTest_6.TestNumber = 6;
            base.effectiveExecuteList.Add(effectiveExecuteTest_6);
            Assert.AreEqual(true, CanExecute());

        }
    }
}