
mp.events.add('chat:api:action',(type, msg, id, idTo)=>{
    try
	{      
        if(global.mediaMute && (type === 0 || type === 1 ||type === 2 ||type === 10)) return;
		
        const player = mp.players.atRemoteId(id);
        if(mp.players.exists(player) && mp.storage.data.mainSettings.muteLowLevel)
		{
            const lvl = global.getVariable(player, 'lvl', 0);
            if(lvl < mp.storage.data.mainSettings.muteLowLevelValue) return;
        }
        
		const playerTo = mp.players.atRemoteId(idTo);
        const fromText = player ? player.name.replace('_', ' ') : '';
        const toText = playerTo ? playerTo.name.replace('_', ' ') : '';
        const isFriend = global.iKnowThisPlayer(player);

        if(type === 10 || type === 11) 
		{
			global.gui.pushChat(type, msg, id, fromText, global.getVariable(player, 'C_ID', 0), toText, isFriend);
			return;
		}
		
		const toStaticId = playerTo ? global.getVariable(playerTo, 'C_ID', idTo) : idTo;
        global.gui.pushChat(type, msg, global.getVariable(player, 'C_ID', 0), fromText, toStaticId, toText, isFriend);

    } catch (e) {
        if(global.sendException) mp.serverLog(`chat:api:action: ${e.name}\n${e.message}\n${e.stack}`);
    }
});

mp.events.add('chat:api:advert',(type, redactorId, msg, from, sim)=>{
    try{ 
        if(global.mediaMute) return;
        let redactor = mp.players.atRemoteId(redactorId);
        global.gui.pushChatAdvert(type, redactor ? redactor.name.replace('_', ' ') : 'Unknown', msg, from, sim);
    } catch (e) {
        if(global.sendException) mp.serverLog(`chat:api:advert: ${e.name}\n${e.message}\n${e.stack}`);
    }
});
global.mediaMute = false;
mp.events.add("media:mute:state", (state)=>{
    global.mediaMute = state;
    if(global.mediaMute)
	{
        global.gui.clearChat();
        mp.events.call('notify', 4, 9, "media:mute:on:self", 3000);
    }
	else mp.events.call('notify', 4, 9, "media:mute:off:self", 3000);
})

mp.events.add('chat:api:clear',()=>{
    global.gui.clearChat();
});

function getChatMembershipState() {
    const fraction = Number(global.getVariable(mp.players.local, 'fraction', 0)) || 0;
    const familyuuid = Number(global.getVariable(mp.players.local, 'familyuuid', 0)) || 0;
    const factionName =
        global.getVariable(mp.players.local, 'fractionName', '') ||
        global.getVariable(mp.players.local, 'fractionname', '') ||
        global.getVariable(mp.players.local, 'factionName', '') ||
        global.getVariable(mp.players.local, 'factionname', '') ||
        '';
    const familyIconId =
        global.getVariable(mp.players.local, 'familyIconId', null) ||
        global.getVariable(mp.players.local, 'familyicon', null) ||
        global.getVariable(mp.players.local, 'familyIcon', null);

    return {
        hasFaction: fraction > 0,
        hasFamily: familyuuid > 0,
        factionId: fraction,
        factionName,
        familyIconId
    };
}

function syncChatMembership() {
    if (!global.gui || !global.gui.browser) return;

    const membership = getChatMembershipState();
    global.gui.browser.execute(`chatAPI.setMembership(${JSON.stringify(membership)})`);
}

mp.keys.bind(global.Keys.Key_T, false, ()=>{
    if(global.chatActive || global.gui.opened) return;
    syncChatMembership();
    global.gui.showChat();
});

mp.events.addDataHandler('fraction', (entity) => {
    if(entity === mp.players.local) syncChatMembership();
});

mp.events.addDataHandler('familyuuid', (entity) => {
    if(entity === mp.players.local) syncChatMembership();
});

mp.events.add("cahat:api:disable",()=>{
    global.chatActive = false;
    global.gui.close();
});
