<template>
  <div v-if="isVote || isFullKillstat" class="full-kill-stat" :style="{'background-image': `url(/img/hud/killstat/${isGlobal ? 'bg-global' : 'bg-result'}.png)`}">
    <div class="full-stat__vote" v-if="isVote">
      <div
        class="vote__item"
        v-for="(item, index) in voteItems"
        :key="index"
        :style="{ 'background': `linear-gradient(to bottom, rgba(0, 0, 0, .5), rgba(0, 0, 0, .5)), url(${setBackground(item.title)}) center / cover no-repeat` }"
        @click="vote(item.title)"
      >
        <div class="item__count">
          <span class="count__current">{{ item.currentCount }}</span>
          <span class="count__separator">/</span>
          <span class="count__max">{{ item.maxCount }}</span>
        </div>
      </div>
    </div>
    <div v-if="isFullKillstat" class="header">
      <h1>{{ isGlobal ? loc('arena_dm_29') : loc('arena_dm_30') }}</h1>
      <div class="line"></div>
      <div class="exit">
     ESC-Watch
      </div>
    </div>
    <div v-if="isFullKillstat" class="box">

      <div class="inner">

        <div class="table">
          <div class="item-head item" :class="{ global: isGlobal }">
            <div class="player">PLAYER</div>
            
            <template v-if="isGlobal">
              <div class="stat-item">place</div>
              <div class="stat-item">glasses</div>
            </template>
            <template v-else>
              <div class="stat-item">Murders</div>
              <div class="stat-item">deaths</div>
              <div class="stat-item">ratio</div>
            </template>
            
          </div>
          <div 
            class="item"
            v-for="(item) in currentItems"
            :key="item.id"
            :class="[{gold: item.place === 1}, {silver: item.place === 2}, {bronze: item.place === 3}, { global: isGlobal }]"
          >
            
            <div class="player">
              <div class="color-line" />
              {{item.username}}
            </div>
            
            <template v-if="isGlobal">
              <div class="stat-item">
                <div class="value">
                  <img v-if="item.place === 1" src="/img/battlegroundStats/first.png" alt="">
                  <img v-else-if="item.place === 2" src="/img/battlegroundStats/second.png" alt="">
                  <img v-else-if="item.place === 3"  src="/img/battlegroundStats/third.png" alt="">
                  <template v-else >{{ item.place }}</template>
                </div>
              </div>
              <div class="stat-item">
                <div class="value">
                  {{ item.points }}
                </div>
              </div>
            </template>
            <template v-else>
              <div class="stat-item">
                <div class="value">{{ item.kills }}</div>
              </div>
              <div class="stat-item">
                <div class="value">{{ item.deaths }}</div>
              </div>
              <div class="stat-item">
                <div class="value">{{ receiveKD(item.kills, item.deaths) }}</div>
              </div>
            </template>
          </div>
        </div>
        <div class="underline"></div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'

export default {
  name: 'FullKillStat',

  computed: {
    ...mapState('hud', [
      'killstatItems',
      'killstatType',
      'voteItems',
      'isVote',
      'isFullKillstat'
    ]),
    ...mapGetters('localization', ['loc']),
    ...mapState('arenaMenu', ['backgrounds']),

    currentItems: function () {
      return this.sortByKills(this.killstatItems)
    },
    isGlobal(){
      return this.killstatType === 'global_rating'
    }
  },

  methods: {
    sortByKills: function (array) {
      return array.sort(this.sortFunc)
    },

    sortFunc: function (a, b) {
      if (b.kills === a.kills) {
        return a.deaths - b.deaths
      } else {
        return b.kills - a.kills
      }
    },

    receiveKD: function (kills, deaths) {
      if (kills > 0 && deaths === 0) {
        return kills
      } else if (kills === 0 || deaths === 0) {
        return null
      } else {
        return (kills / deaths).toFixed(1)
      }
      
    },

    setBackground: function (title) {
      const rightIndex = this.backgrounds.findIndex(item => item.title === title)
      return this.backgrounds[rightIndex].image
    },

    vote: function (title) {
      //console.log('TRIGGER SOME EVENT!')
      window.mp.trigger('ARENA::CHOOSE::MAP::NAME::VUE', title)
    }
  }
}
</script>

