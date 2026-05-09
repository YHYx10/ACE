<template>
  <div :class="[item.key, 'extra-item']" v-if="item.key === 'alert'">
    <img src="img/optionsMenu/statisticsTab/icons/alert.png" class="extra-item__img">
    <div class="extra-item__main">
      <div class="extra-item__title">{{ loc(item.textFirst) }}</div>
      <div class="extra-item__value">{{ statistics[item.keyFirst] }}</div>
    </div>
    <div class="extra-item__main">
      <div class="extra-item__title">{{ loc(item.textSecond) }}</div>
      <div class="extra-item__value">{{ statistics[item.keySecond] }}</div>
    </div>
  </div>
  <div :class="[item.key, 'extra-item']" v-else>
    <img
        v-if="vipActive"
        :src="`img/optionsMenu/statisticsTab/icons/${item.icon1}.png`"
        class="extra-item__img premium-on"
    >
    <img
        v-else
        :src="`img/optionsMenu/statisticsTab/icons/${item.icon}.png`"
        class="extra-item__img"
    >
    <div class="extra-item__main">
      <div class="extra-item__title">{{ item.title }}</div>
      <div v-if="vipActive" class="extra-item__value">on</div>
      <div v-else class="extra-item__value">off</div>
    </div>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'

export default {
  name: 'ExtraItem',

  props: {
    item: Object
  },

  computed: {
    ...mapState('optionsMenu', ['statistics']),
    ...mapGetters('localization', ['loc']),
    ...mapState('newDonateShop/prime', ['days']),
    vipActive() {
      return this.days > 0
    }
  }
}
</script>

<style lang="scss" scoped>
.extra-item {
  display: flex;
  align-items: center;
  border: 1px solid;
  padding: 1.1rem 5rem 1.1rem 2rem;
  border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 69%, rgba(0, 0, 0, 0) 91%) 1;

  &:not(:last-child) {
    margin: 0 0 0.75rem 0;
  }

  &__main {
    display: flex;
    flex-direction: column;
  }

  &__title {
    font-size: 0.7rem;
    line-height: 0.84rem;
    color: rgba(255, 255, 255, 0.55);
    letter-spacing: 0.07em;
    margin: 0 0 0.25rem 0;
  }

  &__value {
    font-size: 0.93rem;
    line-height: 1.1rem;
    font-weight: 600;
    color: #fff;
    letter-spacing: 0.03em;
  }

  &__img {
    width: 3.16rem;
    margin: 0 2rem 0 0;
    transition: transform 0.4s ease;
  }

  &:hover &__img {
    transform: scale(1.08);
  }

  &.premium &__img {
    filter: drop-shadow(0px 0px 25px rgba(200, 200, 200, 0.6));
  }
  &.premium &__img.premium-on {
    filter: drop-shadow(0px 0px 25px rgba(204, 255, 94, 0.6));
  }
  &.premium &__title {
    font-size: 0.84rem;
    line-height: 1rem;
  }
  &.premium &__value {
    font-size: 1.33rem;
    line-height: 1.6rem;
  }

  &.alert &__img {
    filter: drop-shadow(0px 0px 25px rgba(71, 44, 132, 0.6));
  }
  &.alert &__main:last-child {
    margin-left: 2rem;
  }
}
</style>
