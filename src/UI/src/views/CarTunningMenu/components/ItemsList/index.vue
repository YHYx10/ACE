<template>
  <div class="items-list" @wheel="onWheel" ref="itemsList">
    <div
      class="wrap"
    >
      <div
        v-for="item in list"
        :key="item.key"
        @click="setTab(item.key)"
        class="item"
        :class="{ active: item.key === currentSelectItem }"
      >
        <img
          v-if="currentTab === 'ThirdTab'"
          :src="
            `/img/carTunningMenu/${item.image}`
          "
          :alt="item.image"
        />
        <!-- <img v-else :src="`/img/carTunningMenu/${getFolder}/${fourthTabData.parent}.png`" alt=""> -->
        <div class="info">
          <div class="top">
            <div class="number">{{ item.rank }}</div>
            <div v-show="item.price" class="price">$ {{ item.price || 0 }}</div>
          </div>
          <div class="text">
            {{ loc(item.title) }}
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState, mapMutations } from 'vuex'

export default {
  props: {
    list: Array
  },
  computed: {
    ...mapState('carTunningMenu', ['currentSelectItem', 'secondTabData', 'thirdTabData', 'currentTab', 'fourthTabData']),
    ...mapGetters('localization', ['loc']),
    getFolder() {
      switch (this.thirdTabData.parent) {
        case 'FrontWheels':
          return 'thirdTab/FrontWheels'
        case 'BackWheels':
          return 'thirdTab/BackWheels'
        case 'Xenon':
          return 'thirdTab/Xenon'

        default:
          return `secondTab`
      }
    },
    // getData(){
    //   return this.currentTab === "ThirdTab" ? this.thirdTabData : this.currentTab === "FourthTab" ? this.fourthTabData : null
    // }
  },
  methods: {
    ...mapMutations('carTunningMenu', [
      'setCurrentTab',
    ]),
    setTab(key) {
      console.log('OK', this.thirdTabData.parent)
      if(this.currentTab === 'ThirdTab') {
        if (this.thirdTabData.parent === 'FrontWheels' || this.thirdTabData.parent === 'BackWheels') {
        this.setCurrentTab('FourthTab')
        window.mp.trigger('lsCustom:chooseWheelType', key)
      } else {
        window.mp.trigger('lsCustom:clickTun', this.thirdTabData.parent, key);
        // this.$emit('onSelect', key)
      }}
      if(this.currentTab === 'FourthTab') window.mp.trigger('lsCustom:clickTun', this.fourthTabData.parent, key)
    },
    onWheel(e) {
      const width = this.$refs.itemsList.offsetWidth / 5
      this.$refs.itemsList.scrollLeft += e.deltaY < 0 ? -width : width
    },
  },
}
</script>

<style lang="scss" scoped>
.items-list {
  font-family: Akrobat;
  font-weight: 700;
  scroll-behavior: smooth;
  scroll-snap-type: x mandatory;
  width: 100%;
  height: 15.648vh;
  display: flex;
  align-items: flex-start;
  overflow-x: auto;
  overflow-y: hidden;
  .wrap {
    display: flex;
    width: 100%;
    gap: 0.926vh;
  }
  &::-webkit-scrollbar {
    height: 0.463vh;
  }
  &::-webkit-scrollbar-track {
    background: rgba(255, 255, 255, 0.23);
  }
  &::-webkit-scrollbar-thumb {
    background: #ff0000;
  }
  .item {
    scroll-snap-align: center;
    flex-shrink: 0;
    width: 32.963vh;
    height: 14.259vh;
    display: flex;
    background: rgba(7, 7, 7, 0.4);
    align-items: center;
    position: relative;
    &.active {
      background: linear-gradient(
        180deg,
        rgba(71, 44, 132, 0.7) 0%,
        rgba(75, 0, 130, 0.7) 100%
      );
      .info .top .number {
        background: rgba(7, 7, 7, 0.6);
      }
    }
    img {
      position: absolute;
      top: 50%;
      transform: translate(0, -50%);
    }
    .info {
      width: 100%;
      margin-left: 12.13vh;
      display: flex;
      flex-direction: column;
      .top {
        height: 4.259vh;
        display: flex;
        justify-content: flex-end;
        font-size: 2.222vh;
        line-height: 2.778vh;
        .number {
          display: flex;
          align-items: center;
          justify-content: center;
          width: 5.648vh;
          background: linear-gradient(180deg, #301934  0%, #591b87 100%);
          color: #ffffff;
        }
        .price {
          width: 100%;
          background: rgba(7, 7, 7, 0.2);
          display: flex;
          align-items: center;
          justify-content: center;
          color: #a0ff98;
        }
      }
      .text {
        display: flex;
        align-items: center;
        height: 10vh;
        font-size: 2.593vh;
        line-height: 3.241vh;
        color: #ffffff;
      }
    }
  }
}
</style>
