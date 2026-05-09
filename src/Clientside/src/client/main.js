let Peds = [
    { Hash: -1105179493, Pos: new mp.Vector3(638.3456, 206.6573, 97.69418), Angle: 256.1577 }, // JoeMinuteman Parking
    { Hash: -1105179493, Pos: new mp.Vector3(-1216.897, -644.9938, 25.882), Angle: 309.4338 }, // JoeMinuteman Parking
    { Hash: -1105179493, Pos: new mp.Vector3(-17.30688, 6303.709, 31.39), Angle: 35.76939 }, // JoeMinuteman Parking
    { Hash: 1809430156, Pos: new mp.Vector3(-784.1984, 7.0092373, 41.68133), Angle: -144.10992 }, // HasJew01AMM Priest
    { Hash: 1657546978, Pos: new mp.Vector3(2753.742, 3480.687, 55.65), Angle: 242.727 }, // Technician Work
    { Hash: -2078561997, Pos: new mp.Vector3(1417.378, 6343.877, 24.00321), Angle: 274.7821 }, // Car Thief Work
    { Hash: -413447396, Pos: new mp.Vector3(-1189.708, -2933.827, 13.94469), Angle: 165.4088 }, // Transporteur Work
    { Hash: 0xE6631195, Pos: new mp.Vector3(-2188.892, 4275.267, 49.17898), Angle: 63.32428 }, // Hunter shop ped
    { Hash: 330231874, Pos: new mp.Vector3(2439.151, 4962.98, 46.81056), Angle: 340 }, // Alexander Lukashenko
    { Hash: 0x432CA064, Pos: new mp.Vector3(1117.406, 220.53, -51.55516), Angle: 85 }, // CashierCasino
];

Peds.forEach(ped => {
    mp.peds.newValid(ped.Hash, ped.Pos, ped.Angle, 0);
});

global.clientPedLoaded = true;
opened = false;

function ExitMenu() {
    global.gui.close();
    opened = false;
    global.showCursor(false)
}

mp.keys.bind(global.Keys.Key_ESCAPE, false, function () {
    if (opened)
        ExitMenu();
});

mp.events.add('toggleTestInv', function(page) {
    opened = true;
    switch(page){
        case 1: 
            global.gui.openPage('Spawn');
            break;
        // case 2: 
        //     global.gui.openPage('TestMenu');
        //     break;
        // case 3: 
        //     global.gui.openPage('TestCust');
        //     break;
        // // case 4: 
        // //     global.gui.openPage('NewAuth');
        // //     break;
        // case 4: 
        //     global.gui.openPage('MenuPerson');
        //     break;
        default: 
            global.gui.openPage('Spawn');
            break;
    }
});

const Natives = {
    SWITCH_OUT_PLAYER: '0xAAB3200ED59016BC',
    SWITCH_IN_PLAYER: '0xD8295AF639FD9CB8',
    IS_PLAYER_SWITCH_IN_PROGRESS: '0xD9D2CFFF49FAB35F'
};

mp.events.add('toggleTestCam', function(page) {
    switch(page){
        case 0: 
            mp.game.invoke(Natives.SWITCH_OUT_PLAYER, global.localplayer.handle, 0, parseInt(0));
            break;
        case 1: 
            mp.game.invoke(Natives.SWITCH_OUT_PLAYER, global.localplayer.handle, 0, parseInt(1));
            break;
        case 2: 
            mp.game.invoke(Natives.SWITCH_OUT_PLAYER, global.localplayer.handle, 0, parseInt(2));
            break;
        case 3: 
            mp.game.invoke(Natives.SWITCH_OUT_PLAYER, global.localplayer.handle, 0, parseInt(3));
            break;
        case 4: 
            mp.game.invoke(Natives.SWITCH_IN_PLAYER, global.localplayer.handle);
            break;
    }
});

mp.events.add('freeze', (toggle) => {
    global.localplayer.freezePosition(toggle == true);
});

