<template>
  <div :class="[{ active: item.id === currentId }, 'billet-item']">
    <div class="info">
      <div class="title">{{ loc(item.name) }}</div>
      <div class="numbers">{{ loc('mmain:stats:veh:number') }}: {{ item.numbers }}</div>
    </div>
    <button class="item__btn" @click="$emit('set-current-car', item)">choose</button>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'ListItem',

  props: {
    item: Object,
    currentId: Number
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  }
}
</script>

<style lang="scss" scoped>
.billet-item {
  display: flex;
  align-items: center;
  padding-right: 1.5rem !important;

  &.active {
    &:before {
      left: 0;
      height: 100%;
    }
    .item__btn {
      box-shadow: inset 0px 0px 7.5rem #301934 ;
      filter: drop-shadow(0px 0px 15px rgba(71, 44, 132, 0.5));
    }
  }

  .info {
    display: flex;
    align-items: center;

    .title {
      font-size: 1rem;
      line-height: 1.2rem;
      width: 5rem;
      margin-right: 1rem;
    }

    .numbers {
      font-size: 0.8rem;
      line-height: 0.9rem;
      color: #5CFF80;
    }
  }
}
</style>
