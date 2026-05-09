<template>
  <div class="rating-page">
    <div class="left-side">
      <div class="title">{{ loc('fam:menu:memb:rait:1') }}</div>
      <div class="self-position">
        <img src="/img/familyMenu/ico-rate2.svg" alt="" />
        <TitleItem
          :name="'Your place in the ranking'"
          :value="[`${getMemberPosition} place`]"
        />
        <img class="arrow-circle" src="/img/familyMenu/ratingPage/arrow-circle.svg" alt="">
      </div>
    </div>

    <div class="table">
      <div class="title">
        <div class="value">№</div>
        <div class="value">Status</div>
        <div class="value">Mitglied der Organisation</div>
        <div class="value">ID</div>
        <div class="value">
          <img src="/img/familyMenu/ratingPage/arrow-up.svg" alt="">
        </div>
        <div class="value">
          <img src="/img/familyMenu/ratingPage/arrow-down.svg" alt="">
        </div>
        <div class="value">Gesamt</div>
      </div>
      <div class="list">
        <div v-for="(item, index) in sortMembers" :key="item.key" class="item">
          <div class="value">
            <div>{{ index + 1 }}</div>
          </div>
          <div class="value"><div class="online-point" :class="{green: item.online}" />{{ item.online ? 'Online' : 'offline' }}</div>
          <div class="value">{{ item.nickname }}</div>
          <div class="value">{{ item.id }}</div>
          <div class="value">+{{ item.up }}</div>
          <div class="value">{{ item.down }}</div>
          <div class="value">{{ item.rating }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import TitleItem from '../components/TitleItem.vue'
export default {
  name: 'RatingTab',

  components: {
    TitleItem,
  },

  data: function() {
    return {
      currentRating: null,
    }
  },

  computed: {
    ...mapState('familyMenu/membersPage', ['members']),
    ...mapState('familyMenu', ['currentMemberId']),
    ...mapGetters('localization', ['loc']),
    getMemberRank: function() {
      return this.$store.getters['familyMenu/ratingPage/getMemberRank'](
        this.infoPage.familyId
      )
    },
    getMemberPosition() {
      return this.sortMembers.findIndex(v => v.id === this.currentMemberId) + 1
    },

    sortMembers: function() {
      let newArr = this.members
        .slice(0, this.members.length)
        .sort(function(a, b) {
          return b.rating - a.rating
        })
      return newArr
    },
  },
}
</script>

<style lang="scss" scoped>
.rating-page {
  display: flex;
  position: relative;
  padding-top: 5.093vh;
  .left-side {
    position: relative;
    &::before {
      content: '';
      position: absolute;
      top: 25%;
      right: 11.5%;
      transform: translate(0, -50%);
      width: 51.481vh;
      height: 50.833vh;
      background-image: url(/img/familyMenu/ico-rate2.svg);
      background-repeat: no-repeat;
      background-size: cover;
      z-index: -1;
      opacity: 0.04;
    }
    .title {
      margin-top: 2.963vh;
      width: 32.963vh;
      height: 14.259vh;
      position: relative;
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      font-size: 5.926vh;
      line-height: 7.13vh;
      color: #ffffff;
    }
    .self-position {
      margin-top: 5.093vh;
      display: flex;
      gap: 1.852vh;
      position: relative;
      .arrow-circle {
        display: none;
        position: absolute;
        bottom: 0;
        transform: translate(0, 50%);
        right: 1.852vh;
      }
    }
  }

  .table {
    margin-left: 9.074vh;
    display: flex;
    flex-direction: column;
    gap: 0.926vh;
    width: 93.889vh;
    .value {
      display: flex;
      justify-content: left;
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
      text-transform: uppercase;
      align-items: center;
      color: #ffffff;
      img {
        position: absolute;
        bottom: 0;
      }
      .online-point {
        width: 0.741vh;
        height: 0.741vh;
        border-radius: 100%;
        background: rgba(255, 255, 255, 0.15);
        margin-right: 0.741vh;
        &.green {
          background: #5cff80;
        }
      }
      &:nth-child(1) {
        width: 9.815vh;
        justify-content: center;
      }
      &:nth-child(2) {
        width: 8.333vh;
        justify-content: center;
      }
      &:nth-child(3) {
        margin-left: 4.074vh;
        width: 26.204vh;
      }
      &:nth-child(4){
        width: 11.019vh;
        justify-content: center;
      }
      &:nth-child(5) {
        color: #A0FF98;
      }
      &:nth-child(6) {
        color: #FF7D7D;
      }
      &:nth-child(5),
      &:nth-child(6),
      &:nth-child(7) {
        width: 11.019vh;
        justify-content: center;
      }
    }
    .title {
      display: flex;
      width: 92.5vh;
      height: 6.667vh;
      position: relative;
      align-items: center;
      overflow: hidden;
      &::before {
        content: '';
        position: absolute;
        width: 100%;
        height: 12.778vh;
        transform: translate(0, -100%);
        background: rgba(255, 255, 255, 0.55);
        filter: blur(8.241vh);
        top: -5.463vh;
      }
    }

    .list {
      width: 100%;
      height: 53.333vh;
      overflow-y: scroll;
      &::-webkit-scrollbar {
        width: 0.463vh;
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.04);
      }
      &::-webkit-scrollbar-thumb {
        background: #301934 ;
      }
      .item {
        display: flex;
        align-items: center;
        width: 92.5vh;
        height: 6.667vh;
        position: relative;
        box-sizing: border-box;
        border: 0.093vh solid rgba(255, 255, 255, 0);
        border-image-source: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.09) 0%,
          rgba(255, 255, 255, 0) 101.25%
        );
        border-image-slice: 1;
        border-image-width: 1;
        border-image-outset: 0;
        border-image-repeat: stretch;
        overflow: hidden;
        &::before {
          content: '';
          position: absolute;
          background: rgba(255, 255, 255, 0.55);
          filter: blur(8.241vh);
          border-radius: 80%;
          top: 10.278vh;
          left: 50%;
          transform: translate(-50%, 100%);
          width: 100.093vh;
          height: 4.815vh;
          z-index: -1;
        }
        &.gold::before {
          top: 5.648vh;
          background: #ffca42;
        }
        &.silver::before {
          top: 5.648vh;
          background: #ffffff;
        }
        &.bronze::before {
          top: 5.648vh;
          background: #ffa767;
        }
      }
    }
  }
}
</style>
