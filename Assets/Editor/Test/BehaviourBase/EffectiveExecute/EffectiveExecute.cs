namespace Ai.BehaviourBase.EffectiveExecute {
    public abstract class EffectiveExecute {
        protected bool isEffective = false;
        public bool IsEffective {
            get { return isEffective; }
        }

        /// <summary>
        /// サブクラスで有効かどうかを判定する処理を記載するメソッド
        /// </summary>
        public abstract bool DecideIsEffecive();
    }
}

