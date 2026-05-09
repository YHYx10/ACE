import { createEventEmitter } from "./EventEmitter.js";

function createEnviromentPlugin(isDebug = false) {
  const Environment = {
    ...createEventEmitter(false),
    call(eventName, ...args) {
      this._call(eventName, ...args);
    },
  };
  window.Environment = Environment;
  function pluginEmit(eventName, ...args) {
    const wrappedArgs = args.map((arg) => typeof arg === "object" ? JSON.stringify(arg) : arg);
    if (isDebug) console.log(`callClient: "${eventName}"`, ...wrappedArgs);
    window.mp.trigger(eventName, ...wrappedArgs);
  }
  function pluginError(error) {
    if (isDebug) console.log(`callError: `, error);
    window.mp.trigger("Global:Client:SendError", error);
  }
  function pluginOn(eventName, callback) {
    if (isDebug) console.log(`Event "${eventName}" has been succesfully added`);
    Environment._use(true, eventName, callback);
  }
  function pluginOff(eventName, callback) {
    if (isDebug) console.log(`Event "${eventName}" has been succesfully removed`);
    Environment._use(false, eventName, callback);
  }
  const EnvironmentPlugin = {
    install: (Vue) => {
      Vue.prototype.$Environment = Environment;
      Vue.prototype.$onClient = pluginOn;
      Vue.prototype.$offClient = pluginOff;
      Vue.prototype.$callClient = pluginEmit;
      Vue.prototype.$callException = pluginError;
    },
  };
  return EnvironmentPlugin;
}

export default createEnviromentPlugin;