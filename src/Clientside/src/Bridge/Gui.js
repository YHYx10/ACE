import { Client } from './Client';
import { Browser } from './Browser';
import { ulid } from '../Utils/Ulid';
import { EventEmitter } from '../Events/EventEmitter';

class GuiImpl {
    constructor() {
        this.emitter = new EventEmitter();

        Browser.on('ready', () => {
            mp.events.call('gui:ready');

            this.setData('hud/updateData', JSON.stringify({
                name: 'id',
                value: mp.players.local.remoteId,
            }));

            this.setData('optionsMenu/setSettings', JSON.stringify(mp.storage.data.mainSettings));
        });

        Client.on('gui:setData', (func, data) => {
            this.setData(func, data);
        });
    }

    updateLang(lang) {
        this.setData('localiazation/setLang', `'${lang}'`);
    }

    setData(topic, data) {
        Browser.execute(`setData('${topic}', ${data})`);
    }

    dispatch(topic, data) {
        Browser.execute(`dispatch('${topic}', ${data})`);
    }

    /**
     * @param callName
     * @param {(...any) => Promise} handler
     */
    answer(callName, handler) {
        Client.on(`async:${callName}`, (id, ...data) => {
            handler(...data)
                .then(() => {
                    Browser.execute(`clientResponse("${id}")`);
                })
                .catch(error => {
                    Browser.execute(`clientResponse("${id}", ${JSON.stringify({ error })})`);
                });
        });
    }

    /**
     * @param {string} name
     * @param {Array<any>} args
     * @returns {Promise<any>}
     */
    async call(name, ...args) {
        const id = ulid();

        return new Promise((resolve, reject) => {
            let responded = false;

            this.emitter.once(id, (...args) => {
                if (args.length > 0) {
                    const response = args[0];

                    if (typeof response === 'object' && typeof response.error !== 'undefined') {
                        if (!responded) {
                            reject(...args);
                            responded = true;
                        } else {
                            Client.log('Delayed error response', name, id, ...args);
                        }
                    } else {
                        if (!responded) {
                            resolve(...args);
                            responded = true;
                        } else {
                            Client.log('Delayed response', name, id, 'args:', ...args);
                        }
                    }
                } else {
                    if (!responded) {
                        resolve();
                        responded = true;
                    } else {
                        Client.log('Delayed response', name, id, 'noargs');
                    }
                }
            });

            setTimeout(() => {
                if (!responded) {
                    reject('Timeout');
                    responded = true;
                }
            }, 5000);

            Browser.execute(`clientCall("async:${name}", "${id}", ...${JSON.stringify(args)})`);
        });
    }
}

export const Gui = new GuiImpl();
