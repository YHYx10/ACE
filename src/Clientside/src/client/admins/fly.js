import { Keys } from '../keys';
import { Authentication } from '../../Authentication/Authentication';
import { Server } from '../../Bridge/Server';
import { Me } from '../../Entity/Player';
import { Client } from '../../Bridge/Client';

const controlsIds = {
    F11: 0x7A,
    W: 32,
    S: 33,
    A: 34,
    D: 35,
    Space: 321,
    LCtrl: 326,
    LMB: 24,
    RMB: 25,
};

global.fly = {
    flying: false,
    f: 2.0,
    w: 2.0,
    h: 2.0,
    point_distance: 1000,
};

global.gameplayCam = mp.cameras.new('gameplay');

let direction = null;
let coords = null;

function pointingAt(distance) {
    const farAway = new mp.Vector3((direction.x * distance) + (coords.x),
        (direction.y * distance) + (coords.y),
        (direction.z * distance) + (coords.z),
    );

    const result = mp.raycasting.testPointToPoint(coords, farAway, 1, 16);
    if (result === undefined) {
        return 'undefined';
    }
    return result;
}

Client.on('AGM', (toggle) => {
    global.pidrgm = toggle === true;
    Me.entity.setInvincible(toggle === true);
});

mp.keys.bind(Keys.Key_F7, false, function () {
    if (!Authentication.isLoggedin() || Me.adminLevel < 1 && Me.getVariable('IS_MEDIAHELPER') !== true) {
        return;
    }

    const controls = mp.game.controls;
    const fly = global.fly;
    direction = global.gameplayCam.getDirection();
    coords = global.gameplayCam.getCoord();

    fly.flying = !fly.flying;

    const player = Me.entity;

    if (fly.flying) {
        player.setInvincible(true);
    } else if (!global.pidrgm) {
        player.setInvincible(false);
    }

    player.freezePosition(fly.flying);
    player.setAlpha(fly.flying ? 0 : 255);
    const position = Me.position;
    if (!fly.flying && !controls.isControlPressed(0, controlsIds.Space)) {
        position.z = mp.game.gameplay.getGroundZFor3dCoord(position.x, position.y, position.z, 0.0, false);
        Me.entity.setCoordsNoOffset(position.x, position.y, position.z, false, false, false);
    }

    Server.trigger('FlyToggle', fly.flying, position.z);
    // mp.game.graphics.notify(fly.flying ? 'Fly: ~g~Enabled' : 'Fly: ~r~Disabled');
});

Client.on('render', () => {
    if (fly.flying) {
        const controls = mp.game.controls;
        const fly = global.fly;
        direction = global.gameplayCam.getDirection();
        coords = global.gameplayCam.getCoord();

        let updated = false;
        const position = Me.position;
        let speed;
        if (controls.isControlPressed(0, controlsIds.LMB)) {
            speed = 1.0;
        } else if (controls.isControlPressed(0, controlsIds.RMB)) {
            speed = 0.02;
        } else {
            speed = 0.2;
        }
        if (controls.isControlPressed(0, controlsIds.W)) {
            if (fly.f < 8.0) {
                fly.f *= 1.025;
            }
            position.x += direction.x * fly.f * speed;
            position.y += direction.y * fly.f * speed;
            position.z += direction.z * fly.f * speed;
            updated = true;
        } else if (controls.isControlPressed(0, controlsIds.S)) {
            if (fly.f < 8.0) {
                fly.f *= 1.025;
            }
            position.x -= direction.x * fly.f * speed;
            position.y -= direction.y * fly.f * speed;
            position.z -= direction.z * fly.f * speed;
            updated = true;
        } else {
            fly.f = 2.0;
        }
        if (controls.isControlPressed(0, controlsIds.A)) {
            if (fly.l < 8.0) {
                fly.l *= 1.025;
            }
            position.x += (-direction.y) * fly.l * speed;
            position.y += direction.x * fly.l * speed;
            updated = true;
        } else if (controls.isControlPressed(0, controlsIds.D)) {
            if (fly.l < 8.0) {
                fly.l *= 1.05;
            }
            position.x -= (-direction.y) * fly.l * speed;
            position.y -= direction.x * fly.l * speed;
            updated = true;
        } else {
            fly.l = 2.0;
        }
        if (controls.isControlPressed(0, controlsIds.Space)) {
            if (fly.h < 8.0) {
                fly.h *= 1.025;
            }
            position.z += fly.h * speed;
            updated = true;
        } else if (controls.isControlPressed(0, controlsIds.LCtrl)) {
            if (fly.h < 8.0) {
                fly.h *= 1.05;
            }
            position.z -= fly.h * speed;
            updated = true;
        } else {
            fly.h = 2.0;
        }
        if (updated) {
            Me.entity.setCoordsNoOffset(position.x, position.y, position.z, false, false, false);
        }
    }
});

Client.on('getCamCoords', (name) => {
    Server.trigger('saveCamCoords', JSON.stringify(coords), JSON.stringify(pointingAt(fly.point_distance)), name);
});
