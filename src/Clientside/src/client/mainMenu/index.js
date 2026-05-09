require('./fractionMenu.js')
require('./referals.js')
let mainMenuOpened = false;
mp.keys.bind(global.Keys.Key_M, false, ()=> {
    if(mainMenuOpened || global.chatActive || global.gui.isOpened()) return;
    mainMenuOpened = global.gui.openPage("OptionsMenu");
})

mp.keys.bind(global.Keys.Key_ESCAPE, false, ()=> {
    close();
})

mp.events.add("mmenu:stats:update", (data) =>{
    global.gui.setData('optionsMenu/setStats', data);
})

mp.events.add("mmenu:donate:update", (premium, dataMoney) =>{
    // var finalData;
    // data.forEach(el => {
    //     if(el.Type == 1){
    //         finalData.premium.price = el.Price;
    //     }else if(el.Type == 2){
    //         finalData.money = { id: el.Id, name: el.Name, price: el.Price }
    //     }
    // });
    //mp.serverLog(`premium DATA: ${premium}`);
    //mp.serverLog(`DONATE DATA: ${dataMoney}`);
    global.gui.setData('optionsMenu/setDonatePremium', premium);
    global.gui.setData('optionsMenu/setDonateMoney', dataMoney);
    // global.gui.setData('optionsMenu/setDonateMoney', JSON.parse(dataMoney));
})

mp.events.add("mmenu:donateExclusive:update", (price, count, maxcount) =>{
    global.gui.setData('optionsMenu/setExclusivePrice', price);
    global.gui.setData('optionsMenu/setExclusiveCount', count);
    global.gui.setData('optionsMenu/setExclusiveMaxCount', maxcount);
})

mp.events.add("mmenu:open:donate", () =>{
    close();
    mp.events.call("dshop:open");
})

mp.events.add("mmenu:props:update", (data) =>{
    global.gui.setData('optionsMenu/setProps', data);
})

mp.events.add("mmenu:products:update", (data) =>{
    global.gui.setData('optionsMenu/setProducts', data);
})

mp.events.add("mmenu:setting:set", (name, status) =>{   
    mp.storage.data.mainSettings[name] = status;
    mp.storage.flush();
    if(name == 'hint') global.gui.setData('hud/showHelp', status);
})

mp.events.add("mmenu:bp:update", (bp) =>{   
    global.gui.setData("optionsMenu/updateBonusPoints", `${bp}`);
})

mp.events.add("cef:mmenu:close", () =>{
    close();
})

mp.events.add("cef:mmenu:capt:open", ()=>{
    close();
    global.gui.setData("optionsMenu/setAttack", 'false');
    mp.events.callRemote("mmenu:captteam")
})

function close(){    
    if(!mainMenuOpened) return;
    global.gui.close();
    mainMenuOpened = false
}
