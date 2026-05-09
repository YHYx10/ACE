<template>
  <div class="minigame">
    <div class="minigame-keyboards">
      <button :class="{ minigame_press: key === keys[0].keyCode }">
        <img src="/img/hud/fishing/arrow.svg" alt="" />
      </button>
      <div>
        <button
          v-for="elem in 3"
          :key="elem"
          :class="{ minigame_press: key === keys[elem].keyCode }"
        >
          <img src="/img/hud/fishing/arrow.svg" alt="" />
        </button>
      </div>
    </div>

    <div class="minigame_progress">
      <div class="minigame_thumb" :style="{ width: `${progress}%` }"></div>
      <img
        class="minigame_dot"
        src="/img/hud/fishing/fish.svg"
        alt=""
        :style="{ left: `${progress}%` }"
      />
    </div>

    <!-- <div
      class="minigame_btn"
      v-for="(btn, index) in keys"
      :key="index"
      :class="{ minigame_press: key === btn.keyCode }"
    >
      <img src="/img/common/elements/arrow.svg" alt="arrow" />
    </div> -->

    <!-- <div class="minigame_progress">
      <div class="minigame_thumb" :style="{ width: `${progress}%` }"></div>
    </div> -->
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  data() {
    return {
      keys: [
        { pressed: false, keyCode: 38 },
        { pressed: false, keyCode: 37 },
        { pressed: false, keyCode: 40 },
        { pressed: false, keyCode: 39 },
      ],
      key: -1,
      time: 100,
      pressInterval: -1,
      miniGameInterval: -1,
      progress: 50,
      step: 20,
      miss: 0,
    };
  },
  methods: {
    endGame(status) {
      if (process.env.NODE_ENV == "development") this.progress = 50;
      else this.$store.commit("fishing/miniGameEnd", status);
    },
    tickGame() {
      this.progress -= 1;
      if (this.progress > 99) this.endGame(true);
      if (this.progress < 1) this.endGame(false);
    },
    put(e) {
      if (!this.keys.find((k) => k.keyCode === e.keyCode)) return;
      if (e.keyCode === this.key) this.progress += this.step - this.mgDifficult;
      else {
        this.progress -= (this.step - this.mgDifficult) * 2;
      }

      this.key = this.getRandomKey();
    },
    getRandomKey() {
      return this.keys[Math.floor(Math.random() * this.keys.length)].keyCode;
    },
  },
  computed: {
    ...mapState("fishing", ["mgDifficult"]),
  },
  mounted() {
    this.key = this.getRandomKey();
    window.document.addEventListener("keyup", this.put);
    this.pressInterval = window.setInterval(() => {
      this.tickGame();
    }, 100);
  },
  beforeDestroy() {
    window.document.removeEventListener("keyup", this.put);
    window.clearInterval(this.pressInterval);
    window.clearInterval(this.miniGameInterval);
  },
};
</script>

<style lang="scss">
.minigame {
  position: absolute;
  display: flex;
  flex-direction: column;
  align-items: center;
  bottom: 2.368rem;
  left: 50%;
  transform: translateX(-50%);

  &-keyboards {
    display: flex;
    align-items: center;
    flex-direction: column;
    margin-bottom: 1.053rem;

    button {
      display: flex;
      justify-content: center;
      align-items: center;
      width: 2.368rem;
      height: 2.368rem;
      background: rgba(0, 0, 0, 0.4);
      border: 0.053rem solid rgba(255, 255, 255, 0.09);
      border-radius: 0.263rem;

      img {
        width: 1.737rem;
        height: 1.737rem;
      }

      &.minigame_press {
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.5) 0%,
          rgba(75, 0, 130, 0.5) 100%
        );
        border: 0.053rem solid rgba(255, 255, 255, 0.09);
      }
    }

    & > button {
      margin-bottom: 0.526rem;

      img{
        transform: rotate(90deg);
      }
    }

    div {
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      column-gap: 0.526rem;

      button{
        &:nth-child(2) img{
          transform: rotate(-90deg);
        }
        &:last-child{
          transform: rotate(180deg);
        }
      }
    }
  }

  &_progress {
    position: relative;
    width: 100%;
    height: 0.526rem;
    background: rgba(71, 44, 132, 0.1);
  }

  &_thumb {
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    background: #301934 ;
  }

  &_dot {
    position: absolute;
    width: 0.684rem;
    height: 0.421rem;
    top: 50%;
    transform: translate(-50%, -50%);
  }
  /* position: absolute;
  right: 50%;
  bottom: 170px;
  transform: translateX(-50%) translateY(-100%);

  &_btn {
    position: absolute;
    background-color: rgba(0, 0, 0, 0.4);
    width: 45px;
    height: 45px;
    border: 1px solid rgba(255, 255, 255, 0.09);
    border-radius: 5px;
    img {
      width: 50%;
      height: 50%;
      margin: 25% 0 0 25%;
    }
    &:nth-child(1) {
      top: 0;
      left: 6vh;
      box-shadow: 1px 1px 3px 0 rgba(#000, 0.7);
      img {
        transform: rotate(180deg);
      }
    }
    &:nth-child(2) {
      top: 6vh;
      left: 0;
      box-shadow: 1px 1px 3px 0 rgba(#000, 0.7);
      img {
        transform: rotate(90deg);
      }
    }
    &:nth-child(3) {
      top: 6vh;
      left: 6vh;
      box-shadow: 1px 1px 3px 0 rgba(#000, 0.7);
    }
    &:nth-child(4) {
      top: 6vh;
      left: 12vh;
      box-shadow: 1px 1px 3px 1px rgba(#000, 0.7);
      img {
        transform: rotate(-90deg);
      }
    }
  }
  &_press {
    transform: scale(1.01) translate(1px, 1px);
    background: linear-gradient(
      180deg,
      rgba(71, 44, 132, 0.5) 0%,
      rgba(75, 0, 130, 0.5) 100%
    );
    border: 1px solid rgba(255, 255, 255, 0.09);
  }
  &_progress {
    position: absolute;
    top: 13vh;
    left: 0;
    width: 155px;
    height: 10px;
    background: rgba(255, 255, 255, 0.09);
    overflow: hidden;
  }
  &_thumb {
    background-color: #301934 ;
    height: 100%;
    position: absolute;
    left: 0;
  } */
  //     $clr_1: rgb(20, 177, 28);
  // $clr_2: rgb(24, 131, 42);
  // $clr_3: rgb(255,255,255);
  // $clr_4: rgb(203, 241, 195);
  // $clr_5: rgb(10, 10, 10);
}
</style>