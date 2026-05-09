<template>
  <div class="level-up">
    <img src="/img/newhud/level-up-bg.svg" alt="" class="level-up_bg" />
    <span class="level-up_title">PAYDAY</span>
    <div class="level-up_wrap">
      <span class="level-up_number">
        {{ levelUp.currentLevel }}
      </span>
      <div class="level-up_progress">
        <div
          v-for="elem in 15"
          :key="elem"
          :class="{
            active:
              (100 / 15) * elem <= (levelUp.currentExp / levelUp.maxExp) * 100,
          }"
        ></div>
      </div>
      <span class="level-up_number">
        {{ levelUp.currentLevel + 1 }}
      </span>
    </div>
    <div class="level-up_score">
      <span
        >XP&nbsp;{{ levelUp.currentExp }}&nbsp;/&nbsp;{{ levelUp.maxExp }}</span
      >
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from "vuex";
export default {
  data: function () {
    return {
      currentLine: 0,
      timer: null,
    };
  },

  methods: {
    startTimer() {
      this.timer = setInterval(() => {
        this.currentLine++;
      }, 50);
    },
    stopTimer() {
      clearTimeout(this.timer);
      window.mp.trigger("exp:timerStoped");
    },
  },

  computed: {
    ...mapState("hud", ["levelUp"]),
    ...mapGetters("localization", ["loc"]),
    lineExp: function () {
      return (this.currentLine / this.levelUp.maxExp) * 100;
    },
  },

  watch: {
    currentLine(time) {
      if (time === this.levelUp.currentExp) {
        this.stopTimer();
      }
    },
  },

  mounted() {
    this.currentLine = this.levelUp.currentExp - this.levelUp.exp;
    this.startTimer();
  },

  destroyed() {
    this.stopTimer();
  },
};
</script>

<style lang="scss" scoped>
@keyframes fromUp {
  from {
    transform: translateY(-100%) translateX(-50%);
  }
  to {
    transform: translateY(0%) translateX(-50%);
  }
}
@keyframes levelUp {
  from {
  }
  to {
  }
}

.level-up {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: absolute;
  top: 28px;
  left: 50%;
  transform: translateX(-50%);
  animation: fromUp 0.5s;

  &::after {
    content: "";
    width: 170px;
    height: 170px;
    background: #6c0c0c;
    filter: blur(106.766px);
    position: absolute;
    top: -103px;
    left: 50%;
    transform: translateX(-50%);
    border-radius: 50%;
    z-index: -1;
  }

  &_bg {
    position: absolute;
    top: -2px;
    width: 200px;
    height: 101px;
    left: 50%;
    transform: translateX(-50%);
    z-index: 1;
  }

  &_title {
    display: block;
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 32px;
    line-height: 38px;
    text-transform: uppercase;
    color: #ffffff;
    margin-bottom: 10px;
  }

  &_wrap {
    position: relative;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  &_number {
    position: absolute;
    font-family: "Akrobat";
    font-style: normal;
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    text-transform: uppercase;
    color: #ffffff;
    top: 50%;

    &:first-child {
      left: -14px;
      transform: translateY(-50%) translateX(-100%);
    }

    &:last-child {
      right: -14px;
      transform: translateY(-50%) translateX(100%);
    }
  }

  &_progress {
    display: flex;
    align-items: center;
    justify-content: center;

    div {
      width: 10px;
      height: 24px;
      border-radius: 5px;
      background: rgba(0, 0, 0, 0.3);

      &.active {
        background: #301934 ;
        box-shadow: 0px 0px 7px rgba(71, 44, 132, 0.5);
      }

      &:not(:last-child) {
        margin-right: 4px;
      }
    }
  }

  &_score {
    margin-top: 12px;
    display: flex;
    align-items: center;

    span {
      font-family: "Akrobat";
      font-weight: 700;
      font-size: 14px;
      line-height: 17px;
      text-transform: uppercase;
      color: #ffffff;
    }
  }
}
</style>
