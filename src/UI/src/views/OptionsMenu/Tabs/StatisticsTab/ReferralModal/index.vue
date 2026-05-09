<template>
  <div class="referral-modal">
    <div class="left-menu">
      <div class="header">
        <div class="category">Section</div>
        <div class="title">Partner program</div>
      </div>
      <div class="stats">
        <div class="stats-item" v-for="(item, key) in list" :key="key">
          <div class="counter">
            <img src="/img/optionsMenu/statisticsTab/icons/partners.svg" alt="">
            <div class="amount">{{item.count}}</div>
          </div>
          <div class="content">
            <img src="/img/optionsMenu/statisticsTab/icons/burger.svg" alt="">
            <div class="text">
              <div class="text-title">reward</div>
              <div class="text-value">{{item.text}}</div>
            </div>
          </div>
        </div>
      </div>
      
    </div>
    <div class="about">
      <div class="header">
        <div class="category">You have invited </div>
        <div class="title">{{ referals.total }} Human!</div>
      </div>
      <div class="header">
        <div class="category">Ihr Werbecode</div>
        <div class="title">{{referals.code}}</div>
        <div class="info">
          <!-- <div class="info-item">+250.000</div>
          <div class="info-item">+500 Donate points</div> -->
        </div>
      </div>
    </div>
    <img class="preview" src="/img/optionsMenu/statisticsTab/car.png" alt="">
  </div>
</template>

<script>
import { mapState } from 'vuex';
import list from './list.json'

export default {
  computed: {
    ...mapState('optionsMenu', ['referals']),
  },
  data() {
    return {
      list
    }
  }
}
</script>

<style lang="scss" scoped>
.referral-modal {
  // background: rgba(255, 0, 0, 0.563);
  width: 100%;
  height: 100%;
  display: flex;
  gap: 2.95rem;

  .preview{
    position: absolute;
    width: 69.3rem;
    height: auto;
    right: -7.5rem;
    bottom: 0px;
    opacity: 0.8;
  }

  .left-menu {
    .header {
      margin-left: 0.88rem;
    }
    .stats {
      margin-top: 1.5rem;
      display: flex;
      flex-direction: column;
      gap: 0.5rem;
      overflow-y: auto;
    }
    .stats-item {
      height: 4.25rem;
      display: flex;
      gap: 0.45rem;
      .counter {
        width: 4.25rem;
        height: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 0.1rem;
        border: 0.05rem solid rgba(255, 255, 255, 0.09);
        background: radial-gradient(circle, rgba(255,255,255,0.02) 0%, rgba(0,0,0,0) 81%);
        color: white;
      }
      .content {
        display: flex;
        align-items: center;
        width: 25.4rem;
        height: 100%;
        background: radial-gradient(circle, rgba(255,255,255,0.02) 0%, rgba(0,0,0,0) 81%);
        padding-left: 1.4rem;
        gap: 0.535rem;
        border: 0.05rem solid rgba(255, 255, 255, 0.09);
        border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 50%, rgba(0, 0, 0, 0) 100%) 1;
        .text {
          font-weight: 700;
          &-title {
            font-size: 0.6rem;
            line-height: 0.7rem;
            color: rgba(255, 255, 255, 0.55);
          }
          &-value {
            font-weight: 700;
            font-size: 0.8rem;
            line-height: 0.95rem;
            color: white;
          }
        }
      }
    }
  }
}
.header {
  margin: 0;
  position: relative;

  .title {
    margin: 0.4rem 0;
    font-size: 2.4rem;
    line-height: 2.3rem;
  }

  .subtitle {
    font-size: 0.7rem;
    line-height: 0.8rem;
    margin-top: 0;
  }

  .category {
    font-size: 0.66rem;
    line-height: 0.77rem;
    &:before {
      left: -0.88rem;
    }
  }

  .info {
    position: absolute;
    left: 100%;
    bottom: 0%;
    transform: translate(1.1rem, -0.6rem);
    display: flex;
    flex-direction: column;
    gap: 0.15rem;
    &-item {
      color: #33FF60;
      font-weight: 700;
      font-size: 0.6rem;
      line-height: 0.7rem;
      text-transform: uppercase;
      white-space: nowrap;
    }
  }
}
</style>