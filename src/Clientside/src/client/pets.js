const instructions = require('./better_instructions.js');
const interactionDistance = 15.0;
const renderDistance = 7.0;

let lastPetCheck = Date.now();

let petData = 
{
	entity: null,
	blip: null,
	name: null,
	scriptModel: 0,
	model: 0,
	dead: false,
	
	commands: 
	{
		stop: false,
		
		attacking: false,
		attackingType: 0,
		attackingEntity: null,
		
		sitting: false,
		
		sleeping: false,
		
		ball: false,
		ballEntity: null,
		ballPosition: null,
		ballThrown: false,
		
		sniffing: false,
		sniffingEntity: null,
		sniffingTimer: null,
		
		following: false,
		followingEntity: null,
		followingType: 0,
		
		refresh: function()
		{
			this.stop = false;
			this.attacking = false;
			this.attackingType = 0;
			this.attackingEntity = null;
			this.sitting = false;
			this.sleeping = false;
			this.ball = false;
			this.ballEntity = null;
			this.ballPosition = null;
			this.ballThrown = false;
			this.sniffing = false;
			this.sniffingEntity = null;
			this.sniffingTimer = null;
			this.following = false;
			this.followingEntity = null;
			this.followingType = 0;
		}
	},
	
	teleportRequired: false,
	buttons: null,
	buttonsShow: false,
	buttonsRequested: false,
	buttonsInitialize: function(model)
	{
		if (global.petConfigs[model] === undefined)
		{
			mp.events.call('notify', 4, 9, `Загрузка конфига для Вашего питомца не удалась. Свяжитесь с администрацией.`, 5000);
			return;
		}
		if (this.buttons != null) return;
		
		this.buttonsShow = false;
		global.buttonsShow = false;
		this.buttonsRequested = false;
		this.buttons = new instructions(-1);
		const config = global.petConfigs[model];
		this.buttons.addButton('Ждать/Следовать', '1');
		if (config.sit) this.buttons.addButton('Сесть/Встать', '2');
		if (config.sleep) this.buttons.addButton('Лечь/Встать', '3');
		if (config.ball) this.buttons.addButton('Принести мячик', '4');
		if (config.sniff) this.buttons.addButton('Обнюхать', '5');
		if (config.attack) 
		{
			this.buttons.addButton('Атаковать человека', '6');
			this.buttons.addButton('Атаковать животное', '7');
		}
	}
}

global.IsMyPet = (handle) => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return false;
	return petData.entity.handle === handle;
};

const SetPet = (entity) => 
{
	if (!entity || entity.type !== 'ped' || !mp.peds.exists(entity) || entity.handle === 0) return;
	if (!entity.getVariable("pet:isPet")) return;
	
	entity.isPet = true;
}

mp.events.add('client::initPet', (entity, name, model) => 
{
	if (!entity || entity.type !== 'ped' || !mp.peds.exists(entity)) return;
	
	const uintModel = model >>> 0;
	entity.freeze = true;
	petData.commands.refresh();
	petData.entity = entity;
	petData.model = uintModel;
	petData.scriptModel = model;
	petData.name = name;
	petData.blip = mp.blips.new(463, new mp.Vector3(), 
	{ 
		alpha: 255, 
		color: 75, 
		name: `Pet ${name}` 
	});
	petData.blip.setCoords(entity.position);
	petData.buttonsInitialize(uintModel);
});

mp.events.add('client::unloadPet', () => 
{
	petData.entity = null;
	petData.name = null;
	if (petData.blip != null) petData.blip.destroy();
	petData.blip = null;
	petData.model = 0;
	petData.scriptModel = 0;
	petData.teleportRequired = false;
	petData.buttonsShow = false;
	global.buttonsShow = false;
	petData.buttonsRequested = false;
	if (petData.buttons != null) petData.buttons.destroyHud();
	petData.buttons = null;
	petData.commands.refresh();
});

