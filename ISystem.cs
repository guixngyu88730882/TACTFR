using System;

namespace EF.PoliceMod.Core.Contracts
{
    public interface ISystem
    {
        void Initialize();
        void Tick();
        void Shutdown();
    }
}
