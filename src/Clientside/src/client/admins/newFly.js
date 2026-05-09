import { Keys } from '../keys';
import { Authentication } from '../../Authentication/Authentication';
import { Server } from '../../Bridge/Server';
import { Me } from '../../Entity/Player';
import { Natives } from '../configs/Natives';
import { Client } from '../../Bridge/Client';

const flyCamera = mp.cameras.new('default', new mp.Vector3(0, 10, 90), new mp.Vector3(-95, 19, 0), 50);
const controls = mp.game.controls;
const gameplayCam = mp.cameras.new('gameplay');

global.fly = {
    flying: true,
    f: 2.0,
    w: 2.0,
    h: 2.0,
    point_distance: 1000,
    pos: new mp.Vector3(0, 0, 0),
};

Client.on('AGM', (toggle) => {
    global.pidrgm = toggle === true;
    Me.entity.setInvincible(toggle = true);
    // mp.game.graphics.notify(toggle ? 'GM: ~g~Enabled' : 'GM: ~r~Disabled');
});

const controlsIds = {
    W: 32,
    S: 33,
    A: 34,
    D: 35,
    Space: 321,
    LCtrl: 326,
    LMB: 24,
    RMB: 25,
};

function flyOn(toggle) {
    flyCamera.setActive(true);
    const pos = new mp.Vector3(Me.position.x, Me.position.y, Me.position.z + 0.8);
    global.fly.pos = pos;
    flyCamera.setCoord(fly.pos.x, fly.pos.y, fly.pos.z);
    mp.game.cam.renderScriptCams(true, false, 0, true, false);
    Server.trigger('FlyToggle', true, global.fly.pos.z);
    global.fly.flying = toggle;
    setTimeout(() => {
        Me.entity.setInvincible(true);
        Me.freeze();
        Me.entity.setCollision(false, false);
    }, 100);
}

function flyOff() {
    flyCamera.setActive(false);
    if (!global.pidrgm) {
        Me.entity.setInvincible(false);
    }
    Me.unfreeze();
    Me.entity.setCollision(true, true);

    if (!controls.isControlPressed(0, controlsIds.Space)) {
        global.fly.pos.z =
            mp.game.gameplay.getGroundZFor3dCoord(global.fly.pos.x, global.fly.pos.y, global.fly.pos.z, 0.0, false);
        Me.entity.setCoordsNoOffset(global.fly.pos.x, global.fly.pos.y, global.fly.pos.z, false, false, false);
    } else {
        Me.entity.setCoordsNoOffset(global.fly.pos.x, global.fly.pos.y, global.fly.pos.z, false, false, false);
    }
    mp.game.cam.renderScriptCams(false, false, 0, true, false);
    Server.trigger('FlyToggle', false, global.fly.pos.z);
    global.fly.flying = false;
    mp.game.invoke(Natives.RESET_FOCUS_AREA);
}

const oldCamPos = new mp.Vector3();

const lastTimeLogSend = 0;
const logSendInterval = 1000;
Client.on('render', () => {
    if (global.fly.flying || global.spectating) {
        const fly = global.fly;
        const camDir = gameplayCam.getDirection();
        let updated = false;
        let speed;
        if (!global.spectating) {
            if (controls.isControlPressed(0, controlsIds.LMB)) {
                speed = 0.7;
            } else if (controls.isControlPressed(0, controlsIds.RMB)) {
                speed = 0.03;
            } else {
                speed = 0.1;
            }
            if (controls.isControlPressed(0, controlsIds.W)) {
                if (fly.f < 8.0) {
                    fly.f *= 1.025;
                }
                fly.pos.x += camDir.x * fly.f * speed;
                fly.pos.y += camDir.y * fly.f * speed;
                fly.pos.z += camDir.z * fly.f * speed;
                updated = true;
            } else if (controls.isControlPressed(0, controlsIds.S)) {
                if (fly.f < 8.0) {
                    fly.f *= 1.025;
                }
                fly.pos.x -= camDir.x * fly.f * speed;
                fly.pos.y -= camDir.y * fly.f * speed;
                fly.pos.z -= camDir.z * fly.f * speed;
                updated = true;
            } else {
                fly.f = 2.0;
            }
            if (controls.isControlPressed(0, controlsIds.A)) {
                if (fly.l < 8.0) {
                    fly.l *= 1.025;
                }
                fly.pos.x += (-camDir.y) * fly.l * speed;
                fly.pos.y += camDir.x * fly.l * speed;
                updated = true;
            } else if (controls.isControlPressed(0, controlsIds.D)) {
                if (fly.l < 8.0) {
                    fly.l *= 1.05;
                }
                fly.pos.x -= (-camDir.y) * fly.l * speed;
                fly.pos.y -= camDir.x * fly.l * speed;
                updated = true;
            } else {
                fly.l = 2.0;
            }
            if (controls.isControlPressed(0, controlsIds.Space)) {
                if (fly.h < 8.0) {
                    fly.h *= 1.025;
                }
                fly.pos.z += fly.h * speed;
                updated = true;
            } else if (controls.isControlPressed(0, controlsIds.LCtrl)) {
                if (fly.h < 8.0) {
                    fly.h *= 1.05;
                }
                fly.pos.z -= fly.h * speed;
                updated = true;
            } else {
                fly.h = 2.0;
            }
            if (updated) {
                flyCamera.setCoord(fly.pos.x, fly.pos.y, fly.pos.z);
            }
        }
        if (global.spectating) {
            if (global.sptarget && mp.players.exists(global.sptarget)) {
                const pos = new mp.Vector3(global.sptarget.position.x + 7,
                    global.sptarget.position.y,
                    global.sptarget.position.z + 2.3,
                );

                if (fly.pos !== pos) {
                    fly.pos = pos;
                    flyCamera.setCoord(fly.pos.x, fly.pos.y, fly.pos.z);
                }
                flyCamera.pointAtCoord(global.sptarget.position.x,
                    global.sptarget.position.y,
                    global.sptarget.position.z,
                );
            } else {
                Server.trigger('UnSpectate');
            }
        } else {
            flyCamera.pointAtCoord(fly.pos.x + camDir.x, fly.pos.y + camDir.y, fly.pos.z + camDir.z);
        }

        Me.entity.setCoordsNoOffset(fly.pos.x, fly.pos.y, -150.1, true, true, true);
        mp.game.streaming.setFocusArea(fly.pos.x, fly.pos.y, fly.pos.z, 0, 0, 0);
    }
});

mp.keys.bind(Keys.Key_F11, false, function () {
    if (!Authentication.isLoggedin() || Me.adminLevel < 1) {
        return;
    }
    if (global.spectating) {
        global.fly.flying = true;
        Server.trigger('UnSpectate');
    } else {
        if (global.fly.flying) {
            flyOff();
        } else {
            flyOn(true);
        }
    }
});

Client.on('admin:fly:pos', (x, y, z) => {
    fly.pos = new mp.Vector3(x, y, z + 0.8);
    flyCamera.setCoord(fly.pos.x, fly.pos.y, fly.pos.z);
});

Client.on('spmode', (target, toggle) => {
    Me.entity.freezePosition(toggle);
    if (toggle) {
        if (target && mp.players.exists(target)) {
            global.sptarget = target;
            global.spectating = true;
            if (!global.fly.flying) {
                flyOn(false);
            } else {
                flyCamera.setCoord(fly.pos.x, fly.pos.y, fly.pos.z);
            }
        } else {
            Server.trigger('UnSpectate');
        }
    } else {
        global.sptarget = null;
        global.spectating = false;
        if (!global.fly.flying) {
            flyOff();
        }
    }
});
