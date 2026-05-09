<template>
  <div class="value-switches" :class="{'short-style': isShortStyle}">
    <div class="wrap">
      <button @click="slide(index - 1)">
        <svg fill="none" viewBox="0 0 10 18">
          <path
            stroke="inherit"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M9 1 1 9l8 8"
          />
        </svg>
      </button>
      <slot></slot>
      <div v-show="!isShortStyle">
        <div class="title">
          {{ title }}
        </div>
        <div class="value">
          {{ list[index] }}
        </div>
      </div>
      <button @click="slide(index + 1)">
        <svg style="transform: scale(-1)" fill="none" viewBox="0 0 10 18">
          <path
            stroke="inherit"
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M9 1 1 9l8 8"
          />
        </svg>
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  name: 'ValueSelector',

  props: {
    title: String,
    /** type: string[] */
    list: Array,
    index: Number,
    isShortStyle: {
      type: Boolean,
      default: false
    }
  },

  data: function() {
    return {
      value: this.index,
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    slide(newIndex) {
      if (newIndex < 0) this.value = this.list.length - 1
      else if (this.list.length - 1 < newIndex) this.value = 0
      else this.value = newIndex
      this.$emit('input', this.value)
    },
  },
}
</script>

<style lang="scss" scoped>
.value-switches {
  width: 100%;
  height: 6.11vh;
  display: flex;
  align-items: center;
  position: relative;
  border-radius: 0.260417vw;
  background: radial-gradient(
    96.54% 3386.79% at 100% 50%,
    rgba(0, 0, 0, 0) 0%,
    rgba(35, 35, 35, 0.6) 100%
  );
  

  &::before {
    content: '';
    position: absolute;
    inset: 0;
    border-radius: 0.46vh;
    border: 0.104vw solid transparent;
    background: linear-gradient(90deg, rgba(0, 0, 0, 0.35), rgba(14, 15, 20, 0))
      border-box;
    -webkit-mask-composite: xor;
    mask-composite: exclude;
    -webkit-mask: linear-gradient(rgb(255, 255, 255) 0 0) padding-box,
      linear-gradient(#fff 0 0);
  }

  .wrap {
    display: flex;
    width: 100%;
    position: relative;
    justify-content: space-between;
    align-items: center;

    div {
      text-align: center;
      font-family: 'Montserrat';
      font-style: normal;
      color: #ffffff;
      display: flex;
      flex-direction: column;
      justify-content: center;
      gap: 0.74vh;
      .title {
        font-weight: 500;
        font-size: 1.3vh;
        line-height: 1.57vh;
      }
      .value {
        font-weight: 700;
        font-size: 1.67vh;
        line-height: 2.037vh;
      }
    }

    button {
      width: 3.7vh;
      height: 3.7vh;
      display: flex;
      justify-content: center;
      align-items: center;
      background: transparent;
      outline: 0.052vw solid #fff;
      border-radius: 0.313vw;
      outline-offset: -0.1vw;
      svg {
        width: 0.92vh;
        stroke: #fff;
      }
      &:hover {
        background: #fff;
        svg {
          stroke: #000;
        }
      }
      &:nth-child(1) {
        margin-left: 0.78125vw;
      }
      &:nth-child(2) {
        margin-right: 0.78125vw;
      }
    }
  }

  &.short-style {
    background: transparent;
    .wrap button {
      margin: 0;
    }
  }
}
</style>