mp.events.add('client::pet:rename', (name) => 
{
	petData.name = name;
	if (petData.blip == null || petData.entity == null) return;
	
	petData.blip.destroy();
	petData.blip = mp.blips.new(463, new mp.Vector3(), 
	{ 
		alpha: 255, 
		color: 75, 
		name: `Питомец ${name}` 
	});
	petData.blip.setCoords(petData.entity.position);
});

mp.events.add("entityStreamIn", (entity) => 
{
	SetPet (entity);
});

mp.events.add("render", () => 
{
	if (!global.loggedin) return;
	if (petData.entity != null && petData.entity.doesExist() && petData.blip != null) petData.blip.setCoords(petData.entity.getCoords(true));

	const localPos = global.localplayer.position;
	const graphics = mp.game.graphics;

	let petName;
	let pedPosition;
	let dist;
	let getBoneCoords;
	let _getScale;
	let scale;
	mp.peds.forEachInStreamRange((ped) => 
	{
		if (!ped || ped.type !== 'ped') return;
		
		petName = ped.getVariable("pet:name");
		if (!petName || petName.length == 0) return;
		
		pedPosition = ped.getCoords(true);
		dist = mp.game.system.vdist(pedPosition.x, pedPosition.y, pedPosition.z, localPos.x, localPos.y, localPos.z);
		if (dist >= renderDistance) return;
		
		getBoneCoords = ped.getBoneCoords(12844, 0, 0, 0);
		_getScale = GetScale (dist, 25);
		scale = 0.3 * _getScale;
				
		graphics.drawText(petName, [ getBoneCoords.x, getBoneCoords.y, getBoneCoords.z + (1 - _getScale) + (0.35) ],
		{
			font: 0,
			color: [255, 255, 255,255],
			scale: [ scale, scale ],
			outline: true
		});
		
		if (global.localplayer.isInAnyVehicle(true)) return;
		if (petData.entity == null || ped != petData.entity || petData.dead) return;
		
		graphics.drawText("F5", [pedPosition.x, pedPosition.y, pedPosition.z], 
		{
			font: 0,
			color: [255, 255, 255, 150],
			scale: [0.3, 0.3],
			outline: true
		});
	});
});

let updateDimensionAntiFlood = 0;

