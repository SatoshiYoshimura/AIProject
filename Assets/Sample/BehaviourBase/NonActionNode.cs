using System.Collections;using System.Collections.Generic;using UnityEngine;namespace Ai.BehaviourBase {    public abstract class NonActionNode : BehaviourBaseNode {        protected List<BehaviourBaseNode> nodeList = null;        private bool isRoot = false;

        protected abstract void DoExecute();        protected abstract void DecideEffectiveNodes();    }}