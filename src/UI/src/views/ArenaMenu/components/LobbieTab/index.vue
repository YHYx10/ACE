<template>
  <div class="arena-menu__lobbie">
    <div v-if="isLobbieLeave" class="lobbie__modal">
      <span class="modal__text">{{ loc('arena_dm_25') }}</span>
      <div class="modal__actions">
        <DefaultBtn class="actions__btn" @click="leaveLobbie">{{ loc('arena_dm_23') }}</DefaultBtn>
        <DefaultBtn class="actions__btn" @click="setIsLobbieLeave(false)">{{ loc('arena_dm_24') }}</DefaultBtn>
      </div>
    </div>
    <transition appear name="list-left">
      <team-list
        :team="lobbie.redTeam"
        :title="lobbieMode === 'Team fight' ? 'Team Red' : 'arena_dm_27'"
        :color="lobbieMode === 'Team fight' ? '#DB121B' : '#5CFF80'"
        :engTitle="'RED'"
        :owner="lobbie.owner"
        :max="lobbie.maxPlayers"
      />
    </transition>
    <transition appear name="lobbie-desc">
      <div class="lobbie__desc">
        <div class="desc__card" :style="{ 'background-image': `url(${lobbieBg})` }">
          <div class="layer"></div>
          <div class="layer-hover"></div>
          <div class="card__header">
            <span class="header__id">ID: {{ loc(lobbie.id) }}</span>
            <span class="header__title">{{ loc(lobbie.title) }}</span>
          </div>
        </div>
        <div class="desc__item">
          <span class="item__title">{{loc('arena_dm_12')}}</span>
          <span class="item__value">{{ lobbie.rate }}</span>
        </div>
        <div class="desc__item">
          <span class="item__title">{{loc('arena_dm_14')}}</span>
          <span class="item__value">{{ lobbieMode }}</span>
        </div>
        <div class="desc__item" v-if="lobbie.type === 'Team fight'">
          <span class="item__title">{{loc('arena_dm_15')}}</span>
          <span class="item__value">{{ lobbie.rounds }}</span>
        </div>
        <div class="desc__item" v-if="lobbie.type !== 'Gun game'">
          <span class="item__title">{{loc('arena_dm_13')}}</span>
          <span class="item__value">{{ lobbie.weapons }}</span>
        </div>
        <DefaultBtn
          v-show="currentPlayer === lobbie.owner && !lobbie.started"
          class="desc__btn" 
          @click="startGame"
        >
          <span class="btn__text">{{ loc('arena_dm_20') }}</span>
        </DefaultBtn>
      </div>
    </transition>
    <transition appear name="list-right">
      <team-list v-show="lobbieMode === 'Team fight'"
        :owner="lobbie.owner"
        :team="lobbie.greenTeam"
        :title="'Team Green'"
        :color="'#5CFF80'"
        :engTitle="'GREEN'"
        :max="lobbie.maxPlayers"
      />
    </transition>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations } from 'vuex'
import DefaultBtn from '../../../UI/button/DefaultBtn.vue'

import TeamList from './TeamList'

export default {
  name: 'LobbieTab',
  
  components: { TeamList, DefaultBtn },

  data: function () {
    return {
      tooltipText: 'Click to copy!'
    }
  },

  computed: {
    ...mapState('arenaMenu', [
      'lobbies',
      'currentLobbieId',
      'backgrounds',
      'currentPlayer',
      'isLobbieLeave'
    ]),

    ...mapGetters('localization', ['loc']),

    lobbieMode: function () {
      return this.lobbie.type
    },

    lobbie: function () {
      const rightIndex = this.lobbies.findIndex(item => item.id == this.currentLobbieId)
      return this.lobbies[rightIndex]
    },

    lobbieBg: function () {
      const rightItem = this.backgrounds.find(item => item.title == this.lobbie.title)
      return rightItem.image
    }
  },
  methods: {
    ...mapMutations('arenaMenu', ['setIsLobbieLeave']),

    ...mapMutations('sounds', ['play', 'stop']),

    startGame: function () {
      //this.currentLobbieId;
      window.mp.trigger('ARENA::START::LOBBY::VUE', this.currentLobbieId)
    },

    copyToClipboard: function (str) {
      var area = document.createElement('textarea')

      document.body.appendChild(area)
      area.value = str
      area.select()
      document.execCommand("copy")
      document.body.removeChild(area)
      this.tooltipText = 'Copy done!'
    },

    leaveLobbie: function () {
      window.mp.trigger('ARENA::LOBBY::LEAVE::VUE', this.currentLobbieId)
      this.setIsLobbieLeave(false)
      // setCurrentTab('MainTab)
      // setCurrentLobbieId(null)
    }
  },
  mounted: function() {
    this.play({ name: 'choice2', volume: 0.1 })
    // this.play({ name: 'chooseTeam', volume: 0.03, loop: true })
    // window.setData('notifyList/notify', { type: 1, position: 0, message: this.lobbies, time: 10000 })
  },
  
  beforeDestroy: function () {
    // this.stop('chooseTeam')
  }
}
</script>

