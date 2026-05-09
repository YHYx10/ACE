import { Gui } from '../Bridge/Gui';
import { Client } from '../Bridge/Client';
import { HotKeyManager } from '../Client/HotKeys/HotKeyManager';
import { Browser } from '../Bridge/Browser';
import { Cursor } from './Cursor';
import { Phone } from '../Client/Phone/Phone';
import { Me, Player } from '../Entity/Player';
import { Keys } from '../Client/keys';

class CommandMenuImpl {
    constructor() {
        this.focus = false;

        // Migration
        if (typeof mp.storage.data.actions.commandMenu === 'undefined' ||
            mp.storage.data.actions.commandMenu !== Keys.Key_ALT
        ) {
            mp.storage.data.actions.commandMenu = Keys.Key_ALT;
            mp.storage.flush();

            HotKeyManager.loadActions();
        }
        // End migration

        this.opened = false;

        this.closeOnEscape = () => {
            this.close();
        };

        Client.on('gui:ready', () => {
            HotKeyManager.get('commandMenu').on('down', this.open.bind(this));
            HotKeyManager.get('commandMenu').on('up', () => {
                if (!this.focus) {
                    this.close();
                } else {
                    mp.keys.bind(Keys.Key_ESCAPE, false, this.closeOnEscape);
                }
            });
        });

        Client.on('commandMenu:focus', () => {
            this.focus = true;
        });

        Client.on('commandMenu:close', () => {
            this.close();
        });
    }

    isOpen() {
        return this.opened;
    }

    players() {
        const list = [];

        mp.players.forEach(playerMp => {
            const player = new Player(playerMp);

            const item = [];
            const uuid = player.getVariable('C_ID');

            if (uuid >= 0) {
                item.push(uuid);
            } else {
                return;
            }

            item.push(player.name);
            item.push(global.getVariable(player, 'C_LVL', 0));
            item.push(global.getVariable(player, 'Ping', -1));
            list.push(item);
        });

        return list;
    }

    open() {
        if (Me.adminLevel < 2) {
            return;
        }

        if (this.isOpen()) {
            return;
        }

        if (mp.game.ui.isPauseMenuActive() || Phone.isOpen() || Me.isInAction() || global.fishingMiniGame) {
            return;
        }

        if (Browser.isOpened()) {
            return;
        }

        this.opened = true;
        Browser.opened = true;
        Gui.setData('commandMenu/setAdminLevel', Me.adminLevel);
        Gui.setData('commandMenu/setPlayers', JSON.stringify(this.players()));
        Gui.setData('commandMenu/clearParams');
        Gui.setData('commandMenu/setModal', null);
        Gui.setData('setCommandMenuActive', true);
        Cursor.setVisibility(true);
    }

    close() {
        if (!this.isOpen()) {
            return;
        }

        if (Me.isInAir()) {
            return;
        }

        Browser.opened = false;
        Gui.setData('commandMenu/clearParams');
        Gui.setData('commandMenu/setModal', null);
        Gui.setData('setCommandMenuActive', false);
        Cursor.setVisibility(false);
        this.opened = false;
        this.focus = false;
        mp.keys.unbind(Keys.Key_ESCAPE, false, this.closeOnEscape);
    }
}

export const CommandMenu = new CommandMenuImpl();