const UpdateActions = () => 
{
	if (!global.loggedin) return;
	if (petData.entity == null) return;
	
	if (!mp.peds.exists(petData.entity)) 
	{
		if (petData.blip != null) petData.blip.destroy();
		
		petData.commands.refresh();
		petData.entity = null;
		petData.blip = null;
		return;
	}
	if (petData.buttons != null)
	{
		const localPedPosition = global.localplayer.position;
		const pedPosition = petData.entity.getCoords(true);
		const distance = mp.game.gameplay.getDistanceBetweenCoords(pedPosition.x, pedPosition.y, pedPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
		if (!petData.buttonsRequested || distance > interactionDistance) 
		{
			if (petData.buttonsShow)
			{
				petData.buttons.toggleHud(false);
				petData.buttonsShow = false;
				global.buttonsShow = false;
				petData.buttonsRequested = false;
			}
		}
		else if (distance <= interactionDistance)
		{
			if (!petData.buttonsShow && petData.buttonsRequested)
			{
				petData.buttons.toggleHud(true);
				petData.buttonsShow = true;
				global.buttonsShow = true;
			}
		}
	}
	if (petData.dead) return;
	
	const petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	const petConfig = global.petConfigs[petModel];
	const petCommands = petData.commands;
	if (petData.entity.isInMeleeCombat())
	{
		if (petConfig.attack == false || petData.entity.isInCombat(global.localplayer.handle)) ClearActions(true);
		else if (petCommands.attackingEntity == null) ClearActions(true);
		return;
	}
	if (global.localplayer.isInAnyVehicle(true) && petData.entity.isInAnyVehicle(false)) 
	{	
		petData.entity.freeze = false;
		petCommands.followingType = 0;
		return;
	}
	if (petData.teleportRequired && updateDimensionAntiFlood < new Date().getTime()) 
	{
		petData.teleportRequired = false;
		petData.entity.freeze = false;
		petCommands.followingType = 0;
		mp.events.callRemote('server::pet:teleport');
		updateDimensionAntiFlood = new Date().getTime() + 10000;
		return;
	}
	if (petCommands.attackingEntity != null)
	{
		if (petCommands.attackingType == 1 && isAddPlayerToList(petCommands.attackingEntity))
		{
			if (!petCommands.attacking) mp.events.call('notify', 3, 9, `Ваш питомец (${petData.name}) начал атаковать человека.`, 3000);
			petCommands.attacking = true;
			petData.entity.taskCombat(petCommands.attackingEntity.handle, 0, 16);
		}
		else if (petCommands.attackingType == 2 && isAddPedToList(petCommands.attackingEntity))
		{
			if (!petCommands.attacking) mp.events.call('notify', 3, 9, `Ваш питомец (${petData.name}) начал атаковать животное.`, 3000);
			petCommands.attacking = true;
			petData.entity.taskCombat(petCommands.attackingEntity.handle, 0, 16);
		}
		else ClearActions();
		return;
	}
	if (petCommands.ball && petCommands.ballPosition != null && petCommands.ballEntity != null)
	{
		if (mp.game.invoke('0x7239B21A38F536BA', petCommands.ballEntity)) 
		{
			const petPosition = petData.entity.getCoords(true);
			const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, petCommands.ballPosition.x, petCommands.ballPosition.y, petCommands.ballPosition.z, true);
			if (distance <= 1.5)
			{
				petData.entity.freeze = false;
				petCommands.followingType = 0;

				mp.events.callRemote('server::pet:deleteBall', petData.entity, petCommands.ballPosition.x, petCommands.ballPosition.y, petCommands.ballPosition.z);
				petCommands.ballPosition = null;
				petCommands.ballEntity = null;
				petCommands.ballThrown = true;
				petCommands.ball = false;
			}
			else 
			{
				let speed = distance > 5 ? 5 : 1;
				petData.entity.taskGoToCoordAndAimAtHatedEntitiesNearCoord(petCommands.ballPosition.x, petCommands.ballPosition.y, petCommands.ballPosition.z + 0.4, petCommands.ballPosition.x, petCommands.ballPosition.y, petCommands.ballPosition.z + 0.4, speed, false, 0, 0, false, 0, false, petData.scriptModel);
			}
		}
		else ClearActions();
		return;
	}
	if (!petData.entity.freeze)
	{
		if (petCommands.followingEntity != null)
		{
			if (isAddPlayerToList(petCommands.followingEntity))
			{
				petCommands.following = true;
				StartFollow(petCommands.followingEntity, "follow");
			}
			else ClearActions();
			return;
		}
		if (petCommands.sniffingEntity != null)
		{
			if (isAddPlayerToList(petCommands.sniffingEntity))
			{
				petCommands.sniffing = true;
				StartFollow(petCommands.sniffingEntity, "sniff");
			}
			else ClearActions();
			return;
		}
		if (!petCommands.ball && !petCommands.attacking && !petCommands.following && !petCommands.sniffing) StartFollow(global.localplayer, "me");
		return;
	}
	
}

setInterval(UpdateActions, 100 * 5)

