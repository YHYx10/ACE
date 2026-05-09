<template>
  <div
    :class="[{ turned: title === 'team red' }, 'lobbie__team']"
    :style="{ '--curCol': this.color }"
  >
    <div class="team__header">
      <span class="header__title">{{ loc(title) }}</span>
      <span class="header__number">{{loc('arena:tlist:command')}} <span class="header__number-value">{{ numberOfPlayers }}</span></span>
    </div>
    <div class="team__main">
      <div
        class="main__item"
        v-for="(player, index) in team"
        :key="index"
        :class="{ red: title === 'team red'}"
      >
        <div class="wrap">
          <svg :class="[{hidden: player !== owner}, 'item__icon']" xmlns="http://www.w3.org/2000/svg" width="21" height="20" fill="none" viewBox="0 0 21 20">
            <path fill="#FFCD4D" d="M4.515 19.304c-.482.247-1.03-.186-.932-.74L4.62 12.65.217 8.456C-.195 8.064.019 7.346.57 7.27l6.123-.87L9.423.99a.642.642 0 0 1 1.159 0l2.73 5.409 6.122.87c.551.077.765.795.352 1.187l-4.402 4.195 1.038 5.913c.097.554-.45.987-.933.74L10 16.484l-5.486 2.82h.001Z"/>
          </svg>
          <span class="item__title">{{ player }}</span>
          <button
            v-if="currentPlayer === owner && player !== currentPlayer"
            class="item__close"
            @click="kickPlayer(player)"
          >&times;</button>
        </div>
        
      </div>
      <button :class="{ red: title === 'team red'}" v-if="!searchCurrentPlayer" @click="joinTeam" class="main__item main__btn">
        <span>{{ loc('arena_dm_26') }}</span>
      </button>
      <div
        class="main__item disabled"
        v-for="(item, index) in max - team.length - 1"
        :key="index+100"
      >
        <div class="wrap">
          <svg :class="[{hidden: player !== owner}, 'item__icon']" xmlns="http://www.w3.org/2000/svg" width="21" height="20" fill="none" viewBox="0 0 21 20">
            <path fill="#FFCD4D" d="M4.515 19.304c-.482.247-1.03-.186-.932-.74L4.62 12.65.217 8.456C-.195 8.064.019 7.346.57 7.27l6.123-.87L9.423.99a.642.642 0 0 1 1.159 0l2.73 5.409 6.122.87c.551.077.765.795.352 1.187l-4.402 4.195 1.038 5.913c.097.554-.45.987-.933.74L10 16.484l-5.486 2.82h.001Z"/>
          </svg>
          <span class="item__title"> Free storage space</span>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'

export default {
  name: 'TeamList',

  props: [
    'team',
    'title',
    'color',
    'engTitle',
    'owner',
    'max'
  ],

  computed: {
    ...mapState('arenaMenu', ['currentLobbieId', 'currentPlayer']),

    ...mapGetters('localization', ['loc']),

    numberOfPlayers: function () {
      return this.team.length
    },

    searchCurrentPlayer: function () {
      return this.team.find(item => item === this.currentPlayer)
    }
  },

  methods: {
    joinTeam: function () {
      window.mp.trigger('ARENA::JOIN::LOBBY::VUE', this.engTitle, this.currentLobbieId)
    },

    kickPlayer: function (player) {
      window.mp.trigger('ARENA::KICK::LOBBY::VUE', player, this.currentLobbieId)
    }
  }
}
</script>

