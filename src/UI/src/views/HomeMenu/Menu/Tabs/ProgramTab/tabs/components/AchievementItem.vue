<template>
  <div :class="[{active: isExpanded}, 'achievement-item']">
    <div :class="[{done: myAchive.CurrentLevel >= achievement.MaxLevel}, 'achievement-item__icon']">
      <img
          :src="`/img/optionsMenu/programTab/${myAchive.CurrentLevel >= achievement.MaxLevel ? 'done' : 'achievements'}.svg`">
    </div>
    <div class="achievement-item__main">
      <div class="achievement-item__info">
        <div class="achievement-item__info-item">
          <div :class="[{active: isExpanded}, 'expand__btn']" @click="isExpanded = !isExpanded"></div>
          <div class="heading">
            <div class="subtitle">achievement</div>
            <div class="title" style="width: 9rem;">{{ achievement.Name }}</div>
          </div>
        </div>
        <div class="achievement-item__info-item" style="margin-right: auto;">
          <div class="img">
            <img src="/img/optionsMenu/programTab/quests.svg">
          </div>
          <div class="heading">
            <div class="subtitle">Task</div>
            <div class="title" style="width: 16rem;">{{ achievement.ShortDesc }}</div>
          </div>
        </div>
        <div class="achievement-item__info-item" v-if="myAchive.CurrentLevel >= achievement.MaxLevel">
          <div class="subtitle" style="color: #5CFF80;">Performed</div>
        </div>
      </div>
      <div class="achievement-item__expanded" v-if="isExpanded">
        <div class="achievement-item__expanded-main">
          <div class="achievement-item__expanded__progress">
            <div class="achievement-item__expanded__progress-heading">
              <div class="category">{{ achievement.ShortDesc }}</div>
              <div class="title">Performed:</div>
            </div>
            <div class="achievement-item__expanded__progress-main">
              <div class="achievement-item__expanded__progress-bar">
                <div
                    :class="[{active: item <= ((myAchive.CurrentLevel / achievement.MaxLevel) * 36)}, 'achievement-item__expanded__progress-bar-item']"
                    v-for="(item, index) in 36"
                    :key="index"
                ></div>
              </div>
              <div class="achievement-item__expanded__progress-value">{{ myAchive.CurrentLevel }} /
                <span>{{ achievement.MaxLevel }}</span></div>
            </div>
          </div>
          <div class="achievement-item__expanded__reward">
            <div class="achievement-item__expanded__reward-heading">
              <div class="category">This is yours</div>
              <div class="title">reward</div>
            </div>
            <div class="achievement-item__expanded__reward-list">
              <div
                  class="achievement-item__expanded__reward-list__item"
                  v-for="(item, index) in achievement.Rewards"
                  :key="index"
              >
                <div class="achievement-item__expanded__reward-list__item-decorate">
                  <img :src="`img/optionsMenu/programTab/${item.Image}.png`">
                </div>
                <div v-if="item.Value" class="achievement-item__expanded__reward-list__item-value">+{{ item.Value }}</div>
              </div>
            </div>
            <button class="item__btn" :disabled="myAchive.GivenReward || myAchive.CurrentLevel < achievement.MaxLevel" @click="acceptAchievement" style="margin-top: 1rem;">{{myAchive.GivenReward ? 'Performed' : 'Get a reward'}}</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'

export default {
  name: 'AchievementItem',
  data: function () {
    return {
      isExpanded: false
    }
  },
  props: {
    achievement: Object,
  },
  computed: {
    ...mapState('optionsMenu', ['myAchievements']),
    ...mapGetters('localization', ['loc']),
    myAchive() {
      return this.myAchievements[this.achievement.AchieveName]
    }
  },
  methods: {
    acceptAchievement: function() {
      window.mp.triggerServer('personalEvents:acceptReward', this.achievement.AchieveName)
    }
  }
}
</script>