const StartFollow = (player, type) => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity) || !player || !mp.players.exists(player)) return;
	
	const pedCoords = petData.entity.getCoords(false);
	const pedPosition = new mp.Vector3(pedCoords.x, pedCoords.y, pedCoords.z);
	const distance = global.vdist2(pedPosition, player.position);
	const playerDimension = global.localplayer.dimension;
	const pedDimension = petData.entity.dimension;
	const petCommands = petData.commands;
	if (distance <= 2.0 && pedDimension === playerDimension)
	{
		if (petCommands.stop || petData.entity.isInAnyVehicle(false)) return;
	
		petCommands.stop = true;
		petCommands.followingType = 0;
		FreezePet(petData.entity);
	
		if (type == "me" && petCommands.ballThrown) 
		{
			petCommands.ballThrown = false; 
			mp.events.callRemote('server::pet:getBall', petData.entity);
			return;
		}
		if (type == "sniff") 
		{
			petData.entity.freeze = true;
			mp.events.call('notify', 3, 9, `Ваш питомец (${petData.name}) начал обнюхивать человека.`, 3000);
			if (petCommands.sniffingTimer != null) clearTimeout(petData.toSniffTimer);

			petCommands.sniffingTimer = setTimeout (() => 
			{
				petCommands.sniffingTimer = null;
				petData.entity.freeze = false;
				ClearActions ();
				mp.events.callRemote('server::pet:sniffResult', player);
			}, 2500);
		}
		return;
	}
	
	petCommands.stop = false;
	let pedInVehicle = petData.entity.isInAnyVehicle(false);
	if (playerDimension == 0 && distance <= 10 && player.isInAnyVehicle(true) && !pedInVehicle) 
	{
		petData.entity.freeze = true;
		mp.events.call('client::pet:setInVehicle');
		return;
	}
	if (playerDimension == 0 && !player.isInAnyVehicle(true) && pedInVehicle) 
	{
		petData.entity.taskLeaveVehicle(petData.entity.getVehicleIsIn(false), 16);
		petCommands.followingType = 0;
		return;
	}
	if (!petData.ballThrown && distance > 40 && !pedInVehicle && !global.fly.flying) 
	{
		petData.entity.freeze = true;
		petData.entity.clearTasksImmediately();	
		FreezePet(petData.entity);
		petData.teleportRequired = true;
		return;
	}
	MoveToPlayer (player);
}

const MoveToPlayer = (player) => 
{
	if (!petData.entity) return;
	if (petData.entity.isInAnyVehicle(false)) return;
	
	const pedCoords = petData.entity.getCoords(false);
	const pedPosition = new mp.Vector3(pedCoords.x, pedCoords.y, pedCoords.z);
	const distance = global.vdist2(pedPosition, player.position);

	let speed = 1;
	if (distance > 8) speed = 10;
	else if (distance > 6) speed = 5;
	else if (distance > 4) speed = 3;

	petData.entity.taskGoToCoordAndAimAtHatedEntitiesNearCoord(player.position.x, player.position.y, player.position.z, player.position.x, player.position.y, player.position.z, speed, false, parseFloat(0), parseFloat(0), false, 0, false, petData.scriptModel);
	petData.commands.followingType = 1;
}

function FreezePet(pet) 
{
	if (!pet) return;
	
	pet.clearTasks();
}

function SitPet(pet, status, msg = true) 
{
	if (!pet) return;
	
	if (status)
	{
		const petConfig = global.petConfigs[petData.model];
		global.requestAnimDict(petConfig.sitDictionary).then(async () => 
		{
			if (!pet) return;
			
			pet.taskPlayAnim (petConfig.sitDictionary, petConfig.sitAnimation, 1, 1.0, -1, 1, 1.0, false, false, false);
		});
		if (msg) mp.events.call('notify', 2, 9, `Питомец ${petData.name} сел.`, 3000);
		return;
	}
	FreezePet(pet);
	if (msg) mp.events.call('notify', 2, 9, `Питомец ${petData.name} встал.`, 3000);
}

function SleepPet(pet, status, msg = true) 
{
	if (!pet) return;
	
	if (status) 
	{
		const petConfig = global.petConfigs[petData.model];
		global.requestAnimDict(petConfig.sleepDictionary).then(async () => 
		{
			if (!pet) return;
			
			pet.taskPlayAnim (petConfig.sleepDictionary, petConfig.sleepAnimation, 1, 1.0, -1, 1, 1.0, false, false, false);
		});
		if (msg) mp.events.call('notify', 2, 9, `Питомец ${petData.name} лёг.`, 3000);
		return;
	}
	FreezePet(pet);
	if (msg) mp.events.call('notify', 2, 9, `Питомец ${petData.name} встал.`, 3000);
}

