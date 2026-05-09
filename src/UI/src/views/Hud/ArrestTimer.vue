<template>
  <div class="artimer">
    <div class="artimer-time">
      <svg viewBox="0 0 42 42" xmlns="http://www.w3.org/2000/svg">
        <circle
          cx="21"
          cy="21"
          r="19"
          :stroke="'rgba(255, 255, 255, 0.07)'"
          stroke-width="4"
          fill="rgba(0, 0, 0, 0.1)"
          stroke-dasharray="120"
          stroke-dashoffset="0"
        />
        <circle
          cx="21"
          cy="21"
          r="19"
          :stroke="'#301934 '"
          stroke-width="4"
          fill="none"
          stroke-dasharray="120"
          :stroke-dashoffset="-1.2 * (100 - progress)"
        />
      </svg>
      <img src="/img/hud/artimer/time.svg" alt="" />
    </div>
    <div class="artimer-info">
      <span>{{ loc("arrets:timer:tit") }}</span>
      <span>{{ minutes }}:{{ seconds }}</span>
    </div>
    <!--  <img src="/img/hud/artimer.svg" alt="artimer" class="artimer-img" />
    <div class="artimer-tittle">{{ loc("arrets:timer:tit") }}</div>
    <div class="artimer-timer">{{ minutes }}:{{ seconds }}</div>
    <div class="artimer-progress">
      <div
        class="artimer-progress-thumb"
        :style="{ width: `${progress}%` }"
      ></div>
    </div>
    <img src="/img/hud/arwire.png" alt="artimer" class="artimer-wire" /> -->
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  props: {
    timer: Object,
  },
  data() {
    return {
      interval: 0,
      minutes: "00",
      seconds: "00",
      total: 100,
      progress: 100,
    };
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
  methods: {
    tick() {
      const interval = (this.timer.expiried - Date.now()) / 1000;
      if (interval > 0) {
        let min = `${Math.floor(interval / 60)}`;
        if (min == "0") {
          min = "00";
        } else {
          while (min.length < 2) {
            min = "0" + min;
          }
        }
        let seconds = `${Math.floor(interval % 60)}`;
        if (seconds == "0") {
          seconds = "00";
        } else {
          while (seconds.length < 2) {
            seconds = "0" + seconds;
          }
        }
        this.minutes = min;
        this.seconds = seconds;
        this.progress = Math.floor((interval / this.total) * 100);
        //window.console.log(this.progress)
      } else {
        this.$store.commit("hud/resetArrestTimer");
      }
    },
  },
  mounted() {
    this.total = (this.timer.expiried - Date.now()) / 1000;
    this.interval = setInterval(this.tick, 1000);
  },
  beforeDestroy() {
    clearInterval(this.interval);
  },
};
</script>

<style lang="scss" scoped>
.artimer {
  position: fixed;
  top: 1.053rem;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  align-items: center;

  &::after {
    content: "";
    position: absolute;
    top: -4.474rem;
    left: 50%;
    transform: translate(-50%, 0);
    border-radius: 50%;
    width: 8.947rem;
    height: 8.947rem;
    background: #6c0c0c;
    filter: blur(5.619rem);
    z-index: -1;
  }

  &-time {
    position: relative;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 2.211rem;
      height: 2.263rem;
    margin-right: 0.842rem;

    svg {
      width: 2.211rem;
      height: 2.263rem;
      transform: rotate(270deg);
    }

    img {
      height: 0.895rem;
      width: 0.895rem;
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
    }

    &::after {
      content: "";
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      width: 2rem;
      height: 1.842rem;
      background: url(/img/hud/artimer/bg-time.svg) center center no-repeat;
    }
  }

  &-info {
    span {
      display: block;
      font-family: "Akrobat";
      font-weight: 700;
      text-transform: uppercase;

      &:first-child {
        font-size: 0.842rem;
        line-height: 1rem;
        color: rgba(255, 255, 255, 0.55);
        margin-bottom: -0.158rem;
      }

      &:last-child {
        font-size: 1.263rem;
        line-height: 1.526rem;
        color: #ffffff;
      }
    }
  }

  /* &-img {
    width: 8rem;
    height: 8rem;
    position: absolute;
  }
  &-tittle {
    margin-top: 1.8rem;
    width: 8rem;
    text-align: center;
    font-size: 1.2rem;
  }
  &-timer {
    width: 8rem;
    text-align: center;
    font-size: 3rem;
    line-height: 2.6rem;
    font-weight: 300;
  }
  &-progress {
    width: 13rem;
    margin-left: -2.5rem;
    margin-top: 0.5rem;
    height: 0.2rem;
    background-color: rgba(#fff, 0.2);
    position: relative;
    border-radius: 0.2rem;
    overflow: hidden;
    &-thumb {
      background-color: #fff;
      border-radius: 0.2rem;
      height: 100%;
      transition: all 1s linear;
    }
  }
  &-wire {
    position: fixed;
    bottom: 0;
    right: 0;
  } */
}
</style>