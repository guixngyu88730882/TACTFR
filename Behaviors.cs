using EF.PoliceMod;
using EF.PoliceMod.Core;
using GTA;

public class RestrainedBehavior
{
    private Ped _ped;

    public RestrainedBehavior(Ped ped)
    {
        _ped = ped;
        Apply();
    }

    private void Apply()
    {
        if (_ped == null || !_ped.Exists()) return;


        try
        {
            try { _ped.Task.ClearAll(); } catch { }
            _ped.BlockPermanentEvents = true;
            try { _ped.Task.HandsUp(-1); } catch { }
        }
        catch { }
    }
}
