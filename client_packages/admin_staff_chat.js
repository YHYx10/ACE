// packages/admin_staff_chat.js

function isAdmin(player) {
  const levels = ['adminLevel', 'ALVL', 'adminLvl', 'adminlvl', 'staffLevel'];
  const flags = ['isAdmin', 'admin', 'isStaff', 'staff'];

  for (const key of levels) {
    const lvl = Number(player.getVariable(key) || 0);
    if (lvl > 0) return true;
  }

  for (const key of flags) {
    const flag = player.getVariable(key);
    if (flag === true || flag === 1 || flag === '1' || flag === 'true') return true;
  }

  return false;
}

mp.events.add('admin:staff:send', (player, msg) => {
  try {
    if (!player || typeof msg !== 'string') return;
    msg = msg.trim();
    if (!msg.length) return;
    if (!isAdmin(player)) return;

    const name = player.name || 'Unknown';
    const id = player.id;
    const ts = Date.now();

    mp.players.forEach((p) => {
      if (!p || !isAdmin(p)) return;
      p.call('admin:staff:push', [name, id, msg, ts]);
    });
  } catch (e) {
    // Keep chat failures isolated from the rest of the server package.
  }
});