function FindBall() 
{
	if (mp.players.local.weapon == mp.game.joaat('weapon_ball')) 
	{
		mp.events.call('notify', 2, 9, `Для начала Вам нужно убрать мячик из рук.`, 3000);
		return;
	}
	
	const petPosition = petData.entity.getCoords(true);
	let ball = mp.game.object.getClosestObjectOfType(petPosition.x, petPosition.y, petPosition.z, 100.0, mp.game.joaat('w_am_baseball'), false, true, true);
	if (ball === undefined) 
	{
		mp.events.call('notify', 2, 9, `Ваш питомец не смог найти мячик, попробуйте кинуть его ещё раз.`, 3000);
		return;
	}
	
	mp.game.invoke('0x428CA6DBD1094446', ball, true);
	petData.commands.ballEntity = ball;
	petData.commands.ballPosition = mp.game.invokeVector3('0x3FEF770D40960D5A', ball, false);
	petData.commands.ball = true;
}

mp.events.add('client::pet:deleteBall', (xPos, yPos, zPos) => 
{
	let ball = mp.game.object.getClosestObjectOfType(xPos, yPos, zPos, 100.0, mp.game.joaat('w_am_baseball'), false, true, true);
	if (ball === undefined) return;
	
	mp.game.invoke('0xAD738C3085FE7E11', ball, true, true);
	mp.game.object.deleteObject(ball);
});

mp.events.add('client::pet:follow', () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	if (petData.dead)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	ClearActions();
	petData.entity.freeze = !petData.entity.freeze;
	petData.commands.sitting = false;
	petData.commands.sleeping = false;

	if (!petData.entity.freeze) 
	{
		mp.events.call('notify', 2, 9, `Питомец ${petData.name} будет следовать за Вами.`, 3000);
		return;
	}
	
	FreezePet(petData.entity);
	mp.events.call('notify', 2, 9, `Питомец ${petData.name} будет ждать тут.`, 3000);
});

mp.events.add('client::pet:freeze', () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	ClearActions();
	petData.entity.freeze = true;
	petData.commands.sitting = false;
	petData.commands.sleeping = false;
	
	FreezePet(petData.entity);
	mp.events.call('notify', 2, 9, `Питомец ${petData.name} будет ждать тут.`, 3000);
});

mp.events.add('client::pet:unfreeze', () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	petData.entity.freeze = false;
});

mp.events.add('client::pet:sit', async () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	if (petData.dead)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	if (!global.petConfigs[petModel].sit)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец не понимает данной команды.", 3000);
		return;
	}
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	ClearActions();
	petData.entity.freeze = true;
	petData.commands.sitting = !petData.commands.sitting;
	petData.commands.sleeping = false;
	SitPet(petData.entity, petData.commands.sitting);
});

mp.events.add('client::pet:getBall', async () => 
{
	if (petData.entity == null|| !mp.peds.exists(petData.entity)) return;
	
	if (petData.dead)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	if (!global.petConfigs[petModel].ball)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец не понимает данной команды.", 3000);
		return;
	}
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	ClearActions();
	petData.entity.freeze = false;
	petData.commands.sleeping = false;
	petData.commands.sitting = false;
	petData.commands.followingType = 0;
	FindBall(petData.entity);
});

mp.events.add('client::pet:sleep', async () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	if (petData.dead)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	if (!global.petConfigs[petModel].sleep)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец не понимает данной команды.", 3000);
		return;
	}
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	ClearActions();
	petData.entity.freeze = true;
	petData.commands.sitting = false;
	petData.commands.sleeping = !petData.commands.sleeping;
	petData.commands.followingType = 0;
	SleepPet(petData.entity, petData.commands.sleeping);
});

