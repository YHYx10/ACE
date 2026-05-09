let weddingIsOpen = false;

mp.events.add('marriage:invite', () => {
   global.gui.openPage('WeddingMenu');
   weddingIsOpen = true;
   global.gui.setData('weddingMenu/setIsWeddingComplete', JSON.stringify(false));
});

mp.events.add('marriage:inputName', (name) => {
    closeMenu();
    mp.events.callRemote('marriage:callbackInvite', name);
});

mp.events.add('marriage:cancelPropose', () => {
    closeMenu();
});

function closeMenu() 
{
	if (!weddingIsOpen) return;
	
    global.gui.close();
	weddingIsOpen = false;
}

mp.keys.bind(global.Keys.Key_ESCAPE, false, function () {
	closeMenu();
});

mp.events.add('marriage:proposal', (name) => {
    global.gui.openPage('WeddingNotification');
    global.gui.setData('weddingMenu/setWeddingName', JSON.stringify(name));
});

mp.events.add('marriage:apply', () => {
    closeMenu();
    mp.events.callRemote('marriage:callbackProposal', true);
});

mp.events.add('marriage:decline', () => {
    closeMenu();
    mp.events.callRemote('marriage:callbackProposal', false);
});

mp.events.add('marriage:complete', (name) => 
{
	weddingIsOpen = true;
    global.gui.openPage('WeddingMenu');
	global.gui.setData('weddingMenu/setIsWeddingComplete', JSON.stringify(true));
	global.gui.setData('weddingMenu/setCongratulationsName', JSON.stringify(name));
});