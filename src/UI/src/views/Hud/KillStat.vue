<template>
  <div class="killstat__list">
    <span class="list__timer" v-if="killStatTime !== null && killStatTime !== 0">{{ prettyTime }}</span>
    <div
      :class="[{ current: item.id === killstatCurrentUser.id }, 'list__item']"
      v-for="(item, index) in currentItems"
      :key="index"
    >
      <div class="item__content">
        <div class="content__stat">
          <span class="stat__kills">{{ item.kills }}</span>
          <span class="stat__separator">/</span>
          <span class="stat__deaths">{{ item.deaths }}</span>
        </div>
        <span class="content__username">{{ item.username }}</span>
        <span class="content__place">{{ index + 1 }}</span>
      </div>
    </div>
    <div class="list__item current">
      <div class="item__content">
        <div class="content__stat">
          <span class="stat__kills">{{ currentUser.kills }}</span>
          <span class="stat__separator">/</span>
          <span class="stat__deaths">{{ currentUser.deaths }}</span>
        </div>
        <span class="content__username">{{ currentUser.username }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState } from 'vuex'

export default {
  name: 'KillStat',

  data: function () {
    return {
      timer: null
    }
  },

  computed: {
    ...mapState('hud', [
      'killstatItems',
      'killStatTime',
      'killstatCurrentUser'
    ]),

    currentItems: function () {
      return this.killstatItems.slice(0, 3)
    },

    currentUser: function() {
      return this.killstatItems.find(item => item.id === this.killstatCurrentUser.id)
    },

    prettyTime () {
      let time = this.killStatTime / 60
      let minutes = parseInt(time)
      let secondes = Math.round((time - minutes) * 60)

      if (minutes < 10) {
        minutes = '0'+minutes
      }
      if (secondes < 10) {
        secondes = '0'+secondes
      }

      return `${minutes}:${secondes}`
		}
  }
}
</script>

<style lang="scss" scoped>
.killstat__list {
  display: flex;
  flex-direction: column;
  align-items: center;
  position: absolute;
  bottom: 1rem;
  right: 1rem;
  color: #fff;
  gap: 0.5rem;
  font-family: Akrobat;
  .list__timer {
    font-weight: bold;
    letter-spacing: .05rem;
    transform: rotate(2.2deg);
  }
  .list__item {
    display: flex;
    align-items: center;

    // background-image: url('../../../public/img/hudKillStat/stat-last.png');
    background-size: contain;
    background-repeat: no-repeat;
    border-bottom: 0.15rem solid;
    
    background: linear-gradient(270deg, rgba(2, 2, 5, 0.7) 82.63%, rgba(2, 2, 5, 0) 100%),
      linear-gradient(270deg, #FF4E55 8.86%, rgba(255, 0, 0, 0.5) 30.99%, rgba(255, 0, 11, 0) 94.46%);
    border-image: linear-gradient(270deg, #FF4E55 8.86%, rgba(255, 0, 0, 0.5) 30.99%, rgba(255, 0, 11, 0) 94.46%) 1;
    padding: 0.9rem 0.75rem;
    width: 18.5rem;
    height: 3.5rem;
    &.current { 
      background: linear-gradient(270deg, rgba(2, 2, 5, 0.7) 82.63%, rgba(2, 2, 5, 0) 100%),
      linear-gradient(270deg, #5CFF80 8.86%, rgba(92, 255, 128, 0.5) 30.99%, rgba(92, 255, 128, 0) 94.46%);
      border-image: linear-gradient(270deg, #5CFF80 8.86%, rgba(92, 255, 128, 0.5) 30.99%, rgba(92, 255, 128, 0) 94.46%) 1;
      .item__content .content__stat {
        color: #1fbf75;
      }
    }
    &:last-child {
      .item__content .content__username {
        // margin: 0 1.6rem 0 0;
      }
    }
    .item__content {
      display: flex;
      justify-content: flex-end;
      width: 100%;
      height: 1.7rem;
      padding: 0 0.75rem 0 1.4rem;
      border-right: 0.15rem solid #5CFF80;
      // transform: rotate(2.2deg);
      // padding: 1rem .5rem 0 1.5rem;
      align-items: center;
      .content__stat {
        color: #ff4654;
        display: flex;
        font-weight: 700;
        font-size: 1rem;
        line-height: 1.2rem;
      }
      .content__username {
        text-transform: uppercase;
        margin-left: 2rem;
        margin-right: auto;
        font-weight: 700;
        font-size: 1rem;
        line-height: 1.2rem;
        text-align: left;
      }
      .content__place {
        font-weight: 700;
        font-size: 1.8rem;
        line-height: 2.15rem;
        text-transform: uppercase;
        color: #FFFFFF;
        text-shadow: 0rem 0rem 0.55rem #FFFFFF;
      }
    }
    
  }
}
</style>
