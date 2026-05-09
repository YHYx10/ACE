using System;
using GTANetworkAPI;
using Whistler.Core;
using Whistler.Entities;
using Whistler.Helpers;
using System.Collections.Generic;
using Whistler.SDK;
using Whistler.PersonalEvents;

namespace Whistler.MP.Arena.AdminEvents
{
    internal class AdminGameEventsManager : Script
    {
        private WhistlerLogger _logger = new WhistlerLogger(typeof(AdminGameEventsManager));
        public (Vector3, string, uint)? CurrentTeleport;

        private static int playerLimit; // Static field for player limit
        private static List<ExtPlayer> tpPlayers = new List<ExtPlayer>(); // List to track players in the event

        [Command("opentp")]
        public void CreateTeleport(ExtPlayer player, string teleportName, int playerLimit)
        {
            try
            {
                // Check if the player is logged in
                if (!player.IsLogged())
                {
                    player.SendChatMessage("You must be logged in to create a teleport event.");
                    return;
                }

                // Check if the player has the correct permissions to use the command
                if (!Group.CanUseAdminCommand(player, "opentp"))
                {
                    player.SendChatMessage("You do not have permission to use this command.");
                    return;
                }

                // Set the static player limit directly
                AdminGameEventsManager.playerLimit = playerLimit;

                // Notify all admins that the event has started with the specified player limit
                Chat.AdminToAll("events_14".Translate(teleportName));

                // Create the teleport (event teleport is set for the player)
                CurrentTeleport = (player.Position + new Vector3(0, 0, 1), teleportName, player.Dimension);

                // Log a message for debugging purposes
                _logger.WriteInfo($"Event '{teleportName}' started with a limit of {playerLimit}.");

                // Send confirmation to the player who created the event
                player.SendChatMessage($"The teleport event '{teleportName}' has started with a player limit of {playerLimit}.");
            }
            catch (Exception ex)
            {
                _logger.WriteError($"Exception at {nameof(CreateTeleport)}: {ex}");
                player.SendChatMessage("An error occurred while starting the event.");
            }
        }







        [Command("closetp")]
public void DeleteTeleport(ExtPlayer player)
{
    try
    {
        if (!player.IsLogged()) return;
        if (!Group.CanUseAdminCommand(player, "opentp")) return;

        // Close the teleport and clear the player list
        tpPlayers.Clear(); // Optionally clear the list of players if you want to reset it
        CurrentTeleport = null;

        // Notify all admins that the teleport event has been closed
        Chat.AdminToAll("The event has been closed.");
    }
    catch (Exception ex)
    {
        _logger.WriteError($"Exception at {nameof(DeleteTeleport)}: {ex}");
    }
}


        [Command("usetp")]
        public void UseTeleport(ExtPlayer player)
        {
            try
            {
                if (!player.IsLogged()) return;
                if (CurrentTeleport == null)
                {
                    Notify.SendError(player, "usetp:close");
                    return;
                }

                if (player.HasData("usedTp") && player.GetData<string>("usedTp") == CurrentTeleport?.Item2)
                {
                    Notify.SendError(player, "usetp:exists");
                    return;
                }

                if (player.Character.Cuffed)
                {
                    Notify.SendError(player, "Frac_180");
                    return;
                }
                
                SafeTrigger.UpdateDimension(player,  CurrentTeleport?.Item3 ?? 0);
                player.ChangePosition(CurrentTeleport?.Item1);
                SafeTrigger.SetData(player, "usedTp", CurrentTeleport?.Item2);
            }
            catch (Exception ex)
            {
                _logger.WriteError($"Exception at {nameof(UseTeleport)}: {ex}");
            }
        }
    }
}