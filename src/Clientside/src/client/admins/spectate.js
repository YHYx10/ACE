import { Server } from '../../Bridge/Server';
import { Me } from '../../Entity/Player';
import { Client } from '../../Bridge/Client';

Client.on('spmode', (target, toggle) => {
    Me.entity.freezePosition(toggle === true);

    if (toggle === true) {
        if (target && mp.players.exists(target)) {
            global.spectating = true;
            Me.entity.attachTo(target.handle, -1, -1.5, -1.5, 2, 0, 0, 0, true, false, false, false, 0, false);
        } else {
            Server.trigger('UnSpectate');
        }
    } else {
        Me.entity.detach(true, true);
        global.spectating = false;
    }
});
