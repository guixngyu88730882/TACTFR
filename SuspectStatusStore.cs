using EF.PoliceMod.Core;
using System.Collections.Generic;

namespace EF.PoliceMod.Systems
{
    public static class SuspectStatusStore
    {
        private static readonly HashSet<int> _compliantSuspects = new HashSet<int>();
        private static readonly HashSet<int> _resistingSuspects = new HashSet<int>();
        private static readonly HashSet<int> _busySuspects = new HashSet<int>();

        public static void MarkCompliant(int handle)
        {
            if (handle <= 0) return;
            _resistingSuspects.Remove(handle);
            _compliantSuspects.Add(handle);
        }

        public static void MarkResisting(int handle)
        {
            if (handle <= 0) return;
            _compliantSuspects.Remove(handle);
            _resistingSuspects.Add(handle);
        }

        public static void MarkBusy(int handle)
        {
            if (handle <= 0) return;
            _busySuspects.Add(handle);
        }

        public static void ClearBusy(int handle)
        {
            if (handle <= 0) return;
            _busySuspects.Remove(handle);
        }

        public static bool IsCompliant(int handle)
        {
            if (handle <= 0) return false;
            return _compliantSuspects.Contains(handle);
        }

        public static bool IsResisting(int handle)
        {
            if (handle <= 0) return false;
            return _resistingSuspects.Contains(handle);
        }

        public static bool IsBusy(int handle)
        {
            if (handle <= 0) return false;
            return _busySuspects.Contains(handle);
        }

        public static void Clear(int handle)
        {
            if (handle <= 0) return;
            _compliantSuspects.Remove(handle);
            _resistingSuspects.Remove(handle);
            _busySuspects.Remove(handle);
        }

        public static void ClearAll()
        {
            _compliantSuspects.Clear();
            _resistingSuspects.Clear();
            _busySuspects.Clear();
        }
    }
}
