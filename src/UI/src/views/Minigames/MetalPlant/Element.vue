<template>
  <div
    class="mplant-element"
    :style="{
      width: `${2.5 * element.scale}rem`,
      height: `${2.5 * element.scale}rem`,
    }"
  >
    <img
      :src="element.img"
      alt="img"
      class="mplant-element_img"
      :class="{ red: red && !temp }"
    />
    <div
      class="mplant-element_bg"
      :style="{
        background: red ? 'rgba(71, 44, 132, 0.1)' : 'rgba(160, 255, 152, 0.1)',
      }"
    ></div>
    <!-- <img :src="element.bg" alt="bg" class="mplant-element_bg" /> -->
  </div>
</template>

<script>
export default {
  props: ["element", "red", "temp"],
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant-element {
  position: relative;
  display: flex;
  align-items: center;
  justify-content: center;

  img, div {
    position: absolute;
    pointer-events: none;
  }

  &_bg {
    width: 100%;
    height: 100%;
    border-radius: 50%;
  }

  &_img {
    width: 50%;
    height: 50%;

    &.red {
      filter: brightness(0) saturate(100%) invert(16%) sepia(85%)
        saturate(3971%) hue-rotate(348deg) brightness(89%) contrast(104%);
    }

    &:not(.red) {
      &::after {
        content: "";
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        border-radius: 50%;
        width: 1px;
        height: 1px;
        box-shadow: 0px 0px conv(10) rgba(160, 255, 152, 0.4);
      }
    }
  }
}
</style>