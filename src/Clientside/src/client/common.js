
// if(global.sendException)
//     mp.game.enableInvokeDebug();

global.showCursor(true);
mp.game.gameplay.disableAutomaticRespawn(true);
mp.game.gameplay.ignoreNextRestart(true);
mp.game.gameplay.setFadeInAfterDeathArrest(false);
mp.game.gameplay.setFadeOutAfterDeath(false);
mp.game.gameplay.setFadeInAfterLoad(false);

function showGameHud(show) {
    mp.game.ui.displayAreaName(show);
    mp.game.ui.displayRadar(show);
    mp.game.ui.displayHud(show);
    mp.gui.chat.show(show);
}

global.showHud = (show)=>{
    if(show){
        global.gui.setData('hud/showHud', mp.storage.data.mainSettings.showHud == true ? 'true' : 'false');
        showGameHud(mp.storage.data.mainSettings.showMiniMap == true);
    }else{
            global.gui.setData('hud/showHud', 'false');
            showGameHud(false);
    }
}

mp.peds.newValid = (model, position, heading, dimension) => {
    const modelHash = typeof(model) === 'string' ?  mp.game.joaat(model) : model;

    if (!mp.game.streaming.isModelValid(modelHash)){
        if(global.sendException) mp.serverLog(`Bad ped model: ${model}/${modelHash}`);
        return null;
    }
    
    const ped = mp.peds.new(modelHash, position, heading, dimension)
    for (let index = 0; (!ped.doesExist() && ped.handle !== 0 && index < 250); index++) {
        mp.game.wait(0);        
    }
    return ped;
};

function checkPedsLoaded(){
    if(global.bizPedLoaded && global.clientPedLoaded && global.gui.isReady){
        global.localplayer.freezePosition(true);        
        mp.players.local.setCollision(true,true);
        clearInterval(pedLoadChecker);
        global.gui.close();
        global.gui.dispatch("localization/loadLangs");
        // if(mp.storage.data.soundOnStart === undefined){
        //     mp.storage.data.soundOnStart = true;
        //     mp.storage.flush();    
        // }
        // if(mp.storage.data.soundOnStart)
        //     global.gui.playSound("auth_bg", .1, true);  

        // global.gui.setData("auth/updateSoundState", mp.storage.data.soundOnStart);
        if(mp.storage.data.language === undefined){
            
            global.gui.dispatch('localization/setLang', 'ru');
            //global.gui.openPage("SelectLanguage");
        }else{
            global.gui.dispatch('localization/setLang', 'ru');
            sendPlayerToAuth();
        }   
    }
}

function sendPlayerToAuth(){
    global.gui.openPage("Auth");
    let passwd = mp.storage.data.auth && mp.storage.data.auth.save ? mp.storage.data.auth.password : '';
    let login = mp.storage.data.auth && mp.storage.data.auth.save ? mp.storage.data.auth.login : '';
	let isSaved = mp.storage.data.auth && mp.storage.data.auth.save ? true : false;
	global.gui.setData("auth/setIsRememberPass", isSaved);  
    mp.events.callRemote('Auth:PlayerReady', login, passwd);
}

mp.events.add("auth:sond:switch", ()=>{
    // mp.storage.data.soundOnStart = !mp.storage.data.soundOnStart;
    mp.storage.flush();
    // if(mp.storage.data.soundOnStart)
    //     global.gui.playSound("auth_bg", .1, true); 
    // else
    //     global.gui.stopSound();
    // global.gui.setData("auth/updateSoundState", mp.storage.data.soundOnStart);
})

mp.events.add("language:save", (lang)=>{
    if(mp.storage.data.language !== lang){
        mp.storage.data.language = lang;
        mp.storage.flush();
    }
})

mp.events.add("language:next", ()=>{
    global.gui.close();
    sendPlayerToAuth();
})

mp.game.audio.setAudioFlag("DisableFlightMusic", true);
mp.game.audio.setAudioFlag("FrontendRadioDisabled", true);
mp.game.vehicle.defaultEngineBehaviour = false;
global.localplayer.farmAction = -1;

