<template>
  <div class="farm-house">
    <div class="farm-house_wrap">
      <Header
        :currentPage="currentPage"
        @click-help="() => (modalHelp = true)"
        @click-sell="
          () =>
            currentPage === 'Sell'
              ? (currentPage = 'Profile')
              : (currentPage = 'Sell')
        "
      />
      <div class="farm-house_board">
        <component :is="currentPage" class="farm-house_left" />
        <ShopBtn
          @click-shop="
            () =>
              currentPage === 'Shop'
                ? (currentPage = 'Profile')
                : (currentPage = 'Shop')
          "
          :currentPage="currentPage"
        />
      </div>
    </div>
    <Help v-if="modalHelp" @back="() => (modalHelp = false)" />
    <ExitCross class="close" @click="close" />
  </div>
</template>

<script>
import Header from "./Header.vue";
import Board from "./Board";
import ShopBtn from "./ShopBtn.vue";
import Profile from "./Profile.vue";
import Help from "./Help.vue";
import Sell from "./Sell/Sell.vue";
import Shop from "./Shop.vue";
import ExitCross from '../UI/components/ExitCross.vue';

export default {
  name: "FarmHouse",
  components: {
    Shop,
    Board,
    Help,
    Header,
    ShopBtn,
    Profile,
    Sell,
    ExitCross
},
  data() {
    return {
      currentPage: "Profile",
      modalHelp: false,
    };
  },
  methods: {
    close() {
      window.mp.trigger("farm::closeMenu");
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.farm-house {
  width: 100%;
  height: 100%;
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;
  background: url(/img/farmHouse/bg.png) bottom center no-repeat,
    linear-gradient(
      72.44deg,
      rgba(19, 20, 21, 0.95) 0%,
      rgba(31, 34, 37, 0.95) 100%
    );
  background-size: 100% auto;

  * {
    font-family: "Akrobat";
  }

  &_wrap {
    display: flex;
    flex-direction: column;
    width: conv(1727);
    max-width: conv(1727);
    height: conv(906);
    max-height: conv(906);
  }

  &_left {
    width: 100%;
    height: 100%;
  }

  &_board {
    width: 100%;
    height: 100%;
    max-height: conv(742);
    display: grid;
    grid-template-columns: 1fr conv(479);
  }

  .close {
    background: none;
    border: none;
    outline: none;
    position: absolute;
    top: conv(40);
    right: conv(40);
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    width: conv(50);
    height: conv(50);

    img {
      width: conv(29.17);
      height: conv(29.17);
    }
  }

  &__board {
    z-index: 3;
  }
}
</style>
