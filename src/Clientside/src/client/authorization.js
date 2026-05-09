mp.game.cam.renderScriptCams(false, true, 0, true, false);

mp.events.add('auth:startReg', (name) => {
    global.customCamera.setPos(new mp.Vector3(-350.3614, 1205.5157, 345.3723));
    global.customCamera.setPoint(new mp.Vector3(-430.34457, 1106.5614, 328.87384));
    global.customCamera.switchOn(0);

    //global.gui.setData("setLoadScreen", false)
    global.gui.setData("auth/setSocialClub", JSON.stringify({ name: name }));
    global.gui.setData("auth/setCurrentTab", JSON.stringify({ page: "CreateNewAccountTab" }));
    global.gui.close();
    global.gui.openPage("Auth");
});

let waitAutoAuthResponse = false;
mp.events.add('auth:startAuth', (login) => {

    global.customCamera.setPos(new mp.Vector3(-350.3614, 1205.5157, 345.3723));
    global.customCamera.setPoint(new mp.Vector3(-430.34457, 1106.5614, 328.87384));
    global.customCamera.switchOn(0);

    // global.gui.setData("setLoadScreen", false)
    global.gui.close();
    global.gui.setData("auth/setSocialClub", JSON.stringify({ name: login }));    
    global.gui.setData("auth/setCurrentTab", JSON.stringify({ page: "LoginTab" }));  
    global.gui.openPage("Auth");
});


/// ????
mp.events.add('auth:startCreateCharacter', () => {
    // global.gui.setData("setLoadScreen", false)
    global.gui.close();
    global.gui.setData("auth/setCurrentTab", JSON.stringify({ page: "Customization" }));
    global.gui.openPage("Auth");
});

mp.events.add('auth:character:select', (data) => {
    // global.gui.setData("setLoadScreen", false)
    global.gui.close();
    // global.gui.setData("characterSelect/setData", data);
    // global.gui.setData("characterSelect/setCoins", coins);
    // global.gui.setData("characterSelect/setSlots", slots);
    // global.gui.openPage("CharacterSelect");
    
    // const pos = JSON.parse(data[1]);
    // const {streetName,crossingRoad} = mp.game.pathfind.getStreetNameAtCoord(pos.x, pos.y, pos.z, 0, 0);
    // data[1] = mp.game.ui.getStreetNameFromHashKey(streetName);  
    // var dataspawns = [
    //         {
    //             key: "s1",
    //             name: "213a",
    //             subname: "123123a",
    //             x: 1000,
    //             y: 1000,
    //         },
    //         {
    //             key: "s2",
    //             name: "123b",
    //             subname: "123123b",
    //             x: 1300,
    //             y: 1000,
    //         },
    //         {
    //             key: "s3",
    //             name: "333c",
    //             subname: "123123c",
    //             x: 1000,
    //             y: 1300,
    //         },
    // ];
    // data.push(dataspawns);
    global.gui.setData("characterSelect/setData", data);
    global.gui.openPage("Spawn");
});

mp.events.add('auth:spawn:select', (data, charindex) => {
    // global.gui.setData("setLoadScreen", false)
    // const pos = JSON.parse(data[1]);
    // const {streetName,crossingRoad} = mp.game.pathfind.getStreetNameAtCoord(pos.x, pos.y, pos.z, 0, 0);
    // data[1] = mp.game.ui.getStreetNameFromHashKey(streetName);  

    // var dataspawns = [
            // {
            //   key: "s1",
            //   name: "213",
            //   subname: "123123",
            //   x: 1000,
            //   y: 1000,
            // },
            // {
            //   key: "s2",
            //   name: "123",
            //   subname: "123123",
            //   x: 1300,
            //   y: 1000,
            // },
            // {
            //   key: "s3",
            //   name: "333",
            //   subname: "123123",
            //   x: 1000,
            //   y: 1300,
            // },
    // ];
    global.gui.setData("characterSelect/setIndex", charindex);
    global.gui.setData("characterSelect/setSpawns", JSON.stringify(data));
    // global.gui.setData("characterSelect/trashData", data);

    // spawnPoints: [
    //     {
    //       key: "s1",
    //       name: "213",
    //       subname: "123123",
    //       x: 1000,
    //       y: 1000,
    //     },
    //     {
    //       key: "s2",
    //       name: "123",
    //       subname: "123123",
    //       x: 1300,
    //       y: 1000,
    //     },
    //     {
    //       key: "s3",
    //       name: "333",
    //       subname: "123123",
    //       x: 1000,
    //       y: 1300,
    //     },
    //   ],


    //global.gui.close();
    //global.gui.setData("spawnSelect/setData", JSON.stringify(data));
    //global.gui.openPage("SpawnSelect");
});

