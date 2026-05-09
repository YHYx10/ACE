import './global';

let managementPanelOpened = false;

function isGuiReady() {
    return global.gui && typeof global.gui.openPage === 'function' && typeof global.gui.setData === 'function';
}

function requestManagementData() {
    if (!managementPanelOpened) return;
    if (mp.events && typeof mp.events.callRemote === 'function') {
        mp.events.callRemote('management:requestData');
    }
}

function openManagementPanel() {
    if (!global.loggedin || !isGuiReady()) return;
    if (global.checkIsAnyActivity && global.checkIsAnyActivity()) return;

    managementPanelOpened = global.gui.openPage('ManagementPanel');
    if (managementPanelOpened) {
        global.gui.setData('managementPanel/setLoading', 'true');
        requestManagementData();
    }
}

function closeManagementPanel() {
    if (!managementPanelOpened) return;
    if (global.gui && global.gui.curPage === 'ManagementPanel') {
        global.gui.close();
    }
    managementPanelOpened = false;
}

mp.events.add('management:open', openManagementPanel);

mp.events.add('management:refresh', () => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setLoading', 'true');
    requestManagementData();
});

mp.events.add('management:playerAction', (action, targetUuid) => {
    if (!managementPanelOpened) return;
    if (!mp.events || typeof mp.events.callRemote !== 'function') return;
    mp.events.callRemote('management:playerAction', action, Number(targetUuid));
});

mp.events.add('management:punishment:requestHistory', (targetUuid) => {
    if (!managementPanelOpened) return;
    if (!mp.events || typeof mp.events.callRemote !== 'function') return;
    mp.events.callRemote('management:punishment:requestHistory', Number(targetUuid));
});

mp.events.add('management:punishment:execute', (action, targetUuid, duration, reason) => {
    if (!managementPanelOpened) return;
    if (!mp.events || typeof mp.events.callRemote !== 'function') return;
    mp.events.callRemote('management:punishment:execute', action, Number(targetUuid), Number(duration), reason || '');
});

mp.events.add('management:database:searchCharacters', (query) => {
    if (!managementPanelOpened) return;
    if (!mp.events || typeof mp.events.callRemote !== 'function') return;
    mp.events.callRemote('management:database:searchCharacters', query || '');
});

mp.events.add('management:database:getCharacterProfile', (targetUuid) => {
    if (!managementPanelOpened) return;
    if (!mp.events || typeof mp.events.callRemote !== 'function') return;
    mp.events.callRemote('management:database:getCharacterProfile', Number(targetUuid));
});

mp.events.add('management:setData', (data) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setData', data);
});

mp.events.add('management:setError', (message) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setError', JSON.stringify(message || 'Failed to load management data.'));
});

mp.events.add('management:punishment:setHistory', (data) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setPunishmentHistory', data);
});

mp.events.add('management:punishment:setHistoryError', (message) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setPunishmentHistoryError', JSON.stringify(message || 'Failed to load punishment history.'));
});

mp.events.add('management:database:setSearchResults', (data) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setDatabaseSearchResults', data);
});

mp.events.add('management:database:setProfile', (data) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setDatabaseProfile', data);
});

mp.events.add('management:database:setError', (message) => {
    if (!isGuiReady()) return;
    global.gui.setData('managementPanel/setDatabaseError', JSON.stringify(message || 'Failed to load character database data.'));
});

if (global.Keys && global.Keys.Key_ESCAPE) {
    mp.keys.bind(global.Keys.Key_ESCAPE, false, () => {
        closeManagementPanel();
    });
}