const ClearActions = (isAttack = false) => 
{
	if (isAttack || petData.commands.attackingEntity != null)
	{
		petData.commands.attackingType = 0;
		petData.commands.attackingEntity = null;
		petData.commands.attacking = false;
		petData.commands.followingType = 0;

		petData.entity.clearTasksImmediately();
		MoveToPlayer(global.localplayer);
	}
	if (petData.commands.followingEntity != null)
	{
		FreezePet(petData.entity);
		petData.commands.following = false;
		petData.commands.followingEntity = null;
		petData.commands.followingType = 0;
	}
	if (petData.commands.sniffingEntity != null)
	{
		if (petData.commands.sniffingTimer != null) clearTimeout(petData.commands.sniffingTimer);

		FreezePet(petData.entity);
		petData.commands.sniffingEntity = null;
		petData.commands.sniffingTimer = null;
		petData.commands.sniffing = false;
		petData.commands.followingType = 0;
		petData.entity.freeze = false;
	}
	if (petData.commands.ball || petData.commands.ballThrown)
	{
		petData.entity.freeze = false;
		petData.commands.followingType = 0;
		petData.commands.ballPosition = null;
		petData.commands.ballEntity = null;
		petData.commands.ball = false;
		petData.commands.ballThrown = false; 
	}
}

mp.events.add('client::pet:attackPlayer', (playerId, errorNotify = true) => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	if (petData.commands.attackingEntity != null) return;
	
	if (petData.dead)
	{
		if (errorNotify) mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	if (!global.petConfigs[petModel].attack)
	{
		if (errorNotify) mp.events.call('notify', 4, 9, "Ваш питомец не понимает данной команды.", 3000);
		return;
	}
	if (playerId == global.localplayer.remoteId) return;
	
	const player = mp.players.atRemoteId(playerId);
	if (!player) return;
	if (player.vehicle) return;
		
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		if (errorNotify) mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	petData.entity.freeze = false;
	petData.commands.attackingEntity = player;
	petData.commands.attackingType = 1;
	petData.commands.followingType = 0;
});

mp.events.add('client::pet:attackPet', (petId, errorNotify = true) => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	if (petData.commands.attackingEntity != null) return;
	
	if (petData.dead)
	{
		if (errorNotify) mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	if (!global.petConfigs[petModel].attack)
	{
		if (errorNotify) mp.events.call('notify', 4, 9, "Ваш питомец не понимает данной команды.", 3000);
		return;
	}
	
	if (petId == petData.entity.remoteId) return;
	
	const pet = mp.peds.atRemoteId(petId);
	if (!pet) return;
	if (pet.vehicle) return;
		
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		if (errorNotify) mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	petData.entity.freeze = false;
	petData.commands.attackingEntity = pet;
	petData.commands.attackingType = 2;
	petData.commands.followingType = 0;
});

mp.events.add('client::pet:sniff', (playerId) => 
{
	if (petData.entity == null|| !mp.peds.exists(petData.entity)) return;
	
	if (petData.dead)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	
	if (!global.petConfigs[petModel].sniff)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец не понимает данной команды.", 3000);
		return;
	}
	
	const player = mp.players.atRemoteId(playerId);
	if (!player) return;
	if (player.vehicle) return;
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	petData.entity.freeze = false;
	petData.commands.sniffingEntity = player;
	petData.commands.followingType = 0;
});

mp.events.add('client::pet:followTarget', (playerId) => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	if (petData.dead)
	{
		mp.events.call('notify', 4, 9, "Ваш питомец мёртв.", 3000);
		return;
	}
	
	const player = mp.players.atRemoteId(playerId);
	if (!player) return;
	if (petData.entity.isInCombat(player.handle)) return;
	if (player.vehicle) return;
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance >= interactionDistance) 
	{
		mp.events.call('notify', 4, 9, "Подойдите ближе к питомцу, чтобы отдавать ему команды.", 3000);
		return;
	}
	
	petData.entity.freeze = false;
	petData.commands.followingEntity = player;
	petData.commands.followingType = 0;
});

