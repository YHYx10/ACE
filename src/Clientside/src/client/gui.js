class gui{
    constructor(url) {
        this.url = url;
        this.isReady = false;
        this.browser = null;
        this.opened = false;
        this.inventoryOpened = false;
        this.curPage = "";
        this.queue = [];
        this.debug = false;
        this.censored = require('./configs/censure.js');
    }

    init() {
        if (this.browser) return;
        this.browser = mp.browsers.new(this.url);
    }
    
    ready(){
        if (this.isReady) return;
        mp.gui.chat.show(false);
        this.browser.markAsChat();
        this.isReady = true;
        if(this.queue.length > 0){
            this.queue.forEach(element => {
                this.setData(element.fnc, element.data);
            });
            this.queue = [];
        }
        mp.events.call('gui:ready');

        //global.gui.setData('hud/updateData', JSON.stringify({ name: 'id', value: 1488 })); //mp.players.local.remoteId 
        this.setData(
            'optionsMenu/setSettings', 
            JSON.stringify(mp.storage.data.mainSettings)
        );
    }

    isOpened(){
        return (!this.isReady || this.opened || this.inventoryOpened || mp.players.local.getVariable('InDeath') == true)
    }

    setOpened(toggle) {
        this.opened = toggle;
    }
    
    openPage(page) {
        if (this.isOpened()) return false;
        global.showHud(false);
        global.showCursor(true);
        this.opened = true;
        this.setData('setPage', `'${page}'`);
        this.curPage = page;
        return true;
    }

    close(){
        if (!this.isReady) return; 
        global.showHud(true);
        global.showCursor(false);
        if(this.inventoryOpened) this.closeInventory();
        this.opened = false;
        this.curPage = '';
        this.setData('setPage', '');
    }

    setData(fnc, data){
        if (!this.isReady){
            this.queue.push({fnc, data})
        } else this.browser.execute(`setData('${fnc}', ${data})`)
    }

    dispatch(fnc, data){
        if (!this.isReady){
            this.queue.push({fnc, data})
        } else this.browser.execute(`dispatch('${fnc}', ${data})`)
    }

    call(fnc){
        if(!this.browser) return;
        this.browser.execute(fnc)
    }

    playSound(name, volume = 1, loop = false){
        this.setData('sounds/play', JSON.stringify({name,volume,loop}));
    }

    playSoundLang(name, lang, volume = 1, loop = false){
        this.setData('sounds/playLang', JSON.stringify({name, lang, volume, loop}));
    }

    stopSound(){
        this.setData('sounds/stop');
    }

    pushChat(type, msg, id, from, toId = -1, to = "", friend){
        if(!this.browser) return;
        if(mp.storage.data.mainSettings.censore === true){
            msg = this.censureHandle(msg);
        }
        this.browser.execute(`chatAPI.push(${type},'${msg}',${id},'${from}',${toId},'${to}', ${friend})`);
    }

    pushChatAdvert(type, redactor, msg, from, sim){
        if(!this.browser) return;
        if(mp.storage.data.mainSettings.censore === true){
            msg = this.censureHandle(msg);
        }
        this.browser.execute(`chatAPI.push(${type}, '${redactor}', '${msg}', '${from}','${sim}', '')`);
    }

    clearChat(){
        if(!this.browser) return;
        this.browser.execute('chatAPI.clear()');
    }

    openPhone(status, cursor){
        if(status && this.isOpened()) return false;
        if(!status && !global.isPhoneOpened) return false;
        this.setData('setPhoneActive', status);
        if (status)
            this.dispatch('smartphone/messagePage/checkChatIsLoading')
        global.showCursor(cursor);
        this.opened = status;
        return true;
    }

    updateLang(lang){
        this.setData('localiazation/setLang', `'${lang}'`)
    }

    openInventory(){
        if(this.isOpened() || global.IsPlayingDM) return false
        this.inventoryOpened = true;
        global.showCursor(true);
        this.setData('inventoryEnabled', 'true');
        return true;
    }
    
    closeInventory(){
        this.inventoryOpened = false;
        global.showCursor(false);
        this.setData('inventoryEnabled', 'false');   
    }

    showChat(){
        global.chatActive = true;
        this.opened = true;
        global.showCursor(true);
        this.browser.execute("chatAPI.enable(true)");
    }
    hideChat(){
        global.chatActive = false;
        this.opened = false;
        global.showCursor(false);
        this.browser.execute("chatAPI.enable(false)");
    }
    
    censureHandle(msg){
        this.censored.forEach(word => {
            msg = msg.replace(word.reg,  word.rplc);
        });
        return msg;
    }
}

global.gui = new gui('package://gui/index.html');

mp.events.add('browserDomReady', (browser) => {
    if (global.gui && browser === global.gui.browser) {
        global.gui.ready();        
    }
});

mp.events.add('authready', () => {
    global.gui.init();
});

mp.events.add("efwd", (cal, ...args) => {
    if (global.gui.debug) {
        mp.serverLog(`${mp.players.local.name}: ${cal} ${args.toString()}`);
    }    
    mp.events.callRemote(cal, ...args);
});

mp.events.add("guiClose", () => {
    global.gui.close();
});

mp.events.add("guiPlaySound", (name) => {
    global.gui.playSound(name);
});

mp.events.add("gui:setData", (func, data) => {
    if (global.gui.debug)
        mp.serverLog(`${mp.players.local.name}: gui:setData (${func}) - ${data}`);
    global.gui.setData(func, data);
});