<style lang="scss" scoped>
.arena-menu__lobbie {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  position: relative;
  padding: 0 2rem 0 2rem;
  .lobbie__modal {
    font-size: 1.1rem;
    z-index: 99;
    color: #FFF;
    width: 25rem;
    text-transform: uppercase;
    text-align: center;
    position: absolute;
    transform: translate(-50%, -50%);
    left: 50%;
    top: 50%;
    padding: 3rem 3rem;
    border-radius: 0.3rem;
    box-shadow: inset 0rem 0rem 3.2rem rgba(71, 44, 132, 0.38);
    background: #050505;
    border: 0.1rem solid #5D3FD3;
    .modal__actions {
      width: 100%;
      display: flex;
      justify-content: space-around;
      margin: 1rem 0 0 0;
      .actions__btn {
        text-transform: uppercase;
        //margin: .5rem 0 0 0;
        width: 7rem;
        height: 2rem;
        color: #fff;
        //transition: all .2s;
       
      }
    }
  }
  .lobbie__desc {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 16.6rem;
    height: 100%;
    padding: 1.5rem 3rem;
    margin: 0 2rem;
    background: linear-gradient(180deg, #010101 0%, rgba(1, 1, 1, 0.7) 100%);
    .desc__card {
      width: 100%;
      height: 18rem;
      background-size: cover;
      background-position: center;
      margin: 0rem 0 1rem 0;
      display: flex;
      padding: 1rem;
      position: relative;
      text-transform: uppercase;
      .layer-hover {
        position: absolute;
        box-shadow: inset 0 -0.2rem 0 #DB121B;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: linear-gradient(360deg, rgba(255, 0, 11, 0.33) 0%, rgba(255, 0, 11, 0) 50%);
      }
      .layer {
        background: rgba(1, 1, 1, 0.2);
        box-shadow: inset 0rem 0rem 6.5rem #000000;
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
      }
      .card__header {
        display: flex;
        flex-direction: column;
        color: #fff;
        font-size: 1.1rem;
        text-shadow: 0 0 0.4rem rgba(0, 0, 0, 0.47);
        z-index: 1;
        .header__id {
          margin-top: auto;
          font-weight: 700;
          font-size: 1rem;
          line-height: 1.2rem;
        }
        .header__title {
          font-weight: 700;
          font-size: 1.8rem;
          line-height: 2.15rem;
        }
        .header__icon {
          width: 1.25rem;
          height: 1.3rem;
        }
      }
    }
    .desc__item {
      text-transform: uppercase;
      border-bottom: 0.05rem solid;
      border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1;
      padding: 0.5rem;
      font-weight: 700;
      font-size: 0.8rem;
      line-height: 0.95rem;
      width: 100%;
      .item__title {
        color: #FFF;
        text-align: left;
      }
      .item__value {
        float: right;
      }
    }
    .desc__room-id {
      padding: 0.2rem 0.6rem;
      border-radius: 0.4rem;
      position: relative;
      transition: all .2s;
      margin: 1rem 0 0 0;
      .room-id__tooltip {
        position: absolute;
        transform: translate(-50%, -50%);
        left: 50%;
        top: 200%;
        width: 8rem;
        height: 2rem;
        display: flex;
        align-items: center;
        justify-content: center;
        background: rgb(0, 0, 0);
        color: #fff;
        border-radius: 0.4rem;
        opacity: 0;
        &::after {
          content: '';
          position: absolute;
          width: 0.6rem;
          height: 0.6rem;
          top: 0;
          left: 50%;
          transform: translate(-50%,-50%) rotate(45deg);
          background: rgb(0, 0, 0);
        }
      }
      &:hover {
        background: rgb(212, 212, 212);
        .room-id__tooltip {
          opacity: 1;
        }
      }
    }
    .desc__btn {
      margin-top: 2.95rem;
      width: 10rem;
      height: 4.15rem;
      color: #fff;
    }
  }
}
.lobbie-desc-enter-active, .lobbie-desc-leave-active {
  transition: all .4s;
}
.lobbie-desc-enter, .lobbie-desc-leave-to {
  opacity: 0;
  transform: translateY(1.5rem);
}
.list-right-enter-active, .list-right-leave-active {
  transition: all .4s;
}
.list-right-enter, .list-right-leave-to {
  opacity: 0;
  transform: translateX(1.5rem);
}
.list-left-enter-active, .list-left-leave-active {
  transition: all .4s;
}
.list-left-enter, .list-left-leave-to {
  opacity: 0;
  transform: translateX(-1.5rem);
}
</style>