mp.events.add('destroyCamera', function() {
    mp.game.cam.renderScriptCams(false, false, 3000, true, true);
});

let lastScreenEffect = "";
mp.events.add('startScreenEffect', function(effectName, duration, looped) {
    try {
        lastScreenEffect = effectName;
        mp.game.graphics.startScreenEffect(effectName, duration, looped);
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in main.startScreenEffect: ${e.name}\n${e.message}\n${e.stack}`);
	}
});

mp.events.add('stopScreenEffect', function(effectName) {
    try {
        let effect = (effectName == undefined) ? lastScreenEffect : effectName;
        mp.game.graphics.stopScreenEffect(effect);
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in main.stopScreenEffect: ${e.name}\n${e.message}\n${e.stack}`);
	}
});

mp.events.add('stopAndStartScreenEffect', function(stopEffect, startEffect, duration, looped) {
    try {
        mp.game.graphics.stopScreenEffect(stopEffect);
        mp.game.graphics.startScreenEffect(startEffect, duration, looped);
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in main.stopAndStartScreenEffect: ${e.name}\n${e.message}\n${e.stack}`);
	}
});

mp.events.add('setPocketEnabled', function(state) {
   global.pocketEnabled = Boolean(state);
    if (Boolean(state)) {
        mp.gui.execute("fx.set('inpocket')");
        mp.game.invoke(global.getNative("SET_FOLLOW_PED_CAM_VIEW_MODE"), 4);
    } else {
        mp.gui.execute("fx.reset()");
    }
});

mp.events.add('connected', function() {
    mp.game.ui.displayHud(false);
});

mp.events.add('ready', function() {
	mp.game.graphics.setTimecycleModifier("default");
    mp.game.ui.displayHud(true);
});

mp.events.add('kick', (notify) => {
    if(notify != null)
        mp.events.call('notify', 4, 9, notify, 10000);
    mp.events.call('onConnectionLost');
    mp.events.callRemote('kickclient');
});

mp.events.add("onConnectionLost", ()=>{
    try {
        global.customWeaponsModels.forEach(weapon => {
            if(weapon) mp.game.object.deleteObject(weapon);
        });
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in main.onConnectionLost: ${e.name}\n${e.message}\n${e.stack}`);
    }
});

let followingTarget = null;

mp.events.add('setFollow', (toggle, entity) => {
	if (!toggle) 
	{
		global.localplayer.clearTasks();
		followingTarget = null;
		return;
	}
	
	if (!entity || !mp.players.exists(entity)) return;
	
	followingTarget = entity;
	global.localplayer.taskFollowToOffsetOf(entity.handle, 0, 0, 0, 1, -1, 1, true);
});

mp.keys.bind(global.Keys.Key_E, false, function() { // E key
    if (!global.loggedin|| global.chatActive || global.editing || global.lastCheck > Date.now() || global.gui.isOpened() || global.IsPlayingDM || global.inAction || global.cuffed) return;
    if(global.localplayer.farmAction < 0){       
        mp.events.callRemote('interactionPressed');        
        global.lastCheck = Date.now() + 500;
        //global.acheat.pos();
    } else {
        global.farmAction(global.localplayer.farmAction);
    }
});
function onKeyLockPress(){
    if (!global.loggedin|| global.chatActive || global.editing || global.lastCheck > Date.now() || global.gui.isOpened() || global.IsPlayingDM) return;
    mp.events.callRemote('lockCarPressed');     
    global.lastCheck = Date.now() + 500;
}

function onKeyEnginePress(){
    if (!global.loggedin|| global.chatActive || global.editing || global.lastCheck > Date.now() || global.gui.isOpened() || global.IsPlayingDM) return;
    if  (mp.players.local.isInAnyVehicle(false) && mp.players.local.vehicle.getSpeed() <= 3) {
        mp.events.callRemote('engineCarPressed');
        global.lastCheck = Date.now() + 500;
        if(global.isTransporteurWorker) 
            mp.events.callRemote("WORK::TRANSPORTEUR::ENGINE::START::CLIENT");
    }
}

