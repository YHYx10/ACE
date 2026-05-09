<template>
  <div class="product-item" @click="$emit('click', item)">
    <img src="/img/portOrders/box.png" alt="img" />
    <div class="info">
      <div class="title">{{ item.name }}</div>
      <div class="price-title">price</div>
      <div class="price-value">${{ item.cost }}</div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    item: Object,
  },
}
</script>

<style lang="scss" scoped>
.product-item {
  width: 31.019vh;
  height: 27.87vh;
  display: flex;
  flex-direction: column;
  position: relative;
  background: rgba(255, 255, 255, 0.03);
  &:hover {
    background: rgba(255, 255, 255, 0.1);
  }
  img {
    position: absolute;
    height: 10.259vh;
    top: 4.074vh;
    left: 50%;
    transform: translateX(-50%);
  }
  .info {
    width: 100%;
    position: absolute;
    bottom: 0;
    text-transform: uppercase;
    font-weight: 700;
    display: flex;
    flex-direction: column;
    align-items: center;
    .title {
      font-size: 2.963vh;
      line-height: 3.519vh;
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: 1.759vh;
    }
    .price-title {
      color: rgba(255, 255, 255, 0.55);
      margin-bottom: 0.093vh;
      font-size: 1.296vh;
      line-height: 1.574vh;
    }
    .price-value {
      color: #a0ff98;
      margin-bottom: 1.574vh;
      font-size: 2.963vh;
      line-height: 3.519vh;
    }
  }
}
</style>
