<template>
  <div class="title-item" :class="{ margin: margin }">
    <div class="text">{{ text || `The name of the organization` }}</div>
  </div>
</template>

<script>
export default {
  props: {
    text: String,
    margin: {
      type: Boolean,
      default: true,
    },
  },
}
</script>

<style lang="scss" scoped>
.title-item {
  width: 100%;
  height: 2.778vh;
  position: relative;
  .text {
    font-weight: 700;
    font-size: 2.222vh;
    width: 60%;
    text-transform: uppercase;
  }
  &::before {
    content: '';
    position: absolute;
    width: calc(100% - 0.185vh);
    height: 0.185vh;
    top: 50%;
    transform: translate(0vh, -0vh);

    background: linear-gradient(
      90deg,
      rgba(255, 255, 255, 0) 0%,
      rgba(255, 255, 255, 0) 50%,
      rgba(255, 255, 255, 0.2) 100%
    );
  }
  &.margin {
    .text {
      margin-left: 3.519vh;
    }
    &::before {
      background: linear-gradient(
        90deg,
        rgba(255, 255, 255, 0.2) 0%,
        rgba(255, 255, 255, 0) 6%,
        rgba(255, 255, 255, 0) 50%,
        rgba(255, 255, 255, 0.2) 100%
      );
    }
  }
}
</style>
