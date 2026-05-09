import { Cursor } from './Cursor';
import { censore } from './Censore';
import { Keys } from '../Client/keys';
import { Player } from '../Entity/Player';
import { Browser } from '../Bridge/Browser';
import { Client } from '../Bridge/Client';

class ChatImpl {
    constructor() {
        this.visible = false;
        this.mediaMute = false;

        Client.on('chat:api:action', (type, msg, fromId, toId, realFromId = -1, realToId = -1) => {
            if (this.mediaMute && (type === 0 || type === 1 || type === 2 || type === 10)) {
                return;
            }

            const playerFrom = Player.fromId(fromId);
            const fromText = playerFrom.name.replace('_', ' ');

            let toText = '';

            if (toId !== null) {
                const playerTo = mp.players.atRemoteId(toId);

                toText = playerTo ? playerTo.name.replace('_', ' ') : '';
            }

            if (mp.storage.data.mainSettings.muteLowLevel) {
                if (playerFrom.level < mp.storage.data.mainSettings.muteLowLevelValue) {
                    return;
                }
            }

            const isFriend = global.iKnowThisPlayer(playerFrom);
            Chat.push(type, msg, fromId, fromText, toId, toText, isFriend, realFromId, realToId);
        });

        Client.on('chat:api:advert', (type, redactorId, msg, from, sim) => {
            if (this.mediaMute) {
                return;
            }

            const redactor = mp.players.atRemoteId(redactorId);

            Chat.pushAdvert(type, redactor ? redactor.name.replace('_', ' ') : 'Unknown', msg, from, sim);
        });

        Client.on('media:mute:state', (state) => {
            this.mediaMute = state;

            if (this.mediaMute) {
                Chat.clear();
                mp.events.call('notify', 4, 9, 'media:mute:on:self', 3000);
            } else {
                mp.events.call('notify', 4, 9, 'media:mute:off:self', 3000);
            }
        });

        Client.on('chat:api:clear', () => {
            Chat.clear();
        });

        mp.keys.bind(Keys.Key_T, false, () => {
            if (Chat.isVisible() || Browser.opened) {
                return;
            }

            Chat.show();
        });

        Client.on('cahat:api:disable', () => {
            Chat.hide();
            Browser.close();
        });
    }

    show() {
        if (!this.visible) {
            this.visible = true;

            Browser.opened = true;
            Cursor.show();
            Browser.execute('chatAPI.enable(true)');
        }
    }

    hide() {
        if (this.visible) {
            this.visible = false;

            Browser.opened = false;
            Cursor.hide();
            Browser.execute('chatAPI.enable(false)');
        }
    }

    clear() {
        Browser.execute('chatAPI.clear()');
    }

    isVisible() {
        return this.visible;
    }

    push(type, message, id, from, toId = -1, to = '', friend, realId = -1, realToId = -1) {
        if (mp.storage.data.mainSettings.censore === true) {
            message = censore(message);
        }

        Browser.execute(`chatAPI.push(${type}, '${message}', ${id}, '${from}', ${toId}, '${to}', ${friend}, ${realId}, ${realToId})`);
    }

    pushAdvert(type, redactor, message, from, sim) {
        if (mp.storage.data.mainSettings.censore === true) {
            message = censore(message);
        }

        Browser.execute(`chatAPI.push(${type}, '${redactor}', '${message}', '${from}','${sim}', '')`);
    }
}

export const Chat = new ChatImpl();
