<template>
  <div class="arena-menu">
    <transition appear name="arena-header">
      <div class="header">
        <img src="/img/arenaMenu/arena.png" alt="" class="icon">
        <div class="nav">
          <div :class="{ active: currentTab === 'MainTab' }" class="item" @click="setTab('MainTab')">play</div>
          <div class="item" @click="setAttachment(true)">Connect the match</div>
          <div class="item" @click="showRating">Global Ranking</div>
        </div>
        <div class="exit" @click=setLobbieLeave(true)>
          <span>Leave the arena</span>
          <img src="/img/arenaMenu/arrow.svg" alt="">
        </div>
    </div>
  </transition> 
    <div class="box">
      <attachment-modal v-if="isAttachment" />
      <component :is="currentTab" />
      <div class="filters" v-show="currentTab === 'MainTab'">
        <span class="filters__header">{{ loc('arena_dm_4') }}</span>
        <span class="filters__desc">{{ loc('arena_dm_33') }}</span>
        <span class="filters__secdesc">{{ loc('arena_dm_34') }}</span>
        <div class="filters__main" v-if="filters[4].currentValue === 'Gun game'">
          <filters-item
            v-for="item in filtersWithoutRounds"
            :key="item.id"
            :item="item"
            :currentMode="filters[4].currentValue"
            :currentPlayers="filters[1].currentValue"
          />
          <DefaultBtn class="btn" @click="addLobbie">{{ loc('arena_dm_2') }}</DefaultBtn>
        </div>
        <div class="filters__main" v-else-if="filters[4].currentValue === 'Death match'">
          <filters-item
              v-for="item in filtersWithoutOnlyRounds"
              :key="item.id"
              :item="item"
              :currentMode="filters[4].currentValue"
              :currentPlayers="filters[1].currentValue"
          />
          <DefaultBtn class="btn" @click="addLobbie">{{ loc('arena_dm_2') }}</DefaultBtn>
        </div>
        <div class="filters__main" v-else>
          <filters-item
            v-for="item in filters"
            :key="item.id"
            :item="item"
            :currentMode="filters[4].currentValue"
            :currentPlayers="filters[1].currentValue"
          />
          <DefaultBtn class="btn" @click="addLobbie">{{ loc('arena_dm_2') }}</DefaultBtn>
          <!-- <button class="main__btn" @click="addLobbie">{{ loc('arena_dm_2') }}</button> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'

import MainTab from './components/MainTab'
import LobbieTab from './components/LobbieTab'
import FiltersItem from './components/FiltersItem'
import AttachmentModal from './components/AttachmentModal'
import DefaultBtn from '../UI/button/DefaultBtn.vue'

export default {
  name: 'ArenaMenu',

  components: {
    MainTab,
    LobbieTab,
    FiltersItem,
    AttachmentModal,
    DefaultBtn
},

  computed: {
    ...mapState('arenaMenu', [
      'modes',
      'lobbies',
      'filters',
      'currentTab',
      'isCreate',
      'isAttachment'
    ]),

    ...mapGetters('localization', ['loc']),

    filtersWithoutRounds: function () {
      return this.filters.filter(item => item.id !== 5 && item.id !== 3)
    },
    filtersWithoutOnlyRounds: function () {
      return this.filters.filter(item => item.id !== 5)
    },
  },

  methods: {
    ...mapMutations('arenaMenu', [
      'setCurrentTab',
      'createLobbiesItem',
      'setCurrentLobbieId',
      'setIsCreate',
      'setIsAttachment',
      'setIsLobbieLeave'
    ]),

    ...mapMutations('sounds', ['play']),

    setLobbieLeave: function (value) {
      this.setIsLobbieLeave(value)
      this.play({ name: 'choice', volume: 0.1 })
    },

    showRating: function () {
      window.mp.trigger('arena:showRating')
    },
    setAttachment: function (value) {
      this.play({ name: 'choice', volume: 0.1 })
      this.setIsAttachment(value)
    },

    setTab: function (value) {
      this.play({ name: 'choice2', volume: 0.1 })
      if (value === 'MainTab' || value === 'LobbieTab') this.openCreateBar(false)
      this.setCurrentTab(value)
    },

    openCreateBar: function (value) {
      this.setIsCreate(value)
      this.play({ name: 'choice2', volume: 0.1 })
    },

    addLobbie: function () {
      const currentFilters = this.filters.map(item => item.currentValue)
      // const item = {
      //   id: Date.now(),
      //   type: currentFilters[4],
      //   title: currentFilters[0],
      //   maxPlayers: currentFilters[1],
      //   started: false,
      //   rate: currentFilters[2],
      //   weapons: currentFilters[3],
      //   rounds: currentFilters[5],
      //   redTeam: [],
      //   greenTeam: []
      // }

      var mode = currentFilters[4]
      var map = currentFilters[0]
      var maxPlayers = currentFilters[1]
      var weapon = currentFilters[3]
      var rounds = currentFilters[5]
      var bet = currentFilters[2]

      window.mp.trigger('ARENA::ADD::LOBBY::VUE', mode, map, maxPlayers, weapon, rounds, bet)

      //this.createLobbiesItem(item)
      //this.setIsCreate(false)
      //this.setCurrentTab('LobbieTab')
      //this.setCurrentLobbieId(item.id)
    },

    closeArena: function () {
      window.mp.trigger('ARENA::CLOSE::GUI::SERVER')
    }
  },
  
  mounted: function () {
    window.mp.trigger('ARENA::SET::PLAYER::NAME::VUE')
  }
}
</script>

