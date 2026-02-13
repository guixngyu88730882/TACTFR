using System;

namespace EF.PoliceMod.Core
{
    /// <summary>
    /// 嫌疑人唯一状态中枢
    /// ⚠️ 全项目只允许这里修改 SuspectState
    /// </summary>
    public class SuspectStateHub
    {
        private readonly StateMachine<SuspectState> _stateMachine;

        public SuspectState CurrentState => _stateMachine.CurrentState;

        public event Action<SuspectState, SuspectState> OnStateChanged;

        public SuspectStateHub()
        {
            _stateMachine = new StateMachine<SuspectState>(SuspectState.None);
            _stateMachine.OnStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(SuspectState oldState, SuspectState newState)
        {
            OnStateChanged?.Invoke(oldState, newState);
        }

        /// <summary>
        /// 唯一合法的状态修改入口
        /// </summary>
        public void ChangeState(SuspectState newState)
        {
            _stateMachine.ChangeState(newState);
        }

        /// <summary>
        /// 供外部安全查询（只读）
        /// </summary>
        public bool Is(SuspectState state)
        {
            return _stateMachine.CurrentState == state;
        }
    }
}
