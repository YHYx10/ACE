<template>
  <div class="info-item">
    <div class="statistics-tab__left-icon info-item-icon">
      <img :src="`img/optionsMenu/statisticsTab/icons/${item.icon}.svg`">
    </div>
    <div v-if="item.key === 'maritalStatus'" class="info-item__main">
      <div class="value" v-if="statistics[item.key].secondHalf">
        <div class="statistics-tab__left-title" v-if="statistics[item.key].sex === 0">{{ loc(item.title3) }}</div>
        <div class="statistics-tab__left-title" v-else-if="statistics[item.key].sex === 1">{{ loc(item.title2) }}</div>
        <div class="statistics-tab__left-value">{{ statistics[item.key].secondHalf }}</div>
      </div>
      <div class="value" v-else>
        <div class="statistics-tab__left-title">{{ loc(item.title1) }}</div>
        <div class="statistics-tab__left-value" v-if="statistics[item.key].sex === 0">{{ loc('mmain:stats:info:freef') }}</div>
        <div class="statistics-tab__left-value" v-else>{{ loc('mmain:stats:info:freem') }}</div>
      </div>
    </div>
    <div v-else class="info-item__main">
      <div class="statistics-tab__left-title">{{ loc(item.title) }}</div>
      <div class="statistics-tab__left-value">{{ item.key == "smart" ? configuration.Number : loc(statistics[item.key]) }}</div>
    </div>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'

export default {
  name: 'InfoItem',

  props: {
    item: Object
  },

  computed: {
    ...mapState('optionsMenu', ['statistics']),
    ...mapGetters('localization', ['loc']),
    ...mapState('smartphone', ['configuration'])
  }
}
</script>

<style lang="scss" scoped>
.info-item {
  display: flex;
  position: relative;
  margin-top: 1rem;
  border: 1px solid;
  border-image: linear-gradient(90deg, rgba(255,255,255,0.1) 69%, rgba(0,0,0,0) 91%) 1;
  padding: 1.05rem 1.5rem;

  &:hover &-icon:before {
    left: -1.5rem;
  }

  &-icon {
    margin: 0.25rem 0 0 0;

    & img {
      filter: invert(16%) sepia(71%) saturate(643%) hue-rotate(66deg) brightness(96%) contrast(90%) drop-shadow(0px 0px 33.4545px rgba(92, 255, 128, 0.25));
      width: 100%;
      height: 100%;
      object-fit: contain;
    }

    &:before {
      content: '';
      position: absolute;
      left: -0.75rem;
      top: 0;
      height: 100%;
      border: 1px solid #fff;
      transition: left 0.2s ease;
    }
  }

  &__main {
    display: flex;
    flex-direction: column;
    margin: 0 0 0 1rem;
    text-transform: uppercase;
    letter-spacing: 0.03em;
  }
}
</style>
