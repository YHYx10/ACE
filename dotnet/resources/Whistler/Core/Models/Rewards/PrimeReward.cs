using Whistler.Entities;
using Whistler.SDK;

namespace Whistler.Core.Models.Rewards
{
    internal class PrimeReward : RewardBase
    {

        public PrimeReward(int days) : base("Prime", days)
        {
        }

        public override bool GiveReward(ExtPlayer player)
        {
            player.AddPrime(Amount);
            GameLog.Admin("system", $"referal_prime({Amount})", player.Name);
            Notify.Send(player, NotifyType.Success, NotifyPosition.BottomCenter, $"You got Prime on{Amount} Days for the transfer system.", 5000);
            return true;
        }
    }
}
