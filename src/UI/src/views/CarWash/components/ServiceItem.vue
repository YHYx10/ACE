<template>
  <div class="service-item">
    <img :src="`/img/carWash/services/${item.img}`" alt="img" />
    <div class="about">
      <div class="title">{{ loc(item.text) }}</div>
      <div class="price">$ {{ item.cost.toLocaleString('ru') }}</div>
      <button
        :class="{ active: basketList.includes(item) }"
        @click="$emit('interactionWidthBasketList', item)"
      >
        {{ basketList.includes(item) ? 'chosen' : 'choose' }}
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'ServiceItem',
  props: {
    item: Object,
    basketList: Array,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
}
</script>

<style lang="scss" scoped>
.service-item {
  display: flex;
  flex-direction: column;
  width: 39.537vh;

  div,
  button {
    font-family: 'Akrobat';
    font-style: normal;
    text-transform: uppercase;
    font-weight: 700;
    color: #ffffff;
  }

  img {
    width: 100%;
    height: 100%;
  }

  .about {
    display: flex;
    flex-direction: column;
    align-items: center;
    background: rgba(255, 255, 255, 0.03);
    .title {
      margin-top: 2.685vh;
      font-size: 2.222vh;
      line-height: 2.685vh;
      color: #ffcc47;
    }

    .price {
      margin-top: 1.204vh;
      font-size: 4.444vh;
      line-height: 5.37vh;
      display: flex;
      align-items: center;
    }
    button {
      margin-top: 2.685vh;
      width: 100%;
      height: 6.944vh;
      background: rgba(255, 255, 255, 0.15);
      display: flex;
      align-items: center;
      justify-content: center;
      font-size: 2.222vh;
      line-height: 2.685vh;

      &:hover {
        background: rgba(255, 255, 255, 0.25);
      }
      &.active {
        background: linear-gradient(90deg, #301934  0%, #591b87 100%);
        &:hover {
          background: linear-gradient(90deg, #301934  0%, #591b87 100%);
        }
      }
    }
  }
}
</style>
