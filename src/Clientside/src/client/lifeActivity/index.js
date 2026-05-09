require('./hunger.js');
require('./joy.js');
require('./rest.js');
require('./thirst.js');

mp.events.add({
    // COMMON1
    "lifesystem:setStatsByKey": (key, value) => {
        const data = {
            key: key,
            value: value
        };
        
        global.gui.setData('hud/setStatusDisplay', JSON.stringify(data));
    },

    "lifesystem:startScreenEffect": (effectName) => {
        mp.game.graphics.startScreenEffect(effectName, 3000, true);
    },

    "lifesystem:stopScreenEffect": (effectName) => {
        mp.game.graphics.stopScreenEffect(effectName);
    }
});