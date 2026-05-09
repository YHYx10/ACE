<template>
  <div class="nav-menu">
    <div
      class="item"
      :class="{ active: page === item.page }"
      v-for="item in list"
      :key="item.title"
      @click="$emit('toPage', item.img)"
    >
      <div
        class="img"
        :class="item.img"
        :style="{ 'background-image': `url(/img/portOrders/${item.img}.png)` }"
      />
      <div class="title">{{ item.title }}</div>
    </div>
  </div>
</template>

<script>
export default {
  props: ['page'],
  data() {
    return {
      list: [
        {
          title: 'Home',
          img: 'main',
          page: 'Main',
        },
        {
          title: 'goods',
          img: 'products',
          page: 'Products',
        },
        {
          title: 'about Us',
          img: 'about',
          page: 'About',
        },
      ],
    }
  },
}
</script>

<style lang="scss" scoped>
.nav-menu {
  display: flex;
  justify-content: center;
  gap: 1.852vh;
  position: relative;
  z-index: 1;
  .item {
    width: 44.444vh;
    height: 13.426vh;
    overflow: hidden;
    display: flex;
    align-items: center;
    gap: 3.426vh;
    position: relative;
    background: rgba(0, 0, 0, 0.3);
    color: #ffffff;

    &::before {
      content: '';
      width: 54.352vh;
      height: 54.444vh;
      position: absolute;
      background: radial-gradient(
        50% 50% at 50% 50%,
        rgba(255, 255, 255, 0.1) 0%,
        rgba(255, 255, 255, 0) 100%
      );
    }

    &.active {
      background: linear-gradient(
        180deg,
        #301934  0%,
        #591b87 100%,
        #591b87 100%
      );
    }

    .img {
      margin-left: 7.963vh;
      width: 8.333vh;
      height: 8.333vh;
      background-size: cover;
      &::before {
        content: '';
        position: absolute;
        width: 21.574vh;
        height: 21.574vh;
        z-index: -1;
        top: 50%;
        left: 4.167vh;
        transform: translateY(-50%);
        opacity: 0.05;
        background-size: cover;
      }
      &.main::before {
        background-image: url('/img/portOrders/main.png');
      }
      &.products::before {
        background-image: url('/img/portOrders/products.png');
      }
      &.about::before {
        background-image: url('/img/portOrders/about.png');
      }
    }
    .title {
      font-weight: 700;
      font-size: 3.333vh;
      line-height: 3.056vh;
      text-transform: uppercase;
      &::before {
        content: '';
        margin-right: 1.667vh;
        width: 0.185vh;
        height: 1.944vh;
        background: #ffffff;
        border: 0.093vh solid rgba(255, 255, 255, 0.09);
      }
    }
  }
}
</style>
