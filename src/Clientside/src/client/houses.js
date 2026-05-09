const ObjectMover = require("./objectMover.js")

const moveSettings = {
    size: {
        x: 40,
        y: 40
    },
    showIcons:[true, false, false, false],
    values:[
        {//LEFT X
            value: 160,
            min: 110,
            max: 250,
            step: 5,
            invert: true,
            enabled: true,
            callback: "camMoveAngleX"
        },
        {//LEFT Y
            value: 0,
            min: -.5,
            max: 1.5,
            step: .05,
            invert: false,
            enabled: true,
            callback: "camMoveCamZ"
        },
        {//RIGHT X
            value: 2,
            min: 1,
            max: 3,
            step: .1,
            invert: false,
            enabled: false,
            callback: ""
        },
        {//RIGHT Y
            value: 0,
            min: -1,
            max: 1,
            step: .05,
            invert: true,
            enabled: false,
            callback: "camMovePointZ"
        },
        { //WHEELE
            value: 1.5,
            min: .5,
            max: 3,
            step: .5,
            invert: false,
            enabled: true,
            callback: "camSetDist"
        }
    ]
}

mp.events.add({
    // Server events //
    "houses:openInfoPanel": (data, userinfo) => {
        global.gui.setData('homePurchase/setHomeData', data);
        global.gui.setData('homePurchase/setUserBalance', userinfo);

        data = JSON.parse(data);
        const pos = global.localplayer.position;
        global.customCamera.setPos(new mp.Vector3(data.CamPositionX, data.CamPositionY, data.CamPositionZ));
        global.customCamera.setPoint(new mp.Vector3(pos.x, pos.y, pos.z + .6));
        
        global.customCamera.switchOn(500);
        infoMenuOpened = global.gui.openPage('HomePurchase');
    },

    // Client events //
    "housepurchase::buy": (type) => {
        //mp.serverLog(`type buy ${type}`);
        mp.events.callRemote('houses:buy', type);
        closeInfoMenu()
    },
    "housepurchase::enter": () => {
        mp.events.callRemote('houses:enter', true);
        closeInfoMenu()
    },
    "housepurchase::enterGarage": () => {
        mp.events.callRemote('houses:enter', false);
        closeInfoMenu()
    },
    "housepurchase::breakTheDoor": () => {
        closeInfoMenu()
        mp.events.callRemote('houses:breakTheDoor');
    },
    "housepurchase::close": () => {
        closeInfoMenu()
    },
    "house::playerEntered": (furnitureData, dimension) => {
        furnitureData = JSON.parse(furnitureData);
        furnitureModels = [];
        furnitureData.forEach(e => {
            furnitureModels.push(mp.objects.new(mp.game.joaat(e.modelName), e.position,
                {
                    rotation: e.rotation,
                    dimension: dimension
                }));
        });
    },
    "house::playerLeaved": () => {
        furnitureModels.forEach(e => {
            if (e.doesExist()) e.destroy();
        });
        furnitureModels = [];
    },
    "house::updateFurniture": (furnitureData, dimension) => {
        if (furnitureModels !== []) {
            furnitureModels.forEach(e => {
                if (e.doesExist()) e.destroy();
            });
        }
        furnitureModels = [];
        furnitureData = JSON.parse(furnitureData)
        furnitureData.forEach(e => {
            furnitureModels.push(mp.objects.new(mp.game.joaat(e.modelName), e.position,
                {
                    rotation: e.rotation,
                    dimension: dimension
                }));
        });
    },
    "house::ownerInteracted": (menuData) => {
        openOwnerMenu(menuData)
    },
    "house::startFurnitureInstallation": (houseId, furnitureData) => {
        furnitureData = JSON.parse(furnitureData)
        global.sendTip('tip_furniture')
        closeOwnerMenu()
        let hash = mp.game.joaat(furnitureData.name);
        let mover = new ObjectMover(hash, global.localplayer.position, furnitureData.dimension)
        mover.enable()
        if (!global.gui.openPage('FurnitureHud')) return;
        global.showCursor(false);
        //global.gui.setOpened(true)
        mover.addCallback((pos, rot) => placeFurniture(houseId, furnitureData.id, pos, rot))
    },
    "homeMenu:rentCostChanged": (houseId, newValue) => {
        if (isNaN(newValue) || newValue < 0)
		{
            mp.gui.notify(mp.gui.notifyType.INFO, "Пожалуйста, введите корректные данные.", 3000);
            return;
        }
        mp.events.callRemote("house:rentCostChanged", houseId, newValue)
    },
    "homeMenu:sellHouse": (houseId) => {
        closeOwnerMenu()
        mp.events.callRemote("house:sellHouse", houseId)
    },
    "homeMenu:buyGarage": (houseId, index) => {
        closeOwnerMenu()
        mp.events.callRemote("homeMenu:buyGarage", houseId, index)
    },
    "homeMenu:installFurniture": (houseId, index) => {
        closeOwnerMenu()
        mp.events.callRemote("homeMenu:installFurniture", houseId, index)
    },
    "homeMenu:uninstallFurniture": (houseId, index) => {
        mp.events.callRemote("homeMenu:uninstallFurniture", houseId, index)
    },
    "homeMenu:uninstallAllFurniture": (houseId) => {
        mp.events.callRemote("homeMenu:uninstallAllFurniture", houseId)
    },
    "homeMenu:toggleHouseLocked": (houseId, toggle) => {
        mp.events.callRemote("homeMenu:lockToggle", houseId, toggle == true)
    },
    "homeMenu:closeHomeMenu": () => {
        closeOwnerMenu()
    },
    "homeMenu:updateGarage": () => {
        closeOwnerMenu()
    },
})

