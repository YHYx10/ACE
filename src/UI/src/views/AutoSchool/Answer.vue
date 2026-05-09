<template>
  <div
    v-bind:class="{ active: answer.key === this.currentAnswer, answer }"
    @click="() => answers(answer.key, questionKey)"
  >
    <div class="check-icon">
      <img
        src="/img/autoSchool/question/check.svg"
        alt=""
        v-if="answer.key === this.currentAnswer"
      />
    </div>
    <span class="text">{{ loc(answer.title) }}</span>
  </div>
</template>

<script>
import { mapGetters, mapState, mapMutations } from "vuex";

export default {
  name: "Answer",

  props: {
    answer: Object,
    questionKey: Number,
  },

  computed: {
    ...mapState("autoSchool", ["currentAnswer"]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    ...mapMutations("autoSchool", ["setAnswer"]),
    answers(key, questionKey) {
      this.setAnswer({ answer: key, question: questionKey });
      this.$emit("reset");
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.answer {
  width: 100%;
  min-height: conv(60);
  display: flex;
  align-items: center;
  padding: conv(12);
  background: linear-gradient(
    90deg,
    rgba(255, 255, 255, 0.03) 0%,
    rgba(255, 255, 255, 0) 100%
  );
  position: relative;
  outline: none;

  .check-icon {
    height: conv(35);
    width: conv(35);
    min-width: conv(35);
    background: rgba(255, 255, 255, 0.05);
    margin-right: conv(19);
    display: flex;
    justify-content: center;
    align-items: center;

    img {
      height: conv(16);
      width: auto;
    }
  }

  .text {
    font-weight: 700;
    font-size: conv(20);
    line-height: conv(24);
    color: #ffffff;
    max-width: 100%;
  }

  &:not(.active) {
    cursor: pointer;
  }

  div,
  span {
    z-index: 7;
    position: relative;
  }

  &::after {
    content: "";
    transition: 0.3s ease;
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(90deg, #301934  0%, #591b87 100%);
    z-index: 6;
    opacity: 0;
  }

  &.active {
    &::after {
      opacity: 1;
    }

    .check-icon {
      background: rgba(255, 255, 255, 0.1);
    }
  }

  &:not(:last-child) {
    margin-bottom: conv(10);
  }
}
/* .answer{
    background: #1A0424;
    border-radius: .45rem;
    display: flex;
    align-items: center;
    justify-content: flex-start;
    padding: 1.15rem;
    border: 1px solid transparent;
    transition: .2s;
    .check-icon{
      width: 1.5rem;
      min-width: 1.5rem;
      height: 1.5rem;
      min-height: 1.5rem;
      margin-right: 1.75rem;
      position: relative;
      border: .1rem solid #550163;
      box-sizing: border-box;
      border-radius: .25rem;
      display: flex;
      align-items: center;
      justify-content: center;
    }
    .text{
      font-weight: 500;
      font-size: .95rem;
      line-height: 1.1rem;
      color: #FFFFFF;
    }
    &.active{
      border: 1px solid #E700A6;
      transition: .2s;
      .check-icon{
        &:before{
          content: '';
          width: .9rem;
          height: .9rem;
          background: #DB00FF;
          box-shadow: 0 0 .8rem rgba(255, 0, 199, 0.57);
          border-radius: .15rem;
          transition: .2s;
        }
      }
    }
  } */
</style>
