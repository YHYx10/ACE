const EventEmitter = {
  _debug: false,
  _events: new Map(),
  _add(eventName, callback) {
    if (this._debug) {
      console.log(`Event "${eventName}" has been succesfully added`);
    }
    let eventList = [];
    if (this._events.has(eventName)) {
      eventList = this._events.get(eventName);
    }
    eventList.push(callback);
    this._events.set(eventName, eventList);
  },
  _remove(eventName, callback) {
    if (this._debug) {
      console.log(`Event "${eventName}" has been succesfully removed`);
    }
    if (this._events.has(eventName)) {
      let eventList = [];
      if (callback !== undefined) {
        eventList = this._events.get(eventName);
        for (let i = 0; i < eventList.length; i++) {
          if (eventList[i] === callback) {
            eventList.splice(i, 1);
            break;
          }
        }
      }
      if (eventList.length) {
        this._events.set(eventName, eventList);
      } else {
        this._events.delete(eventName);
      }
    }
  },
  _call(eventName, ...args) {
    if (this._debug) {
      console.log(
        `Event "${eventName}" has been succesfully triggered`,
        ...args
      );
    }
    if (this._events && this._events.has(eventName)) {
      this._events.get(eventName).forEach((callback) => {
        callback(...args);
      });
    }
  },
  _useSingle(isAdding, eventName, callback) {
    if (typeof eventName === "string") {
      if (typeof callback === "function") {
        if (isAdding) {
          this._add(eventName, callback);
        } else {
          this._remove(eventName, callback);
        }
      } else if (typeof callback === "object" && Array.isArray(callback)) {
        let callbacks = callback;
        if (callbacks.length) {
          let checkTypes = callbacks.every(
            (_callback) => typeof _callback === "function"
          );
          if (checkTypes) {
            callbacks.forEach((_callback) => {
              if (isAdding) {
                this._add(eventName, _callback);
              } else {
                this._remove(eventName, _callback);
              }
            });
          } else {
            throw new Error("Array of Callbacks must contain only functions");
          }
        } else {
          throw new Error("Array of callbacks must have at least 1 element");
        }
      } else {
        throw new Error('"callback" type must be a function or array');
      }
    } else {
      throw new Error('"eventName" type must be a string');
    }
  },
  _use(isAdding, eventName, callback) {
    if (eventName !== undefined) {
      if (callback !== undefined) {
        if (typeof eventName === "string") {
          this._useSingle(isAdding, eventName, callback);
        } else {
          throw new Error(
            'If you use this function only with 2 parameters, "eventName" must be a string'
          );
        }
      } else if (typeof eventName === "object" && !Array.isArray(eventName)) {
        let eventNames = eventName;
        Object.entries(eventNames).forEach(([key, value]) => {
          this._useSingle(isAdding, key, value);
        });
      } else if (
        typeof eventName === "object" &&
        Array.isArray(eventName) &&
        !isAdding
      ) {
        let eventNames = eventName;
        if (eventNames.length) {
          if (
            eventNames.every((_eventName) => typeof _eventName === "string")
          ) {
            eventNames.forEach((_eventName) => this._remove(_eventName));
          } else {
            throw new Error("Array of eventNames must contain only strings");
          }
        } else {
          throw new Error("Array of eventNames must have at least 1 element");
        }
      } else if (typeof eventName === "string" && !isAdding) {
        this._remove(eventName);
      } else {
        let allowedTypes = isAdding ? "Object" : "Object / Array / String";
        throw new Error(
          `If you use this function only with 1 parameter, it must be ${allowedTypes}`
        );
      }
    } else {
      throw new Error(`This function must contain at least 1 parameter`);
    }
  },
};

function createEventEmitter(debug = false) {
  return Object.assign({}, EventEmitter, {
    _events: new Map(),
    _debug: debug,
  });
}

export { EventEmitter, createEventEmitter };
