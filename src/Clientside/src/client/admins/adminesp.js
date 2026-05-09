import '../global';

function syncAdminHudState() {
    try {
        if (!global.gui || !global.gui.isReady || typeof global.gui.setData !== 'function') return;

        const isAuthenticatedAdmin = Number(global.LOCAL_ADMIN_LVL || 0) >= 1;
        global.gui.setData('hud/updateAdminData', isAuthenticatedAdmin);
        if (!isAuthenticatedAdmin) global.gui.setData('hud/setReportsCount', 0);
    } catch (e) {
        if (global.sendException) mp.serverLog(`Error in syncAdminHudState: ${e.name}\n${e.message}\n${e.stack}`);
    }
}

mp.events.add('setadminlvl', (newLvl) => {
    global.LOCAL_ADMIN_LVL = Number(newLvl || 0);
    syncAdminHudState();
});

mp.events.add('gui:ready', syncAdminHudState);

const MAX_SCALE = 0.33;
const MIN_SCALE = 0.26;
const EXCRETION_PLAYER_LEVEL = 3;
const HIDE_ADMIN_LEVEL = 7;

if (global.Keys && global.Keys.Key_F12) {
    mp.keys.bind(global.Keys.Key_F12, false, function () {
        if (!isLoggedIn() || getLocalAdminLevel() < 1 && getLocalVariable('IS_MEDIAHELPER', false) !== true) {
            return;
        }

        if (global.esptoggle === 3) {
            global.esptoggle = 0;
        } else {
            global.esptoggle++;
        }
    });
}

mp.events.add('render', () => {
    if (!isLoggedIn() || getLocalAdminLevel() < 1) {
        return;
    }

    if (global.esptoggle >= 1) {
        let position;
        const pos = mp.players.local.position;
        let distance;
        if (global.esptoggle === 1 || global.esptoggle === 3) {
            mp.players.forEachInStreamRange(player => {
                if (player.handle !== 0 && player !== mp.players.local) {
                    const isInvisible = global.getVariable(player, 'INVISIBLE', false);
                    const adminLevel = global.getVariable(player, 'ALVL', 0);
                    const adminLevelFalse = getLocalVariable('ALVL', false);

                    if (!isInvisible || adminLevel < HIDE_ADMIN_LEVEL || adminLevelFalse >= adminLevel) {
                        position = player.position;
                        distance =
                            mp.game.gameplay.getDistanceBetweenCoords(pos.x,
                                pos.y,
                                pos.z,
                                position.x,
                                position.y,
                                position.z,
                                true,
                            );

                        let correctScale = MAX_SCALE - distance / 2000;

                        if (correctScale < MIN_SCALE) {
                            correctScale = MIN_SCALE;
                        }

                        let text = '';
                        const lvl = global.getVariable(player, 'lvl', 0);
                        const color = getColorNameTag(player);
                        const voiceColor = player.isVoiceActive ? (lvl >= EXCRETION_PLAYER_LEVEL ? '~g~' : '~HUD_COLOUR_REDLIGHT~') : color;
                        const hpNarmor = `[${player.getHealth()}|${player.getArmour()}]`;
                        const fraction = global.getVariable(player, 'fraction', 0);
                        const family = global.getVariable(player, 'familyname', '-');
                        const id = global.getVariable(player, 'C_ID', `${player.remoteId}~`);
                        if (global.getVariable(player, 'InDeath', false)) {
                            text += (global.getVariable(player, 'ALVL', 0) > 0 ? '~p~' : '~r~') + 'Dead ';
                        }
                        text += voiceColor + `#${id} \n`;
                        text += `Fr: ${fraction}, Fam: ${family}\n`;
                        text += color + player.name + ` ${hpNarmor}`;

                        mp.game.graphics.drawText(text,
                            [position.x,
                                position.y,
                                position.z + 1.2],
                            {
                                scale: [correctScale,
                                    correctScale],
                                outline: true,
                                color: [255,
                                    255,
                                    255,
                                    255],
                                font: 4,
                            },
                        );
                    }
                }
            });
        }
        if (global.esptoggle === 2 || global.esptoggle === 3) {
            mp.vehicles.forEachInStreamRange(vehicle => {
                if (vehicle.handle !== 0 /* && vehicle !== Me.entity */) {
                    position = vehicle.position;
                    distance =
                        mp.game.gameplay.getDistanceBetweenCoords(pos.x,
                            pos.y,
                            pos.z,
                            position.x,
                            position.y,
                            position.z,
                            true,
                        );
                    let correctScale = MAX_SCALE - distance / 2000;
                    if (correctScale < MIN_SCALE) {
                        correctScale = MIN_SCALE;
                    }
                    mp.game.graphics.drawText(mp.game.vehicle.getDisplayNameFromVehicleModel(vehicle.model) + ` (${vehicle.getNumberPlateText()} | ${vehicle.remoteId})` + `\n${global.getVariable(
                        vehicle,
                        'HOLDERNAME',
                        '',
                    )}`,
                    [position.x,
                        position.y,
                        position.z - 0.5],
                    {
                        scale: [correctScale,
                            correctScale],
                        outline: true,
                        color: [255,
                            255,
                            255,
                            255],
                        font: 4,
                    },
                    );
                }
            });
        }
    }
});

