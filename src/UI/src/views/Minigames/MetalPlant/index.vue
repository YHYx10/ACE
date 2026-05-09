<template>
  <div class="mplant">
    <!-- :style="{
      background: `url(/img/miniGames/metalPlant/bg/${background}.jpg) no-repeat center`,
    }" -->
    <component :is="currantState" />
    <Preparation v-if="preparation" />
    <button
      class="close"
      @click="close"
      :class="{ bottom: currantState === 'Game' && !preparation }"
    >
      <img src="/img/autoSchool/question/x.svg" alt="" />
    </button>
  </div>
</template>

<script>
import { mapState } from "vuex";
import Inventory from "./Inventory.vue";
import Preparation from "./Preparation.vue";
import Game from "./Game.vue";
import GameOver from "./GameOver.vue";
import GameWin from "./GameWin.vue";

export default {
  computed: {
    ...mapState("gameMetalPlant", ["currantState", "preparation"]),
    background() {
      switch (this.currantState) {
        case "Inventory":
          return 3;
        case "Game":
          return 4;
        case "GameOver":
          return 2;
        default:
          return 1;
      }
    },
  },
  components: {
    Inventory,
    Preparation,
    Game,
    GameOver,
    GameWin,
  },
  methods: {
    close() {
      window.mp.trigger("mg:metalplant:close");
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant {
  color: #fff;
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background-size: cover;
  background: rgba(7, 7, 7, 0.96);

  * {
    font-family: "Akrobat";
  }

  .close {
    background: none;
    border: none;
    display: flex;
    align-items: center;
    justify-content: center;
    border: none;
    cursor: pointer;
    width: conv(50);
    height: conv(50);
    position: absolute;
    top: conv(40);
    right: conv(40);
    z-index: 1000;

    &.bottom{
      bottom: conv(75);
      transform: translateY(50%);
      top: auto;
    }

    img {
      height: conv(30);
      width: conv(30);
    }
  }
}
</style>