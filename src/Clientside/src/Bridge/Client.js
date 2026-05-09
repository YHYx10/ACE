import { debugFormat } from '../Utils/DebugFormat';

class ClientImpl {
    constructor() {
        this.ignoreEvents =
            ['render',
                'click',
                'client:log',
                'browserDomReady',
                'customShape:load',
                'warForEnterprice:loadPeds',
                'dockLoader:init',
                'questPeds:load',
                'houses:getHouseIcons',
                'vehicleRent:loadPeds',
                'phone:gps:load',
                'player:loadedRoulleteBoard',
                'entityStreamOut',
                'entityStreamIn',
                ['gui:setData',
                    (namespace) => {
                        return [
                            'newDonateShop/setUpdatedPrices',
                            'smartphone/gps_loadData',
                            'smartphone/weatherPage/setFuture',
                        ].includes(namespace);
                    }]];

        mp.console.clear();

        this.on('client:log', (...args) => {
            this.doLog('[GUI]', ...args.map(arg => debugFormat(arg)));
        });
    }

    doLog(scope, ...args) {
        if (process.env.NODE_ENV === 'development') {
            mp.console.logInfo(`${scope} ${args.map(item => item.toString()).join(' ')}`);
        }
    }

    log(...args) {
        this.doLog('[CLIENT]', ...args.map(arg => debugFormat(arg)));
    }

    ignoreEvent(eventName, ...args) {
        for (const ignoreEvent of this.ignoreEvents) {
            if (typeof ignoreEvent === 'string' && ignoreEvent === eventName) {
                return true;
            } else if (Array.isArray(ignoreEvent) && ignoreEvent[0] === eventName && ignoreEvent[1](...args)) {
                return true;
            }
        }

        return false;
    }

    on(eventName, handler) {
        mp.events.add(eventName, (...args) => {
            if (process.env.NODE_ENV === 'development') {
                if (!this.ignoreEvent(eventName, ...args)) {
                    this.doLog(`[CLIENT][EVENT][${eventName}]`);
                }
            }

            handler(...args);
        });
    }
}

export const Client = new ClientImpl();