mp.events.add('client::pet:clearFollow', () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;

	ClearActions();
});

mp.events.add('client::pet:deathStatus', (status) => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;

	petData.dead = status;
	ClearActions(true);
});

mp.events.add('client::pet:setInVehicle', () => 
{
	if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	
	const vehicle = global.localplayer.vehicle;
	if (!vehicle || !mp.vehicles.exists(vehicle)) 
	{
		petData.entity.freeze = false;
		return;
	}
	
	ClearActions();
	
	let vehMaxNumberOfPassengers = vehicle.getMaxNumberOfPassengers();
	for(var i = 0; i < vehMaxNumberOfPassengers; i++) 
	{
		if (vehicle.isSeatFree(i)) 
		{
			petData.entity.setIntoVehicle(vehicle.handle, i);
			return;
		}
	}
	mp.events.call('notify', 4, 9, `Питомец ${petData.name} не смог найти свободное место в транспорте и теперь ждет новую команду!`, 7000);
	petData.entity.freeze = true;
	FreezePet(petData.entity);
	petData.commands.sitting = true;
	SitPet(petData.entity, true, false);
});

mp.keys.bind(global.Keys.Key_F5, false, function () 
{
    if (petData.entity == null || !mp.peds.exists(petData.entity)) return;
	if (!petData.buttons) return;
	if (petData.dead) return;
	
	petData.buttonsRequested = !petData.buttonsRequested;
	if (!petData.buttonsRequested) return;
	
	const petPosition = petData.entity.getCoords(true);
	const localPedPosition = global.localplayer.position;
	const distance = mp.game.gameplay.getDistanceBetweenCoords(petPosition.x, petPosition.y, petPosition.z, localPedPosition.x, localPedPosition.y, localPedPosition.z, true);
	if (distance > interactionDistance) 
	{
		mp.events.call('notify', 3, 9, `Меню управления питомцем нельзя открыть находясь далеко от своего питомца.`, 5000);
		petData.buttonsRequested = false;
		return;
	}

	mp.events.call('notify', 2, 9, `Меню управления питомцем открывается.`, 2000);
});

function UsePetAction(action)
{
	if(!global.loggedin || mp.players.local.getVariable('InDeath') == true || global.fishingMiniGame || global.isPhoneOpened || global.cuffed || global.chatActive || mp.players.local.isInAnyVehicle(true) || lastPetCheck > Date.now() ||  global.gui.isOpened() ||  mp.gui.cursor.visible ||  global.IsPlayingDM == true) return;
	
	lastPetCheck = Date.now() + 1000;
	if (!petData.buttonsShow) return;
	
	mp.events.call(action);
}

mp.keys.bind(global.Keys.Key_1, false, function () 
{
    UsePetAction('client::pet:follow');
});

mp.keys.bind(global.Keys.Key_2, false, function () 
{
    UsePetAction('client::pet:sit');
});

mp.keys.bind(global.Keys.Key_3, false, function () 
{
    UsePetAction('client::pet:sleep');
});

mp.keys.bind(global.Keys.Key_4, false, function () 
{
    UsePetAction('client::pet:getBall');
});

mp.keys.bind(global.Keys.Key_5, false, function () 
{
	if(!global.loggedin || mp.players.local.getVariable('InDeath') == true || global.fishingMiniGame || global.isPhoneOpened || global.cuffed || global.chatActive || mp.players.local.isInAnyVehicle(true) || lastPetCheck > Date.now() ||  global.gui.isOpened() ||  mp.gui.cursor.visible ||  global.IsPlayingDM == true) return;
	
	lastPetCheck = Date.now() + 1000;
	if (!petData.buttonsShow) return;
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	if (!global.petConfigs[petModel].sniff) return;
	
	global.input.set("Обнюхать человека", "Введите ID", 11, "petSniffPlayer");
    global.input.open();
});

