let answersTemplates = [];

let reports = {};

let opened = false;

let openPage = null;

let openReport = -1;

function jsString(v) {
    return String(v || '').replace(/\\/g, '\\\\').replace(/'/g, "\\'").replace(/\r?\n/g, ' ');
}

function execUi(js) {
    try {
        if (global.gui && global.gui.browser) global.gui.browser.execute(js);
    } catch (e) {}
}

function buildOverlayTickets() {
    return Object.values(reports)
        .filter(item => item && item.closed === false)
        .sort((a, b) => Number(b.time || 0) - Number(a.time || 0))
        .map(item => ({
            id: item.id,
            playerId: item.reporterId || 0,
            playerName: item.reporterName || 'Player',
            playerLevel: 0,
            subject: item.messages && item.messages.length ? item.messages[0].text : 'Issue of the report',
            message: item.messages && item.messages.length ? item.messages[0].text : 'Issue of the report',
            status: item.closed ? 'closed' : (item.adminName ? 'assigned' : 'open'),
            assignedToName: item.adminName || null,
            createdAt: Number(item.time || Date.now()),
            messages: Array.isArray(item.messages) ? item.messages.map((message, index) => ({
                id: message.id || `${item.id}-${index}`,
                sender: message.sender || item.reporterName || 'Player',
                text: message.text || '',
                createdAt: Number(item.time || Date.now()),
                isPlayer: (message.sender || '').toLowerCase() === String(item.reporterName || '').toLowerCase()
            })) : []
        }));
}

function syncOverlay() {
    const tickets = buildOverlayTickets();
    let selfId = 0;
    try { selfId = mp.players.local.remoteId; } catch (e) {}
    if (!selfId) {
        try { selfId = global.getVariable(mp.players.local, 'C_ID', 0); } catch (e) {}
    }
    const selfName = mp.players.local.name ? mp.players.local.name.replace('_', ' ') : 'Administrator';
    execUi(`try{
        if(window.adminAPI){
            window.adminAPI.setConnected && window.adminAPI.setConnected(true);
            window.adminAPI.setSelf && window.adminAPI.setSelf(${Number(selfId || 0)}, '${jsString(selfName)}');
            window.adminAPI.setTickets && window.adminAPI.setTickets('${jsString(JSON.stringify(tickets))}');
        }
    }catch(e){}`);
}

mp.keys.bind(global.Keys.Key_F6, false, function () {
    if (opened)
        CloseMenu();
    else
        OpenMenu()
});

mp.keys.bind(global.Keys.Key_ESCAPE, false, function () {
    if (opened)
        CloseMenu();
});

mp.events.add('report:closemenu', () => {
    CloseMenu();
});

function OpenMenu() {
    if (!global.loggedin || getAdminLevel() < 1) return;
    if (global.checkIsAnyActivity()) return;
    opened = global.gui.openPage('ReportMenu');
    if (!opened) return;
    execUi("try{window.adminAPI&&window.adminAPI.open&&window.adminAPI.open();}catch(e){}");
    syncOverlay();
}

function CloseMenu() {
    execUi("try{window.adminAPI&&window.adminAPI.close&&window.adminAPI.close();}catch(e){}");
    if (global.gui && global.gui.curPage === 'ReportMenu') {
        global.gui.close();
    } else {
        global.showCursor(false);
    }
    opened = false;
}

function getAdminLevel() {
    const syncedLevel = global.getVariable(mp.players.local, 'ALVL', undefined);
    if (syncedLevel !== undefined) return syncedLevel;
    return global.LOCAL_ADMIN_LVL || 0;
}

mp.events.add('report:addreport', (reportJson) => {
    let report = JSON.parse(reportJson);
    if(!report) return;
    reports[report.ID] = {
        id: report.ID,
        time: report.OpenedDate * 1000,
        reporterName: report.Author,
        reporterId: report.AuthorApiID,
        reporterStatus: report.StateAuthor,
        adminName: report.AdminName,
        closed: report.Closed,
        unreadMessages: 0,
        messages: report.Answers
    };
    global.gui.setData('reportMenu/addReport', JSON.stringify(reports[report.ID]));
    UpdateClosedReport();
    syncOverlay();
});

mp.events.add('report:loadreports', (reportListJson, access) => {    
    if (!mp.storage.data.answerTemplate)
        mp.storage.data.answerTemplate = [];
    global.gui.setData('reportMenu/setAnswersTemplates', JSON.stringify(mp.storage.data.answerTemplate));
    global.gui.setData('reportMenu/setMySettingsName', JSON.stringify(mp.players.local.name.replace('_',' ')));

    let reportList = JSON.parse(reportListJson);
    reportList.forEach(report => {
        reports[report.ID] = {
            id: report.ID,
            time: report.OpenedDate * 1000,
            reporterName: report.Author,
            reporterId: report.AuthorApiID,
            reporterStatus: report.StateAuthor,
            adminName: report.AdminName,
            closed: report.Closed,
            unreadMessages: 0,
            messages: report.Answers
        };
    });
    global.gui.setData('reportMenu/setReports', JSON.stringify(Object.values(reports).filter(item => item.closed == false && item.adminName == null)));
    global.gui.setData('reportMenu/setMyReports', JSON.stringify(Object.values(reports).filter(item => item.closed == true && item.adminName == mp.players.local.name.replace('_',' '))));
    global.gui.setData('reportMenu/setReportsLogs', JSON.stringify(Object.values(reports).filter(item => access && item.closed == true || (item.closed == false && item.adminName != null))));
    UpdateClosedReport();
    syncOverlay();
});

mp.events.add('report:updatetakereport', (access, reportId, adminName) => {
    if(!reports[reportId]) return;
    reports[reportId].adminName = adminName;
    if (adminName != null) {
        if (adminName != mp.players.local.name.replace('_',' ')) {
            global.gui.setData('reportMenu/deleteReport', JSON.stringify(reportId));
            if (access)
                global.gui.setData('reportMenu/addReportLogs', JSON.stringify(reports[reportId]));
        }
    }
    else {
        global.gui.setData('reportMenu/deleteReportLogs', JSON.stringify(reportId));
        global.gui.setData('reportMenu/addReport', JSON.stringify(reports[reportId]));
    }
    UpdateClosedReport();
    syncOverlay();
});

mp.events.add('report:addmessage', (messageDTO) => {
    let message = JSON.parse(messageDTO);
    if (!reports[message.reportId])
        return;
    reports[message.reportId].messages.push(message);
    global.gui.setData('reportMenu/addReportMessage', JSON.stringify(message));
    global.gui.setData('reportMenu/addReportLogsMessage', JSON.stringify(message));
    syncOverlay();
});

mp.events.add('report:closereport', (access, reportId, adminName) => {
    if(!reports[reportId]) return;
    reports[reportId].adminName = adminName;
    reports[reportId].closed = true;
    global.gui.setData('reportMenu/deleteReport', JSON.stringify(reportId));
    global.gui.setData('reportMenu/addReportsLogAdmins', JSON.stringify(adminName));

    global.gui.setData('reportMenu/updateAdminNameForReportLog', JSON.stringify({ id: reportId, adminName: adminName }));


    if (access)
        global.gui.setData('reportMenu/addReportLogs', JSON.stringify(reports[reportId]));
    if (adminName == mp.players.local.name.replace('_',' '))
        global.gui.setData('reportMenu/addMyReport', JSON.stringify(reports[reportId]));
    UpdateClosedReport();
    syncOverlay();
});


mp.events.add('report:sendrating', (reportId, rating) => {
    reports[reportId].rating = rating;
    global.gui.setData('reportMenu/updateRating', JSON.stringify({ id: reportId, rating: rating }));
});

mp.events.add('reportMenu:trytakereport', (reportId, text) => {
    mp.events.callRemote('report:takereport', reportId, text)
});

mp.events.add('reportMenu:addAnswerTemplate', (text) => {
    if (!mp.storage.data.answerTemplate)
        mp.storage.data.answerTemplate = [];
    mp.storage.data.answerTemplate.push({ text: text });
    mp.storage.flush();
});

mp.events.add('reportMenu:deleteAnswerTemplate', (index) => {
    if (!mp.storage.data.answerTemplate)
        mp.storage.data.answerTemplate = [];
    mp.storage.data.answerTemplate.splice(index, 1);
    mp.storage.flush();
});

mp.events.add('reportMenu:kickPlayer', (reportId, text) => {
    mp.events.callRemote('report:kick', reportId, text)
});

mp.events.add('reportMenu:closereport', (reportId, rating) => {
    mp.events.callRemote('report:sendclosereport', reportId, rating)
});

mp.events.add('reportMenu:sendmessage', (reportId, message) => {
    mp.events.callRemote('report:sendmessage', reportId, message)
});

mp.events.add('reportMenu:hotkeys', (reportId, key) => {
    mp.events.callRemote('report:presshotkey', reportId, key)
});

let menuHash = mp.game.joaat("FE_MENU_VERSION_MP_PAUSE");
global.reportPosirion = -1;
mp.events.add('reportMenu:position', (reportId)=>{
    try {
        global.reportPosirion = reportId;
        mp.game.ui.activateFrontendMenu(menuHash, true, -1);       
    } catch (e) {
        if(global.sendException) mp.serverLog(`Error in reportMenu:position: ${e.name}\n${e.message}\n${e.stack}`);
	}
});

function UpdateClosedReport() {
    let reportsInQueue = Object.values(reports).filter(item => item.closed == false && item.adminName == null).length;
    let reportsAnswered = Object.values(reports).filter(item => item.closed == true).length;
    global.gui.setData('reportMenu/setReportsInQueue', JSON.stringify(reportsInQueue));
    global.gui.setData('reportMenu/setReportsAnswered', JSON.stringify(reportsAnswered));
    global.gui.setData('hud/setReportsCount', reportsInQueue);
}
