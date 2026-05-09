import './adminesp';
import './spectate';
import './fly';
import './fun';
import { Keys } from '../keys';
import { Cursor } from '../../Gui/Cursor';
import { Gui } from '../../Bridge/Gui';
import { Server } from '../../Bridge/Server';
import { Me } from '../../Entity/Player';
import { Client } from '../../Bridge/Client';

Client.on('admin:checkinventory:responce', (equip, items) => {
    Gui.setData('inventory/updateCheckInventory', JSON.stringify({
        equip,
        items,
    }));
    Cursor.show();
});

Client.on('admin:toDemorgan', (freeze) => {
    Me.demorgan(freeze);
});

Client.on('admin:releaseDemorgan', () => {
    Me.releaseDemorgan();
});

let spamProtection = 0;

mp.keys.bind(Keys.Key_F10, false, () => {
    if (spamProtection > Date.now()) {
        return;
    }
    spamProtection = Date.now() + 1000;
    if (Me.getVariable('IS_MEDIA', false) || Me.adminLevel > 7) {
        Server.trigger('media:mute:press');
    }
});
