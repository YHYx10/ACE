using GTANetworkAPI;
using Whistler.Entities;
using Whistler.Helpers;
using Whistler.SDK;

namespace Whistler.Core
{
    class AdminSP : Script
    {        
        public static void Spectate(ExtPlayer player, ExtPlayer target)
        {
            if (!player.IsLogged()) return;
            if (target == null) return;
            if (target == player)
            {
                Chat.SendTo(player, "You can't follow yourself");
                return;
            }
            if (!target.IsLogged())
            {
                Chat.SendTo(player, "The ID player has not yet been authorized");
                return;
            }
            if (target.Session.SPActivated)
            {
                Chat.SendTo(player, "This administrator cannot now be followed.");
                return;
            }

            if (!player.Session.SPActivated)
            {
                player.Session.SPActivated = true;
                player.Session.SPPosition = player.Position;
                player.Session.SPDimension = player.Session.Dimension;
                player.Session.SPInvisible = player.Session.Invisible;

                Admin.SetInvisible(player, true);
            }
            else SafeTrigger.ClientEvent(player, "spmode", null, false);

            SafeTrigger.UpdateDimension(player, target.Dimension);
            player.Session.SPClient = target.Value;
            player.ChangePosition(new Vector3(target.Position.X, target.Position.Y, (target.Position.Z + 3)));
            SafeTrigger.ClientEvent(player, "spmode", target, true);
            Chat.SendTo(player, $"You are watching {target.Name} [ID: {target.Character.UUID}].");
        }

        [RemoteEvent("UnSpectate")]
        public static void RemoteUnSpectate(ExtPlayer player)
        {
            if (!player.IsLogged()) return;
            if (!Group.CanUseAdminCommand(player, "sp")) return;
            UnSpectate(player);
        }
        
        public static void UnSpectate(ExtPlayer player)
        {
            if (!player.IsLogged()) return;
            if (!player.Session.SPActivated)
            {
                Chat.SendTo(player, "You are not in observer mode ");
                return;
            }

            player.Session.SPActivated = false;
            player.Session.SPClient = -1;
            SafeTrigger.ClientEvent(player, "spmode", null, false);
            SafeTrigger.UpdateDimension(player, player.Session.SPDimension);
            player.ChangePosition(player.Session.SPPosition);
            Chat.SendTo(player, "You left the spectate mode");

            NAPI.Task.Run(() =>
            {
                Admin.SetInvisible(player, player.Session.SPInvisible);
            }, 500);
        }
    }
}
