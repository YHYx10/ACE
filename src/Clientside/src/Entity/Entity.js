/**
 * @template {EntityMp} T
 */
export class Entity {
    /**
     * @param {T|EntityMp} entity
     */
    constructor(entity) {
        this.entity = entity;
    }

    /**
     * @returns {number}
     */
    get remoteId() {
        return this.entity.remoteId;
    }

    /**
     * @returns {Vector3}
     */
    get position() {
        return this.entity.position;
    }

    /**
     * @param {Vector3} value
     */
    set position(value) {
        this.entity.position = value;
    }

    /**
     * @returns {number}
     */
    get dimension() {
        return this.entity.dimension;
    }

    get handle() {
        return this.entity.handle;
    }

    isInAir() {
        return this.entity.isInAir();
    }

    getVariable(key, defaultValue) {
        const value = this.entity.getVariable(key);

        if (value || value === 0) {
            return value;
        }

        return defaultValue;
    }

    getBoneIndexByName(name) {
        return this.entity.getBoneIndexByName(name);
    }

    freeze() {
        this.entity.freezePosition(true);
    }

    unfreeze() {
        this.entity.freezePosition(false);
    }

    /**
     * @param {boolean} enable
     */
    setInvincible(enable) {
        this.entity.setInvincible(enable);
    }
}
