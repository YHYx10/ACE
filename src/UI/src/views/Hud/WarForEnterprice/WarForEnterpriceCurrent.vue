<template>
  <div
    class="war-for-enterprice-current"
    :style="{ zoom: zoom, right: inVeh ? '410px' : '23px' }"
  >
    <div class="war-for-enterprice-current__title">
      {{ loc("war_for_enterprice_4") }}
    </div>
    <div class="war-for-enterprice-current__name">
      {{ companiesConfig[currentEnterprice].Name }}
    </div>
    <div class="war-for-enterprice-current__progress-bar">
      <div
        class="war-for-enterprice-current__progress-bar-line"
        :style="{ width: `${currentProgressWidth}%` }"
      ></div>
    </div>

    <div class="war-for-enterprice-current__progress-title">
      <div class="war-for-enterprice-current__progress-title-desc">
    Progress
      </div>
      <div class="war-for-enterprice-current__progress-title-value">
        {{ currentProgressWidth }}%
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
import warcompanies from "../../../configs/families/warcompanies";
export default {
  name: "WarForEnterpriceCurrent",
  data: function () {
    return {
      time: 0,
      timer: null,
    };
  },
  props: {
    zoom: Number,
    inVeh: Boolean,
  },
  computed: {
    ...mapState("hud/warForEnterprice", ["currentTime", "currentEnterprice"]),
    ...mapGetters("localization", ["loc"]),
    companiesConfig: function () {
      return warcompanies;
    },
    currentProgressWidth: function () {
      return Math.round(100 - (this.time / this.currentTime) * 100);
    },
  },
  methods: {
    startTimer() {
      this.timer = setInterval(() => {
        this.time--;
      }, 1000);
    },
    stopTimer() {
      clearTimeout(this.timer);
    },
  },
  watch: {
    time(val) {
      if (val === 0) {
        this.stopTimer();
      }
    },
  },
  mounted() {
    this.time = this.currentTime;
    this.startTimer();
  },
  destroyed() {
    this.stopTimer();
  },
};
</script>

<style lang="scss" scoped>
.war-for-enterprice-current {
  position: absolute;
  right: 410px;
  bottom: 16px;
  width: 316px;
  height: 178px;
  background: linear-gradient(
    270deg,
    rgba(12, 14, 20, 0.55) 0%,
    rgba(12, 14, 20, 0) 100%
  );
  padding: 23px;

  &,
  span,
  button {
    font-family: "Akrobat";
  }

  &__title,
  &__name {
    text-align: right;
    font-weight: 700;
  }

  &__title {
    font-size: 28px;
    line-height: 34px;
    color: #ffffff;
    margin-bottom: 2px;
    text-transform: uppercase;
  }

  &__name {
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #fdff8f;
    margin-bottom: 8px;
  }

  &__progress-bar {
    width: 100%;
    height: 15px;
    background: rgba(0, 0, 0, 0.4);
    border-radius: 36px;
    position: relative;
    overflow: hidden;
    margin-bottom: 11px;

    & > div {
      position: absolute;
      top: 0;
      left: 0;
      height: 100%;
      background: #fe2a2a;
      border-radius: 36px;
      transition: 0.3s;
    }
  }

  &__progress-title {
    display: flex;
    align-items: flex-end;
    justify-content: flex-end;

    div {
      font-weight: 700;
    }

    &-desc {
      font-size: 20px;
      line-height: 24px;
      color: rgba(255, 255, 255, 0.55);
      text-transform: uppercase;
    }

    &-value {
      margin-left: 27px;
      font-size: 32px;
      line-height: 38px;
      color: #ffffff;
    }
  }
}
/* .war-for-enterprice-current{
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 30rem;
  position: absolute;
  left: 50%;
  bottom: 3rem;
  transform: translateX(-50%);
  text-transform: uppercase;
  color: #FFFFFF;
  font-weight: bold;
  &:before{
    content: '';
    width: 46rem;
    height: 17.2rem;
    background: radial-gradient(37.34% 100% at 51.87% 0%, rgba(32, 91, 88, 0.5) 0%, rgba(27, 84, 81, 0) 100%);
    transform: rotate(180deg);
    position: absolute;
    pointer-events: none;
  }
  &__name{
    font-size: 1rem;
    line-height: 1rem;
    color: #29FFF2;
    position: absolute;
    top: -.1rem;
    left: 0;  
    transform: translateY(-100%);
  }
  &__head{
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    margin-bottom: .5rem;
  }
  &__title{
    font-size: 2rem;
    line-height: 2rem;
  }
  &__progress-title{
    display: flex;
    align-items: center;
    height: fit-content;
    &-desc{
      font-size: 1rem;
      line-height: 1.2rem;
      letter-spacing: 0.1em;
      margin-right: .5rem;
      position: relative;
      font-weight: 300;
      display: flex;
      align-items: flex-end;
      height: 1.6rem;
      &:before{
        content: '';
        width: 100%;
        height: .1rem;
        background: #29FFF2;
        border-radius: 5rem;
        position: absolute;
        top: 0;
        left: 0;
      }
    }
    &-value{
      font-size: 2rem;
      line-height: 2rem;
    }
  }
  &__progress-bar{
    width: 100%;
    height: 1rem;
    position: relative;
    background: rgba(255, 255, 255, 0.2);
    border: 1px solid rgba(255, 255, 255, 0.5);
    border-radius: 1005rem;
    display: flex;
    align-items: center;
    justify-content: flex-start;
    padding: .2rem;
    &-line{
      height: 100%;
      background: #29FFF2;
      border-radius: 5rem;
      transition: .3s;
    }
  }
} */
</style>