mp.events.add("gui:dispatch", (func, data) => {
    if (global.gui.debug)
        mp.serverLog(`${mp.players.local.name}: gui:dispatch (${func}) - ${data}`);
    global.gui.dispatch(func, data);
});

mp.events.addDataHandler("InDeath", (entity, isDeath) => {
    if (entity === mp.players.local && isDeath == true){
        if(global.gui.opened)
            global.gui.close();        
    }
});

setInterval(function () {
    var street = ["AIRP", "ALAMO", "ALTA", "ARMYB", "BANHAMC", "BANNING", "BEACH", "BHAMCA", "BRADP", "BRADT", "BURTON", "CALAFB", "CANNY", "CCREAK", "CHAMH", "CHIL", "CHU", "CMSW", "CYPRE", "DAVIS", "DELBE", "DELPE", "DELSOL", "DESRT", "DOWNT", "DTVINE", "EAST_V", "EBURO", "ELGORL", "ELYSIAN", "GALFISH", "golf", "GRAPES", "GREATC", "HARMO", "HAWICK", "HORS", "HUMLAB", "JAIL", "KOREAT", "LACT", "LAGO", "LDAM", "LEGSQU", "LMESA", "LOSPUER", "MIRR", "MORN", "MOVIE", "MTCHIL", "MTGORDO", "MTJOSE", "MURRI", "NCHU", "NOOSE", "OCEANA", "PALCOV", "PALETO", "PALFOR", "PALHIGH", "PALMPOW", "PBLUFF", "PBOX", "PROCOB", "RANCHO", "RGLEN", "RICHM", "ROCKF", "RTRAK", "SanAnd", "SANCHIA", "SANDY", "SKID", "SLAB", "STAD", "STRAW", "TATAMO", "TERMINA", "TEXTI", "TONGVAH", "TONGVAV", "VCANA", "VESP", "VINE", "WINDF", "WVINE", "ZANCUDO", "ZP_ORT", "ZQ_UAR", "BAYTRE", "OBSERV"],
        zones = mp.game.zone.getNameOfZone(localplayer.position.x, localplayer.position.y, localplayer.position.z),
        district = street.includes(zones) ? ["Los Santos International Airport", "Alamo Sea", "Alta", "Fort Zancudo", "Banham Canyon Dr", "Banning", "Vespucci Beach", "Banham Canyon", "Braddock Pass", "Braddock Tunnel", "Burton", "Calafia Bridge", "Raton Canyon", "Cassidy Creek", "Chamberlain Hills", "Vinewood Hills", "Chumash", "Chiliad Mountain State Wilderness", "Cypress Flats", "Davis", "Del Perro Beach", "Del Perro", "La Puerta", "Grand Senora Desert", "Downtown", "Downtown Vinewood", "East Vinewood", "El Burro Heights", "El Gordo Lighthouse", "Elysian Island", "Galilee", "GWC and Golfing Society", "Grapeseed", "Great Chaparral", "Harmony", "Hawick", "Vinewood Racetrack", "Humane Labs and Research", "Bolingbroke Penitentiary", "Little Seoul", "Land Act Reservoir", "Lago Zancudo", "Land Act Dam", "Legion Square", "La Mesa", "La Puerta", "Mirror Park", "Morningwood", "Richards Majestic", "Mount Chiliad", "Mount Gordo", "Mount Josiah", "Murrieta Heights", "North Chumash", "N.O.O.S.E", "Pacific Ocean", "Paleto Cove", "Paleto Bay", "Paleto Forest", "Palomino Highlands", "Palmer-Taylor Power Station", "Pacific Bluffs", "Pillbox Hill", "Procopio Beach", "Rancho", "Richman Glen", "Richman", "Rockford Hills", "Redwood Lights Track", "San Andreas", "San Chianski Mountain Range", "Sandy Shores", "Mission Row", "Stab City", "Maze Bank Arena", "Strawberry", "Tataviam Mountains", "Terminal", "Textile City", "Tongva Hills", "Tongva Valley", "Vespucci Canals", "Vespucci", "Vinewood", "Ron Alternates Wind Farm", "West Vinewood", "Zancudo River", "Port of South Los Santos", "Davis Quartz", "Baytree Canyon", "Galileo Observatory"][street.indexOf(zones)] : zones,
        //"",
    street2 = mp.game.pathfind.getStreetNameAtCoord(localplayer.position.x, localplayer.position.y, localplayer.position.z, 0, 0),
        street_hash = mp.game.ui.getStreetNameFromHashKey(street2.streetName);

    // var playerHeadingDegrees = 360.0 - mp.players.local.getHeading();
    global.gui.setData('hud/updateMap', JSON.stringify({title: district, descr: street_hash}));
}, 1000);

// function degreesToIntercardinalDirection(dgr) {
// 	dgr %= 360.0;

// 	if (dgr >= 0.0 && dgr < 22.5 || dgr >= 337.5) return 'N';
// 	if (dgr >= 22.5 && dgr < 67.5) return 'NE';
// 	if (dgr >= 67.5 && dgr < 112.5) return 'E';
// 	if (dgr >= 157.5 && dgr < 202.5) return 'S';
// 	if (dgr >= 112.5 && dgr < 157.5) return 'SE';
// 	if (dgr >= 202.5 && dgr < 247.5 || dgr > -112.5 && dgr <= -65.7) return 'SW';
// 	if (dgr >= 247.5 && dgr <= 292.5 || dgr > -65.7 && dgr <= -22.5) return 'W';
// 	if (dgr >= 292.5 && dgr < 337.5 || dgr > -22.5 && dgr <= 0) return 'NW';
// }
