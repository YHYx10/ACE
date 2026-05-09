mp.events.addPhone({
    "phone::finder::loadProfile": () => {
        mp.events.callRemote('phone:finder:loadProfile');
    },

    "phone::finder::saveProfile": (profileJson) => {
        mp.events.callRemote('phone:finder:saveProfile', profileJson);
    },

    "phone::finder::loadProfiles": () => {
        mp.events.callRemote('phone:finder:loadProfiles');
    }
});
