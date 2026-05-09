<template>
    <div class="equip nopoint">
        <div class="equip-list">
            <div class="equip-item equip-clothes" id="drop" v-for="(clth, slot) in equip.clothes" :key="`cl_${slot}`"
                :adress="adress(2, slot)" @mousedown="onMouseDown(2, slot, clth, $event)"
                @mousemove="onMouseMoveX(clth, slot)"
                :class="[{'equip-active': isActive(2, slot), 'equip-available': isAvailable(2, slot)}, getHighlightClassByType(clth)]">
                <img :src="getClothImage(clth, slot)" :style="{
                    'width': clth == null ? '50%' : '70%',
                    'height': clth == null ? '50%' : '70%'
                }" alt="clothes">
                <div class="weight" v-if="clth && clth.count > 1">{{clth.count}}</div>
            </div>
            <!-- <div class="equip-item equip-weapon" id="drop"
                v-for="(weap, slot) in equip.weapons" 
                :key="`wep_${slot}`" 
                :adress="adress(1, slot)"
                @mousedown="onMouseDown(1, slot, weap, $event)"
                :class="{'equip-active': isActive(1, slot), 'equip-available': isAvailable(1, slot), 'equip-with-item': weap != null, 'equip-promo': weap && weap.promo}"                
            >
                <img 
                    :src="getWeapImage(weap, slot)" 
                    alt="weapon"                     
                    :style="{
                        'width': weap == null ? '50%' : '70%', 
                        'height': weap == null ? '50%' : '70%'
                    }"
                >
                <div class="equip-item-tittle" 
                    :style="{
                        'color': weap == null ? 'rgba(255, 255, 255, 0.2)' : '#fff'
                    }"
                >
                    Weapon {{slot}}
                </div>
            </div> -->
            <!-- <div class="equip-delimiter"></div> -->
        </div>

    </div>
</template>

<script>
import dragObject from './classes/dargObject'
import { mapState, mapGetters } from 'vuex'

export default {
    props: ['drag', 'checkInventory'],
    data() {
        return {
            lastClick: 0,
            clickDelay: 500,
            iconsMap: {
                "1": "2",
                "2": "1",
                "3": "3",
                "4": "7",
                "5": "15",
                "6": "12",
                "7": "8",
                "8": "5",
                "9": "4",
                "10": "13",
                "11": "6",
                "12": "11",
                "13": "14",
                "14": "9",
                "15": "10"
            } 
        }
    },
    methods: {
        getHighlightClassByType (item) {
            return item ? 'list-item-type-'+ item.config.Type : ''
        },
        onMouseMoveX(index, item) {
            const adress = this.adress(2, item);
            this.$store.commit('inventory/setSelectedObjectX', [adress, index]);

            // index = item
        },
        getClothImage(item, slot) {
            return item == null ? `/img/inventory/equip/cloth${this.iconsMap[slot]}.svg` : item.getImage();
        },
        getWeapImage(item, slot) {
            return item == null ? `/img/inventory/equip/weap${slot}.svg` : item.getImage();
        },
        onMouseDown(type, slot, item, e) {
            if (e.button != 0 || item == null) return;
            if (this.lastClick > Date.now()) {
                if (item.isUsable())
                    this.$emit("onDoubleClick", this.adress(type, slot), item)
            } else {
                const object = new dragObject(this.adress(type, slot), item, e.clientX, e.clientY);
                this.$emit("onMouseDown", object)
            }
            this.lastClick = Date.now() + this.clickDelay;
        },
        isActive(type, slot) {
            if (!this.drag || !this.drag.dragStart || !this.drag.item) return false;
            return this.drag.overAdress == this.adress(type, slot) && this.isAvailable(type, slot);
        },
        isAvailable(type, slot) {
            if (!this.drag || !this.drag.dragStart || !this.drag.item) return false;
            if (this.drag.item.isWeapon()) {
                if (!this.drag.item.isType(type)) return false;
                if (!this.drag.item.isSlotValid(slot)) return false;
            } else {
                if (type == 1 && this.drag.item.isEquipClothes()) return false;
                if (!this.drag.item.isSlotValid(slot)) return false;
            }
            return true;
        },
        adress(type, slot) {
            return `eq_${type}_${slot}`
        }
    },
    computed: {
        ...mapState('hud', ['statusDisplays']),
        ...mapGetters('localization', ['loc']),
        equip() {
            if (this.checkInventory) return this.checkInventory.equip
            else return this.$store.state.inventory.equip
        }
    },
}
</script>

<style lang="scss" scoped>
.equip {
    width: 100%;
    height: 100%;
    position: relative;

    &-man {
        position: absolute;
        top: 2vh;
        right: -5vh;
        pointer-events: none;
    }

    &-state {
        margin: .7vh 0;
        display: flex;
        justify-content: flex-start;
        align-items: center;
        pointer-events: none;

        &-body {
            width: 6vh;
            margin-left: .5vh;
        }

        &-tittle {
            color: rgb(#fff, .5);
            font-weight: normal;
            display: flex;
            align-items: center;
            justify-content: space-between;
            pointer-events: none;

            span {
                &:first-child {
                    font-size: .8vh;
                    line-height: .8vh;
                }
            }
        }

        &-progress {
            height: 4px;
            width: 100%;
            background-color: rgba(#fff, .5);
            position: relative;
            overflow: hidden;
            border-radius: 6px;

            &-thumb {
                background-color: #FF3380;
                height: 100%;
            }
        }
    }

    &-tittle {
        color: #fff;
        width: 100%;
        font-size: 2.5vh;
        line-height: 2.5vh;
    }

    &-subtittle {
        width: 100%;
        height: 100%;
        color: rgba(#fff, .5);
        margin-top: .3vh;
    }

    &-list {
        display: flex;
        justify-content: space-around;
        flex-wrap: wrap;
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

        img {
            -webkit-user-drag: none;
            -o-user-drag: none;
            width: 70%;
            height: 70%;
            pointer-events: none;
        }

        

        &-tittle {
            position: absolute;
            transform: rotate(-90deg);
            font-weight: normal;
            letter-spacing: .05vh;
            left: -2.3vh;
            bottom: 1.2vh;
            text-align: left;
        }
    }

    .list-item-type-1 {
        background: radial-gradient(50% 50% at 50% 50%, rgba(115, 92, 255, 0.0275) 0%, rgba(115, 92, 255, 0.088) 100%);
        border: 1px solid #735CFF;
    }

    .list-item-type-2 {
        border: 1px solid rgba(255, 127, 55, 0.55);
        background: radial-gradient(50% 50% at 50% 50%, rgba(255, 127, 55, 0.0275) 0%, rgba(255, 127, 55, 0.088) 100%);
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
    &-promo {
        background: radial-gradient(80.56% 80.56% at 50% 76.11%, rgba(182, 211, 0, 0.2) 0%, rgba(182, 211, 0, 0) 100%);
        border: 1px solid rgba(#B6D300, .2);
    }

    &-with-item {
        background-color: rgba(#fff, .03);
    }

    &-weapon {
        left: 1.8vh;

        &:nth-child(16) {
            bottom: 0;
        }

        &:nth-child(17) {
            bottom: 5.5vh;
        }

        &:nth-child(18) {
            bottom: 11vh;
        }

        &:nth-child(19) {
            bottom: 16.5vh;
        }
    }

    &-active {
        background-color: rgba(#fff, .08);
    }

    &-available {
        border: 2px solid rgba(255, 255, 255, 0.6);
    }
}
</style>