mp.events.add('render', () => {
    if (!isLoggedIn() || getLocalVariable('IS_MEDIAHELPER', false) !== true || getLocalAdminLevel() > 0) {
        return;
    }

    if (global.esptoggle >= 1) {
        let position;
        const pos = mp.players.local.position;
        let distance;
        if (global.esptoggle === 1 || global.esptoggle === 3) {
            mp.players.forEachInStreamRange(player => {
                if (player.handle !== 0 && player !== mp.players.local) {
                    const invis = global.getVariable(player, 'INVISIBLE', false);
                    const admlvl = global.getVariable(player, 'ALVL', 0);
                    if (!invis || admlvl < HIDE_ADMIN_LEVEL) {
                        position = player.position;
                        distance =
                            mp.game.gameplay.getDistanceBetweenCoords(pos.x,
                                pos.y,
                                pos.z,
                                position.x,
                                position.y,
                                position.z,
                                true,
                            );
                        let correctScale = MAX_SCALE - distance / 2000;
                        if (correctScale < MIN_SCALE) {
                            correctScale = MIN_SCALE;
                        }

                        const color = getColorNameTag(player);
                        const voiceColor = player.isVoiceActive ? '~HUD_COLOUR_REDLIGHT~' : color;
                        const text = voiceColor + `#${player.remoteId}`;

                        mp.game.graphics.drawText(text,
                            [position.x,
                                position.y,
                                position.z + 1.2],
                            {
                                scale: [correctScale,
                                    correctScale],
                                outline: true,
                                color: [255,
                                    255,
                                    255,
                                    255],
                                font: 4,
                            },
                        );
                    }
                }
            });
        }
    }
});

function getColorNameTag(player) {
    if (global.getVariable(player, 'ALVL', 0) > 0) {
        return '~r~';
    }
    if (global.getVariable(player, 'IS_MEDIA', false)) {
        return '~b~';
    }
    if (global.getVariable(player, 'IS_MEDIAHELPER', false)) {
        return '~HUD_COLOUR_NET_PLAYER23~';
    }
    const lvl = global.getVariable(player, 'lvl', 0);
    if (lvl < EXCRETION_PLAYER_LEVEL) {
        return '~HUD_COLOUR_NET_PLAYER31~';
    }
    return '~w~';
}

function isLoggedIn() {
    return global.loggedin === true;
}

function getLocalVariable(name, fallback) {
    try {
        const value = mp.players.local.getVariable(name);
        return value === undefined ? fallback : value;
    } catch (e) {
        return fallback;
    }
}

function getLocalAdminLevel() {
    const syncedLevel = getLocalVariable('ALVL', undefined);
    if (syncedLevel !== undefined) {
        return syncedLevel;
    }
    return global.LOCAL_ADMIN_LVL || 0;
}
