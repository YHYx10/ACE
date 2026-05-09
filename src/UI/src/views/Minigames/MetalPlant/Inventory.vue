<template>
  <div class="mplant-inv">
    <div class="mplant-inv-blocks">
      <div class="mplant-inv-block">
        <div class="mplant-inv-block_title">{{ loc("mplant:inv:ore") }}</div>
        <div class="mplant-inv_body">
          <inventory-list
            :items="inventory.ore"
            @onitemselect="selectOreItem"
          />
        </div>
      </div>

      <div class="mplant-inv-block">
        <div class="mplant-inv-block_title">{{ loc("mplant:inv:fuel") }}</div>
        <div class="mplant-inv_body">
          <inventory-list
            :items="inventory.fuel"
            @onitemselect="selectFuelItem"
          />
        </div>
      </div>
    </div>

    <div class="mplant-inv-furnace">
      <!-- <img src="/img/miniGames/metalPlant/common/title.png" alt="title" class="mplant-inv-furnace_bg"> -->
      <div class="mplant-inv-furnace_title">
        {{ loc("mplant:inv:furnace") }}
      </div>
      <div class="mplant-inv-furnace_sub">
        {{ loc("mplant:inv:sub:1") }} {{ loc("mplant:inv:sub:2") }}
      </div>
      <div class="mplant-inv-furnace_body">
        <div>
          <div class="mplant-inv-furnace_name">{{ loc(oreName) }}</div>
          <div class="mplant-inv-furnace_item">
            <img
              :src="`/img/miniGames/metalPlant/resources/${image(
                furnace.ore
              )}.png`"
              alt="item"
              v-if="furnace.ore"
            />
          </div>
        </div>
        <div>
          <div class="mplant-inv-furnace_name">{{ loc(fuelName) }}</div>
          <div class="mplant-inv-furnace_item">
            <img
              :src="`/img/miniGames/metalPlant/resources/${image(
                furnace.fuel
              )}.png`"
              alt="item"
              v-if="furnace.fuel"
            />
          </div>
        </div>
      </div>
      <div class="mplant-inv-furnace_horn">
        <Horn />
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from "vuex";
import InventoryList from "./InventoryList.vue";
import Horn from "./Horn.vue";

export default {
  computed: {
    ...mapState("gameMetalPlant", ["inventory", "furnace", "itemConfigs"]),
    ...mapGetters("localization", ["loc"]),
    oreName() {
      return this.itemConfigs[this.furnace.ore]
        ? this.itemConfigs[this.furnace.ore].DisplayName
        : "mplant:res:none";
    },
    fuelName() {
      return this.itemConfigs[this.furnace.fuel]
        ? this.itemConfigs[this.furnace.fuel].DisplayName
        : "mplant:res:none";
    },
  },
  components: {
    InventoryList,
    Horn,
  },
  methods: {
    selectOreItem(ore) {
      this.loadOreToFurnace(ore);
    },
    selectFuelItem(fuel) {
      this.loadFuelToFurnace(fuel);
    },
    image(id) {
      return this.itemConfigs[id].Image;
    },
    ...mapMutations("gameMetalPlant", [
      "loadFuelToFurnace",
      "loadOreToFurnace",
    ]),
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant-inv {
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;

  &-blocks {
    display: grid;
    grid-template-rows: conv(398) conv(398);
    width: conv(543);
    row-gap: conv(52);
    margin-right: conv(142);
  }

  &-block {
    width: 100%;
    height: 100%;
    position: relative;
    display: flex;
    flex-direction: column;

    &_title {
      font-weight: 700;
      font-size: conv(48);
      line-height: conv(58);
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: conv(20);
    }
  }

  &_body {
    width: 100%;
    height: 100%;
  }

  &-furnace {
    width: conv(735);
    height: conv(499);
    position: relative;
    display: flex;
    flex-direction: column;
    align-items: center;

    &_title {
      font-weight: 700;
      font-size: conv(48);
      line-height: conv(58);
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: conv(4);
    }

    &_sub {
      width: 100%;
      text-align: center;
      white-space: nowrap;
      font-weight: 700;
      font-size: conv(20);
      line-height: conv(24);
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: conv(26);
    }

    &_body {
      width: 100%;
      height: conv(290);
      display: flex;
      align-items: center;
      justify-content: center;
      align-items: center;
      background: url(/img/miniGames/metalPlant/fire-bg.png) center center no-repeat;
      background-size: cover;

      & > div:first-child {
        margin-right: conv(60);
      }

      & > div {
        display: flex;
        flex-direction: column;
        align-items: center;
      }
    }

    &_name {
      font-weight: 700;
      font-size: conv(16);
      line-height: conv(19);
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: conv(13);
    }

    &_item {
      width: conv(150);
      height: conv(150);
      background: radial-gradient(
        50% 50% at 50% 50%,
        rgba(0, 0, 0, 0.5) 0%,
        rgba(0, 0, 0, 0.6) 100%
      );
      border: conv(1) solid rgba(255, 255, 255, 0.1);
      display: flex;
      align-items: center;
      justify-content: center;

      img {
        width: conv(120);
        height: conv(120);
        -webkit-user-drag: none;
      }
    }

    &_horn {
      width: 100%;
      display: flex;
      align-items: center;
      position: relative;
    }

    /* &_bg {
      position: absolute;
      top: -0.2rem;
      left: 0;
      opacity: 0.5;
      width: 100%;
    } */
  }
}
</style>