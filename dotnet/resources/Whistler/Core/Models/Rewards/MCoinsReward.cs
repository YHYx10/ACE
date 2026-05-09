using Whistler.Entities;
using Whistler.SDK;

namespace Whistler.Core.Models.Rewards
{
    internal class MCoinsReward : RewardBase
    {
        public MCoinsReward(int amount) : base("MCoins", amount)
        {

        }

        public override bool GiveReward(ExtPlayer player)
        {
            player.AddMCoins(Amount);
            GameLog.Admin("system", $"referal_mcoins({Amount})", player.Name);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have received {Amount} Donat currency for the transfer system.", 5000);
            return true;
        }
    }
}
