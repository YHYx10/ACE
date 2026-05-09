<template>
    <div class="access" v-if="fastAccessButtons">
        <div class="access-list nopoint">
            <div class="access-item" id="drop" v-for="(button, key) in fastAccessButtons" :key="key"
                :adress="adress(key)" :class="{'access-active': isActive(key), 'access-with-item': button !== null}"
                @mousedown="onMouseDown(adress(key), button, $event)">
                <div class="access-item-tittle"
                    :class="{'access-item-with-item': button !== null && getItem(button) !== null}">{{key}}</div>
                <img v-if="button !== null && getItem(button) !== null" :src="getImage(button)" alt="image"
                    class="access-item-img" :style="{
                        'width': getItem(button) == null ? '50%' : '70%',
                        'height': getItem(button) == null ? '50%' : '70%'
                    }">
                <div v-if="button !== null && getItemCount(button, key) > 1" class="weight">
                    {{getItemCount(button)}}
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import dragObject from './classes/dargObject'

export default {
    props: ['drag'],
    data() {
        return {
            empty: false
        }
    },
    methods: {
        isActive(key) {
            return this.drag && this.adress(key) == this.drag.overAdress
        },
        adress(key) {
            return `access_${key}`;
        },
        getItemCount(button) {
            const item = this.getItem(button);
            if (button.type == "eq") return -1;
            if (item == null || !item.isStackable()) {
                //this.$store.commit("inventory/resetFastAccessButton", key);
                return -1
            } else return item.count;
        },
        getItem(button) {
            if (button.type == "eq")
                return this.equip.weapons[button.id];
            else {
                const inventory = this.getById(this.personalId);
                return inventory.items.find(i => i.id == button.id) || null;
            }
        },
        getImage(button) {
            const item = this.getItem(button);
            if (item == null) {
                this.empty = true;
                return button.type == "eq" ? `/img/inventory/equip/weap${button.id}.svg` : '';
            }
            else {
                this.empty = false;
                return item.getImage();
            }
        },
        onMouseDown(adress, button, e) {
            if (e.button !== 0 || button == null) return;
            const object = new dragObject(adress, this.getItem(button), e.clientX, e.clientY, true);
            this.$emit("onMouseDown", object)
        }
    },
    computed: {
        ...mapState('inventory', ['fastAccessButtons', 'equip', 'personalId']),
        ...mapGetters('inventory', ['getById']),
        ...mapGetters('localization', ['loc'])
    },
    mounted() {
        if (this.fastAccessButtons == undefined) {
            if (process.env.NODE_ENV == 'development') {
                this.$store.commit('inventory/setFastAccessData', { "1": null, "2": null, "3": null, "4": null, "5": null, "6": null, "7": null, "8": null, "9": null, "10": null });
            } else {
                window.mp.trigger('cef:access:requestData')
            }
        }
    }
}
</script>

<style lang="scss" scoped>
.access {
    &-list {
        display: flex;
        justify-content: flex-start;
        align-items: center;
        // padding: 1.5rem 0;
        padding-bottom: 2vh;
        flex-wrap: wrap;
    }

    &-img {
        position: absolute;
        top: 0vh;
        pointer-events: none;
    }

    .weight {
      color: #fff;
      position: absolute;
      top: 0.463vh;
      left: 0.741vh;
      font-size: .9vh;
      line-height: 1.2vh;
      letter-spacing: .03vh;
      font-family: 'Akrobat';
      font-weight: 600;
      font-size: 1.111vh;
      line-height: 1.296vh;
    }

    &-title {
        color: #fff;
        position: absolute;
        left: 0;
        top: 3.4vh;
        font-size: 1vh;
        line-height: 1vh;
        letter-spacing: 0.04vh;
        pointer-events: none;
    }

    &-item {
        width: 8vh;
        height: 8vh;
        margin: 0 1vh 1vh 0;
        box-sizing: border-box;
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;
        background: radial-gradient(50% 50% at 50% 50%, rgba(0, 0, 0, 0.05) 0%, rgba(0, 0, 0, 0.09) 100%);
        border: 1px solid rgba(255, 255, 255, 0.1);
        border-radius: 2px;

        transition: .5s all;

        &:hover {
            border: 1px solid white;
        }

        &-img {
            -webkit-user-drag: none;
            -o-user-drag: none;
            pointer-events: none;
            width: 70%;
            height: 70%;
        }

        &-count {
            position: absolute;
            bottom: 0;
            right: .2vh;
            color: #fff;
            font-size: .8vh;
            letter-spacing: .05vh;
        }

        &-tittle {
            font-size: 0.8vh;
            line-height: 0.85vh;
            letter-spacing: 0.03em;
            font-weight: normal;
            color: rgba(255, 255, 255, 0.5);
            position: absolute;
            right: 7px;
            bottom: 7px;
            border: 2px solid red;
            border-radius: 50%;
            width: 1.4vh;
            height: 1.4vh;
            display: flex;
            justify-content: center;
            align-items: center;
        }

        &-with-item {
            background-color: red;
            color: black;
        }
    }

    &-with-item {
        border: 1px solid rgba(255, 255, 255, 0.2);
    }

    &-active {
        border: 1px solid rgba(255, 255, 255, 0.6);
    }
}
</style>