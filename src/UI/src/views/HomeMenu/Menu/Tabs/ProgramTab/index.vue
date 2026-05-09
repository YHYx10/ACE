<template>
  <div class="program-tab">
    <div class="program-tab__nav">
      <div class="program-tab__nav-heading">
        <div class="category">section</div>
        <div class="title">{{ currentTab }}</div>
      </div>
      <div
          :class="[{active: item.tab === currentTab}, 'program-tab__nav__item']"
          v-for="(item, index) in navList"
          :key="index"
          @click="setCurrentTab(item.tab)"
      >
        <div class="program-tab__nav__item-icon">
          <img :src="`/img/optionsMenu/programTab/${item.icon}.svg`">
        </div>
        <div class="program-tab__nav__item-heading">
          <div class="category">category</div>
          <div class="title">{{ loc(item.desc) }}</div>
        </div>
      </div>
    </div>
    <component :is="`${currentTab}Tab`"/>
  </div>
</template>

<script>
import ContractsTab from './tabs/ContractsTab'
import QuestsTab from './tabs/QuestsTab'
import AchievementsTab from './tabs/AchievementsTab'
import {mapGetters, mapState} from 'vuex'

export default {
  name: 'ProgramTab',
  components: {
    ContractsTab,
    QuestsTab,
    AchievementsTab
  },
  data: function () {
    return {
      currentTab: 'Contracts',
      navList: [
        {
          icon: 'contracts',
          desc: 'options_program_2',
          tab: 'Contracts'
        },
        {
          icon: 'achievements',
          desc: 'options_program_4',
          tab: 'Achievements'
        },
        {
          icon: 'quests',
          desc: 'options_program_3',
          tab: 'Quests'
        },
      ]
    }
  },
  computed: {
    ...mapState('optionsMenu', ['myContracts', 'moneyEarned']),
    ...mapGetters('localization', ['loc']),
    countOfFinishContracts() {
      return this.myContracts.reduce((sum, current) => sum + current.CountCompleted, 0);
    }

  },
  methods: {
    setCurrentTab: function (value) {
      this.currentTab = value
    }
  },
  mounted() {
    this.currentTab = this.navList[0].tab
  }
}
</script>

<style lang="scss" scoped>
.program-tab {
  display: flex;
  margin-top: 2.5rem;

  &__nav {
    min-width: 16.9rem;
    max-width: 16.9rem;
    margin-right: 1.2rem;

    &-heading {
      margin: 0 0 1rem 0.7rem;
    }

    &__item {
      position: relative;
      display: flex;
      align-items: center;
      border: 1px solid rgba(255,255,255,0.1);
      padding: 1rem 4rem 1rem 2rem;
      margin-bottom: 0.7rem;

      &:before {
        content: '';
        position: absolute;
        width: 100%;
        height: 100%;
        left: 0;
        background: radial-gradient(at center bottom, rgba(255,255,255,0.15) 10%, rgba(0,0,0,0) 75%);
        opacity: 0;
        transition: 0.35s cubic-bezier(0.68,-0.55,0.265,1.55);
      }
      &.active:before {
        opacity: 1;
      }
      &.active .title {
        color: #fff;
      }
      &.active &-icon {
        filter: drop-shadow(0 0 0.8rem #00FF38);
      }
      &:not(&.active):hover:before {
        background: radial-gradient(at center 200%, rgba(255,255,255,0.15) 5%, rgba(0,0,0,0) 70%);
        opacity: 1;
      }
      &:not(&.active):hover &-icon {
        filter: drop-shadow(0 0 1rem #00FF38);
      }


      &-heading {
        margin-left: 1rem;
        .title {
          font-size: 1.3rem;
          line-height: 1.3;
          color: rgba(255,255,255,0.5)
        }
        .category {
          font-size: 0.75rem;
          &:before {
            left: -4rem
          }
        }
      }

      &-icon {
        width: 2rem;
        height: 2rem;
        filter: drop-shadow(0 0 1.2rem #00FF38);
        transition: 0.3s cubic-bezier(0.75, -1, 0.21, -1);
        & img {
          width: 100%;
          height: 100%;
          object-fit: contain;
          filter: invert(19%) sepia(55%) saturate(6271%) hue-rotate(93deg) brightness(102%) contrast(106%);
        }
      }
    }
  }
}
</style>
