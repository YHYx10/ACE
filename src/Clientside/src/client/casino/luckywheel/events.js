import { luckywheel } from "./module";

const player = mp.players.local;

mp.events.add(
{
    'render': () => 
    {
        const data = luckywheel.interaction;
        if (mp.game.gameplay.getDistanceBetweenCoords(player.position.x, player.position.y, player.position.z, data.pos.x, data.pos.y, data.pos.z, true) < data.radius 
            && !(!global.loggedin || global.chatActive || global.editing || global.menuOpened || global.cuffed)
            )
        {
            if (!data.isNear)
            {
                data.isNear = true;
                mp.keys.bind(data.button, false, luckywheel.onClick);
                data.sendNotify('Нажмите ~INPUT_CONTEXT~ , для прокрутки колеса');
            }
        }
        else if (data.isNear)
        {
            data.isNear = false;
            mp.keys.unbind(data.button, false, luckywheel.onClick);
            data.clearNotify();
        }
    },
    'luckywheel.cometoluckywheel': (pos) => 
    {
        luckywheel.comeToLuckyWheel(pos);
    },
    'luckywheel.spin': (pos) => 
    {
        luckywheel.object.spin(pos);
    }
});