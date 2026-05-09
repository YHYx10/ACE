<template>
  <div class="mplant-gover">
    <div class="mplant-gover_body">
      <div class="mplant-gover_title">FAILURE!</div>
      <div
        class="mplant-prepare_delimiter"
        style="transform: scale(0.5, 0.3)"
      ></div>
      <div class="mplant-gover_sub" v-html="subtitle"></div>
      <div class="mplant-gover_img">
        <img
          :src="`/img/miniGames/metalPlant/common/${
            gameResult.state === 0 ? 'temp_lose' : 'elements_lose'
          }.png`"
          alt="el"
          class="mplant-gover_el"
        />
      </div>
      <div class="bank-btn mplant-gover_btn" @click="confirm">Continue</div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
export default {
  computed: {
    ...mapState("gameMetalPlant", ["gameResult"]),
    ...mapGetters("localization", ["loc"]),
    subtitle() {
      return this.loc(
        this.gameResult.state === 0
          ? "mplant:gover:tit:1"
          : "mplant:gover:tit:2"
      );
    },
  },
  methods: {
    confirm() {
      window.mp.trigger("mg:metalplant:quit");
    },
  },
  mounted() {
    this.$store.commit("sounds/play", {
      name: `metalPlant_gameOver`,
      volume: 0.2,
    });
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant-gover {
  position: absolute;
  left: 0;
  top: 0;
  z-index: 501;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;

  div:not(.mplant-gover_img),
  img,
  button,
  span {
    z-index: 2;
    position: relative;
  }

  &_body {
    width: conv(487);
    height: conv(606);
    background: linear-gradient(
      180deg,
      rgba(24, 24, 24, 0.95) 0%,
      rgba(16, 16, 16, 0.95) 100%
    );
    position: relative;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding: conv(64) 0 conv(44);
  }

  &_title {
    font-weight: 700;
    font-size: conv(36);
    line-height: conv(43);
    text-align: center;
    text-transform: uppercase;
    color: #301934 ;
    margin-bottom: conv(26);
  }

  &_img {
    position: relative;
    width: conv(249);
    height: conv(249);
    display: flex;
    align-items: center;
    justify-content: center;

    &::after {
      content: "";
      position: absolute;
      top: 50%;
      left: 50%;
      width: conv(421);
      pointer-events: none;
      height: conv(421);
      border-radius: 50%;
      transform: translate(-50%, -50%);
      background: radial-gradient(
        50% 50% at 50% 50%,
        rgba(71, 44, 132, 0.1) 0%,
        rgba(71, 44, 132, 0) 100%
      );
      z-index: 1;
    }
  }

  img {
    height: 100%;
  }

  &_sub {
    max-width: conv(226);
    font-weight: 700;
    font-size: conv(20);
    line-height: conv(24);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: conv(24);
  }

  &_btn {
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    margin-top: conv(33);
    display: flex;
    justify-content: center;
    align-items: center;
    width: conv(310);
    height: conv(75);
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    text-align: center;
    text-transform: uppercase;
    color: #ffffff;
    cursor: pointer;
  }
}
</style>