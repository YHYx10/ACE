<template>
  <div class="actions-item" @click="someEvent">
    <div class="actions-item-title">{{ loc(item.title) }}</div>
    <div class="actions-item-icon">
      <img :src="`img/optionsMenu/statisticsTab/transportModal/${item.icon}.png`">
    </div>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'ActionsItem',

  props: {
    item: Object,
    currentCarId: Number
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    someEvent: function () {
      this.$emit("onClick", this.item.event);
    }
  }
}
</script>

<style lang="scss" scoped>
.actions-item {
  position: relative;
  height: 14.5rem;
  padding: 1rem 1.4rem;
  border: 1px solid rgba(255, 255, 255, 0.1);
  background: url("/img/optionsMenu/statisticsTab/transportModal/bg.png") bottom right / contain no-repeat;

  &-title {
    position: relative;
    color: #fff;
    font-size: 1.2rem;
    font-weight: 600;

    &:before {
      content: '';
      position: absolute;
      height: 1.44rem;
      top: 50%;
      left: -0.7rem;
      transform: translateY(-50%);
      color: #fff;
      background: #fff;
      border: 1px solid #fff;
      box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
    }
  }

  &-icon {
    position: absolute;
    cursor: pointer;
    width: 100%;
    height: 100%;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    padding: 25% 30% 15%;

    &>img {
      width: 100%;
      height: 100%;
      object-fit: contain;
    }

    &:hover {
      background: rgba(255,255,255,0.1);
    }
  }
}
</style>
