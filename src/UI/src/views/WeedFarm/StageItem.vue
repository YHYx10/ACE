<template>
  <div class="stage-item">
    <div class="circle"></div>
    <div class="stage-level"> stage {{ index + 1 }}</div>
    <div class="title">{{ loc(`weedfarm:stady:name:${index}`) }}</div>
    <div class="quantity"> {{ count }} / {{ maxOnStady[index] }}</div>
    <img :class="`img-${index+1}`" :src="`/img/weedFarm/lvl${index+1}.png`"/>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'

export default {
  props: {
    index: Number,
    count: Number,
  },
  computed: {
      ...mapGetters('localization', ['loc']),
      ...mapState('weedFarm', ['maxOnStady'])
  },
}
</script>

<style lang="scss" scoped>

.stage-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 20rem;
  height: 32.05rem;
  position: relative;
  background: linear-gradient(180deg, rgba(23, 23, 28, 0.7) 0%, rgba(23, 23, 28, 0) 100%);
  .circle {
    width: 1.5rem;
    height: 1.5rem;
    background: rgba(255, 255, 255, 0.15);
    margin-top: 2.05rem;
    border-radius: 100%;
    position: relative;
    &::before,
    &::after {
      z-index: 10;
      position: absolute;
      top: 50%;
      content: '';
      opacity: 0.15;
      width: 9.75rem;
      height: 0.05rem;
      background-image: linear-gradient(to right, rgb(255, 255, 255) 50%, rgba(255,255,255,0) 0%);
      background-position: bottom;
      background-size: 1.6rem 0.05rem;
      background-repeat: repeat-x;
    }
    &::before {
      transform: translate(-0.75rem, -50%);
      right: 50%;
    }
    &::after {
      transform: translate(0.75rem, -50%);
      left: 50%;
    }
  }

  &:first-child .circle::before{
    display: none;
  }

  &:nth-child(4) .circle::after{
    display: none;
  }
  .stage-level {
    margin-top: 1.35rem;
    font-weight: 700;
    font-size: 1.6rem;
    line-height: 1.9rem;
    text-align: center;
    background: linear-gradient(180deg, #301934  0%, #EA0505 49.48%, #301934  100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
    text-fill-color: transparent;
    text-shadow: 0rem 0rem 2.45rem rgba(255, 0, 0, 0.52);
    text-transform: uppercase;
  }

  .title {
    margin-top: 1.05rem;
    font-weight: 700;
    font-size: 2rem;
    line-height: 2.4rem;
    color: #FFFFFF;
    text-transform: uppercase;
  }

  .quantity {
    font-weight: 700;
    font-size: 3.2rem;
    line-height: 3.85rem;
    text-align: center;
    color: #FFFFFF;
  }

  img {
    position: absolute;
    bottom: 10%;
    left: 50%;
    transform: translateX(-50%);
    margin-top: 1.65rem;
    background-size: cover;
    // width: 10rem;
    height: 10rem;
    &.img-1 {
      height: 12.9rem
    }
    &.img-2 {
      height: 11.75rem
    }
    &.img-3 {
      height: 9.25rem;
      margin-bottom: 1.25rem;
    }
    &.img-4 {
      height: 9.1rem;
      margin-bottom: 1.25rem;
    }
  }

  
}
</style>