mp.keys.bind(global.Keys.Key_6, false, function () 
{
	if(!global.loggedin || mp.players.local.getVariable('InDeath') == true || global.fishingMiniGame || global.isPhoneOpened || global.cuffed || global.chatActive || mp.players.local.isInAnyVehicle(true) || lastPetCheck > Date.now() ||  global.gui.isOpened() ||  mp.gui.cursor.visible ||  global.IsPlayingDM == true) return;
	
	lastPetCheck = Date.now() + 1000;
	if (!petData.buttonsShow) return;
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	if (!global.petConfigs[petModel].attack) return;
	
	global.input.set("Атаковать человека", "Введите ID", 11, "petAttackPlayer");
    global.input.open();
});

mp.keys.bind(global.Keys.Key_7, false, function () 
{
	if(!global.loggedin || mp.players.local.getVariable('InDeath') == true || global.fishingMiniGame || global.isPhoneOpened || global.cuffed || global.chatActive || mp.players.local.isInAnyVehicle(true) || lastPetCheck > Date.now() ||  global.gui.isOpened() ||  mp.gui.cursor.visible ||  global.IsPlayingDM == true) return;
	
	lastPetCheck = Date.now() + 1000;
	if (!petData.buttonsShow) return;
	
	let petModel = petData.model;
	if (global.petConfigs[petModel] === undefined) return;
	if (!global.petConfigs[petModel].attack) return;
	
	global.input.set("Атаковать животное", "Введите ID", 11, "petAttackPet");
    global.input.open();
});

// Utility
const isAddPlayerToList = (player) => 
{
	if (!player || !mp.players.exists(player)) return false;
	if (!player.handle) return false;
	if (player == global.localplayer) return false;
	if (global.getVariable(player, 'AGM', false)) return false;
	if (global.getVariable(player, 'InDeath', false)) return false;
	if (global.getVariable(player, 'INVISIBLE', false)) return false;
	if (player.getHealth() < 1) return false;
	if (player.isInAnyVehicle(false)) return false;
	return true;
};

const isAddPedToList = (ped) => 
{
	if (!ped || !mp.peds.exists(ped)) return false;
	if (!ped.handle) return false;
	if (ped == petData.entity) return false;
	if (!global.getVariable(ped, 'pet:isPet', false)) return false;
	return true;
}

function GetScale(realDist, maxDist)
{
	return Math.max(0.1, 1 - realDist / maxDist);
};

global.requestAnimDict = (animDictionary) => new Promise(async (resolve, reject) => 
{
	if (animDictionary == null) return resolve(`Bad request dictionary`);
    if (mp.game.streaming.hasAnimDictLoaded(animDictionary)) return resolve(true);
    mp.game.streaming.requestAnimDict(animDictionary);

    let time = 0;
    while (!mp.game.streaming.hasAnimDictLoaded(animDictionary)) 
	{
        if (time > 5000) return resolve(`RequestAnimDictionary error | Dictionary: ${animDictionary}`);
        time++;        
        await global.wait (0);
    }

    return resolve(true);
});

global.vdist2 = (_Pos1, _Pos2, zCheck = true) => 
{
    if (!_Pos1 || !_Pos2) return -1;
	const _rY = _Pos1.y - _Pos2.y, _rX = _Pos1.x - _Pos2.x;
	
	if (zCheck) 
	{
		const _rZ = _Pos1.z - _Pos2.z;
		return Math.sqrt(_rY * _rY + _rX * _rX + _rZ * _rZ);
	}
	return Math.sqrt(_rY * _rY + _rX * _rX);
};

global.wait = time => new Promise(resolve => setTimeout(resolve, time));