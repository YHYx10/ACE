import { Entity } from './Entity';
import { Vehicle } from './Vehicle';
import { Costumes } from '../Client/configs/Costumes';
import { Gui } from '../Bridge/Gui';
import { Natives } from '../Client/configs/Natives';
import { Client } from '../Bridge/Client';

export const DefaultClothes = {
    [true]: {
        3: 15,
        4: 61,
        5: 0,
        6: 34,
        7: 0,
        8: 15,
        9: 0,
        10: 0,
        11: 15,
    },
    [false]: {
        3: 15,
        4: 56,
        5: 0,
        6: 35,
        7: 0,
        8: 6,
        9: 0,
        10: 0,
        11: 101,
    },
};

const EmptyClothes = {
    1: {
        1: 0,
        3: 15,
        4: 21,
        5: 0,
        6: 34,
        7: 0,
        8: 15,
        9: 0,
        10: 0,
        11: 15,
    },
    0: {
        1: 0,
        3: 15,
        4: 15,
        5: 0,
        6: 35,
        7: 0,
        8: 6,
        9: 0,
        10: 0,
        11: 15,
    },
};

const clothComponents = {
    0: {
        0: 1,
        1: 196,
        2: 80,
        3: 241,
        4: 145,
        5: 100,
        6: 106,
        7: 121,
        8: 233,
        9: 56,
        10: 128,
        11: 400,
    },
    1: {
        0: 1,
        1: 195,
        2: 76,
        3: 196,
        4: 138,
        5: 100,
        6: 102,
        7: 152,
        8: 187,
        9: 56,
        10: 120,
        11: 382,
    },
};

const propComponents = {
    0: 156,
    1: 34,
    2: 43,
    3: 0,
    4: 0,
    5: 0,
    6: 42,
    7: 9,
    8: 0,
    9: 0,
    10: 0,
    11: 0,
    12: 0,
};

/**
 * @extends {Entity<PlayerMp>}
 */
export class Player extends Entity {
    get name() {
        return this.entity.name;
    }

    get level() {
        return this.getVariable('lvl', 0);
    }

    get adminLevel() {
        return this.getVariable('ALVL', 0);
    }

    /**
     * @returns {number}
     */
    get fraction() {
        return this.getVariable('fraction', 0);
    }

    get vehicle() {
        const vehicle = this.entity.vehicle;

        if (!vehicle) {
            throw new Error('Player is not in vehicle. Check with isInAnyVehicle() before calling this method');
        }

        return new Vehicle(vehicle);
    }

    /**
     * @returns {boolean}
     */
    get gender() {
        return this.getVariable('GENDER');
    }

    static fromId(id) {
        const player = mp.players.atRemoteId(id);

        if (!player) {
            throw new Error('Player not found');
        }

        return new Player(player);
    }

    /**
     * @param {Player} player
     * @returns {boolean}
     */
    static exists(player) {
        return mp.players.exists(player.entity);
    }

    isDeath() {
        return this.getVariable('InDeath', false);
    }

    isDead() {
        return this.entity.isDead();
    }

    /**
     * @param {number} propId
     */
    clearProp(propId) {
        this.entity.clearProp(propId);
    }

    /**
     * @param {boolean} atGetIn
     * @returns {boolean}
     */
    isInAnyVehicle(atGetIn) {
        return this.entity.isInAnyVehicle(atGetIn);
    }

    /**
     * @param {Player} player
     * @returns {boolean}
     */
    isSame(player) {
        return this.entity === player.entity;
    }

    /**
     * @param {boolean} enable
     */
    setEnableHandcuffs(enable) {
        this.entity.setEnableHandcuffs(enable);
    }

    setClothing(componentId, drawable, textureId, paletteId) {
        const component = clothComponents[this.gender ? '1' : '0'][componentId];

        let drawableFixed;

        if (drawable > 499) {
            drawableFixed = drawable - 500 + component;
        } else {
            drawableFixed = drawable;
        }

        this.entity.setComponentVariation(componentId, drawableFixed, textureId, paletteId);
    }

