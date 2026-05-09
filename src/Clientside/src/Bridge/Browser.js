import { EventEmitter } from '../Events/EventEmitter';
import { Client } from './Client';

class BrowserImpl extends EventEmitter {
    constructor(url) {
        super();

        this.url = url;
        this.isReady = false;
        this.browser = null;
        this.opened = false;
        this.inventoryOpened = false;
        this.curPage = '';
        this.queue = [];
        this.debug = false;

        Client.on('browserDomReady', (browser) => {
            if (this.browser) {
                this.ready();
            }
        });

        Client.on('authready', () => {
            this.init();
        });

        Client.on('guiClose', () => {
            this.close();
        });

        Client.on('gui:dispatch', (func, data) => {
            this.dispatch(func, data);
        });

        mp.events.addDataHandler('InDeath', (entity, isDeath) => {
            if (entity === mp.players.local && isDeath === true) {
                if (this.opened) {
                    this.close();
                }
            }
        });
    }

    init() {
        if (this.browser || global.gui) {
            return;
        }
        this.browser = mp.browsers.new(this.url);
    }

    ready() {
        if (this.isReady) {
            return;
        }
        mp.gui.chat.show(false);
        this.browser.markAsChat();
        this.isReady = true;

        if (this.queue.length > 0) {
            this.queue.forEach(code => {
                this.browser.execute(code);
            });

            this.queue = [];
        }

        this.emit('ready');
    }

    __do_not_use_is_death() {
        const value = mp.players.local.getVariable('InDeath');

        if (value === undefined) {
            return false;
        }

        return !!value;
    }

    isOpened() {
        return (!this.isReady || this.opened || this.inventoryOpened || this.__do_not_use_is_death());
    }

    setOpened(toggle) {
        this.opened = toggle;
    }

    openPage(page) {
        if (this.isOpened()) {
            return false;
        }

        this.emit('open', page);

        this.opened = true;
        this.curPage = page;

        return true;
    }

    close() {
        if (!this.isReady) {
            return;
        }

        this.emit('close');

        this.opened = false;
        this.curPage = '';
    }

    execute(code) {
        if (!this.isReady) {
            this.queue.push(code);
        } else {
            this.browser.execute(code);
        }
    }
}

export const Browser = new BrowserImpl('package://gui/index.html');
