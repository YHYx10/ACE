<template>
  <div :class="[item.unicName, 'list-item']">
    <div class="img" :class="[item.unicName]" />
    <div class="col">
      <div class="title">
        {{ item.name }}
      </div>
      <div class="level">
        {{ item.typeLevel }}
      </div>
      <div class="progress-bar">
        <div
          v-for="(col, index) in item.maxLevel"
          :key="index"
          :class="[{ active: index < item.currentLevel }, 'item']"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ListItem',

  props: {
    item: Object,
  },
}
</script>

<style lang="scss" scoped>
.list-item {
  width: 282px;
  height: 159px;
  background: rgba(0, 0, 0, 0.2);
  display: flex;
  align-items: center;
  transition: 0.3s ease;
  &:hover {
    background: rgba(0, 0, 0, 0.3);
  }
  .img {
    &.engine {
      width: 125px;
      height: 123px;
      transform: translateX(-23px);
      background-image: url(/img/technicalCertificate/engine.png);
    }

    &.turbine {
      width: 122px;
      height: 122px;
      transform: translateX(-22px);
      background-image: url(/img/technicalCertificate/turbine.png);
    }

    &.transmission {
      width: 121px;
      height: 82px;
      transform: translateX(-20px);
      background-image: url(/img/technicalCertificate/transmission.png);
    }

    &.brakes {
      width: 126px;
      height: 114px;
      transform: translateX(-26px);
      background-image: url(/img/technicalCertificate/brakes.png);
    }
  }
  .col {
    font-weight: 700;
    text-transform: uppercase;
    display: flex;
    flex-direction: column;
    justify-content: center;
    .title {
      font-size: 24px;
      line-height: 29px;
      color: #ffffff;
    }
    .level {
      font-size: 20px;
      line-height: 24px;
      color: rgba(255, 255, 255, 0.55);
    }

    .progress-bar {
      margin-top: 20px;
      display: flex;
      justify-content: space-between;
      gap: 9px;
      .item {
        width: 21px;
        height: 30px;
        border-radius: 20px;
        background: rgba(255, 255, 255, 0.05);
        &.active {
          background: #301934 ;
          box-shadow: 0px 0px 7px rgba(71, 44, 132, 0.5);
        }
      }
    }
  }
}
</style>
