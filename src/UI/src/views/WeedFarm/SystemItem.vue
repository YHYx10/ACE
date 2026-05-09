<template>
  <div class="system-item" :class="{'can-fix': value < 100}">
    <div class="main">
      <div class="info">
        <div class="title">{{loc(`weedfarm:sys:${index}`)}}</div>
        <div class="fix-name">serviceability</div>
        <div class="fix-value">{{value}} / 100</div>
      </div>
      <img :src="`/img/weedFarm/system-${index + 1}.png`" alt="">
    </div>
    <div class="progress">
      <div class="value" :style="{width: `${value}%`}" />
    </div>
    <div class="hover-window">
      <button @click="fixComponent">
      fix
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
  props: {
    index: Number,
    value: Number
  },
  computed: {
      ...mapGetters('localization', ['loc']),
  },
  methods: {
      fixComponent(){
          if(this.spam > Date.now()) return;
          this.spam = Date.now() + 1000;
          window.mp.triggerServer("weedfarm:systems:fix", this.index)
      }
  },
}
</script>


<style lang="scss" scoped>
  .system-item {
    * {
      font-family: 'Akrobat';
    }
    width: 20rem;
    height: 8rem;
    background: rgba(23, 23, 28, 0.7);
    position: relative;
    .main {
      display: flex;
      justify-content: space-between;
      margin-top: 1rem;
      .info {
        margin-left: 1.95rem;
        .title {
          margin-top: 0.6rem;
          font-weight: 700;
          font-size: 1.2rem;
          line-height: 1.45rem;
          margin-bottom: 0.5rem;
          color: #FFFFFF;
          text-transform: uppercase;
        }
        .fix-name {
          font-weight: 700;
          font-size: 0.8rem;
          line-height: 0.95rem;
          color: rgba(255, 255, 255, 0.5);
          margin-bottom: 0.25rem;
        }
        .fix-value {
          font-weight: 700;
          font-size: 0.8rem;
          line-height: 0.95rem;
          color: #FFFFFF;
        }
      }
      img {
        height: 5.6rem;
        margin-right: 0.5rem;
      }
    }

    .progress {
      width: 100%;
      height: 0.5rem;
      bottom: 0;
      position: absolute;
      background: rgba(0, 0, 0, 0.25);
      .value {
        width: 20%;
        height: 100%;
        background: #301934 ;
      }
    }

    .hover-window {
      top: 0;
      width: 100%;
      height: 100%;
      position: absolute;
      display: none;
      background: rgba(23, 23, 28, 0.9);
      // display: flex;
      align-items: center;
      justify-content: center;
      button {
        width: 14.75rem;
        height: 3.75rem;
        display: flex;
        align-items: center;
        justify-content: center;
        text-transform: uppercase;
        font-weight: 700;
        font-size: 1.2rem;
        line-height: 1.45rem;
        color: #FFFFFF;
        border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
        background: linear-gradient(
          rgba($color: #301934 , $alpha: 0.25),
          rgba($color: #591b87, $alpha: 0.25)
        );
        transition: 0.3s ease;
        box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

        &:hover {
          transition: 0.3s ease;
          box-shadow: inset 0px 0px 7.5rem #301934 ;
          filter: drop-shadow(0px 0px 15.78px rgba(71, 44, 132, 0.5));
        }
      }
    }

    &.can-fix:hover {
      transition: 0.3s ease;
      .hover-window {
        display: flex;
      }
    }
  }
</style>