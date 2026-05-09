<template>
  <div class="control-page">
    <div class="content">
      <LeftColumn />
      <CenterColumn />
      <RightColumn />
    </div>
  </div>
</template>

<script>
import CenterColumn from './CenterColumn.vue'
import LeftColumn from './LeftColumn.vue'
import RightColumn from './RightColumn.vue'

export default {
  name: 'ControlPage',

  components: {
    LeftColumn,
    CenterColumn,
    RightColumn,
  },
}
</script>

<style lang="scss" scoped>
.control-page {
  display: flex;
  justify-content: center;
  width: 100%;
  div,
  span,
  button,
  input {
    font-family: 'Akrobat';
    font-style: normal;
    color: #ffffff;
  }
  .content {
    margin-top: 5.556vh;
    display: flex;
    height: 75.185vh;
  }

  .title {
    width: 100%;
    height: 2.778vh;
    position: relative;
    transform: translate(0, -50%);
    .text {
      margin-left: 3.519vh;
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
        rgba(255, 255, 255, 0.2) 0%,
        rgba(255, 255, 255, 0) 6%,
        rgba(255, 255, 255, 0) 50%,
        rgba(255, 255, 255, 0.2) 100%
      );
    }
  }
  .col-left {
    width: 41.944vh;
    height: 100%;
    position: relative;
    .fraction-name {
      background: rgba(255, 255, 255, 0.04);
      border: 0.093vh solid rgba(255, 255, 255, 0.09);
      font-weight: 700;
      font-size: 2.963vh;
      line-height: 3.519vh;
      text-transform: uppercase;
      padding: 0 3.519vh;
      display: flex;
      align-items: center;
      width: 38.519vh;
      height: 7.87vh;
      font-weight: 700;
      font-size: 2.963vh;
      line-height: 3.519vh;
      text-transform: uppercase;
    }
    &::after {
      content: '';
      position: absolute;
      top: 0;
      right: 0;
      width: 0.185vh;
      height: 100%;
      background: linear-gradient(
        180deg,
        rgba(255, 255, 255, 0.2) 70%,
        rgba(255, 255, 255, 0) 100%
      );
    }
  }
  .col-center {
    width: 44.537vh;
    height: 100%;
    position: relative;
    &::after {
      content: '';
      position: absolute;
      top: 0;
      right: 0;
      width: 0.185vh;
      height: 100%;
      background: linear-gradient(
        180deg,
        rgba(255, 255, 255, 0.2) 70%,
        rgba(255, 255, 255, 0) 100%
      );
    }
  }
  .col-right {
    width: 54.444vh;
    height: 100%;
  }
}
</style>
