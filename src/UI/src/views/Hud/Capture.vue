<template>
  <div class="capture">
    <transition name="capture-state" appear>
      <div class="capture__state" v-if="isCaptureItems">
        <div v-for="(team, index) in captureTeams" :key="index" :class="[{ reversed: index === 0 }, 'side']">
          <div class="side__desc">
            <h6 class="title" :style="{ color: colors[team.key] }">{{ team.title }}</h6>
            <span class="count">{{ team.players }}</span>
          </div>
        </div>
        <div class="timer">
          <span class="timer__value">{{ formatTimeLeft(captureTimeCurrent) }}</span>
          <img src="/img/hud/artimer/time.svg" alt="" />
          <svg class="timer__svg" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 95 95">
            <circle class="circle" :stroke-dasharray="appWidthUnit * 12.55" fill="none" stroke="rgba(255, 255, 255, 0.07)" :stroke-width="appWidthUnit / 4" cx="47.5" cy="47.5" r="45" />
            <circle class="circle" :stroke-dasharray="appWidthUnit * 12.55" :stroke-dashoffset="circleValue" cx="47.5" cy="47.5" r="45" fill="none" stroke="#35b4ff" :stroke-width="appWidthUnit / 4" />
          </svg>
        </div>
      </div>
    </transition>

    <transition-group tag="div" name="list" class="capture__log" v-if="isCaptureLog" appear>
      <div class="capture__log-head" key="log-head">
        <span class="title">? KILL LOG</span>
      </div>

      <div v-for="(item, index) in captureLog" :key="`key_${index}`" class="item">
        <span v-if="item.killerName" :style="{ color: colors[item.killerFraction] }" class="item__killer">{{ item.killerName }}</span>
        <img :src="`/img/hud/captureUI/weapons/${item.weaponId}.png`" :alt="item.weaponId" class="item__icon" />
        <span class="item__deceased" :style="{ color: colors[item.deceasedFraction] }">{{ item.deceasedName }}</span>
      </div>

      <div class="capture__log-footer" key="log-footer">KILL LOG IS VISIBLE FOR ADMINS ONLY</div>
    </transition-group>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  name: "Capture",
  data() {
    return {
      colors: {
        neutral: "#d6d6d6",
        attackers: "#ff4048",
        defenders: "#67ff77",
        ballas: "#cf7eff",
        bloods: "#ff6756",
        vagos: "#f8dd3f",
        families: "#7dff68",
        marabunta: "#61c6ff",
      },
    };
  },
  computed: {
    ...mapState("hud", [
      "captureTeams",
      "captureLog",
      "isCaptureItems",
      "isCaptureLog",
      "captureTimeLimit",
      "captureTimeCurrent",
    ]),
    appWidthUnit() {
      const appWidth = document.getElementById("app").offsetWidth / 100;
      return Math.ceil(appWidth);
    },
    circleValue() {
      const maxCircleValue = this.appWidthUnit * 12.55;
      const maxCircleValuePer = (maxCircleValue / 100 / this.captureTimeLimit) * 100;
      return maxCircleValue - maxCircleValuePer * this.captureTimeCurrent;
    },
  },
  methods: {
    formatTimeLeft(time) {
      const minutes = String(Math.floor(time / 60)).padStart(2, "0");
      const seconds = String(time % 60).padStart(2, "0");
      return `${minutes}:${seconds}`;
    },
  },
};
</script>

<style lang="scss" scoped>
.capture {
  width: 100%;
  height: 100%;
  text-transform: uppercase;
  position: absolute;
  top: 0;
  left: 0;

  &__state {
    transform: translate(-50%, 0);
    position: absolute;
    top: 0;
    left: 50%;
    width: 50rem;
    height: 10rem;
    display: flex;
    margin: 1.579rem 0 0 0;

    .side {
      display: flex;
      align-items: flex-start;
      width: 50%;
      padding: 0 0 0 3.8rem;

      &__desc {
        color: #fff;
        font-family: "Montserrat", sans-serif;
        display: flex;
        align-items: center;
        flex-direction: column;

        .title {
          font-weight: 700;
          font-size: 1.08rem;
        }

        .count {
          font-weight: 800;
          font-size: 1.6rem;
          color: #ffffff;
        }
      }

      &.reversed {
        flex-direction: row-reverse;
        padding: 0 3.8rem 0 0;
      }
    }

    .timer {
      position: absolute;
      transform: translate(-50%, 0);
      top: 0;
      left: 50%;
      width: 2.263rem;
      height: 2.263rem;
      display: flex;
      justify-content: center;

      &__value {
        position: absolute;
        transform: translate(-50%, 0%);
        top: 2.579rem;
        left: 50%;
        color: #fff;
        letter-spacing: 0.1em;
      }

      &__svg {
        width: 2.263rem;
        height: 2.263rem;
        transform: rotate(90deg) scale(-1, 1);
      }

      img {
        position: absolute;
        top: 1.1315rem;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 0.895rem;
        height: 0.895rem;
      }
    }
  }

  &__log {
    position: absolute;
    top: 11.9rem;
    right: 1.2rem;
    min-width: 18.8rem;
    max-width: 22rem;
    padding: 0.75rem 0.85rem;
    border-radius: 1.05rem;
    border: 0.0625rem solid rgba(255, 255, 255, 0.1);
    background: linear-gradient(180deg, rgba(11, 14, 23, 0.85), rgba(8, 11, 19, 0.64));
    box-shadow: 0 0.85rem 2.1rem rgba(0, 0, 0, 0.52);
    backdrop-filter: blur(0.45rem);
    display: flex;
    flex-direction: column;
    gap: 0.48rem;
    font-family: "Montserrat", sans-serif;

    &-head .title {
      color: #ff3f4a;
      font-size: 1.02rem;
      font-weight: 800;
      letter-spacing: 0.04rem;
    }

    .item {
      display: flex;
      align-items: center;
      justify-content: flex-end;
      gap: 0.52rem;
      min-height: 1.5rem;

      &__killer,
      &__deceased {
        font-weight: 700;
        font-size: 1.01rem;
        text-shadow: 0 0 0.6rem rgba(0, 0, 0, 0.6);
      }

      &__icon {
        width: 2.08rem;
        height: 0.95rem;
        object-fit: contain;
        filter: drop-shadow(0 0 0.45rem rgba(255, 255, 255, 0.26));
      }
    }

    &-footer {
      margin-top: 0.45rem;
      font-size: 0.78rem;
      color: rgba(255, 255, 255, 0.5);
      font-weight: 600;
      letter-spacing: 0.02rem;
    }
  }
}

.list-enter-active,
.list-leave-active,
.capture-state-enter-active,
.capture-state-leave-active {
  transition: all 0.25s;
}

.list-enter,
.list-leave-to {
  opacity: 0;
  transform: translateX(1rem);
}

.capture-state-enter,
.capture-state-leave-to {
  opacity: 0;
  transform: translate(-50%, -1rem);
}
</style>