//init settings
const settingVersion = 7;
if(mp.storage.data.mainSettings === undefined){
    mp.storage.data.mainSettings = {
        noMicro: true,
        hint: false,
        showNames: true,
        showHud: true,
        voiceValue: 0,
        showMiniMap: true,
        muteLowLevel: false,
        muteLowLevelValue: 7,
        showFamilyMembers: false,
        version: settingVersion,
        trafficOff: true,
        censore: false
    }
    mp.storage.flush();
}

if(mp.storage.data.mainSettings.version !== settingVersion){
    mp.storage.data.mainSettings.version = settingVersion;
    mp.storage.data.mainSettings.trafficOff = true;
    mp.storage.flush();
}

//relations
const PlayerHash = mp.game.joaat("PLAYER");
const NonFriendlyHash = mp.game.joaat("FRIENDLY_PLAYER");
const FriendlyHash = mp.game.joaat("NON_FRIENDLY_PLAYER");
global.localplayer.setRelationshipGroupHash(PlayerHash);
mp.game.ped.addRelationshipGroup("FRIENDLY_PLAYER", 0);
mp.game.ped.addRelationshipGroup("NON_FRIENDLY_PLAYER", 0);
mp.game.ped.setRelationshipBetweenGroups(0, PlayerHash, FriendlyHash);
mp.game.ped.setRelationshipBetweenGroups(5, PlayerHash, NonFriendlyHash);
mp.game.ped.setRelationshipBetweenGroups(5, NonFriendlyHash, PlayerHash);

// LOAD ALL DEFAULT IPL'S
const ipls =[
    "hei_dlc_windows_casino",
    "bh1_47_joshhse_unburnt",
    "bh1_47_joshhse_unburnt_lod",
    "CanyonRvrShallow",
    "ch1_02_open",
    "Carwash_with_spinners",
    "sp1_10_real_interior",
    "sp1_10_real_interior_lod",
    "ferris_finale_Anim",
    "fiblobby",
    "fiblobby_lod",
    "apa_ss1_11_interior_v_rockclub_milo_",
    "hei_sm_16_interior_v_bahama_milo_",
    "hei_hw1_blimp_interior_v_comedy_milo_",
    "gr_case6_bunkerclosed",
    "EntitySet_DJ_Lighting",
    "hei_dlc_casino_door",
    "vw_dlc_casino_door",
    "vw_casino_main",
    "vw_casino_garage",
    "vw_casino_carpark",
    "vw_casino_penthouse"
];

ipls.forEach(ipl => {
    if(!mp.game.streaming.isIplActive(ipl))
        mp.game.streaming.requestIpl(ipl);
});

mp.game.streaming.removeIpl("hei_bi_hw1_13_door");


//events
let pedLoadChecker = setInterval(checkPedsLoaded, 100);

mp.events.add('gui:ready', () => global.showHud(false));

mp.events.add('svem', (pm, tm) => {
    if (!global.localplayer.isInAnyVehicle(true)) return;
    let vehc = mp.players.local.vehicle;
    vehc.setEnginePowerMultiplier(pm);
    vehc.setEngineTorqueMultiplier(tm);
});

let dmgdisabled = false;
mp.events.add('disabledmg', (toggle) => {
	if(toggle == true) {
		dmgdisabled = true;
		mp.players.forEachInStreamRange(
			(entity) => {
				if(entity != global.localplayer) entity.setRelationshipGroupHash(FriendlyHash);
			}
		);
	} else {
		dmgdisabled = false;
		mp.players.forEachInStreamRange(
			(entity) => {
				if(entity != global.localplayer) entity.setRelationshipGroupHash(NonFriendlyHash);
			}
		);
	}
});

mp.events.add('entityStreamIn', (entity) => {
    try {
        if (!entity || entity.type !== 'player' ) return;
        if(dmgdisabled == true) entity.setRelationshipGroupHash(FriendlyHash);
        else entity.setRelationshipGroupHash(NonFriendlyHash);
    } catch (e) { 
        if(global.sendException) mp.serverLog(`common.entityStreamInPlayer: ${e.name}\n${e.message}\n${e.stack}`);
    }
});

