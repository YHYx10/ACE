<template>
  <div class="access">
    <div class="access-list nopoint">
      <div
        class="access-item"
        id="drop"
        v-for="(button, key) in equip.weapons"
        :key="`wep_${key}`"
        :adress="adress(1, key)"
        @mousedown="onMouseDown(1, key, button, $event)"
        draggable="false"
      >
        <span class="index">{{ key }}</span>
        <img
        draggable="false"
          v-show="button"
          :src="getWeapImage(button, key)"
          alt="weapon"
          :style="{
            width: button == null ? '50%' : '70%',
            height: button == null ? '50%' : '70%',
          }"
        />
        <div class="weight" v-if="button && button.count > 1">{{button.count}}</div>
        <!-- <div v-if="button !== null && getItemCount(button, key) > 0" class="access-item-count">
                    Weapon {{slot}}
                </div> -->
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
      empty: false,
    }
  },
  methods: {
    getClothImage(item, slot) {
            return item == null ? `/img/inventory/equip/cloth${slot}.svg` : item.getImage();
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
    }
}
</script>

<style lang="scss" scoped>
.access {
  .access-list {
    display: flex;
  }
  &-img {
    position: absolute;
    top: 0vh;
    pointer-events: none;
  }

  span.index {
    border: 0.185vh solid red;
    border-radius: 50%;
    width: 1.852vh;
    height: 1.852vh;
    display: flex;
    justify-content: center;
    align-items: center;
    bottom: -2.6vh;
    position: absolute;
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.111vh;
    line-height: 1.296vh;

    color: #ffffff;
  }
  

  &-item {
    width: 8vh;
    height: 8vh;
    margin: 0 0.9vh 0.9vh 0;
    box-sizing: border-box;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;

    transition: 0.5s all;

    background: radial-gradient(
      50% 50% at 50% 50%,
      rgba(0, 0, 0, 0.05) 0%,
      rgba(0, 0, 0, 0.09) 100%
    );
    border: 0.1vh solid rgba(255, 255, 255, 0.1);
    border-radius: 0.1vh;

    &.list-item-type-1 {
        background: radial-gradient(50% 50% at 50% 50%, rgba(115, 92, 255, 0.0275) 0%, rgba(115, 92, 255, 0.088) 100%);
        border: 1px solid #735CFF;
    }

    &.list-item-type-2 {
        border: 1px solid rgba(255, 127, 55, 0.55);
        background: radial-gradient(50% 50% at 50% 50%, rgba(255, 127, 55, 0.0275) 0%, rgba(255, 127, 55, 0.088) 100%);
    }
    &:hover {
        border: 1px solid rgba(255, 255, 255, 1);
    }

    &-img {
      -webkit-user-drag: none;
      -o-user-drag: none;
      pointer-events: none;
      width: 70%;
      height: 70%;
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
      background-color: #ff3380;
    }
  }

  &-with-item {
    background: radial-gradient(
      80.56% 80.56% at 50% 76.11%,
      rgba(255, 255, 255, 0.15) 0%,
      rgba(255, 255, 255, 0) 100%
    );
  }

  &-active {
    border: 2px solid rgba(255, 255, 255, 0.6);
  }
}
</style>

<style lang="scss" scoped>
.equip-weapon__list {
  display: flex;
  .equip-weapon__item {
    display: flex;
    align-items: center;
    gap: 0.9vh;

    span.index {
      border: 0.1vh solid red;
      border-radius: 50%;
      width: 2.6vh;
      height: 2.6vh;
      display: flex;
      justify-content: center;
      align-items: center;
    }

    &-slot {
      width: 8vh;
      height: 8vh;
      margin: 0 0.9vh 0.9vh 0;
      box-sizing: border-box;
      display: flex;
      align-items: center;
      justify-content: center;
      position: relative;

      transition: 0.5s all;

      &:hover {
        border: 0.05vh solid white;
      }

      background: radial-gradient(
        50% 50% at 50% 50%,
        rgba(0, 0, 0, 0.05) 0%,
        rgba(0, 0, 0, 0.09) 100%
      );
      border: 0.1vh solid rgba(255, 255, 255, 0.1);
      border-radius: 0.1vh;
    }
  }
}
</style>
