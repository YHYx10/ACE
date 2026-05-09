<template>
  <div
    class="card"
    @click="onclick"
    :class="{ active: isActive, round: layout === 'round' }"
  >
    <div class="header">
      <div class="title">
        {{ title }}
      </div>
    </div>
    <div class="item-img">
      <img :style="`width: ${imgSize*100}%;`" :src="img" :alt="img" />
    </div>
    <MoneyBlock
      v-show="layout !== 'round'"
      :balance="price"
      minVersion
      class="price"
    />
    <div v-show="layout !== 'round'" class="corner corner-a" />
    <div v-show="layout !== 'round'" class="corner corner-b" />
    <div v-show="layout !== 'round'" class="corner corner-c" />
    <div v-show="layout !== 'round'" class="corner corner-d" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import MoneyBlock from './MoneyBlock.vue'
export default {
  props: {
    index: Number,
    isActive: Boolean,
    price: Number,
    title: String,
    img: String,
    imgSize: {
      type: Number,
      default: 1,
    },
    layout: {
      type: String,
      default: 'default',
    },
  },
  data: () => {
    return {}
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    onclick() {
      this.$emit('onSelect', this.index)
    },
  },
  components: { MoneyBlock },
}
</script>

<style lang="scss" scoped>
.card {
  text-transform: none;
  color: #ffffff;
  box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 0.15);
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;
  overflow: hidden;
  transition: 0.3s;
  &:hover {
    transition: 0.1s;
    box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 0.55);
  }
  &.active {
    box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 1);
    background: url('./img/highlight.svg');
    background-repeat: no-repeat;
    background-size: cover;
  }
  .header {
    margin-top: 0.556vh;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
    .title {
      padding: 0 0.278vh;
      text-align: center;
      font-family: 'Montserrat';
      font-style: normal;
      font-weight: 600;
      font-size: 1.481vh;
      line-height: 2.222vh;
      color: #ffffff;
    }
  }
  &.round {
    border-radius: 0.741vh;
    .header {
      margin-top: 72%;
      .title {
        font-weight: 600;
        font-size: 1.296vh;
        line-height: 1.481vh;
      }
    }
  }
  .item-img {
    width: 100%;
    margin-top: -0.28vh;
    display: flex;
    justify-content: center;
    align-items: center;
    position: absolute;
    top: 50%;
    transform: translate(0, -50%);
    img {
      width: 40%;
    }
  }
  .price {
    margin-bottom: 1.204vh;
  }
  .corner {
    border-radius: 100%;
    position: absolute;
    width: 0.46vh;
    height: 0.46vh;
    background: #ffffff;
    box-shadow: 0vh 0vh 1.02vh #ffffff;
  }
  .corner-a {
    left: 0;
    top: 0;
    transform: translate(-50%, -50%);
  }
  .corner-b {
    right: 0;
    top: 0;
    transform: translate(50%, -50%);
  }
  .corner-c {
    right: 0;
    bottom: 0;
    transform: translate(50%, 50%);
  }
  .corner-d {
    left: 0;
    bottom: 0;
    transform: translate(-50%, 50%);
  }
}
</style>