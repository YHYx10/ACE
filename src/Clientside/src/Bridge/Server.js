import { debugFormat } from '../Utils/DebugFormat';

class ServerImpl {
    constructor() {
        /**
         * Event Forwarding
         * Used to forward events from the browser gui to the server
         */
        mp.events.add('efwd', (eventName, ...args) => {
            this.trigger(eventName, ...args);
        });

        mp.events.add('server:log', (...args) => {
            this.log(...args);
        });
    }

    trigger(eventName, ...args) {
        if (process.env.NODE_ENV === 'development' && eventName !== 'server:log') {
            mp.console.logInfo(`[RemoteEvent]: ${eventName}(${args.map(arg => debugFormat(arg)).join(', ')})`);
        }

        mp.events.callRemote(eventName, ...args);
    }

    log(...args) {
        this.trigger('server:log', ...args);
    }
}

export const Server = new ServerImpl();
