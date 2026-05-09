using GTANetworkAPI;

namespace Whistler.Entities
{
    public class ExtPed : Ped
    {
        public int OwneruUid { get; set; } = -1;
        public ExtPed(NetHandle handle) : base(handle)
        {

        }
    }
}