<style lang="scss" scoped>
.arena-menu {
  width: 100vw;
  height: 100vh;
  background-image: url('/img/arenaMenu/bg.png');
  background-size: cover;
  font-family: Akrobat;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-start;
  text-transform: uppercase;
  color: #fff;
  .header {
    display: grid;
    grid-template-columns: auto 1fr auto;
    align-items: end;
    justify-content: space-between;
    gap: 5.9rem;
    height: 5.65rem;
    width: 73rem;
    img.icon {
      width: 9.15rem;
      height: 2.9rem;
    }
    .nav {
      width: 100%;
      height: 100%;
      display: flex;
      gap: 2rem;
      .item {
        display: flex;
        align-items: end;
        position: relative;
        height: 100%;
        font-weight: 700;
        font-size: 1rem;
        line-height: 1.2rem;
        color: rgba(255, 255, 255, 0.25);
        &.active {
          background: linear-gradient(180deg, rgba(71, 44, 132, 0.06) 0%, rgba(71, 44, 132, 0) 100%);
          border-top: 0.25rem solid #301934 ;
          color: #fff;
        }
        &:hover {
          color: rgba(255, 255, 255, .45);
        }
      }
    }
    .exit {
      display: flex;
      align-items: center;
      gap: 0.5rem;
      opacity: 0.9;
      img {
        width: 1.2rem;
        height: 1.2rem;
      }
      &:hover {
        opacity: 1;
      }
    }
  }
  .box {
    display: flex;
    justify-content: center;
    width: 73rem;
    height: 38.4rem;
    gap: 4.25rem;
    margin: auto 0 5.75rem 0;
    background: linear-gradient(180deg, rgba(1, 1, 1, 0.81) 0%, rgba(1, 1, 1, 0.45) 114.32%);
    .filters {
      margin-top: 3.35rem;
      width: 21.45rem;
      height: 32.55rem;
      display: flex;
      flex-direction: column;
      box-shadow: inset 0 0.2rem 0 #301934 ;
      padding: 2.95rem 2.25rem 2.25rem 2.25rem;
      background: linear-gradient(180deg, #010101 0%, rgba(1, 1, 1, 0) 167.01%);
      &__header {
        font-weight: 700;
        font-size: 1.8rem;
        line-height: 2.15rem;
        position: relative;
        &::before {
          content: "";
          position: absolute;
          left: -0.7rem;
          top: 50%;
          transform: translateY(calc(-50% - 0.05rem));
          height: 1.3rem;
          width: 0.1rem;
          background: white;
        }
      }

      &__desc {
        font-weight: 700;
        font-size: 0.8rem;
        line-height: 0.95rem;
        color: rgba(255, 255, 255, 0.5);
        margin-top: 1rem;
        width: 10.85rem;
      }

      &__secdesc {
        margin-top: 1.25rem;
        font-weight: 600;
        font-size: 0.8rem;
        line-height: 0.95rem;
      }

      &__main {
        margin-top: 0.65rem;
        display: flex;
        flex-direction: column;
        gap: 0.25rem;
      }

      .btn {
        color: #fff;
        margin-top: 1.4rem;
        width: 16.95rem;
        height: 4.15rem;
      }
    }
  }
}

.arena-header-enter-active, .arena-header-leave-active {
  transition: all .4s;
}
.arena-header-enter, .arena-header-leave-to {
  opacity: 0;
  transform: translateY(-30px);
}
</style>
