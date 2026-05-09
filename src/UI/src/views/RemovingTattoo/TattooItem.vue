<template>
  <div class="item" :class="{active: selected}" @click="$emit('click')">
    <div class="title">
      {{ title }}
    </div>
    <img
      :src="img"
      :alt="title"
      class="img"
    />
    <div class="corner corner-a" />
    <div class="corner corner-b" />
    <div class="corner corner-c" />
    <div class="corner corner-d" />
  </div>
</template>

<script>
export default {
  props: ['title', 'img', 'selected'],
}
</script>

<style lang="scss" scoped>

.item {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 15.741vh;
  height: 15.648vh;
  position: relative;
  overflow: hidden;
  background: rgba(255, 255, 255, 0.05);
  outline: 0.093vh solid rgba(255, 255, 255, 0.09);
  outline-offset: -0.093vh;
  img {
    position: absolute;
    top: 58%;
    left: 50%;
    transform: translate(-50%, -50%);
    height: 8.333vh;
  }
  .title {
    margin-top: 0.741vh;
    color: #ffffff;
    font-weight: 700;
    font-size: 1.481vh;
    line-height: 1.759vh;
    text-transform: uppercase;
  }
  .corner {
    border-radius: 100%;
    position: absolute;
    width: 0.278vh;
    height: 0.278vh;
    background: #ffffff;
    box-shadow: 0vh 0vh 1.019vh #ffffff;
  }
  
  &.active {
    background: rgba(255, 255, 255, 0.13);
    outline: 0.093vh solid rgba(71, 44, 132, 0.3);
    .corner {
      background: #301934 ;
    }
  }
  .corner-a {
    left: 0;
    top: 0;
    transform: translate(-50%, -50%);
  }
  .corner-b {
    right: 0;
    top: 0;
    transform: translate(50%, -50%);
  }
  .corner-c {
    right: 0;
    bottom: 0;
    transform: translate(50%, 50%);
  }
  .corner-d {
    left: 0;
    bottom: 0;
    transform: translate(-50%, 50%);
  }
}
</style>