mp.events.add("gui:ready", ()=>{
    global.keyActions["engine"].subscribe(onKeyEnginePress, true);

    global.keyActions["lock"].subscribe(onKeyLockPress, true);
    
})


// mp.keys.bind(global.Keys.Key_L, false, function() { // L key
//     if (!global.loggedin|| global.chatActive || global.editing || global.lastCheck > Date.now() || global.gui.isOpened() || global.IsPlayingDM) return;
//     mp.events.callRemote('lockCarPressed');        
//     global.lastCheck = Date.now() + 500;
// });

mp.keys.bind(global.Keys.Key_LEFT, true, () => {
    SetTurnSignal(1);
});

mp.keys.bind(global.Keys.Key_RIGHT, true, () => {
    SetTurnSignal(2);
});

mp.keys.bind(global.Keys.Key_DOWN, true, () => {
    SetTurnSignal(3);
});

//1 - left, 2 - right, 3 - both
function SetTurnSignal(turnSignal) {
    if (global.checkIsAnyActivity()) return;
    if (mp.gui.cursor.visible) return;
    if  (mp.players.local.vehicle) {
        if (mp.players.local.vehicle.getPedInSeat(-1) != mp.players.local.handle) return;
        if (global.lastCheck > Date.now()) return;
        global.lastCheck = Date.now() + 500;
        let currTurnSignal = mp.players.local.vehicle.getVariable('veh:turnSignal');
        if ((typeof currTurnSignal) === 'undefined' || currTurnSignal == null)
            currTurnSignal = 0;
        if (currTurnSignal != turnSignal)
            mp.events.callRemote("veh:setTurnSignal", mp.players.local.vehicle, turnSignal);
        else
            mp.events.callRemote("veh:setTurnSignal", mp.players.local.vehicle, 0);
    }
}

let engineLastCheck = 0;

mp.keys.bind(global.Keys.Key_X, false, function() { // X key
    if (!global.loggedin|| global.chatActive || global.editing ||  global.lastCheck > Date.now() || global.gui.isOpened() || global.IsPlayingDM || global.inAction) return;
    mp.events.callRemote('playerPressCuffBut');
    global.lastCheck = Date.now() + 500;
});

function CheckMyWaypoint() {
    try {
        let coord = global.getWayPoint();       
        if(coord) mp.events.callRemote('syncWaypoint', coord.x, coord.y);
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in main.CheckMyWaypoint: ${e.name}\n${e.message}\n${e.stack}`);
	}
}

global.getWayPoint = ()=>{
    try {
        if (mp.game.invoke('0x1DD1F58F493F1DA5')) {
            let foundblip = false;
            let blipIterator = mp.game.invoke('0x186E5D252FA50E7D');
            let totalBlipsFound = mp.game.invoke('0x9A3FF3DE163034E8');
            let FirstInfoId = mp.game.invoke('0x1BEDE233E6CD2A1F', blipIterator);
            let NextInfoId = mp.game.invoke('0x14F96AA50D6FBEA7', blipIterator);
            let coord;
            for (let i = FirstInfoId, blipCount = 0; blipCount != totalBlipsFound; blipCount++, i = NextInfoId) {
                if (mp.game.invoke('0x1FC877464A04FC4F', i) == 8) {
                    coord = mp.game.ui.getBlipInfoIdCoord(i);
                    foundblip = true;
                    break;
                }
            }
            if (foundblip) return {x: coord.x, y: coord.y};        
        }
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in global.getWaypoint: ${e.name}\n${e.message}\n${e.stack}`);
	}
}

