<template>
  <div class="battleground-stats">
    <div class="header">
      <h1>{{loc('BattleStats_0')}}</h1>
    </div>
    <div class="box">

      <div class="inner">
        <div class="nav">
          <div @click="setCurrentNav(1)" :class="{active: currentNav === 1}" class="item">
         Current agreement
          </div>
          <div @click="setCurrentNav(0)" :class="{active: currentNav === 0}" class="item">
     Für die ganze Zeit
          </div>
          <div class="search">
            <div class="wrap">
              <img @click="setSearch" src="/img/battlegroundStats/search.svg" alt="">
              <input @keyup.enter="setSearch" v-model="currentFilter" type="text" placeholder="Search for a participant by name">
            </div>
          </div>
        </div>

        <div class="table">
          <div class="item-head item">
            <div class="number">№</div>
            <div class="player">Player</div>
            <div class="kills">The number of murders</div>
          </div>
          <div 
            class="item"
            v-for="(item, index) in currentList"
            :key="index"
          >
            <div class="number">
              <div class="color-line" :class="[{gold: item.place === 1}, {bronze: item.place === 3}]" />
              <div class="value">
                <img v-if="item.place === 1" src="/img/battlegroundStats/first.png" alt="">
                <img v-else-if="item.place === 2" src="/img/battlegroundStats/second.png" alt="">
                <img v-else-if="item.place === 3"  src="/img/battlegroundStats/third.png" alt="">
                <template v-else >{{ item.place }}</template>
              </div>
            </div>
            <div class="player">{{item.nickname}}</div>
            <div class="kills">
              <div class="count">{{ item.kills }}</div>
              <img src="/img/battlegroundStats/kill.svg" alt="">
            </div>
          </div>
        </div>
        <div class="underline"></div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
export default {
  name: 'BattlegroundStats',

  data: function() {
    return {
      currentNav: 1, // 1 - current, 0 - all time
      isSearchList: false,
      currentFilter: null
    }
  },

  methods: {
    setCurrentNav: function(value) {
      this.currentNav = value
      window.mp.trigger('battlegroundStats:setCurrentNav', value)
    },
    setSearch: function() {
      window.mp.trigger('battlegroundStats:setSearch', this.currentFilter)
      this.isSearchList = true;
    }
  },

  computed: {
    ...mapState('battlegroundStats', ['currentMatch', 'allTime', 'listSearch']),
    ...mapGetters('localization', ['loc']),
    currentList: function() {
      let array = null
      if (this.currentNav) {
        array = this.currentMatch
      } else if (this.isSearchList) {
        array = this.listSearch
      } else {
        array = this.allTime
      }
        return array
    }
  },
  watch: {
    currentFilter: function(value) {
      if (!value) {
        this.isSearchList = false
      }
    }
  }
}
</script>

<style lang="scss" scoped>
@keyframes show{
  from {
    transform: translateY(-100%);
    opacity: 0;
  }
  to {
    transform: translateY(0%);
    opacity: 1;
  }
}
.battleground-stats {
  width: 100%;
  height: 100vh;
  background-color: #1D232C;
  background-image: url('/img/battlegroundStats/bg.png');
  background-size: cover;
  display: flex;
  flex-flow: column;
  align-items: center;
  justify-content: center;
  text-transform: uppercase;
  .header {
    width: 54.5rem;
    margin-bottom: 3rem;
    padding-left: 6.55rem;
    h1 {
      font-family: Akrobat;
      font-weight: 800;
      font-size: 3.3rem;
      line-height: 3.95rem;
      background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0rem 0rem 5.6296rem rgba(190, 32, 32, 0.25);
    }
  }
  
  .box {
    width: 54.5rem;
    height: 37.55rem;
    display: flex;
    justify-content: center;
    background: linear-gradient(180deg, rgba(1, 1, 1, 0.5) 0%, rgba(1, 1, 1, 0) 90.1%);
    .inner {
      min-width: 40.8rem;
      display: flex;
      flex-direction: column;
      gap: 0.8rem;
    }
    .nav {
      display: flex;
      gap: 2rem;
      justify-content: flex-start;
      border-bottom: 0.05rem solid #FFFFFF;
      width: 100%;
      height: 3rem;
      .item {
        font-weight: 700;
        font-size: 1rem;
        line-height: 1.2rem;
        color: rgba(255, 255, 255, 0.25);
        display: flex;
        align-items: center;
        justify-content: center;
        &.active {
          box-shadow: 0 0.1685rem 0rem #301934  inset;
          color: #FFFFFF;
        }
      }
      .search {
        margin-left: auto;
        display: flex;
        align-items: center;
        .wrap {
          width: 19rem; 
          height: 2.2rem;
          border-style: solid;
          border-width: 0.05rem;
          border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1;
          display: flex;
          align-items: center;
          gap: 0.5rem;
          img {
            margin-left: 1.05rem;
            width: 0.95rem;
            height: 0.95rem;
            opacity: 0.9;
            &:hover {
              opacity: 1;
            }
          }

          input {
            width: 100%;
            background: transparent;
            font-family: Akrobat;
            color: #fff;
            font-weight: 700;
            font-size: 1rem;
            line-height: 1.2rem;
            text-transform: uppercase;
            &::placeholder {
              color: rgba(255, 255, 255, 0.25);
            }
          }
        }
      }
    }

    .table {
      height: 29.3rem;
      overflow-y: scroll;
      &::-webkit-scrollbar {
        display: none;
      }

      .item {
        width: 100%;
        height: 3.6rem;
        display: flex;
        align-items: center;
        gap: 3.25rem;
        color: #fff;
        border-style: solid;
        border-width: 0.05rem;
        border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1;
        border-collapse: collapse;
        position: relative;
        overflow: hidden;
        &:not(.item-head)::after {
          content: '';
          width: 22.85rem;
          height: 1.1rem;
          top:  4rem;
          left: 50%;
          transform: translate(-50%);
          position: absolute;
          background: rgba(255, 255, 255, 0.55);
          border-radius: 70%;
          filter: blur(4.45rem);
        }
        &-head {
          height: 2.6rem;
          border-width: 0rem;
        }
        .number {
          text-align: center;
          width: 7rem;
          position: relative;
          // display: flex;
          .color-line {
            margin-left: 0.7rem;
            width: 0.1rem;
            height: 1.3rem;
            background: #fff;
            &.gold {
              background: #FFCD4D;
            }
            &.bronze {
              background: #F8B463;
            }
          }
          .value {
              position: absolute;
              left: 50%;
              top: 50%;
              transform: translate(-50%, -50%);
            img {
              
            }
          }
        }
        .player {
          width: 17.7rem;
        }

        .kills {
          .count {
            left: 0.5rem;
            top: 50%;
            transform: translate(0, -50%);
            position: absolute;
            width: 3rem;
            text-align: right;
            font-weight: 700;
            font-size: 1rem;
            line-height: 1.2rem;
          }
          img {
            width: 2rem;
            height: 2rem;
          }
          position: relative;
          text-align: center;
          width: 9.6rem;
        }
      }
    }
    .underline {
      width: 100%;
      margin-top: 1.5rem;
      border-bottom: 0.05rem solid #DA131F;
    }
  }
}
</style>
