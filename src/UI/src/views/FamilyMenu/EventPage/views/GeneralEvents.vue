<template>
  <transition name="slide-fade-reversed">
    <div v-if="showNavElem == 'GeneralEvents'" class="general-events">
      <div class="organization-block">
        <div class="title">
          {{
            isCompleatedActive
              ? loc('familyMenu_EventCompleate')
              : loc('familyMenu_EventNew')
          }}
        </div>
        <div class="desc">{{ loc('fam:btl:gen:desc') }}.</div>
        <div class="nav-list">
          <div
            :class="[
              { active: isCompleatedActive === false },
              'nav-list__item',
            ]"
            @click="setCurrentEvents(false)"
          >
            {{ loc('fam:btl:gen:new') }}
          </div>
          <div
            :class="[{ active: isCompleatedActive === true }, 'nav-list__item']"
            @click="setCurrentEvents(true)"
          >
            {{ loc('fam:btl:gen:compl') }}
          </div>
        </div>
      </div>
      <div class="content" v-if="currentBattleData">
        <div class="slider">
          <div
            class="preview"
            :style="{
              backgroundImage:
                'url(/img/familyMenu/eventsPage/places/' +
                getPlace.img +
                '.png)',
            }"
          ></div>
          <div class="switchers">
            <div @click="prevMP" class="arrow">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="15"
                height="18"
                fill="none"
                viewBox="0 0 15 18"
              >
                <path
                  fill="#fff"
                  d="m1.496 10.664 9.543 6.362c1.684 1.123 3.78-.68 2.92-2.513l-2.186-4.664a2 2 0 0 1 0-1.698l2.186-4.664c.86-1.833-1.236-3.636-2.92-2.513L1.496 7.336a2 2 0 0 0 0 3.328Z"
                />
              </svg>
            </div>
            <div class="value">
              {{ currentBattle + 1 }}/{{ currentEvents.length }}
            </div>
            <div @click="nextMP" class="arrow">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="15"
                height="18"
                fill="none"
                viewBox="0 0 15 18"
                style="transform: rotate(-180deg);"
              >
                <path
                  fill="#fff"
                  d="m1.496 10.664 9.543 6.362c1.684 1.123 3.78-.68 2.92-2.513l-2.186-4.664a2 2 0 0 1 0-1.698l2.186-4.664c.86-1.833-1.236-3.636-2.92-2.513L1.496 7.336a2 2 0 0 0 0 3.328Z"
                />
              </svg>
            </div>
          </div>
        </div>
        <div class="right-col">
          <div class="list">
            <TitleItem name="date" :value="[getDate]" :fontSize="24" />
            <TitleItem name="time" :value="[getTime]" :fontSize="24" />
            <TitleItem
              name="place"
              :value="[loc(getPlace.name)]"
              :fontSize="24"
            />
            <TitleItem
              name="description"
              :value="[loc(currentBattleData.Description)]"
              :fontSize="24"
            />
          </div>
          <DefaultBtn v-if="isCompleatedActive" @click="showBattleResults"
            >Battle statistics</DefaultBtn
          >
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
import { mapState, mapMutations, mapGetters } from 'vuex'
import TitleItem from '../../components/TitleItem.vue'
import DefaultBtn from '../../../UI/button/DefaultBtn.vue'

