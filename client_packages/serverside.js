mp.events.add('banOrJailPlayer', (player, targetPlayer, adminName, reason, daysLeft, banDate) => {
    player.call('showBanAnimation', [targetPlayer, adminName, reason, daysLeft, banDate]);
});