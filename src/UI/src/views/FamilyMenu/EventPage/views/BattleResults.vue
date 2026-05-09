<template>
  <div class="battle-results-content">
    <Back @click="hideResults()" />

    <div class="content">
      <img class="ico-rate" src="/img/familyMenu/ico-rate.svg" alt="" />
      <div class="title">
        {{loc('fam:btl:res:win')}}
      </div>
      <div class="name">
        {{currentEvent.WinnerFamilyName}}
      </div>
      <div class="row">
        <TitleItem name="date" :value="[getDate]" :fontSize="24" />
        <TitleItem name="time" :value="[getTime]" :fontSize="24" />
        <TitleItem name="place" :value="[loc(getPlace.name)]" :fontSize="24" />
      </div>
      <div class="table-title">
    Erstklassiges Mord
      </div>
      <div class="list">
        <div
          class="item"
          v-for="(item, index) in currentKillLog"
          :key="item.UUID"
        >
          <div class="position">
            {{ index + 1 }}
          </div>
          <div class="member-name">
            {{item.Name}}
          </div>
          <div class="kills">
            <img src="/img/familyMenu/eventsPage/kill.svg" alt="" />
            <div class="count">
              {{item.Kills}}
            </div>
          </div>
        </div>
      </div>
    </div>
</div>
</template>

<script>
import { mapMutations, mapState, mapGetters } from 'vuex'
import Back from '../../components/Back'
import TitleItem from '../../components/TitleItem.vue'

export default {
  name: 'BattleResults',
  data() {
    return {
      show: false,
      exitBtnHovered: false,
    }
  },
  components: {
    Back,
    TitleItem,
  },
  mounted() {
    this.show = true
  },
  destroyed() {
    this.toggleNav(true)
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('familyMenu/eventsPage', ['currentEvent', 'placesList']),
    currentKillLog() {
      return this.currentEvent.KillLog.slice().sort((a, b) => b.Kills - a.Kills)
    },
    getTime() {
      let date = new Date(this.currentEvent.Date * 1000)
      var options = {
        timezone: 'Europe/Moscow',
        hour: 'numeric',
        minute: 'numeric',
      }
      return date.toLocaleString('ru', options)
    },
    getDate() {
      let date = new Date(this.currentEvent.Date * 1000)
      var options = {
        timezone: 'Europe/Moscow',
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
      }
      return date.toLocaleString('ru', options)
    },
    getPlace() {
      return this.placesList.find(
        (item) => item.id == this.currentEvent.Location
      )
    },
  },
  methods: {
    ...mapMutations('familyMenu/eventsPage', ['toggleBattleResults']),
    ...mapMutations('familyMenu', ['toggleNav']),
    hideResults() {
      this.show = false
      setTimeout(() => {
        this.toggleBattleResults(null)
      }, 300)
    },
  },
}
</script>

<style lang="scss" scoped>

div,
button {
  font-family: 'Akrobat';
  text-transform: uppercase;
  font-weight: 700;
}

.battle-results-content {
  display: flex;
  position: absolute;
  top: 0;
  left: 0;
  justify-content: center;
  align-items: center;
  width: 100vw;
  height: 100vh;
  .back {
    position: absolute;
    justify-self: left;
    align-self: flex-start;
    top: 4.722vh;
    left: 8.426vh;
  }
  .content {
    display: flex;
    flex-direction: column;
    align-items: center;
    width: 77.593vh;
    height: 100%;
    position: relative;
    &::before {
      content: '';
      top: 18.889vh;
      background-image: url(/img/familyMenu/ico-rate.svg);
      background-size: cover;
      background-repeat: no-repeat;
      position: absolute;
      width: 77.593vh;
      height: 66.111vh;
      z-index: -1;
      opacity: 0.04;
    }

    .ico-rate {
      margin-top: -1.852vh;
      width: 11.759vh;
      height: 10.093vh;
      margin-top: 22.037vh;
    }
    .title {
      margin-top: 3.148vh;
      margin-bottom: 0.093vh;
      font-size: 3.704vh;
      line-height: 4.63vh;
    }
    .name {
      font-size: 5.926vh;
      line-height: 7.407vh;
    }
    .row {
      margin-top: 2.222vh;
      display: flex;
      gap: 3.426vh;
    }

    .table-title {
      margin-bottom: 1.204vh;
      font-weight: 700;
      font-size: 2.963vh;
      line-height: 3.704vh;
      margin-top: 3.889vh;
      color: #a0ff98;
    }

    .list {
      .item {
        display: flex;
        align-items: center;
        width: 43.148vh;
        height: 6.667vh;
        position: relative;
        border: 0.093vh solid transparent;
        border-image: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.2) 0%,
          rgba(255, 255, 255, 0) 100%
        )
        1 1 round;
        overflow: hidden;
        font-size: 1.852vh;
        line-height: 2.315vh;
        &::before {
          content: '';
          position: absolute;
          bottom: -8.056vh;
          transform: translate(0, 100%);
          width: 43.148vh;
          height: 4.815vh;
          background: rgba(255, 255, 255, 0.55);
          filter: blur(8.241vh);
        }
        .position {
          width: 8.148vh;
          display: flex;
          justify-content: center;
          margin-right: 0.926vh;
          margin-left: 0.185vh;
          font-size: 2.963vh;
          line-height: 3.704vh;
        }
        .member-name {
          width: 24.259vh;
        }
        .kills {
          display: flex;
          align-items: center;
          gap: 0.648vh;
          img {
            width: 2.87vh;
            height: 2.87vh;
          }
        }
      }
    }
  }
}
</style>
