<template>
  <div class="form__leather">
    <div class="form__box">
      <h3 v-text="text"></h3>
    </div>

    <div class="form__value">
      <Slider
        div
        class="customiztion-nav-range"
        :min="itemData.min"
        :max="itemData.max"
        :interval="itemData.step"
        v-model="itemData.value"
        @change="onChange"
      />
      <div class="customiztion-nav-range_value">{{ itemData.value }}</div>
    </div>
  </div>
</template>

<script>
import Slider from "vue-slider-component";

export default {
  props: {
    itemData: Object,
    text: String,
  },
  methods: {
    onChange() {
      window.mp.trigger(
        "customization:update",
        this.itemData.tag,
        this.itemData.value
      );
    },
  },
  components: {
    Slider,
  },
};
</script>

<style lang="scss">
.customiztion-nav-range {
  display: flex;
  align-items: center;
  justify-content: space-between;
  position: relative;
  margin: 0.25rem 0;
  &_value {
    color: #b6d300;
  }
  .vue-slider {
    width: 14rem;
    &-disabled {
      opacity: 0.5;
      cursor: not-allowed;
    }
    &-rail {
      margin: auto;
      background: none;
      background-color: rgba(#fff, 0.1);
      width: 13rem;
    }
    &-process {
      background: none;
    }
    &-mark {
      z-index: 4;
      &:first-child .vue-slider-mark-step,
      .vue-slider-mark:last-child .vue-slider-mark-step {
        display: none;
      }
      &-step {
        width: 100%;
        height: 100%;
        border-radius: 50%;
      }
    }
    &-dot {
      &-handle {
        cursor: pointer;
        width: 0.8rem;
        height: 0.8rem;
        border-radius: 50%;
        margin-top: -0.05rem;
        background: #b6d300;
        border: none;
        box-shadow: none;
        box-sizing: border-box;
      }
      &-tooltip {
        &-inner {
          opacity: 0;
        }
        &-wrapper {
          opacity: 0;
          transition: all 0.3s;
        }
        &-wrapper-show {
          opacity: 1;
        }
      }
    }
  }
}
</style>