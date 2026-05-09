<template>
  <div :class="[{ active: isSelect }, 'main__item']" @click="toggleIsSelect">
    <span class="item__title">{{ loc(item.title) }}</span>
    <div class="item__value">
      <span class="value__text">{{ item.currentValue }}</span>
      <svg :class="[{ active: isSelect }, 'value__icon']" xmlns="http://www.w3.org/2000/svg" width="14" height="8" fill="none" viewBox="0 0 14 8">
        <path fill="#301934 " fill-rule="evenodd" d="M.646.646a.5.5 0 0 1 .708 0L7 6.293 12.646.646a.501.501 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708Z" clip-rule="evenodd"/>
      </svg>
    </div>
    <transition name="slide-fade">
      <div class="item__select" v-if="isSelect && item.id === 0 && currentPlayers == 2 && (currentMode === 'Gun fight' || currentMode === 'Team fight' || currentMode === 'Death match')">
        <span
          class="select__item"
          v-for="(value, index) in item.values[2]"
          :key="index"
          @click="setCurrentValue({ id: item.id, value })"
        >{{ value }}</span>
      </div>
      <div class="item__select" v-else-if="isSelect && item.id === 0 && (currentMode === 'Gun fight' || currentMode === 'Death match')">
        <span
          class="select__item"
          v-for="(value, index) in item.values[0]"
          :key="index"
          @click="setCurrentValue({ id: item.id, value })"
        >{{ value }}</span>
      </div>
      <div class="item__select" v-else-if="isSelect && item.id === 0 && currentMode === 'Team battle'">
        <span
          class="select__item"
          v-for="(value, index) in item.values[1]"
          :key="index"
          @click="setCurrentValue({ id: item.id, value })"
        >{{ value }}</span>
      </div>
      <div class="item__select" v-else-if="isSelect">
        <span
          class="select__item"
          v-for="(value, index) in item.values"
          :key="index"
          @click="setCurrentValue({ id: item.id, value })"
        >{{ value }}</span>
      </div>
    </transition>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'

export default {
  name: 'FiltersItems',

  props: [
    'item',
    'currentMode',
    'currentPlayers'
  ],

  data: function () {
    return {
      isSelect: false
    }
  },

  computed: {
    ...mapState('arenaMenu', ['filters']),

    ...mapGetters('localization', ['loc'])
  },

  methods: {
    ...mapMutations('arenaMenu', ['setFiltersCurrentValue']),

    ...mapMutations('sounds', ['play']),

    setCurrentValue: function (value) {
      const mapsArray = this.filters[0].values

      if ((this.item.id == 1 && value.value == 2) && (this.currentMode === 'Team fight' || this.currentMode === 'Gun game' || this.currentMode === 'Death match')) {
        this.setFiltersCurrentValue({ id:0, value: mapsArray[2][0] })
      } else if ((this.item.id == 4 && value.value === 'Team fight' && this.currentPlayers != 2) || ((this.item.id == 1 && value.value > 2) && this.currentMode === 'Team fight')) {
        this.setFiltersCurrentValue({ id:0, value: mapsArray[1][0] })
      } else if ((this.item.id == 4 && value.value === 'Gun game' && this.currentPlayers != 2) || ((this.item.id == 1 && value.value > 2) && this.currentMode === 'Gun game')) {
        this.setFiltersCurrentValue({ id:0, value: mapsArray[0][0] })
      }
      else if ((this.item.id == 4 && value.value === 'Death match' && this.currentPlayers != 2) || ((this.item.id == 1 && value.value > 2) && this.currentMode === 'Death match')) {
          this.setFiltersCurrentValue({ id:0, value: mapsArray[0][0] })
      }

      this.play({ name: 'switch', volume: 0.1 })
      this.setFiltersCurrentValue(value)
    },

    setIsSelect: function (value) {
      this.isSelect = value
    },

    toggleIsSelect: function () {
      this.isSelect = !this.isSelect
      this.play({ name: 'switch', volume: 0.1 })
    }
  }
}
</script>

<style lang="scss" scoped>
.main__item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  font-size: 1.15rem;
  letter-spacing: 0.05rem;
  padding: 0.5rem;
  padding-top: 1.1rem;
  padding-bottom: 1rem;
  border-bottom: 0.05rem solid;
  border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1;
  background: linear-gradient(-90deg, rgba(255, 255, 255, 0.01) 0%, rgba(255, 255, 255, 0) 120% );
  width: 16.95rem;
  height: 1.95rem;
  position: relative;
  //transition: .15s all ease;
  font-size: .9rem;
  &.active {
    // background-color: rgba(255, 255, 255, 0.4);
    // border-bottom-left-radius: 0;
    // border-bottom-right-radius: 0;
  }
  &:hover {
    //background: rgba(255, 255, 255, 0.5);
  }
  &:first-child {
    margin: 0;
  }
  .item__title {
    color: #FFF;
    text-transform: uppercase;
    font-weight: 700;
    font-size: 0.8rem;
    line-height: 0.95rem;
  }
  .item__value {
    display: flex;
    align-items: center;
    width: 9.25rem;
    .value__text {
      width: 6.25rem;
      text-align: left;
      padding-left: 2rem;
      font-weight: bold;
      color: #fff;
      text-transform: uppercase;
      font-weight: 700;
      font-size: 0.8rem;
      line-height: 0.95rem;
      white-space: nowrap;
    }
    .value__icon {
      width: 1rem;
      margin: 0 0 0 auto;
      transition: .2s all ease;
      &.active {
        transform: rotate(180deg);
      }
    }
  }
  .item__select {
    position: absolute;
    top: 100%;
    right: 0;
    width: 100%;
    width: 9.75rem;
    display: flex;
    flex-direction: column;
    z-index: 1;
    font-weight: bold;
    text-transform: uppercase;
    background: linear-gradient(90deg, #0D0D0D 0%, #020202 100%);
    color: #FFF;
    overflow: hidden;
    .select__item {
      display: flex;
      align-items: center;
      justify-content: flex-start;
      text-align: center;
      transition: all .15s ease;
      height: 1.45rem;
      padding-left: 2rem;
      padding-right: 1.3rem;
      font-weight: 700;
      font-size: 0.8rem;
      line-height: 0.95rem;
      &:hover {
        background: linear-gradient(90deg, rgba(71, 44, 132, 0.4) 0%, rgba(6, 6, 6, 0) 100%);
      }
    }
  }
}
.slide-fade-enter-active, .slide-fade-leave-active {
  transition: all .3s ease;
}

.slide-fade-enter, .slide-fade-leave-to {
  transform: translateY(-1rem);
  opacity: 0;
}
</style>