mp.keys.bind(global.Keys.Key_Z, false, function() { // Z key
    if(global.lastCheck > Date.now()) return;
    if(global.reportPosirion !== -1){
        global.lastCheck = Date.now() + 500;
        let coord = global.getWayPoint();       
        if(coord) {
            mp.events.callRemote('report:position', global.reportPosirion, coord.x, coord.y);
            mp.game.ui.setFrontendActive(false);
            global.reportPosirion = -1;
            mp.game.wait(200);
            mp.gui.cursor.visible = true;
        }
        return;
    }
    if (!global.loggedin|| global.chatActive || global.editing || global.gui.isOpened() || global.IsPlayingDM) return;
    if(mp.players.local.vehicle) {
        if(mp.players.local.vehicle.getPedInSeat(-1) != mp.players.local.handle) CheckMyWaypoint();
        else {
            if(mp.players.local.vehicle.getClass() == 18) 
                mp.events.callRemote('syncSirenSound', mp.players.local.vehicle);
        }
    } else mp.events.callRemote('playerPressFollowBut');
    global.lastCheck = Date.now() + 500;
});

mp.keys.bind(global.Keys.Key_OEM_3, false, function() {
    if (global.__astroAdminPanelIsOpen) {
        mp.events.call('admin:panel:toggleMouse');
        return;
    }
    if(mp.gui.cursor.visible){
        if(global.gui.opened && !global.gui.curPage) 
            global.gui.close();
        if(global.chatActive) 
            global.gui.hideChat();        
        global.showCursor(false);
    }else
        global.showCursor(true);
});


let lastPos = new mp.Vector3(0, 0, 0);

mp.game.gameplay.setFadeInAfterDeathArrest(false);
mp.game.gameplay.setFadeInAfterLoad(false);

mp.events.add('render', () => {
    if  (global.chatActive || /*global.cursorShow ||*/ mp.players.local.getVariable('InDeath') == true || global.IsFreezeDM) {
        mp.game.gameplay.setFadeOutAfterDeath(false);
        mp.game.controls.disableAllControlActions(2);
        mp.game.controls.enableControlAction(2, 1, true);
        mp.game.controls.enableControlAction(2, 2, true);
        mp.game.controls.enableControlAction(2, 3, true);
        mp.game.controls.enableControlAction(2, 4, true);
        mp.game.controls.enableControlAction(2, 5, true);
        mp.game.controls.enableControlAction(2, 6, true);
    }

    if (mp.game.controls.isControlPressed(0, 32) ||
        mp.game.controls.isControlPressed(0, 33) ||
        mp.game.controls.isControlPressed(0, 321) ||
        mp.game.controls.isControlPressed(0, 34) ||
        mp.game.controls.isControlPressed(0, 35) ||
        mp.game.controls.isControlPressed(0, 24) ||
        mp.players.local.getVariable('InDeath') == true) {
        afkSecondsCount = 0;
    } else if  (mp.players.local.isInAnyVehicle(false) && mp.players.local.vehicle && mp.players.local.vehicle.getSpeed() != 0) {
        afkSecondsCount = 0;
    } else if (global.spectating) { // Чтобы не кикало администратора в режиме слежки
        afkSecondsCount = 0;
    }
});


var jail_interval = void 0;
mp.events.add("Client_CheckIsInJail", function () {
	if (jail_interval) return;
	if (!jail_interval) {
		jail_interval = setInterval(function () {
            // mp.events.call('notify', 4, 2, mp.game.system.vdist(1692.573, 2519.978, 45.565, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z), 1000);
			if (!skip_jail_check && mp.game.system.vdist(1692.573, 2519.978, 45.565, mp.players.local.position.x, mp.players.local.position.y, mp.players.local.position.z) > 220) {
				mp.events.callRemote('Server_GetJailBack');
			}
		}, 1000);
	}
});

var skip_jail_check = false;
mp.events.add("Client_SkipCheckJail", function (arg) {
	skip_jail_check = arg;
});

mp.events.add("Client_CheckIsInJailDestroy", function () {
	if (jail_interval) {
		clearInterval(jail_interval);
		jail_interval = null;
	}
});


// mp.events.add('render', () => {
//     //ARREST_TIMER
//     if(mp.players.local.getVariable('ARREST_TIMER')){
//         const pos1 = mp.players.local.position;
//         const pos2 = new mp.Vector3(1711.0012, 2581.5393, 45.588715);
//         const distance = mp.game.system.vdist(pos1.x, pos1.y, pos1.z, pos2.x, pos2.y, pos2.z);

