<template>
  <div class="rating-page">
    <div class="left-side">
      <Back @click="goBack" />
      <div class="title">{{ loc('familyMenu_62') }}</div>
      <div class="self-position">
        <img src="/img/familyMenu/ico-rate.svg" alt="" />
        <TitleItem
          :name="'Your place in the ranking '"
          :value="[`${getFamilyRank} place`]"
        />
      </div>
    </div>

    <div class="table">
      <div class="title">
        <div class="value">№</div>
        <div class="value">{{ loc('familyMenu_65') }}</div>
        <div class="value">{{ loc('familyMenu_66') }}</div>
        <div class="value">{{ loc('familyMenu_67') }}</div>
        <div class="value">{{ loc('familyMenu_68') }}</div>
        <div class="value">{{ loc('familyMenu_69') }}</div>
        <div class="value">rating</div>
      </div>
      <div class="list">
        <div
          v-for="(item, index) in orgList"
          :key="item.key"
          :class="{
            gold: index === 0,
            silver: index === 1,
            bronze: index === 2,
          }"
          class="item"
        >
          <div class="value">
            <div v-if="index > 2">{{ index + 1 }}</div>
            <img
              v-if="index === 0"
              src="/img/familyMenu/ratingPage/medal-gold.svg"
              alt="gold"
            />
            <img
              v-if="index === 1"
              src="/img/familyMenu/ratingPage/medal-silver.svg"
              alt="silver"
            />
            <img
              v-if="index === 2"
              src="/img/familyMenu/ratingPage/medal-bronze.svg"
              alt="bronze"
            />
          </div>
          <div class="value">{{ item.name }}</div>
          <div class="value">{{ item.owner }}</div>
          <div class="value">{{ item.victories }}</div>
          <div class="value">{{ item.buissCount }}</div>
          <div class="value">{{ item.membersCount }}</div>
          <div class="value">{{ item.rating }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
import Back from '../components/Back.vue'
import TitleItem from '../components/TitleItem.vue'
export default {
  name: 'RatingPage',
  methods: {
    ...mapMutations('familyMenu', ['toggleNav', 'setCurrentPage']),
    goBack: function() {
      this.toggleNav(true)
      this.setCurrentPage('InfoPage')
    },
  },
  computed: {
    ...mapState('familyMenu', ['infoPage']),
    ...mapState('familyMenu/ratingPage', ['orgList']),
    ...mapGetters('localization', ['loc']),
    getFamilyRank: function() {
      return this.$store.getters['familyMenu/ratingPage/getFamilyRank'](
        this.infoPage.familyId
      )
    },
  },
  components: { Back, TitleItem },
}
</script>

<style lang="scss" scoped>
.rating-page {
  display: flex;
  justify-content: center;
  position: absolute;
  left: 0;
  top: 0;
  width: 100vw;
  height: 100vh;
  padding-top: 4.722vh;
  .left-side {
    position: relative;
    z-index: 1;
    margin-left: 8.889vh;
    margin-right: 12.315vh;
    &::before {
      content: '';
      position: absolute;
      top: 50%;
      left: 50%;
      transform: translate(-50%, -50%);
      width: 77.593vh;
      height: 66.111vh;
      background-image: url(/img/familyMenu/ico-rate.svg);
      background-repeat: no-repeat;
      background-size: cover;
      z-index: 0;
      opacity: 0.04;
    }
    .title {
      margin-top: 2.963vh;
      width: 32.963vh;
      height: 14.259vh;
      position: relative;
      z-index: 1;
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
      z-index: 1;
      img {
        width: 5.833vh;
        height: 6.944vh;
      }
    }
  }

  .table {
    margin-right: 17.407vh;
    display: flex;
    flex-direction: column;
    gap: 0.926vh;
    width: 106.204vh;
    .value {
      display: flex;
      justify-content: left;
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
      text-transform: uppercase;
      color: #ffffff;
      &:nth-child(1) {
        width: 9.259vh;
        justify-content: center;
      }
      &:nth-child(2) {
        width: 25.556vh;
      }
      &:nth-child(3) {
        width: 25.926vh;
      }
      &:nth-child(4),
      &:nth-child(5),
      &:nth-child(6),
      &:nth-child(7) {
        width: 11.019vh;
        justify-content: center;
      }
      img {
        width: 2.87vh;
        height: 4.259vh;
      }
    }
    .title {
      display: flex;
      width: 104.815vh;
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
      height: 80vh;
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
        width: 104.815vh;
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
