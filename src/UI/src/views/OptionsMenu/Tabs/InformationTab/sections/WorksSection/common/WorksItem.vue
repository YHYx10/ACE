<template>
  <div class="item">
    <div :class="[{ active: isAdditional }, 'item__main billet-item']">
      <div class="info" @click="isAdditional ? { click: isAdditional = !isAdditional } : {}">
        <div class="info__title">{{ loc(item.title) }}</div>
        <div v-if="!item.locked" class="info__level unlocked">Available with {{ item.entryLevel }} {{ loc("mm_info_work_lvl") }}</div>
        <div v-else class="info__level locked">will be available with {{ item.entryLevel }} {{ loc("mm_info_work_lvl") }}</div>
      </div>
      <button v-if="isAdditional" class="item__btn" @click="getDirections">{{ loc("mm_info_work_rout") }}</button>
      <button v-else class="item__btn"
              :disabled="item.locked"
              @click="isAdditional = !isAdditional"
      >
      sehen
      </button>
    </div>
    <div class="item__additional" v-if="isAdditional">
      <img :src="`img/optionsMenu/informationTab/works/${item.img}.png`" class="img">
      <div class="main">
        <div class="main__desc">{{ loc(item.description) }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters} from 'vuex'

export default {
  name: 'WorksItem',

  props: {
    item: Object
  },

  data: function () {
    return {
      isAdditional: false
    }
  },
  computed: {
    ...mapGetters('localization', ['loc'])
  },

  methods: {
    getDirections: function () {
      if (this.item.locked) return;
      window.mp.triggerServer("mmenu:job:waypoint", this.item.point)
    },
    select() {

    }
  }
}
</script>

<style lang="scss" scoped>
.item {
  border: 1px solid;
  border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 50%, rgba(0, 0, 0, 0) 100%) 1;
  background: linear-gradient(90deg, rgba(255, 255, 255, 0) 10%, rgba(255, 255, 255, 0.05) 35%, rgba(12, 16, 10, 0) 70%);

  &:not(:last-child) {
    margin: 0 0 0.5rem 0;
  }

  .item__btn {
    padding: 1rem;
  }
  &__main {
    border: 0 !important;
    background: none !important;
    padding: 0.7rem 0rem 0.7rem 2rem !important;
    margin: 0 !important;

    &.active {
      cursor: pointer;

      &:before {
        left: 0rem;
        height: 100%;
      }
    }

    .info {
      display: flex;
      position: relative;
      align-items: center;
      flex: 1 1 100%;

      &__level {
        font-size: 0.85rem;
        color: rgba(255, 255, 255, 0.5);

        &.unlocked {
          color: #5CFF80;
        }
      }

      &__title {
        flex: 0 0 25%;
        font-weight: 600;
        font-size: 1.05rem;
        color: #fff;
        margin: 0 2.05rem 0 0;
      }
    }
  }

  &__additional {
    display: flex;
    flex-flow: column nowrap;

    .img {
      display: block;
      position: relative;
      height: 15rem;
      object-fit: cover;
    }

    .main {
      flex: 1 1 100%;
      padding: 0.7rem 4rem 0.9rem 1rem;

      &__desc {
        font-weight: 300;
        font-size: 0.83rem;
        line-height: 1rem;
        letter-spacing: 0.03em;
        color: rgba(255, 255, 255, 0.55);
      }
    }
  }
}
</style>