let furnitureModels = [];

// mp.keys.bind(global.Keys.Key_H, true, () => {
//     let mover = new ObjectMover(mp.game.joaat("apa_mp_h_stn_sofacorn_05"), global.localplayer.position)
//     mover.enable()
// })

let ownerMenuOpened = false
function openOwnerMenu(menuData) 
{
    if (ownerMenuOpened) return
    ownerMenuOpened = global.gui.openPage("HomeMenu");

    global.gui.setData("homeMenu/setFullState", menuData)
}

let infoMenuOpened = false
function closeInfoMenu() {
    if (!infoMenuOpened) return
    global.customCamera.switchOff(500);
    infoMenuOpened = false
    global.gui.close()
}

mp.keys.bind(global.Keys.Key_ESCAPE, false, closeOwnerMenu)

function closeOwnerMenu() {
    closeInfoMenu()
    if (!ownerMenuOpened) return
    ownerMenuOpened = false
    global.gui.close()
}

function placeFurniture(houseId, furnitureId, position, rotation) {
    global.gui.close();
    global.showCursor(false);
    mp.events.callRemote("homeMenu:furniturePlaced", houseId, furnitureId, JSON.stringify(position), JSON.stringify(rotation))
}


let interiorsProps = {
    [1]: ['entity_set_style_1', 'entity_set_tints'],
    [2]: ['entity_set_style_2'],
    [3]: ['entity_set_style_3'],
    [4]: ['entity_set_style_4'],
    [5]: ['entity_set_style_5'],
    [6]: ['entity_set_style_6'],
    [7]: ['entity_set_style_7'],
    [8]: ['entity_set_style_8'],
    [9]: ['entity_set_style_9'],
}
let currentIndex = {};

function loadProps(interiorID, disable, enable) {
    if (disable)
        disable.forEach(prop => {
            mp.game.interior.disableInteriorProp(interiorID, prop);
        });
    if (enable)
        enable.forEach(prop => {
            mp.game.interior.enableInteriorProp(interiorID, prop);
        });
    mp.game.interior.refreshInterior(interiorID);
}
mp.events.add('garage:loadInteriors', (pos, index) => {
    let interiorID = mp.game.interior.getInteriorAtCoords(pos.x, pos.y, pos.z);
    let newProps = interiorsProps[index];
    let oldProps = [];
    if (currentIndex[interiorID]) {
        if (currentIndex[interiorID] == index) return;
        oldProps = interiorsProps[currentIndex[interiorID]];
    }
    loadProps(interiorID, oldProps, newProps)
    mp.game.interior.refreshInterior(interiorID);
    currentIndex[interiorID] = index;
});

mp.events.call('garage:loadInteriors', new mp.Vector3(-1350, 156, -99), 1);

loadProps(mp.game.interior.getInteriorAtCoords(-2000.0, 1113.211, -25), [],
    [
        'entity_set_meet_crew',
        'entity_set_meet_lights',
        'entity_set_meet_lights_cheap',
        'entity_set_player',
        'entity_set_test_crew',
        'entity_set_test_lights',
        'entity_set_test_lights_cheap',
        'entity_set_time_trial',
    ]);