export default {
  name: 'GeneralEvents',
  data() {
    return {
      isCompleatedActive: false,
      hoveredElem: null,
      currentBattle: 0,
      currentBattleData: null,
    }
  },
  components: {
    TitleItem,
    DefaultBtn,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('familyMenu/eventsPage', [
      'showNavElem',
      'globalEvents',
      'placesList',
    ]),
    currentEvents() {
      return this.globalEvents
        .filter((item) => item.IsFinished == this.isCompleatedActive)
        .sort((a, b) => a.Date - b.Date)
    },
    getTime() {
      let date = new Date(this.currentBattleData.Date * 1000)
      var options = { timezone: 'UTC', hour: 'numeric', minute: 'numeric' }
      return date.toLocaleString('ru', options)
    },
    getDate() {
      let date = new Date(this.currentBattleData.Date * 1000)
      var options = {
        timezone: 'UTC',
        year: 'numeric',
        month: 'numeric',
        day: 'numeric',
      }
      return date.toLocaleString('ru', options)
    },
    getPlace() {
      return this.placesList.find(
        (item) => item.id == this.currentBattleData.Location
      )
    },
  },
  methods: {
    ...mapMutations('familyMenu', ['toggleNav']),
    ...mapMutations('familyMenu/eventsPage', ['toggleBattleResults']),
    showBattleResults() {
      this.toggleNav(false)
      this.toggleBattleResults(this.currentBattleData)
    },
    setCurrentEvents(value) {
      this.currentBattle = 0
      this.isCompleatedActive = value
      this.currentBattleData = this.currentEvents[0]
    },
    marqueeBounced(elem) {
      console.log(elem)
    },
    prevMP() {
      this.currentBattle--
      if (this.currentBattle < 0)
        this.currentBattle = this.currentEvents.length - 1
      if (this.currentEvents.length > this.currentBattle)
        this.currentBattleData = this.currentEvents[this.currentBattle]
    },
    nextMP() {
      this.currentBattle++
      if (this.currentBattle >= this.currentEvents.length)
        this.currentBattle = 0
      if (this.currentEvents.length > this.currentBattle)
        this.currentBattleData = this.currentEvents[this.currentBattle]
    },
  },
  mounted() {
    this.currentBattleData = this.currentEvents[0]
    this.currentBattle = 0
  },
}
</script>

<style lang="scss" scoped>
.slide-fade-reversed-enter-active,
.slide-fade-reversed-leave-active {
  transition: all 0.3s ease;
}
.slide-fade-reversed-enter,
.slide-fade-reversed-leave-to {
  transform: translateX(-3rem);
  opacity: 0;
}

div,
button {
  font-family: 'Akrobat';
}

button {
  width: 23.241vh;
  height: 5.833vh;
  color: #ffffff;
  font-weight: 700;
  font-size: 1.852vh;
}

.organization-block {
  display: flex;
  flex-flow: column;
  width: 32.593vh;
  color: #ffffff;
  text-transform: uppercase;
  .title {
    margin-bottom: 0.741vh;
    font-weight: 700;
    font-size: 5.926vh;
    line-height: 7.407vh;
  }
  .desc {
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.315vh;
    color: rgba(255, 255, 255, 0.44);
    margin-bottom: 2.222vh;
  }
  .nav-list {
    display: flex;
    flex-flow: column;
    width: 100%;
    gap: 0.926vh;
    &__item {
      display: flex;
      align-items: center;
      justify-content: flex-start;
      padding: 0 1.778vh;
      font-weight: 300;
      color: rgb(255, 255, 255);
      transition: 0.2s;
      width: 32.407vh;
      height: 8.148vh;
      border: 0.093vh solid transparent;
      border-right: none;
      font-style: normal;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.778vh;
      border-image: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.2) 0%,
          rgba(255, 255, 255, 0) 100%
        )
        1 1 round;
      &::before {
        content: '';
        height: 2.517vh;
        width: 0.194vh;
        background: #ffffff;
        margin-right: 1.852vh;
        box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
      }
      &:hover,
      &.active {
        background: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.15) 0%,
          rgba(255, 255, 255, 0) 100%
        );
        transition: 0.2s;
      }
    }
  }
}

.general-events {
  display: flex;

  .content {
    display: flex;
    gap: 1.944vh;
    margin-left: 10.278vh;
    .slider {
      display: flex;
      flex-direction: column;
      gap: 0.926vh;
      .preview {
        width: 59.352vh;
        height: 36.19vh;
        background-color: rgb(121, 121, 121);
        background-size: cover;
      }
      .switchers {
        display: flex;
        justify-content: space-between;
        align-items: center;
        .arrow {
          display: flex;
          justify-content: center;
          align-items: center;
          width: 6.111vh;
          height: 6.111vh;
          background: rgba(255, 255, 255, 0.1);
          svg {
            width: 1.389vh;
            height: 1.667vh;
          }
          &:hover {
            background: rgba(255, 255, 255, 0.14);
          }
        }
        .value {
          font-weight: 700;
          font-size: 2.222vh;
          line-height: 2.778vh;
        }
      }
    }

    .right-col {
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      height: 36.204vh;
      .list {
        display: flex;
        flex-direction: column;
        gap: 1.481vh;
      }
    }
  }
}
</style>
