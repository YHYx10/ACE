<template>
  <div class="achievements-tab">
      <div class="achievements-tab__list">
        <AchievementItem
            v-for="(item, index) in Object.keys(achieves)"
            :key="index"
            :achievement="achieves[item]"
        />
      </div>
      <div class="achievements-tab__progress">
        <div class="achievements-tab__progress-header">
          <div class="achievements-tab__progress-heading">
            <div class="category">section</div>
            <div class="title">progress</div>
          </div>
          <div class="achievements-tab__progress-heading">
            <div class="category">all</div>
            <div class="title">{{ completedAchievements }} from {{ Object.keys(achieves).length }}</div>
          </div>
        </div>
        <div class="achievements-tab__progress-bar">
          <div
              :class="[
                { active: item < ((completedAchievements / Object.keys(achieves).length) * 36 )},
                'achievements-tab__progress-bar__item',
              ]"
              v-for="(item, index) in 36"
              :key="index"
          ></div>
        </div>
      </div>
  </div>
</template>

<script>
import {mapGetters, mapState} from 'vuex'
import AchievementItem from './components/AchievementItem'
import achievesConfig from '../../../../../configs/personalEvents/achievesConfig'

export default {
  name: 'AchievementsTab',
  components: {
    AchievementItem,
  },
  computed: {
    ...mapState('optionsMenu', ['myAchievements']),
    ...mapGetters('localization', ['loc']),
    completedAchievements() {
      return Object.keys(this.myAchievements).filter(key => this.achieves[key] !== undefined && this.myAchievements[key].CurrentLevel >= this.achieves[key].MaxLevel).length
    },
    achieves() {
      return achievesConfig
    }
  },
  mounted() {
  }
}
</script>

<style lang="scss" scoped>
.achievements-tab {
  flex: 1 1 100%;
  display: flex;

  &__list {
    width: 47rem;
    height: 37rem;
    margin-top: 4.7rem;
    padding-right: 0.5rem;
    overflow-y: auto;

    scrollbar-width: thin;
    scrollbar-color: #5cff80 #444444;

    &::-webkit-scrollbar {
      display: block;
      width: 0.1rem;
      height: 0;
    }

    &::-webkit-scrollbar-track {
      background: #444444;
    }

    &::-webkit-scrollbar-thumb {
      background-color: #5cff80;
    }
  }

  &__progress {
    margin-left: 1rem;
    display: flex;
    flex-direction: column;

    &-header {
      display: flex;
      margin-bottom: 1rem;
    }

    &-heading:last-child {
      margin-left: 2rem;
      display: flex;
      flex-flow: column nowrap;
      justify-content: space-between;
      .category:before {
        display: none;
      }
      .title {
        font-size: 1.8rem;
        line-height: 1.3;
      }
    }

    &-bar {
      width: 11rem;
      height: 100%;
      display: flex;
      flex-flow: column-reverse nowrap;
      justify-content: space-between;

      &__item {
        width: 100%;
        height: 0.5rem;
        background: rgba(255, 255, 255, 0.15);
        border-radius: 1rem;
        &:not(&:last-child) {
          margin-top: 0.5rem;
        }

        &.active {
          background: #5CFF80;
          filter: drop-shadow(0 0 0.5rem rgba(92, 255, 128, 0.7));
        }
      }
    }
  }
}
</style>