//         if (distance > 100) {
//             mp.events.call('notify', 4, 2, mp.players.local.getVariable('ARREST_TIMER'), 1000);
//         } 
//     }

// })    



mp.events.add("playerRuleTriggered", (rule, counter) => {
    if (rule === 'ping' && counter > 5) {
        mp.events.call('notify', 4, 2, "client_36", 5000);
        mp.events.callRemote("kickclient");
    }
});

mp.events.add('GetWPAdmin', () => { // Возвращает координаты точки админа
    try {
        let blipIterator = mp.game.invoke('0x186E5D252FA50E7D');
        let totalBlipsFound = mp.game.invoke('0x9A3FF3DE163034E8');
        let FirstInfoId = mp.game.invoke('0x1BEDE233E6CD2A1F', blipIterator);
        let NextInfoId = mp.game.invoke('0x14F96AA50D6FBEA7', blipIterator);
        let coord;
        for (let i = FirstInfoId, blipCount = 0; blipCount != totalBlipsFound; blipCount++, i = NextInfoId) {
            if (mp.game.invoke('0x1FC877464A04FC4F', i) == 8) {
                coord = mp.game.ui.getBlipInfoIdCoord(i);
                break;
            }
        }
        mp.events.callRemote('getWayPoint', coord.x, coord.y, id);
    } catch (e) {
        if(global.sendException)mp.serverLog(`Error in main.GetWPAdmin: ${e.name }\n${e.message}\n${e.stack}`);
    }
});

mp.keys.bind(global.Keys.Key_F4, false, ()=>{
    if(mp.keys.isDown(17) || mp.keys.isDown(18)) return;
    if (!global.loggedin || global.getVariable(mp.players.local, 'ALVL', 0) < 1 ||  global.lastCheck > Date.now()) return;
    global.lastCheck = Date.now() + 500;
    mp.events.callRemote('tpmark');
});

mp.events.add('GetMyWaypoint', () => { // Передача по метке
    try {
        let blipIterator = mp.game.invoke('0x186E5D252FA50E7D');
        let totalBlipsFound = mp.game.invoke('0x9A3FF3DE163034E8');
        let FirstInfoId = mp.game.invoke('0x1BEDE233E6CD2A1F', blipIterator);
        let NextInfoId = mp.game.invoke('0x14F96AA50D6FBEA7', blipIterator);
        let coord = {x:0,y:0,z:0};
        for (let i = FirstInfoId, blipCount = 0; blipCount != totalBlipsFound; blipCount++, i = NextInfoId) {
            if (mp.game.invoke('0x1FC877464A04FC4F', i) == 8) {
                coord = mp.game.ui.getBlipInfoIdCoord(i);
                break;
            }
        }
        mp.game.streaming.setFocusArea(coord.x, coord.y, coord.z, 0, 0, 0);

        setTimeout(()=>{
            coord.z = mp.game.gameplay.getGroundZFor3dCoord(coord.x, coord.y, 1000.1, 1.4, false);
            mp.events.callRemote('getWayPoint', coord.x, coord.y, coord.z + 1);
            mp.game.invoke(global.NATIVES.RESET_FOCUS_AREA);
        }, 50)
	}catch(e){
        if(global.sendException)mp.serverLog(`Error in main.GetMyWaypoint: ${e.name }\n${e.message}\n${e.stack}`);
    }
});

let toggleBigMap = false;
let showFullMap = false;

let seetdown = false;
mp.keys.bind(global.Keys.Key_P, false, function() { // O key
    if (!global.loggedin || global.chatActive || mp.gui.cursor.visible || global.editing || global.gui.isOpened()) return;
    seetdown = !seetdown;
    if (seetdown)
        mp.events.callRemote('aSelected', 12, 7);
    else
        mp.events.callRemote('aSelected', 12, 0);
});