<style lang="scss" scoped>

.full-kill-stat {
  position: absolute;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  z-index: 1;
  background-size: cover;
  display: flex;
  flex-flow: column;
  align-items: center;
  justify-content: center;
  text-transform: uppercase;
  font-family: Akrobat;
  .header {
    width: 54.5rem;
    margin-bottom: 3rem;
    display: grid;
    grid-template-columns: auto 1fr auto;
    gap: 1rem;
    align-items: center;
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
    .line {
      min-width: 100%;
      border-bottom: 0.1rem solid rgba(255, 255, 255, 0.25);
      margin-top: 0.9rem;
    }
    .exit {
      margin-top: 0.9rem;
      font-weight: 700;
      font-size: 1rem;
      line-height: 1.2rem;
      color: #fff;
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
    }

    .table {
      height: 32.9rem;
      overflow-y: scroll;
      &::-webkit-scrollbar {
        display: none;
      }

      .item {
          width: 100%;
          height: 3.6rem;
          display: grid;
          align-items: center;
          gap: 3.5rem;
          color: #fff;
          border-style: solid;
          border-width: 0.05rem;
          border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1;
          border-collapse: collapse;
          position: relative;
          overflow: hidden;
          grid-template-columns: 1fr auto auto auto;
          --currentColor: #fff;
          --currentFill: rgba(255, 255, 255, 0.55);
          &.global {
            grid-template-columns: 1fr auto auto;
            &.gold {
              --currentColor: #FFCD4D;
              --currentFill: #FFC532;
            }
            &.silver {
              --currentFill: rgba(255, 255, 255, 0.9);
            }
            &.bronze {
              --currentColor: #E47A4D;
              --currentFill: rgba(255, 181, 113, 0.73);
            }
            &.gold, &.silver, &.bronze {
              .player {
                text-shadow: 0rem 0rem 1.5rem var(--currentColor);
              }
              .stat-item {
                color: var(--currentColor);
              }
            }
          }
          
          
          &:not(.item-head)::after {
            content: '';
            width: 22.85rem;
            height: 1.1rem;
            top:  4rem;
            left: 50%;
            transform: translate(-50%);
            position: absolute;
            background: var(--currentFill);
            border-radius: 70%;
            filter: blur(4.45rem);
          }
          &-head {
            margin-top: 1.5rem;
            height: 2.6rem;
            border-width: 0rem;
            font-weight: 700;
            font-size: 1rem;
            line-height: 1.2rem;
            .player {
              margin-left: 2.95rem;
            }
          }
          .stat-item {
            text-align: center;
            width: 3.5rem;
            height: 100%;
            position: relative;
            display: flex;
            align-items: center;
            justify-content: center;
            
            img {
                position: absolute;
                left: 50%;
                top: 50%;
                transform: translate(-50%, -50%);
            }
          }
          .player {
            width: 17.7rem;
            display: flex;
            color: var(--currentColor);
            font-weight: 700;
            font-size: 1rem;
            line-height: 1.2rem;
            .color-line {
              margin-left: 0.7rem;
              margin-right: 2.15rem;
              width: 0.1rem;
              height: 1.3rem;
              background: var(--currentColor);
              box-shadow: 0rem 0rem 0.7rem var(--currentColor);
          }
        }
      }
    }
    .underline {
      width: 100%;
      margin-top: 1.25rem;
      border-bottom: 0.05rem solid #DA131F;
    }
  }
}

.full-stat {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  display: flex;
  flex-direction: column;
  width: 54rem;
  z-index: 1;
  &__vote {
    display: flex;
    justify-content: space-between;
    .vote__item {
      width: 17rem;
      height: 10rem;
      display: flex;
      align-items: center;
      justify-content: center;
      border-top: .2rem solid #1fbf75;
      box-shadow: inset 0 -0.2rem 0.5rem #1fbf75;
      transition: all .15s;
      &:hover {
        transform: translateY(-.2rem) scale(1.01);
        box-shadow: inset 0 -0.3rem 0.9rem #1fbf75;
      }
      .item__count {
        font-size: 3.4rem;
        color: #fff;
        display: flex;
        .count__current {
          color: #1fbf75;
        }
      }
    }
  }
}

</style>
