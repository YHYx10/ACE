mp.attachments = {
	attachments: {},

	addFor: async function(entity, id)
	{
		try
		{
			if(!this.attachments)
				this.attachments = {};

			if(this.attachments.hasOwnProperty(id))
			{
				if(!entity.__attachmentObjects) 
					entity.__attachmentObjects = {};

				if(!entity.__attachmentObjects.hasOwnProperty(id))
				{
					let attInfo = this.attachments[id];

					let object = mp.objects.new(attInfo.model, entity.position, {
						dimension: -1
					});

					await global.IsLoadEntity (object);
					
					if (object && object.handle && entity && entity.handle) {

						object.attachTo(entity.handle,
							(typeof(attInfo.boneName) === 'string') ? entity.getBoneIndexByName(attInfo.boneName) : entity.getBoneIndex(attInfo.boneName),
							attInfo.offset.x, attInfo.offset.y, attInfo.offset.z, 
							attInfo.rotation.x, attInfo.rotation.y, attInfo.rotation.z, 
							false, false, false, false, 2, attInfo.fixedRot);
							
						entity.__attachmentObjects[id] = object;					
					}
					else if(mp.objects.exists(object)) 
						object.destroy();
				}
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.addFor: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	removeFor: function(entity, id)
	{
		try
		{
			if(!entity.__attachmentObjects)
				entity.__attachmentObjects = {};

			if(entity.__attachmentObjects.hasOwnProperty(id))
			{
				let obj = entity.__attachmentObjects[id];
				delete entity.__attachmentObjects[id];
				
				if(mp.objects.exists(obj))
				{
					obj.destroy();
				}
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.removeFor: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	initFor: function(entity)
	{
		try
		{
			for(let attachment of entity.__attachments)
			{
				mp.attachments.addFor(entity, attachment);
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.initFor: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	shutdownFor: function(entity)
	{
		try
		{
			for(let attachment in entity.__attachmentObjects)
			{
				mp.attachments.removeFor(entity, attachment);
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.shutdownFor: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	register: function(id, model, boneName, offset, rotation, fixedRot = true)
	{
		try
		{
			if(typeof(id) === 'string')
			{
				id = mp.game.joaat(id);
			}
			
			if(typeof(model) === 'string')
			{
				model = mp.game.joaat(model);
			}

			if(!this.attachments)
				this.attachments = {};

			if(!this.attachments.hasOwnProperty(id))
			{
				if(mp.game.streaming.isModelInCdimage(model))
				{
					this.attachments[id] = {
						id: id,
						model: model,
						offset: offset,
						rotation: rotation,
						boneName: boneName,
						fixedRot: fixedRot
					};
				}
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.register: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	unregister: function(id) 
	{
		try
		{
			if(typeof(id) === 'string')
			{
				id = mp.game.joaat(id);
			}

			if(!this.attachments)
				this.attachments = {};

			if(this.attachments.hasOwnProperty(id))
			{
				this.attachments[id] = undefined;
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.unregister: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	addLocal: function(attachmentName)
	{
		try
		{
			if(typeof(attachmentName) === 'string')
			{
				attachmentName = mp.game.joaat(attachmentName);
			}
			
			let entity = mp.players.local;
			
			if(!entity.__attachments || entity.__attachments.indexOf(attachmentName) === -1)
			{
				mp.events.callRemote("staticAttachments.Add", String (attachmentName));
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.addLocal: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	removeLocal: function(attachmentName)
	{
		try
		{
			if(typeof(attachmentName) === 'string')
			{
				attachmentName = mp.game.joaat(attachmentName);
			}
			
			let entity = mp.players.local;
			
			if(entity.__attachments && entity.__attachments.indexOf(attachmentName) !== -1)
			{
				mp.events.callRemote("staticAttachments.Remove", String (attachmentName));
			}
		}
		catch (e) 
		{
			if(global.sendException) mp.serverLog(`static-attachments.removeLocal: ${e.name}\n${e.message}\n${e.stack}`);
		}
	},
	
	getAttachments: function()
	{
		return Object.assign({}, this.attachments);
	}
};

mp.events.add("entityStreamIn", (entity) =>
{
	if (!entity || !entity.__attachments) return;
	
	mp.attachments.initFor(entity);
});

mp.events.add("entityStreamOut", (entity) =>
{
	if (!entity || !entity.__attachmentObjects) return;
	
	mp.attachments.shutdownFor(entity);
});


mp.events.add("onChangeDimension", (oldDim, newDim) =>
{
	try {		
		if(!mp.players.local.__attachmentObjects) return;
		
		mp.attachments.shutdownFor(mp.players.local);
		mp.attachments.initFor(mp.players.local);
		
	} catch (e) {		
		if(global.sendException) mp.serverLog(`static-attachments.entityStreamOut: ${e.name}\n${e.message}\n${e.stack}`);
	}
});

mp.events.addDataHandler("attachmentsData", (entity, data) =>
{
	try
	{
		let newAttachments = (data.length > 0) ? JSON.parse(data) : [];

		if (entity.handle !== 0) 
		{	
			let oldAttachments = entity.__attachments;	
		
			if(!oldAttachments)
			{
				oldAttachments = [];
				entity.__attachmentObjects = {};
			}
			
			for(let attachment of oldAttachments)
			{
				if(newAttachments.indexOf(attachment) === -1)
				{
					mp.attachments.removeFor(entity, attachment);
				}
			}
			
			for(let attachment of newAttachments)
			{
				if(oldAttachments.indexOf(attachment) === -1)
				{
					mp.attachments.addFor(entity, attachment);
				}
			}
		}
		entity.__attachments = newAttachments;
	} catch (e) {
		if(global.sendException) mp.serverLog(`static-attachments.attachmentsData: ${e.name}\n${e.message}\n${e.stack}`);		
	}
});

function InitAttachmentsOnJoin()
{
	let data;
	let atts;
	mp.players.forEach(player =>
	{
		data = player.getVariable("attachmentsData");
		
		if(data && data.length > 0)
		{
			atts = (data.length > 0) ? JSON.parse(data) : [];
			
			if(!atts) 
				atts = [];

			player.__attachments = atts;
			player.__attachmentObjects = {};
		}
	});
}

InitAttachmentsOnJoin();

global.IsLoadEntity = entity => new Promise(async (resolve, reject) => {
	try {
		if (entity && entity.doesExist() && entity.handle !== 0)
			return resolve(true);
        let d = 0;
		while (!entity || !entity.doesExist() || entity.handle === 0) {
            if (d > 1000) return resolve("Ошибка IsLoadEntity.");
            d++;
            await mp.game.waitAsync(10);
        }
        return resolve(true);
    } 
    catch (e) 
	{
		if (global.sendException) mp.serverLog(`static-attachments.IsLoadEntity: ${e.name}\n${e.message}\n${e.stack}`);		
		resolve();
	}
});

mp.attachments.register('Mobile', 'p_amb_phone_01', 'IK_R_Hand', new mp.Vector3(0.07, 0.035, 0.0), new mp.Vector3(110, -20, 0));
mp.attachments.register('Burger', 'prop_cs_burger_01', 'IK_R_Hand', new mp.Vector3(0.1, -0.015,  -0.07), new mp.Vector3(40, -20, 110));
mp.attachments.register('Sandwich', 'prop_sandwich_01', 'IK_R_Hand', new mp.Vector3(0.1, -0.015, -0.07), new mp.Vector3(40, -20, 110));	
mp.attachments.register('HotDog', 'prop_cs_hotdog_01', 'IK_R_Hand', new mp.Vector3(0.04, -0.015, -0.02), new mp.Vector3(10, -110, 170));
mp.attachments.register('Cuffs', 'p_cs_cuffs_02_s', 'IK_R_Hand', new mp.Vector3(-0.02, -0.063, 0.00), new mp.Vector3(75.0, 0.0, 76.0));
mp.attachments.register('SupplyBox', 'prop_box_ammo03a', 'IK_Root', new mp.Vector3(0.0, 0.36, 0.0), new mp.Vector3(0.0, 0.0, 0.0));
mp.attachments.register('RobberyBox', 'prop_box_tea01a', 'IK_Root', new mp.Vector3(0.0, 0.36, 0.0), new mp.Vector3(0.0, 0.0, 0.0));
mp.attachments.register('Tablet', 'prop_cs_tablet', 60309, new mp.Vector3(0.115, 0.001, 0.125), new mp.Vector3(-150.001, 9.99, 55.001));
mp.attachments.register('Guitar', 'prop_acc_guitar_01', 60309, new mp.Vector3(0.015, 0.001, 0.05), new mp.Vector3(-0.01, -5.009, 5));
mp.attachments.register('Microphone', 'p_ing_microphonel_01', 28422, new mp.Vector3(0.009, 0.0, 0.001), new mp.Vector3(4.989, -0.09, 0.0));
mp.attachments.register('Camera', 'prop_v_cam_01',  28422, new mp.Vector3(-0.01, 0.001, 0.001), new mp.Vector3(-0.1, 4.99, -5));
mp.attachments.register('Drink', 'ng_proc_sodacan_01a', 'IK_R_Hand', new mp.Vector3(0.07, 0.085, -0.02), new mp.Vector3(40, -100, 110));
mp.attachments.register('ball', 'w_am_baseball', 17188, new mp.Vector3(0.120, 0.010, 0.010), new mp.Vector3(5.0, 150.0, 0.0));