    reloadClothes() {
        let clothes = this.getVariable('clothes::compon1');

        if (clothes !== undefined) {
            this.setClothing(1, clothes[0], clothes[1], 0);
        }

        clothes = this.getVariable('clothes::compon2');

        if (clothes !== undefined) {
            this.setClothing(2, clothes[0], clothes[1], 0);
        }

        clothes = this.getVariable('clothes::compon9');

        if (clothes !== undefined) {
            this.setClothing(9, clothes[0], clothes[1], 0);
        }

        const color = this.getVariable('makeup');

        if (color !== undefined) {
            this.entity.setHeadOverlayColor(4, 2, +color, +color);
        }

        const costume = this.getVariable('clothes::costume', -1);

        if (costume < 3) {
            this.reloadVariableClothes();
            this.reloadProps();
        } else {
            this.setCostume(costume);
        }
    }

    setProp(componentId, drawable, textureId) {
        const component = propComponents[componentId];

        let drawableFixed;
        if (drawable > 499) {
            drawableFixed = drawable - 500 + component;
        } else {
            drawableFixed = drawable;
        }

        if (drawableFixed > -1) {
            this.entity.setPropIndex(componentId, drawableFixed, textureId, true);
        } else {
            this.entity.clearProp(componentId);
        }
    };

    reloadProps() {
        for (let index = 0; index <= 12; index++) {
            const props = this.getVariable('clothes::prop' + index);

            if (props !== undefined) {
                this.setProp(index, props[0], props[1]);
            }
        }
    }

    reloadVariableClothes() {
        for (let index = 3; index <= 11; index++) {
            const clothes = this.getVariable('clothes::compon' + index);

            if (clothes !== undefined) {
                this.setClothing(index, clothes[0], clothes[1], 0);
            }
        }
    }

    setCostume(variant) {
        const config = Costumes[variant];

        if (!config) {
            this.reloadVariableClothes();
            this.reloadProps();
        } else {
            for (let i = 3; i <= 11; i++) {
                if (i === 9) {
                    continue;
                }

                if (config.ClothesDto[i] === undefined) {
                    this.setClothing(i, DefaultClothes[config.Gender][i], 0, 0);
                } else {
                    this.setClothing(i, config.ClothesDto[i].Drawable, config.ClothesDto[i].Texture, 0);
                }
            }

            for (let i = 0; i <= 12; i++) {
                if (config.PropsDto[i] === undefined) {
                    this.setProp(i, -1, -1);
                } else {
                    this.setProp(i, config.PropsDto[i].Drawable, config.PropsDto[i].Texture);
                }
            }
        }
    }

    setHeadOverlayColor(overlayID, colorType, colorID, secondColorID) {
        this.entity.setHeadOverlayColor(overlayID, colorType, colorID, secondColorID);
    }

    clearClothes() {
        const gender = this.gender ? 1 : 0;

        this.clearProp(0);
        this.clearProp(1);
        this.clearProp(2);
        this.clearProp(6);
        this.clearProp(7);

        this.setClothing(1, EmptyClothes[gender][1], 0, 0);
        this.setClothing(3, EmptyClothes[gender][3], 0, 0);
        this.setClothing(4, EmptyClothes[gender][4], 0, 0);
        this.setClothing(5, EmptyClothes[gender][5], 0, 0);
        this.setClothing(6, EmptyClothes[gender][6], 0, 0);
        this.setClothing(7, EmptyClothes[gender][7], 0, 0);
        this.setClothing(8, EmptyClothes[gender][8], 0, 0);
        this.setClothing(9, EmptyClothes[gender][9], 0, 0);
        this.setClothing(10, EmptyClothes[gender][10], 0, 0);
        this.setClothing(11, EmptyClothes[gender][11], 0, 0);
    }

    taskPlayAnimAdvanced(animDict,
        animName,
        posX,
        posY,
        posZ,
        rotX,
        rotY,
        rotZ,
        speed,
        speedMultiplier,
        duration,
        flag,
        animTime,
        p14,
        p15,
    ) {
        this.entity.taskPlayAnimAdvanced(animDict,
            animName,
            posX,
            posY,
            posZ,
            rotX,
            rotY,
            rotZ,
            speed,
            speedMultiplier,
            duration,
            flag,
            animTime,
            p14,
            p15,
        );
    }

