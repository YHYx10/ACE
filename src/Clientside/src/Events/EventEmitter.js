export class EventEmitter {
    constructor() {
        this.listeners = {};
    }

    on(event, listener) {
        if (!this.listeners[event]) {
            this.listeners[event] = [];
        }

        this.listeners[event].push(listener);
    }

    addListener(event, listener) {
        this.on(event, listener);
    }

    once(event, listener) {
        const onceListener = (...args) => {
            listener(...args);
            this.removeListener(event, onceListener);
        };

        this.on(event, onceListener);
    }

    removeListener(event, listener) {
        if (!this.listeners[event]) {
            return;
        }

        this.listeners[event] = this.listeners[event].filter((l) => l !== listener);
    }

    removeAllListeners(event) {
        if (!this.listeners[event]) {
            return;
        }

        this.listeners[event] = [];
    }

    emit(event, ...args) {
        if (!this.listeners[event]) {
            return;
        }

        this.listeners[event].forEach((listener) => listener(...args));
    }
}