<style lang="scss" scoped>
.achievement-item {
  width: 100%;
  display: flex;

  &:not(&:last-child) {
    margin-bottom: 0.5rem;
  }

  &__icon {
    height: 5.05rem;
    min-width: 5.05rem;
    max-width: 5.05rem;
    padding: 1.5rem;
    border: 1px solid rgba(255, 255, 255, 0.1);
    margin-right: 0.5rem;

    &.done {
      padding: 1.2rem 0.95rem 1.2rem 1.15rem;
      &>img {
        filter: invert(19%) sepia(55%) saturate(6271%) hue-rotate(93deg) brightness(102%) contrast(106%);
      }
    }

    & img {
      width: 100%;
      height: 100%;
      object-fit: contain;
    }
  }

  &__main {
    width: 100%;
  }

  &__info {
    display: flex;
    border: 1px solid rgba(255, 255, 255, 0.1);
    background: radial-gradient(at 40% bottom, rgba(255, 255, 255, 0.15) 0%, rgba(0, 0, 0, 0) 50%);
    padding: 1.5rem;

    &-item {
      display: flex;
      align-items: center;

      &:not(&:first-child) {
        margin-left: 2rem;
      }

      .expand__btn {
        position: relative;
        width: 1.3rem;
        height: 1.1rem;
        border-top: 2px solid white;
        border-bottom: 2px solid white;
        margin-right: 1rem;
        overflow: hidden;
        transition: 0.3s ease;
        &:before {
          content: '';
          position: absolute;
          top: calc(50% - 1px);
          left: 0;
          width: 100%;
          border: 1px solid white;
          transition: 0.3s ease;
        }

        &.active {
          transform: rotate(45deg);
          border-bottom: none;
          overflow: visible;
          &:before {
            top: 50%;
            left: calc(-50%);
            transform: rotate(-90deg);
          }
        }
      }

      .subtitle {
        font-size: 0.7rem;
        line-height: 1;
        margin-top: 0;
      }
      .title {
        font-size: 0.9rem;
        line-height: 1.4;
      }
      .img {
        width: 1rem;
        height: 1.3rem;
        margin-right: 0.7rem;
        filter: invert(19%) sepia(55%) saturate(6271%) hue-rotate(93deg) brightness(102%) contrast(106%) drop-shadow(0 0 1.5rem #00FF38);
        & img {
          width: 100%;
          height: 100%;
          object-fit: contain;
        }
      }
    }
  }

  &__expanded {
    margin: 1rem 0;

    &-main {
      display: flex;
    }

    &__progress {
      margin: 0 3.7rem 0 0.7rem;
      &-heading {
        margin-bottom: 0.5rem;
        .category {
          font-size: 0.7rem;
        }
        .title {
          font-size: 1.4rem;
          line-height: 1.2;
        }
      }
      &-value {
        font-weight: 600;
        font-size: 0.85rem;
        line-height: 1;
        margin-top: 0.25rem;
        color: rgba(255,255,255,0.5);
      }
      &-bar {
        display: flex;
        filter: drop-shadow(0 0 1rem rgba(255,255,255,0.4));
        &-item {
          width: 0.25rem;
          border-radius: 1rem;
          height: 2rem;
          background: rgba(255,255,255,0.2);

          &.active {
            background: #46e268;
          }
          &:not(&:last-child) {
            margin-right: 0.3rem;
          }
        }
      }
    }

    &__reward {
      width: 100%;
      &-heading {
        .category {
          font-size: 0.7rem;
        }
        .title {
          font-size: 1.4rem;
          line-height: 1.2;
        }
      }

      &-list{
        display: flex;
        flex-wrap: wrap;
        align-items: start;
        &__item{
          display: flex;
          flex-flow: column nowrap;
          justify-content: space-between;
          align-items: center;
          width: 4rem;
          height: 4rem;
          padding: 0.3rem;
          margin-top: 0.5rem;
          border: 1px solid #5CFF80;
          overflow: hidden;

          &:not(&:last-child){
            margin-right: 0.25rem;
          }

          &-decorate{
            display: block;
            width: 100%;
            height: 65%;
            position: relative;
            margin-bottom: 0.2rem;
            filter: drop-shadow(0 0 1.5rem #5CFF80);

            & img {
              display: inline-block;
              width: 100%;
              height: 100%;
              object-fit: contain;
            }
          }

          &-value{
            display: block;
            font-weight: 700;
            font-size: .7rem;
            line-height: 1.05rem;
            color: #fff;
          }
        }
      }
    }
  }
}
</style>