let showRoomPedsData = 
[
	{ 
		ped: mp.peds.newValid('mp_m_freemode_01', new mp.Vector3(-130.63432, -597.8847, 37.753284), -121.75311, 0), // По середине
		gender: true,
		items: 
		[
			{
				key: 1, // Маска
				value: 8
			},
			{
				key: 3, // Торс
				value: 1
			},
			{
				key: 4, // Штаны
				value: 547
			},
			{
				key: 6, // Ботинки
				value: 530
			},
			{
				key: 8, // Undershirt
				value: 15
			},
			{
				key: 11, // Верх
				value: 500
			},
		],
		itemsColors:
		[
			{
				key: 4,
				value: 2
			}
		],
		animation: 
		{
			name: "idle_b",
			dictionary: "amb@world_human_muscle_flex@arms_in_front@idle_a",
			flag: 39
		}
	},
	{ 
		ped: mp.peds.newValid('mp_m_freemode_01', new mp.Vector3(-131.59741, -595.9171, 37.753284), -114.47082, 0),
		gender: true,
		items: 
		[
			{
				key: 1, // Маска
				value: 8
			},
			{
				key: 3, // Торс
				value: 1
			},
			{
				key: 4, // Штаны
				value: 553
			},
			{
				key: 6, // Ботинки
				value: 531
			},
			{
				key: 8, // Undershirt
				value: 15
			},
			{
				key: 11, // Верх
				value: 501
			},
		],
		itemsColors: [ ],
		animation: 
		{
			name: "mini_strip_club_idles_bouncer_go_away_go_away",
			dictionary: "anim@amb@nightclub@peds@",
			flag: 39
		}
	},
	{ 
		ped: mp.peds.newValid('mp_m_freemode_01', new mp.Vector3(-132.88054, -598.28076, 37.753284), -111.46851, 0),
		gender: true,
		items: 
		[
			{
				key: 1, // Маска
				value: 8
			},
			{
				key: 3, // Торс
				value: 1
			},
			{
				key: 4, // Штаны
				value: 554
			},
			{
				key: 6, // Ботинки
				value: 532
			},
			{
				key: 8, // Undershirt
				value: 15
			},
			{
				key: 11, // Верх
				value: 502
			},
		],
		itemsColors: [ ],
		animation: 
		{
			name: "mini_strip_club_idles_bouncer_go_away_go_away",
			dictionary: "anim@amb@nightclub@peds@",
			flag: 39
		}
	}
]

mp.events.add('entityStreamIn', (entity) =>
{
	try
	{
        if (!entity || entity.type !== 'ped' ) return;
		
		const showRoomPedData = showRoomPedsData.find(x => x.ped == entity);
		if (!showRoomPedData) return;
		
		let color;
		let itemObject;
		let itemCustomColor;
		const showRoomPed = showRoomPedData.ped;
		const showRoomPedItems = showRoomPedData.items;
		const showRoomPedItemsColors = showRoomPedData.itemsColors;
		const showRoomPedGender = showRoomPedData.gender;
		const showRoomPedAnimation = showRoomPedData.animation;
		showRoomPed.freezePosition(true);
		showRoomPed.setInvincible(true);
		showRoomPed.setProofs(true, true, true, true, true, true, true, true); 
		
		for (let item = 0; item < showRoomPedItems.length; item++)
		{
			color = 0;
			itemObject = showRoomPedItems[item];
			
			if (showRoomPedItemsColors.length > 0) 
			{
				itemCustomColor = showRoomPedItemsColors.find(x => x.key == itemObject.key);
				if (itemCustomColor) color = itemCustomColor.value;
			}
			global.setPedClothing(showRoomPed, showRoomPedGender, itemObject.key, itemObject.value, color, 0);
		}

		global.preloadAnimDictionary(`${showRoomPedAnimation.dictionary}`);
		entity.taskPlayAnim(`${showRoomPedAnimation.dictionary}`, `${showRoomPedAnimation.name}`, 8.0, 0, -1, showRoomPedAnimation.flag, 0, false, false, false);
	}
	catch (e)
	{ 
		if (!global.sendException) return;
		mp.serverLog(`common.entityStreamInPed: ${e.name}\n${e.message}\n${e.stack}`);
    }
});