<template>
  <div class="timer-to-capture" v-if="untilCaptureTimerEnabled">
    <div>
      <img src="/img/hud/timeCapture/icon.svg" alt="" />
      <img src="/img/hud/timeCapture/icon.svg" alt="" />
    </div>
    <div class="timer-to-capture__info">
      <div class="title">{{ loc(untilCaptureTimerMessage) }}</div>
      <div class="value">
        {{ formatTimeLeft(untilCaptureTimerCurrentTime) }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  name: "TimeToCapture",

  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("hud", [
      "untilCaptureTimerMaxTime",
      "untilCaptureTimerCurrentTime",
      "untilCaptureTimerEnabled",
      "untilCaptureTimerMessage",
    ]),
    appWidthUnit: function () {
      const appWidth = document.getElementById("app").offsetWidth / 100;
      return Math.ceil(appWidth);
    },

    circleValue: function () {
      const maxCircleValue = this.appWidthUnit * 15.06;
      const maxCircleValuePer =
        (maxCircleValue / 100 / this.untilCaptureTimerMaxTime) * 100;
      const curCircleVal =
        maxCircleValue - maxCircleValuePer * this.untilCaptureTimerCurrentTime;

      return curCircleVal;
    },
  },

  methods: {
    formatTimeLeft: function (time) {
      let minutes = Math.floor(time / 60);
      let seconds = time % 60;

      if (seconds < 10) {
        seconds = `0${seconds}`;
      }
      if (minutes < 10) {
        minutes = `0${minutes}`;
      }

      return `${minutes}:${seconds}`;
    },
  },
};
</script>

<style lang="scss" scoped>
.timer-to-capture {
  display: flex;
  position: absolute;
  align-items: center;
  left: 2.105rem;
  bottom: 11.646rem;
  text-transform: uppercase;
  padding-right: 0.6rem;

  & > div:first-child {
    position: relative;
    width: 43.98px;
    height: 41.72px;
    margin-right: 7.02px;

    img {
      &:first-child {
        width: 100%;
        height: 100%;
        opacity: 0.2;
      }

      &:last-child {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 29.95px;
        height: 28.42px;
      }
    }
  }

  &__info {
    display: flex;
    flex-flow: column;
    align-items: flex-start;

    div {
      font-family: "Akrobat";
      font-weight: 700;
      text-transform: uppercase;
    }

    .title {
      font-size: 0.579rem;
      line-height: 0.684rem;
      color: rgba(255, 255, 255, 0.5);
      margin-bottom: -0.263rem;
    }

    .value {
      font-size: 1.263rem;
      line-height: 1.526rem;
      color: #ffffff;
    }
  }
}
</style>
