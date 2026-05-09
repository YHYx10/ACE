using Whistler.Entities;
using Whistler.SDK;

namespace Whistler.Core.Models.Rewards
{
    internal class EmptyReward : RewardBase
    {
        public EmptyReward(string name) : base(name, 0)
        {

        }

        public override bool GiveReward(ExtPlayer player)
        {
            GameLog.Admin("system", $"referal_empty({Name})", player.Name);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have received a reward ({Name}) For the transfer system!Contact the administration to get you.", 5000);
            return true;
        }
    }
}
