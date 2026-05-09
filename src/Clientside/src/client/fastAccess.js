
let lastCheck = Date.now();
const floodTime = 1000;

mp.events.add("cef:access:requestData", () => {
    if(mp.storage.data.fastAccessButtons == undefined){
        mp.storage.data.fastAccessButtons = {"1": null, "2": null, "3": null, "4": null, "5": null, "6": null, "7": null, "8": null, "9": null, "10": null};
    }
    global.gui.setData('inventory/setFastAccessData', JSON.stringify(mp.storage.data.fastAccessButtons));

    if(mp.storage.data.fastAccessWeaponButtons == undefined){
        mp.storage.data.fastAccessWeaponButtons = {"1": {type: "eq", id: 1}, "2": {type: "eq", id: 2}, "3": {type: "eq", id: 3}, "4": {type: "eq", id: 4}};
    }
    global.gui.setData('inventory/setFastAccessWeaponData', JSON.stringify(mp.storage.data.fastAccessWeaponButtons));
});

mp.events.add("cef:access:setButton", (key, button) => {
    mp.storage.data.fastAccessButtons[key] = JSON.parse(button);
    mp.storage.flush();
});

mp.events.add("cef:access:setWeaponButton", (key, button) => {
    mp.storage.data.fastAccessWeaponButtons[key] = JSON.parse(button);
    mp.storage.flush();
});

const keys = [global.Keys.Key_1, global.Keys.Key_2, global.Keys.Key_3, global.Keys.Key_4, global.Keys.Key_5, global.Keys.Key_6, global.Keys.Key_7, global.Keys.Key_8, global.Keys.Key_9, global.Keys.Key_0];
const weapkeys = [global.Keys.Key_1, global.Keys.Key_2, global.Keys.Key_3, global.Keys.Key_4];

function canUseFastKey(){
    if (
        !global.loggedin || 
        mp.players.local.getVariable('InDeath') == true ||
        global.fishingMiniGame ||
        global.isPhoneOpened ||
        global.cuffed ||
        //global.cursorShow ||
        global.chatActive || 
        mp.players.local.isInAnyVehicle(true) ||
        lastCheck > Date.now() || 
        global.gui.isOpened() || 
        mp.gui.cursor.visible || 
        global.IsPlayingDM == true ||
		global.buttonsShow
    ) return false;
    return true
}

function useFastKey(key){
    const button = mp.storage.data.fastAccessButtons[key];
    if(button == null) return;
    switch (button.type) {
        case "eq":
            if(global.playerEquip.weapons[button.id]){
                // mp.events.call('notify', 4, 9, "СКИБИДИ ВАПА", 3000);
                // mp.events.call('notify', 4, 9, global.playerEquip.weapons[button.id], 3000);
                //global.setActiveWeapon(button.id || 0);
                let ammo = 0
                if(mp.players.local.currentWeaponData){
                    ammo = mp.players.local.currentWeaponData.ammo || 0;
                }
                lastCheck = Date.now() + floodTime;
                mp.events.callRemote("weapon:activate", button.id || 0, ammo)
            }
            break;
        case "inv":
            // mp.events.call('notify', 4, 9, "СКИБИДИ ВАПА 2", 3000);
            // mp.events.call('notify', 4, 9, JSON.stringify(global.playerEquip.weapons), 3000);
            const index = global.playerInventory.items.findIndex(i=>i.id == button.id);
            if(index === -1) 
                global.gui.setData('inventory/resetFastAccessButton', `'${key}'`);
            else {                
                lastCheck = Date.now() + floodTime;
                mp.events.callRemote("inv:use:fast", button.id)
            }
            break;
            default:
            break;
    }
}

let lastMessage = 0;


weapkeys.forEach((key, index) => {
    mp.keys.bind(key, false, function () {
        if (mp.keys.isUp(global.Keys.Key_ALT)) return;
        if(global.inAction || global.isPhoneOpened) return;
        // mp.events.call('notify', 4, 9, mp.storage.data.fastAccessWeaponButtons, 3000);
        if(canUseFastKey()){
            const button = mp.storage.data.fastAccessWeaponButtons[index+1];
            if(button == null) return;
            let ammo = 0
            if(mp.players.local.currentWeaponData){
                ammo = mp.players.local.currentWeaponData.ammo || 0;
            }
            lastCheck = Date.now() + floodTime;
            mp.events.callRemote("weapon:activate", button.id || 0, ammo)
        }
    });
});

keys.forEach((key, index) => {
    mp.keys.bind(key, false, function() {
        if (!mp.keys.isUp(global.Keys.Key_ALT)) return;
        if(global.inAction || global.isPhoneOpened) return;
        if(canUseFastKey()){
            useFastKey(index + 1);
        }
    });
});
