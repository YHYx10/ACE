// Common phone storage init
if (!mp.storage.data.phone) {
    mp.storage.data.phone = { };
    // mp.storage.flush();
}

mp.phone = {
    onStartCallback: [],
    
    onStart: (callback) => {
        mp.phone.onStartCallback.push(callback);
    },

    invokeStart: () => {
        mp.phone.onStartCallback.forEach(callback => {
            callback();
        });
    }
}

require('./eventsProxy.js');
require('./main.js');
require('./states.js');

require('./apps/settings.js');
require('./apps/appStore.js');
require('./apps/contacts.js');
require('./apps/calls.js');
require('./apps/messenger.js');
require('./apps/gps.js');
require('./apps/taxi.js');
require('./apps/taxiJob.js');
require('./apps/camera.js');
require('./apps/finder.js');

mp.events.add('gui:ready', () => {
    mp.phone.invokeStart();
});

mp.events.add('cefLog', (txt) => {
    if (global.gui.debug)
        mp.serverLog(txt);
});