mp.events.add("auth:save:pass", (login, password, save) => {
    checkAuthStorage();
    if(save){
        if(mp.storage.data.auth.login !== login || mp.storage.data.auth.password !== password || !mp.storage.data.auth.save){
            mp.storage.data.auth.login=login;
            mp.storage.data.auth.password=password;
            mp.storage.data.auth.save = true;
            mp.storage.flush();
        }
    }else{
        if(mp.storage.data.auth.login !== '' || mp.storage.data.auth.password !== '' || mp.storage.data.auth.save){
            mp.storage.data.auth.login='';
            mp.storage.data.auth.password='';
            mp.storage.data.auth.save = false;
            mp.storage.flush();
        }
    }
})

mp.events.add('auth:charCreated', function (name, surname) {
    mp.events.callRemote("newchar", name, surname)
});

mp.events.add('auth:doSpawn', spawn);
function spawn() {
    try {
        // global.gui.setData("setLoadScreen", true);
        global.customCamera.switchOff(0);
        mp.game.cam.doScreenFadeOut(0);
        global.gui.close();
        setTimeout(()=>{
            global.showHud(false);
        }, 10)
        
        setTimeout(()=>{
            // global.gui.setData("setLoadScreen", false)
            mp.game.cam.doScreenFadeIn(1700);
            global.showHud(true);
            global.checkFarm();
            if (global.characterEditor && global.gui.curPage !== "Customization"){
                global.gui.close();
                global.gui.openPage("Customization");
            }
        }, 3000);

        setTimeout(()=>{
            if (global.characterEditor && global.gui.curPage !== "Customization"){
                global.gui.close();
                global.gui.openPage("Customization");
            }
        }, 6000)
    
        mp.discord.update(`Playing Astro Roleplay..`, ``);
        
        global.gui.stopSound();
        global.showHud(true);
        global.loggedin = true;
        mp.game.player.setHealthRechargeMultiplier(0);
        global.gui.setData("setBackground", "0");
        global.activateAntiCheat();
    
        // finishCamera();

        setTimeout(() => {
            global.chw();
            mp.events.call("switchTime", 0);
            global.localplayer.freezePosition(false);
            global.gui.close();
        }, 500)
    } catch (e) {
        if(global.sendException)mp.serverLog(`authorization.spawn: ${e.name }\n${e.message}\n${e.stack}`);
    }
    
}

function checkAuthStorage() {
    if (!mp.storage.data.hasOwnProperty('auth')) {
        mp.storage.data.auth = {
            login: '',
            password: '',
            save: false
        };
    }
}


// function startSkyCamera(type, param) {
//     while (mp.game.invoke("0xD9D2CFFF49FAB35F") == false) {
//         mp.game.invoke("0xAAB3200ED59016BC", mp.players.local.handle, 0, 1);
//     }
//     mp.game.invoke("0xF36199225D6D8C86", 0.01)

//     let cameraWaitS = setInterval(() => {
//         if (mp.game.invoke("0x470555300D10B2A5") > 4) {
//             if(type == 'startReg'){
//                 global.gui.setData("setLoadScreen", false)
//                 global.gui.setData("auth/setSocialClub", JSON.stringify({ name: param }));
//                 global.gui.setData("auth/setCurrentTab", JSON.stringify({ page: "CreateNewAccountTab" }));
//                 global.gui.close();
//                 global.gui.openPage("Auth");
//             }else{
//                 global.gui.setData("setLoadScreen", false)
//                 global.gui.close();
//                 global.gui.setData("auth/setSocialClub", JSON.stringify({ name: param }));    
//                 global.gui.setData("auth/setCurrentTab", JSON.stringify({ page: "LoginTab" }));  
//                 global.gui.openPage("Auth");
//             }
//             clearInterval(cameraWaitS)
//         }
//     }, 10)



// };

// function finishCamera() {
//     setTimeout(() => {
//         mp.game.invoke("0xD8295AF639FD9CB8", mp.players.local.handle);
//     }, 5000);
// }