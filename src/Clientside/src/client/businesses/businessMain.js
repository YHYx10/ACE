let onBizColshape = false;
let isInfoPanelOpen = false;

mp.events.add({
    // Server events //
    "businesses:openInfoPanel": (data, userinfo) => {

        // mp.serverLog(data);
        global.gui.setData("businessPurchase/setBusinessData", data);
		global.gui.setData("businessPurchase/setUserBalance", userinfo);
        data = JSON.parse(data);
        const pos = global.localplayer.position;
        global.customCamera.setPos(new mp.Vector3(data.CamPositionX, data.CamPositionY, data.CamPositionZ));
        global.customCamera.setPoint(new mp.Vector3(pos.x, pos.y, pos.z + .6));
        
        global.customCamera.switchOn(500);


        isInfoPanelOpen = global.gui.openPage("BusinessPurchase");
    },

    // CEF events //
    "businesses::infoPanel_closeClick": () => {
        global.customCamera.switchOff(500);
        global.gui.close();
        global.showCursor(false);

        isInfoPanelOpen = false; 
    },

    "businesses::infoPanel_buyClick": (type) => {
        mp.events.callRemote('businesses::buyBusiness', type);
    }
});

mp.keys.bind(global.Keys.Key_ESCAPE, false, function() {
    if (!isInfoPanelOpen) return;
    
    mp.events.call('businesses::infoPanel_closeClick');
});