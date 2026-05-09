<template>
  <div class="deathscreen">
    <div class="deathscreen-body">
      <img src="/img/deathScreen/dies.png" alt="" />
      <div class="deathscreen-body_title">You are dead</div>
      <div class="deathscreen-body_subtitle" v-html="currentText"></div>
      <div class="deathscreen-body_time">{{ minutes }}:{{ seconds }}</div>
      <div @click="callEms" class="deathscreen-body_btn primary" v-if="medicsBtn">
        <img src="/img/deathScreen/ems.svg" alt="ems" />
        {{ loc("death_menu_3") }}
      </div>
      <div
        @click="die"
        class="deathscreen-body_btn deathscreen-body_die"
        v-if="deathBtn"
      >
        <img src="/img/deathScreen/die.svg" alt="die" />
        {{ loc("death_menu_5") }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  data() {
    return {
      radius: 80,
      hintTime: 60,
      hintLength: 60,
      hintInterval: null,
      currentHintIndex: 0,
      shufledHints: [],
      currentText: "Death will come through:",
    };
  },
  computed: {
    ...mapState("deathScreen", [
      "time",
      "startTime",
      "medicsBtn",
      "deathBtn",
      "hints",
    ]),
    ...mapGetters("localization", ["loc"]),
    dashArray() {
      const plength = Math.floor(this.percentLength * this.currentPercent);
      return `${plength} ${this.circleLength - plength}`;
    },
    circleLength() {
      return Math.floor(2 * Math.PI * this.radius);
    },
    percentLength() {
      return this.circleLength / 100;
    },
    currentPercent() {
      return Math.floor((this.hintTime / this.hintLength) * 100);
    },
    currentHint() {
      return this.shufledHints[this.currentHintIndex];
    },
    timerProgress() {
      return 100 - Math.floor((this.time / this.startTime) * 100);
    },
    minutes() {
      const minutes = Math.floor(this.time / 60);
      return minutes < 10 ? `0${minutes}` : `${minutes}`;
    },
    seconds() {
      const seconds = this.time % 60;
      return seconds < 10 ? `0${seconds}` : `${seconds}`;
    },
  },
  methods: {
    die() {
      window.mp.triggerServer("dieEms");
    },
    callEms() {
      this.currentText = "Expect help";
      window.mp.triggerServer("callEms");
    },
    hintHandler() {
      this.hintTime--;
      if (this.hintTime < 1) {
        this.nextHint();
      }
    },
    nextHint() {
      if (this.currentHintIndex + 1 >= this.shufledHints.length)
        this.currentHintIndex = 0;
      else this.currentHintIndex++;

      this.hintTime = this.hintLength;
    },
    shuffle(array) {
      const result = [...array];
      for (let i = result.length - 1; i > 0; i--) {
        let j = Math.floor(Math.random() * (i + 1));

        [result[i], result[j]] = [result[j], result[i]];
      }
      return result;
    },
  },
  mounted() {
    if (process.env.NODE_ENV == "development")
      this.$store.commit("deathScreen/setTime", 300);
    this.hintLength = this.hintTime;
    this.shufledHints = this.shuffle(this.hints);
    this.hintInterval = setInterval(this.hintHandler, 1000);
  },
  beforeDestroy() {
    clearInterval(this.hintInterval);
  },
};
</script>

<style lang="scss" scoped>
.deathscreen {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: radial-gradient(
    50% 50% at 50% 50%,
    rgba(28, 23, 23, 0.8) 0%,
    #17171c 100%
  );
  font-family: "Akrobat";

  &-body {
    position: absolute;
    left: 50%;
    transform: translateX(-50%);
    top: 13.947rem;
    display: flex;
    flex-direction: column;
    align-items: center;

    & > img {
      width: 9.632rem;
      height: 9.632rem;
      margin-bottom: 0.158rem;
      display: block;
    }

    &_title {
      font-weight: 800;
      font-size: 3.368rem;
      line-height: 4.053rem;
      background: linear-gradient(90deg, #301934  0%, #591b87 100%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0px 0px 5.895rem rgba(255, 255, 255, 0.25);
      margin-bottom: 1.053rem;
    }

    &_subtitle {
      font-weight: 700;
      font-size: 1.263rem;
      line-height: 1.526rem;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    &_time {
      font-weight: 700;
      font-size: 5.053rem;
      line-height: 6.053rem;
      text-transform: uppercase;
      color: #ffffff;
    }

    &_btn {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 15.526rem;
      height: 3.947rem;
      font-weight: 700;
      font-size: 1.263rem;
      line-height: 1.526rem;
      display: flex;
      align-items: center;
      text-align: center;
      text-transform: uppercase;
      color: #ffffff;

      &:not(.deathscreen-body_die) {
        background: linear-gradient(180deg, #301934  0%, #591b87 100%);
        box-shadow: 0px 0px 0.789rem rgba(207, 16, 25, 0.8);

        img {
          display: block;
          margin-right: 0.842rem;
          width: 1.158rem;
          height: 1.158rem;
        }
      }
      transition: 0.5s ease;
      border: none;

      &:hover {
        background: rgba(255, 255, 255, 0.1);
        box-shadow: 0px 0px 1rem rgba(0, 0, 0, 0.2);
      }

      &.primary {
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.25) 0%,
          rgba(75, 0, 130, 0.25) 100%
        );
        outline: 1px solid #301934 ;
        box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
        &:hover {
          box-shadow: inset 0px 0px 3.789rem rgba(75, 0, 130, 0.86);
          background: linear-gradient(
            180deg,
            rgba(71, 44, 132, 0.35) 0%,
            rgba(75, 0, 130, 0.35) 100%
          );
        }
      }
    }

    &_die {
      margin-top: 0.526rem;
      background: rgba(0, 0, 0, 0.6);
      // box-shadow: 0px 0px 0.789rem rgba(0, 0, 0, 0.4);

      img {
        display: block;
        margin-right: 0.526rem;
        width: 1.632rem;
        height: 1.632rem;
      }
    }

    & > div:nth-child(5) {
      margin-top: 1.684rem !important;
    }
  }
}
</style>