<template>
  <div class="nav-categories">
    <div v-if="isFirstNesting" class="title">Select the category</div>
    <div @click="$emit('back')" v-else class="btn-back">
      <div class="arrow">
        <img src="/img/carTunningMenu/arrow-back.svg" alt="" />
      </div>
      <div class="text">Back</div>
    </div>

    <div class="list">
      <div
        :class="{ active: selected === key && !isFirstNesting }"
        @click="$emit('select', item, key)"
        v-for="(item, key) of list"
        :key="item.key"
        class="nav-item"
      >
        <img
          :src="`/img/carTunningMenu/${getFolder}/${getSubFolder}${item.key}.png`"
          :alt="item.key"
        />
        <div class="name">{{ loc(item.title) }}</div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'

export default {
  props: {
    list: Array,
    isFirstNesting: Boolean,
    selected: Number,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('carTunningMenu', ['secondTabData',]),
    getFolder(){
      return this.isFirstNesting ? 'firstTab' : 'secondTab'
    },
    getSubFolder(){
      return this.isFirstNesting ? '' : this.secondTabData.parent + '/'
    }
  },
}
</script>

<style lang="scss" scoped>
.nav-categories {
  color: white;
  font-family: 'Akrobat';
  font-weight: 700;
  .title {
    font-size: 2.593vh;
    line-height: 4.352vh;
    text-transform: uppercase;
  }
  .btn-back {
    width: 13.519vh;
    height: 4.352vh;
    display: flex;
    background: rgba(7, 7, 7, 0.2);
    &:hover {
      .text {
        background: rgba(7, 7, 7, 0.2);
      }
    }
    .arrow {
      width: 4.352vh;
      height: 4.352vh;
      display: flex;
      justify-content: center;
      align-items: center;
      background: rgba(7, 7, 7, 0.2);
      svg {
        width: 2.5vh;
        height: 1.296vh;
      }
    }
    .text {
      display: flex;
      height: inherit;
      justify-content: center;
      align-items: center;
      width: 9.167vh;
      font-size: 1.852vh;
      line-height: 2.315vh;
    }
  }

  .list {
    margin-top: 0.926vh;
    display: flex;
    flex-direction: column;
    gap: 0.926vh;
    overflow: auto;
    height: 53.519vh;
    overflow-x: hidden;
    &::-webkit-scrollbar {
      width: 0.463vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.23);
    }
    &::-webkit-scrollbar-thumb {
      background: #ff0000;
    }
    .nav-item {
      flex-shrink: 0;
      width: 32.963vh;
      height: 8.148vh;
      background: rgba(7, 7, 7, 0.4);
      display: flex;
      align-items: center;
      &:hover {
        background: rgba(7, 7, 7, 0.7);
      }
      img {
        margin-left: 1.852vh;
        width: 5.556vh;
      }
      .name {
        font-size: 2.222vh;
        line-height: 2.778vh;
        &::before {
          width: 0.185vh;
          height: 2.5vh;
          content: '';
          margin: 0 1.389vh;
          background: #ffffff;
          border: 0.097vh solid rgba(255, 255, 255, 0.09);
          box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
        }
      }

      &.active {
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.8) 0%,
          rgba(75, 0, 130, 0.8) 100%
        );
      }
    }
  }
}
</style>
