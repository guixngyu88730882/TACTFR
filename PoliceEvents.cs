// Events/PoliceEvents.cs
using GTA;
using System;
namespace EF.PoliceMod.Core
{
    /// <summary>
    /// Published when the player is aiming at a ped (used to make suspects raise hands).
    /// </summary>
    // PlayerAimedAtPedEvent 事件已禁用，移除或注释掉此处定义

    public struct SuspectCompliantEvent
    {
        // 只传 Ped.Handle，订阅方需自行解析并做 Exists()/IsAlive 检查
        public int SuspectHandle;
        public SuspectCompliantEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }
    public struct SuspectBoardedVehicleEvent
    {
        public int SuspectHandle { get; }

        public SuspectBoardedVehicleEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }

    public class SuspectEscortRequestEvent
    {
        public Ped Suspect { get; }
        public Ped Player { get; }
        public Vehicle Vehicle { get; }
        // Escort 生命周期事件（Executor 专用）

        public class SuspectEscortBeginEvent
        {
            public int SuspectHandle { get; }
            public int PlayerHandle { get; }

            public SuspectEscortBeginEvent(int suspectHandle, int playerHandle)
            {
                SuspectHandle = suspectHandle;
                PlayerHandle = playerHandle;
            }
        }

        // ❌ PlayerAimedAtPedEvent 已被彻底移除
        // 原因：瞄准驱动的行为会在多个系统（Escort / Arrest / Task）中
        // 造成 native Task 并发，导致无日志崩溃。
        // 若未来需要瞄准逻辑，必须通过 StateMachine + Executor 重做。
        // （此处严禁保留同名事件）



        public class SuspectEscortEnterVehicleEvent
        {
            public int SuspectHandle { get; }
            public int VehicleHandle { get; }

            public SuspectEscortEnterVehicleEvent(int suspectHandle, int vehicleHandle)
            {
                SuspectHandle = suspectHandle;
                VehicleHandle = vehicleHandle;
            }
        }


        public SuspectEscortRequestEvent(Ped suspect, Ped player, Vehicle vehicle)
        {
            Suspect = suspect;
            Player = player;
            Vehicle = vehicle;
        }
    }
    public class SuspectExitVehicleEvent
    {
        public int SuspectHandle { get; }

        public SuspectExitVehicleEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }

    public class DutyStartedEvent { }
    public class DutyEndedEvent { }
    public class CaseEndedEvent { }
    public class LockTargetClearRequestedEvent { }
    public class SuspectFollowRequestEvent { }

    /// <summary>
    /// 调度菜单：请求小队对当前锁定嫌疑人进行“逮捕压制”。
    /// </summary>
    public class DispatchArrestSuppressRequestedEvent { }

    /// <summary>
    /// 呼叫小队（2人）事件：用于音效/提示等低耦合系统订阅。
    /// </summary>
    public class OfficerSquadSummonRequestedEvent { }

    /// <summary>
    /// 巡逻模式（自由行动）切换事件：由终端/菜单发布。
    /// </summary>
    public class TogglePatrolModeEvent { }

    public class TargetLockedEvent
    {
        public int TargetHandle { get; }

        public TargetLockedEvent(int targetHandle)
        {
            TargetHandle = targetHandle;
        }
    }

    public class SuspectResistingEvent
    {
        public int SuspectHandle { get; }
        public SuspectResistingEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }


    public class SuspectFollowOnFootRequestEvent
    {
        public int SuspectHandle { get; }
        public int PlayerHandle { get; }

        public SuspectFollowOnFootRequestEvent(int suspectHandle, int playerHandle)
        {
            SuspectHandle = suspectHandle;
            PlayerHandle = playerHandle;
        }
    }


    public class TargetLostEvent { }

    public class ArrestRequestedEvent { }

    public class SuspectArrestedEvent
    {
        public int SuspectHandle { get; }

        public SuspectArrestedEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }

    /// <summary>
    /// 成功拘捕后触发“上拷演出”（仅 CuffAndLead 风格使用）。
    /// </summary>
    public struct CuffingSequenceRequestedEvent
    {
        public int SuspectHandle { get; }
        public CuffingSequenceRequestedEvent(int suspectHandle)
        {
            SuspectHandle = suspectHandle;
        }
    }

    public class DeliverRequestedEvent { }

    // E 键：押送交互（上下车）
    public class EscortVehicleInteractEvent
    {
        public int PlayerHandle { get; }

        public EscortVehicleInteractEvent(int playerHandle)
        {
            PlayerHandle = playerHandle;
        }
    }
}

