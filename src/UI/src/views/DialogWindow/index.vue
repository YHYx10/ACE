<template>
  <div class="dialog-window">
    <div class="dialog-window__display">
      <div class="dialog-window__display-info">
        <div class="dialog-window__display-info__name">
          {{ loc(dialogData.name) }}
        </div>
        <div class="dialog-window__display-info__desc">
          {{ loc(dialogData.desc) }}
        </div>
      </div>
      <div class="dialog-window__display-text">{{ loc(dialogData.text) }}</div>
    </div>
    <div class="dialog-window__answers">
      <div
        :class="[
          { active: answer.id === currentAnswer },
          'dialog-window__answers-info',
        ]"
        v-for="answer in dialogData.answers"
        :key="answer.key"
        @click="setCurrentAnswer(answer.id)"
      >
        {{ loc(answer.text) }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";
export default {
  name: "DialogWindow",

  data: function () {
    return {
      currentAnswer: null,
    };
  },

  computed: {
    ...mapState("dialogWindow", ["dialogData"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    setCurrentAnswer: function (id) {
      this.currentAnswer = id;
      window.mp.trigger("dialogWindow:currentAnswer", id);
    },
  },
};
</script>

<style lang="scss" scoped>
.dialog-window {
  position: absolute;
  left: 4.211rem;
  bottom: 1.789rem;
  display: flex;
  flex-direction: column;
  width: 34.211rem;
  font-family: "Akrobat";

  &::before {
    pointer-events: none;
    content: "";
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: radial-gradient(
      50% 99.27% at 50% 50%,
      rgba(0, 0, 0, 0) 0%,
      #000000 100%
    );
    opacity: 0.8;
    z-index: -1;
  }

  &__display {
    &-info {
      margin-left: 2.158rem;

      div {
        text-transform: uppercase;
        font-weight: 700;
      }

      &__name {
        font-size: 1.684rem;
        line-height: 2rem;
        color: #ffffff;
      }

      &__desc {
        font-size: 1.053rem;
        line-height: 1.263rem;
        color: rgba(255, 255, 255, 0.55);
      }
    }

    &-text {
      margin: 1.368rem 0 1.053rem;
      width: 100%;
      background: rgba(12, 14, 20, 0.5);
      padding: 1.184rem 2.474rem 1.184rem 2.105rem;
      font-weight: 700;
      font-size: 1.474rem;
      line-height: 1.789rem;
      color: #ffffff;
    }
  }

  &__answers {
    display: grid;
    grid-template-columns: 1fr 1fr;
    column-gap: 0.526rem;
    row-gap: 0.526rem;

    &-info {
      width: 100%;
      height: 3.947rem;
      display: flex;
      align-items: center;
      justify-content: center;
      background: rgba(12, 14, 20, 0.8);
      font-weight: 700;
      font-size: 0.842rem;
      line-height: 1rem;
      text-transform: uppercase;
      color: #ffffff;
      transition: 0.5s ease;
      cursor: pointer;
      text-align: center;
      padding: 0 1rem;

      &:hover {
        box-shadow: inset 0px 0px 7.5rem #301934 ;
        filter: drop-shadow(0px 0px 15px rgba(71, 44, 132, 0.5));
      }
    }
  }
}
</style>