    clearTasksImmediately() {
        this.entity.clearTasksImmediately();
    }

    setConfigFlag(flagId, value) {
        this.entity.setConfigFlag(flagId, value);
    }

    taskMoveNetwork(task, multiplier, p3, animDict, flags) {
        this.entity.taskMoveNetwork(task, multiplier, p3, animDict, flags);
    }

    applyDamageTo(damageAmount, p2) {
        this.entity.applyDamageTo(damageAmount, p2);
    }
}

class LocalPlayerImpl extends Player {
    constructor() {
        super(mp.players.local);

        this.demorganed = false;
        this.cuffed = false;
        this.inAction = false;
    }

    isDemorgan() {
        return this.demorganed;
    }

    demorgan(freeze) {
        global.isDemorgan = true;

        if (freeze) {
            Gui.setData('setLoadScreen', 'true');
            this.freeze();

            setTimeout(() => {
                this.unfreeze();
                Gui.setData('setLoadScreen', 'false');
            }, 3000);
        }
    }

    releaseDemorgan() {
        this.demorganed = false;
    }

    /**
     * @returns {number}
     */
    getCurrentWeapon() {
        return mp.game.invoke(Natives.GET_SELECTED_PED_WEAPON, Me.handle) >>> 0;
    }

    isCuffed() {
        return this.cuffed;
    }

    setCuffed(cuffed) {
        this.cuffed = cuffed;
    }

    isInAction() {
        return this.inAction;
    }

    setInAction(inAction) {
        this.inAction = inAction;
    }
}

export const Me = new LocalPlayerImpl();

// Subscribers

mp.events.addDataHandler('GENDER', (entity, gender) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        player.reloadClothes();
    }
});

Client.on('entityStreamIn', (entity) => {
    if (entity.type === 'player') {
        const player = new Player(entity);

        player.reloadClothes();
    }
});

Client.on('playerSpawn', (entity) => {
    if (entity.type === 'player') {
        const player = new Player(entity);

        player.reloadClothes();
    }
});

mp.events.addDataHandler('clothes::costume', (entity, costume) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (!costume || costume < 3) {
            player.reloadVariableClothes();
            player.reloadProps();
        } else {
            player.setCostume(costume);
        }
    }
});

mp.events.addDataHandler('clothes::compon1', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        player.setClothing(1, clothes[0], clothes[1], 0);
    }
});

mp.events.addDataHandler('clothes::compon2', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        player.setClothing(2, clothes[0], clothes[1], 0);
    }
});

mp.events.addDataHandler('clothes::compon3', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(3, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon4', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(4, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon5', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(5, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon6', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(6, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon7', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(7, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon8', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(8, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon9', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        player.setClothing(9, clothes[0], clothes[1], 0);
    }
});

mp.events.addDataHandler('clothes::compon10', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(10, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::compon11', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setClothing(11, clothes[0], clothes[1], 0);
        }
    }
});

mp.events.addDataHandler('clothes::prop0', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(0, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop1', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(1, clothes[0], clothes[1]);
        }
    }
});
mp.events.addDataHandler('clothes::prop2', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(2, clothes[0], clothes[1]);
        }
    }
});
mp.events.addDataHandler('clothes::prop3', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(3, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop4', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(4, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop5', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(5, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop6', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(6, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop7', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(7, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop8', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(8, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop9', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(9, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop10', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(10, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop11', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(11, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('clothes::prop12', (entity, clothes) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        if (player.getVariable('clothes::costume', -1) < 3) {
            player.setProp(12, clothes[0], clothes[1]);
        }
    }
});

mp.events.addDataHandler('makeup', (entity, color) => {
    if (entity.type === 'player' && mp.players.exists(entity)) {
        const player = new Player(entity);

        player.setHeadOverlayColor(4, 2, +color, +color);
    }
});
