<template>
  <div class="work-timer">
    <div>
      <img src="/img/hud/artimer/time.svg" alt="" />
      <img src="/img/hud/artimer/time.svg" alt="" />
    </div>
    <span class="work-timer__text">{{ prettyTime }}</span>
  </div>
</template>

<script>
import { mapState, mapMutations } from "vuex";

export default {
  name: "WorkTimer",

  data: function () {
    return {
      timer: null,
    };
  },

  computed: {
    ...mapState("hud", ["workTime"]),

    prettyTime() {
      let time = this.workTime / 60;
      let minutes = parseInt(time);
      let secondes = Math.round((time - minutes) * 60);

      if (minutes < 10) {
        minutes = "0" + minutes;
      }
      if (secondes < 10) {
        secondes = "0" + secondes;
      }

      return `${minutes}:${secondes}`;
    },
  },

  methods: {
    ...mapMutations("hud", ["startWorkTimer"]),
  },
};
</script>

<style lang="scss" scoped>
.work-timer {
  position: absolute;
  left: 1.474rem;
  bottom: 26vh;
  padding-left: 0.125rem;
  display: flex;
  align-items: center;

  div {
    position: relative;
    margin-right: 0.368rem;
    height: 1.632rem;
    width: 1.632rem;

    img {
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      &:first-child {
        height: 100%;
        width: 100%;
        opacity: 0.07;
      }

      &:last-child {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 0.895rem;
        height: 0.895rem;
      }
    }
  }

  span {
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 1.263rem;
    line-height: 100%;
    text-transform: uppercase;
    color: #ffffff;
  }
}
</style>
