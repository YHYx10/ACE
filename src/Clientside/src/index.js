global.petConfigs = require('./client/configs/pets.js');

require('./client/global.js');
require('./client/keys.js');
require('./client/camera.js');
require('./client/gui.js');
require('./client/actionHandler/index.js');
require('./client/chat.js');
require('./client/common.js');
require('./client/customSync/index.js');
require('./client/doors/index.js');
require('./client/render.js');
require('./client/hud.js');
require('./client/gametags.js');
require('./client/globalMenu.js');
require('./client/customization/Customization.js');  

require('./client/voice.js');
require('./client/main.js');
require('./client/bizsettings.js');
require('./client/checkpoints.js');
require('./client/inventory/index.js');

require('./client/shops/furnitureStore.js');
require('./client/vehiclesync/index.js');
require('./client/gangzones.js');
require('./client/enviroment.js');
require('./client/radiosync.js');
require('./client/speedcheck.js');
require('./client/fishing.js');
require('./client/casino/index.js');

require('./client/marriage.js'); 
require('./client/fingerpointing.js');
require('./client/judge.js');
require('./client/lscustomsNew.js');
require('./client/deathScreen.js');
require('./client/arena/index')
require('./client/technicianWork')
require('./client/carThiefWork')
require('./client/transporteur')

require('./client/configs/index.js');
require('./client/clothing.js');
require('./client/props.js');
require('./client/businesses/index.js');
require('./client/works/index.js');
//require('./client/pets/index.js');
require('./client/pets.js');

require('./client/farm.js');
require('./client/busWaysCreator.js');
require('./client/houses.js');
require('./client/interactionShapes/index.js');
require('./client/captureUI.js');
require('./client/farms/index.js');
require('./client/fractions.js');
require('./client/fractions/index.js');
require('./client/cigarettes/index.js');
require('./client/lifeActivity/index.js');
require('./client/admins/index.js');
require('./client/school.js');
require('./client/autoRepair.js');
require('./client/greenscreen.js');
require('./client/ui/index.js');
require('./client/animationsMenu.js');
require('./client/weaponSystem/index.js');
require('./client/fastAccess')
require('./client/illegalShop.js');
require('./client/carRoom.js');
require('./client/startQuest/index.js');
require('./client/phone/index.js');
require('./client/mainMenu/index.js');
require('./client/personalDigitalAssistant.js');
require('./client/lsnews.js');
require('./client/sound.js');
require('./client/families/family.js');
require('./client/families/updateData.js');
require('./client/families/cefEvents.js');
require('./client/families/syncMemberBlip.js');
require('./client/families/familyBattle.js');

require('./client/cityhall/menu.js');
require('./client/cityhall/cefEvents.js');
require('./client/cityhall/updateData.js');
require('./client/royalBattle/index.js');

require('./client/reports/admin.js');
require('./client/reports/player.js');
require('./client/reports/transferMoney.js');
require('./client/managementPanel.js');

require('./client/lift.js');
// require('./client/parliament.js');
require('./client/antiafk.js');
require('./client/docs.js');
require('./client/vehicleTrading.js');
require('./client/zoneSystem/zones.js');
require('./client/islandCapture/index.js');
require('./client/vehicleRent.js');
require('./client/moneySystem/index.js');
require('./client/priceMenu.js');
require('./client/personalEvents/index.js');

require('./client/customColShapes/index.js');
require('./client/steelMaking/index.js');

//shops
require('./client/shops/tattoShop.js');
require('./client/shops/barberShop.js');
require('./client/shops/clothisngShop.js');
require('./client/shops/maskShop.js');
require('./client/shops/weaponShop/index.js');
require('./client/shops/shop24.js');
require('./client/shops/burgerShot.js');
require('./client/shops/alcoBar.js');
require('./client/shops/carWash.js');
require('./client/newDonateShop/index.js');
require('./client/shops/handlingModShop.js');


require('./client/docks/dock.js');
require('./client/docks/dockLoaderJob.js');
require('./client/questPeds.js');
require('./client/interactionMenu.js');
require('./client/authorization.js');
require('./client/world.js');
require('./client/anticheat/index.js');
//require('./client/inventory/items/itemsAnimator.js');
require('./client/tablet/index.js');
require('./client/tip.js');
require('./client/miniGames/index.js');
require('./client/scenes/index.js');
require('./client/binocular/index.js');
require('./client/interiorsCheck.js');
require('./client/weedFarm/index.js');
require('./client/hudQuestMessage.js');
require('./client/bigInfo.js');
require('./client/costumeMenu.js');

global.effectManager = require('./client/EffectManager.js');
global.controlsManager = require('./client/ControlsManager.js');
global.enviromentManager = require('./client/EnviromentActions.js');

if(global.debug) require('./client/malboro.js');
