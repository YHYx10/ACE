<template>
  <div
    class="selector"
    :class="{ open: showList }"
    @focusout="showList = false"
    tabindex="0"
  >
    <div class="selected" @click="showList = !showList">
      <div>{{ loc(list[currentIndex]) }}</div>
      <svg :style="showList ? 'transform: rotate(180deg)' : ''" fill="none" viewBox="0 0 17 10">
        <path fill="#FF7D7D" d="M0 1.415 8.485 9.9l8.485-8.485L15.556 0 8.485 7.072 1.414 0 0 1.415Z"/>
      </svg>
    </div>
    <div v-show="showList" class="list">
      <div
        v-for="(item, index) in list"
        :key="item.id"
        :class="[{ active: index === currentIndex }, 'item']"
        @click="select(index)"
      >
        {{ loc(item) }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  props: {
    list: Array,
    currentIndex: Number
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  data() {
    return {
      showList: false,
    }
  },
  methods: {
    select(index) {
      this.showList = false
      this.$emit('onSelect', index)
    }
  }
}
</script>

<style lang="scss" scoped>
.selector {
  width: 18.241vh;
  font-family: 'Akrobat';
  font-weight: 500;
  font-size: 1.389vh;
  line-height: 1.667vh;
  text-transform: uppercase;
  display: flex;
  flex-direction: column;
  position: relative;
  border: 0.093vh solid rgba(255, 255, 255, 0.09);
  box-sizing: border-box;
  background-color: rgb(15, 15, 15);
  &.open {
    border-bottom: 0.093vh solid transparent;
  }
  .selected {
    color: #ffffff;
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 3.333vh;
    margin-left: 1.667vh;
    svg {
      width: 1.574vh;
      height: 0.926vh;
      margin-right: 1.667vh;
    }
  }
  .list {
    position: absolute;
    z-index: 99;
    bottom: 0;
    left: -0.093vh;
    width: calc(100% + 0.185vh) ;
    transform: translate(0, 100%);
    border: 0.093vh solid rgba(255, 255, 255, 0.09);
    box-sizing: border-box;
    border-top: 0vh;
    background-color: rgb(15, 15, 15);
    .item {
      display: flex;
      align-items: center;
      height: 3.333vh;
      color: rgba(255, 255, 255, 0.5);
      margin-left: 1.667vh;
      &:hover {
        color: rgba(255, 255, 255, 0.7);
      }
      &.active {
        color: #ff7d7d;
        font-weight: 700;
      }
    }
  }
}
</style>
