using Whistler.Entities;
using Whistler.SDK;

namespace Whistler.Core.Models.Rewards
{
    internal class MoneyReward : RewardBase
    {
        public MoneyReward(int amount) : base("Money", amount)
        {

        }

        public override bool GiveReward(ExtPlayer player)
        {
            bool success = MoneySystem.Wallet.MoneyAdd(player.Character, Amount, "Referal system");
            if (success) GameLog.Admin("system", $"referal_money({Amount})", player.Name);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You have {Amount}$ For the transfer system",5000);
            return success;
        }
    }
}
