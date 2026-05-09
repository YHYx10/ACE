import { Authentication } from './Authentication';
import { Gui } from '../Bridge/Gui';
import { SoundPlayer } from '../Gui/SoundPlayer';
import { Server } from '../Bridge/Server';
import { Me } from '../Entity/Player';
import { Browser } from '../Bridge/Browser';
import { Client } from '../Bridge/Client';

import { Hud } from '../Client/Hud';

mp.game.cam.renderScriptCams(false, true, 0, true, false);

Client.on('auth:startReg', (name) => {
    Gui.setData('setLoadScreen', false);
    Gui.setData('auth/setSocialClub', JSON.stringify({ name }));
    Gui.setData('auth/setCurrentTab', JSON.stringify({ page: 'CreateNewAccountTab' }));
    Browser.close();
    Browser.openPage('Auth');
});

/**
 * @param {string} login
 */
Client.on('auth:startAuth', (login) => {
    Gui.setData('setLoadScreen', false);
    Browser.close();
    Gui.setData('auth/setSocialClub', JSON.stringify({ name: login }));
    Gui.setData('auth/setCurrentTab', JSON.stringify({ page: 'LoginTab' }));
    Browser.openPage('Auth');
});

Client.on('auth:startCreateCharacter', () => {
    Gui.setData('setLoadScreen', false);
    Browser.close();
    Gui.setData('auth/setCurrentTab', JSON.stringify({ page: 'Customization' }));
    Browser.openPage('Auth');
});

Client.on('auth:character:select', (data, coins, slots) => {
    Gui.setData('setLoadScreen', false);
    Browser.close();
    Gui.setData('characterSelect/setData', data);
    Gui.setData('characterSelect/setCoins', coins);
    Gui.setData('characterSelect/setSlots', slots);
    Browser.openPage('CharacterSelect');
});

Client.on('auth:spawn:select', (data) => {
    Gui.setData('setLoadScreen', false);

    const pos = JSON.parse(data[1]);
    const {
        streetName,
        crossingRoad,
    } = mp.game.pathfind.getStreetNameAtCoord(pos.x, pos.y, pos.z, 0, 0);
    data[1] = mp.game.ui.getStreetNameFromHashKey(streetName);

    Browser.close();
    Gui.setData('spawnSelect/setData', JSON.stringify(data));
    Browser.openPage('SpawnSelect');
});

Client.on('auth:save:pass', (login, plainPassword, save) => {
    checkAuthStorage();

    if (save) {
        if (mp.storage.data.auth.login !== login || mp.storage.data.auth.plainPassword !== plainPassword || !mp.storage.data.auth.save) {
            mp.storage.data.auth.login = login;
            mp.storage.data.auth.plainPassword = plainPassword;
            mp.storage.data.auth.save = true;
            mp.storage.flush();
        }
    } else {
        if (mp.storage.data.auth.login !== '' || mp.storage.data.auth.plainPassword !== '' || mp.storage.data.auth.save) {
            mp.storage.data.auth.login = '';
            mp.storage.data.auth.plainPassword = '';
            mp.storage.data.auth.save = false;
            mp.storage.flush();
        }
    }
});

Client.on('auth:charCreated', function (name, surname) {
    Server.trigger('newchar', name, surname);
});

Client.on('auth:doSpawn', spawn);

function spawn() {
    Gui.setData('setLoadScreen', true);
    mp.game.cam.doScreenFadeOut(0);
    Browser.close();
    setTimeout(() => {
        Hud.hide();
    }, 10);

    setTimeout(() => {
        Gui.setData('setLoadScreen', false);
        mp.game.cam.doScreenFadeIn(1700);
        Hud.show();
        global.checkFarm();
        if (global.characterEditor && Browser.curPage !== 'Customization') {
            Browser.close();
            Browser.openPage('Customization');
        }
    }, 3000);

    setTimeout(() => {
        if (global.characterEditor && Browser.curPage !== 'Customization') {
            Browser.close();
            Browser.openPage('Customization');
        }
    }, 6000);

    mp.discord.update('Playing Astro RolePlay', 'discord.gg/gta5astrorp');

    SoundPlayer.stop();
    Hud.show();
    Authentication.login();
    mp.game.player.setHealthRechargeMultiplier(0);

    Gui.setData('setBackground', '0');
    global.activateAntiCheat();
    setTimeout(() => {
        global.chw();
        mp.events.call('switchTime', 0);
        Me.unfreeze();
        Browser.close();
    }, 500);
}

function checkAuthStorage() {
    if (!mp.storage.data.hasOwnProperty('auth')) {
        mp.storage.data.auth = {
            login: '',
            plainPassword: '',
            save: false,
        };
    }
}