<style lang="scss" scoped>
.lobbie__team {
  display: flex;
  flex-direction: column;
  font-family: Akrobat;
  width: 24.2rem;
  height: 100%;
  overflow-y: auto;
  text-transform: uppercase;
  padding: 1rem 0 1.35rem;
  .header__number {
    font-weight: 700;
    font-size: 0.8rem;
    line-height: 0.95rem;
    color: rgba(255, 255, 255, 0.5);
  }
  .header__title {
    font-weight: 800;
    font-size: 3.2rem;
    line-height: 3.85rem;
    color: var(--curCol);
    text-shadow: 0rem 0rem 4.45rem var(--curCol);
  }
  .team__header {
    font-size: 2.3rem;
    color: var(--curCol);
    border-bottom: 0.1rem solid rgba(255, 255, 255, 0.25);
    padding-bottom: 1rem;
    display: flex;
    flex-direction: column;
    margin-bottom: 1rem;
  }
  .team__main {
    display: flex;
    flex-direction: column;
    flex-wrap: nowrap;
    margin: 1rem 0 0 0;
    overflow-y: scroll;
    &::-webkit-scrollbar {
      display: none;
    }
  }
  .main__item {
    display: flex;
    align-items: center;
    margin: .5rem 0 0 0;
    position: relative;
    min-height: 2.7rem;
    overflow: hidden;
    padding: 0.55rem 0.5rem;
    border: 0.05rem solid;
    border-image: linear-gradient(90deg, rgba(92, 255, 128, 0.9) 0%, rgba(92, 255, 128, 0) 101.25%) 1;
    &.red {
      border-image: linear-gradient(90deg, rgba(71, 44, 132, 0.9) 0%, rgba(255, 0, 11, 0) 101.25%) 1;
    }
    &.disabled {
      border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.25) 0%, rgba(0, 0, 0, 0) 101.25%) 1;
      .wrap {
        border-color: rgba(255, 255, 255, 0.5);
      }
      .item__title {
        color: rgba(255, 255, 255, 0.25);
      }
    }
    .wrap {
      display: flex;
      align-items: center;
      height: 1.6rem;
      width: 100%;
      border-left: 0.1rem solid var(--curCol);
    }
    &::after {
      content: '';
      position: absolute;
      width: 22.85rem;
      height: 1.1rem;
      left: -4.35rem;
      top: 2.15rem;
      background: rgba(255, 255, 255, 0.55);
      filter: blur(4.45rem);
    }
    &:first-child {
      margin: 0;
    }
    .item__icon {
      width: 1rem;
      height: 1rem;
      margin-left: 0.75rem;
      &.hidden {
        visibility: hidden;
      }
    }
    .item__title {
      display: inline-block;
      margin: 0 0 0 0.5rem;
      color: #FFF;
      font-weight: 700;
      font-size: 1rem;
      line-height: 1.2rem;
    }
    .item__close {
      margin-left: auto;
      font-size: 1.5rem;
      color: rgba(255, 255, 255, 0.4);
      background-color: transparent;
      border: 0.1rem solid transparent;
      width: 1.6rem;
      display: flex;
      align-items: center;
      justify-content: center;
      height: 1.6rem;
      transition: all .2s;
      // z-index: 20;
      &:hover {
        color: rgba(255, 255, 255, 1);
      }
    }
  }
  .main__btn {
    width: 100%;
    display: flex;
    justify-content: center;
    text-transform: uppercase;
    align-items: center;
    color: #fff;
    // border: 0.1rem solid #B6D300;
    border: 0.1rem solid;
    font-family: Akrobat;
    border-image: linear-gradient(90deg, rgba(92, 255, 128, 0.9) 0%, rgba(255, 0, 11, 0) 51.15%, rgba(92, 255, 128, 0.9) 101.25%) 1;
    font-weight: 700;
    font-size: 1rem;
    line-height: 1.2rem;
    box-sizing: border-box;
    transition: 0.2s;
    position: relative;
    background: transparent;
    &::before {
      content: '';
      width: 22.85rem;
      height: 1.1rem;
      background: rgb(92, 255, 128);
      position: absolute;
      filter: blur(1.25rem);
      top: 50%;
      left: 50%;
      opacity: 0.6;
      // border-radius: 80%;
      transform: translate((-50%, -50%));
      transition: 0.2s;
    }
    &:hover {
      border-image: linear-gradient(90deg, rgba(92, 255, 128, 0.9) 0%, rgba(92, 255, 128, 0.9) 51.15%, rgba(92, 255, 128, 0.9) 101.25%) 1;
      transition: 0.3s;
      &::before { 
        border-radius: 0%;
        transition: 0.3s;
        opacity: 1;
      }
    }
    &.red {
      border-image: linear-gradient(90deg, rgba(71, 44, 132, 0.9) 0%, rgba(255, 0, 11, 0) 51.15%, rgba(255, 0, 11, 0.9) 101.25%) 1;
      &::before {
        content: '';
        width: 22.85rem;
        height: 1.1rem;
        background: #FF1B25;
        position: absolute;
        filter: blur(1.25rem);
        top: 50%;
        left: 50%;
        opacity: 0.6;
        // border-radius: 80%;
        transform: translate((-50%, -50%));
        transition: 0.2s;
      }
      &:hover {
        border-image: linear-gradient(90deg, rgba(71, 44, 132, 0.9) 0%, rgba(71, 44, 132, 0.9) 51.15%, rgba(255, 0, 11, 0.9) 101.25%) 1;
        transition: 0.3s;
        &::before { 
          border-radius: 0%;
          transition: 0.3s;
          opacity: 1;
        }
      }
    }
    
    
    span {
      display: inline-block;
      position: relative;
    }
  }
}
</style>
