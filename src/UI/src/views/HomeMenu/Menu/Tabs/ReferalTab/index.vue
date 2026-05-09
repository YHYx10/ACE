<template>
  <div class="referals-tab">
    <div class="referals-tab__main">
      <div class="referals-tab__main-heading">
        <div class="category">section</div>
        <div class="title">Partnerprogramm</div>
      </div>
      <div class="referals-tab__main-progress">
        <ProgressItem v-for="(item, key, index) in items"
                      :key="index"
                      :item="item"
                      :count="key"
                      :compleeted="key <= referals.total"/>
      </div>
    </div>
    <div class="referals-tab__side">
      <div class="referals-tab__side-heading">
        <div class="category">Du hast eingeladen</div>
        <div class="title">{{ referals.total }} Personen</div>
      </div>
      <div class="referals-tab__side-main">
        <div class="category">Ihr Aktionscode</div>
        <div class="title" id="refcode">
          {{ referals.code }}
          <div class="bonus">
            <div class="bonus-item" v-for="(item, index) in description" :key="index">+{{ loc(item) }}</div>
          </div>
        </div>
      </div>
    </div>
    <img src="/img/optionsMenu/referalsTab/bg.png" class="referals-tab__bgimg">
  </div>
</template>

<script>
import {mapGetters, mapState} from 'vuex'
import ProgressItem from './ProgressItem.vue'
import referalRewards from '../../../../configs/optionsMenu/referalRewards'

export default {
  name: 'ReferalsTab',
  data() {
    return {
      // items:{
      //     "10": {title: "100000 $", img: "money"},
      //     "20": {title: "Уникальная одежда", img: "cloth"},
      //     "30": {title: "Семья бесплатно", img: "grenade"},
      //     "40": {title: "VIP PREMIUM на 2 месяца", img: "ticket"},
      //     "50": {title: "Lada Priora", img: "car"},
      //     "60": {title: "100000 $", img: "money"},
      //     "70": {title: "Уникальная одежда", img: "cloth"},
      //     "80": {title: "Семья бесплатно", img: "grenade"},
      //     "90": {title: "VIP PREMIUM на 2 месяца", img: "ticket"},
      //     "100": {title: "Lada Priora", img: "car"},
      // },
      description: [
        // "mmain:referal:desc:1",
        "mmain:referal:desc:2",
        "mmain:referal:desc:3"
      ]
    }
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("optionsMenu", ["referals"]),
    items() {
      return referalRewards;
    }
  },
  components: {
    ProgressItem
  }
}
</script>

<style lang="scss" scoped>
.referals-tab {
  position: relative;
  display: flex;
  justify-content: space-between;
  padding: 2.5rem 12rem 0 18rem;

  &__main {
    flex: 1 1 100%;
    margin-right: 3rem;

    &-heading {
      margin-bottom: 2rem;
    }

    &-progress {
      height: 38rem;
      padding-right: 1rem;
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
  }

  &__side-main {
    margin-top: 2rem;

    .title {
      display: flex;
      min-width: 17rem;
      max-width: 17rem;
      align-items: center;
      word-break: break-word;

      .bonus {
        margin-left: 1rem;

        &-item {
          letter-spacing: 0.03em;
          font-weight: 600;
          color: #33FF60;
          font-size: 0.9rem;
          line-height: 1.2;
        }
      }
    }
  }

  :not(&__bgimg) {
    z-index: 2;
  }

  &__bgimg {
    width: 69rem;
    height: 28rem;
    position: fixed;
    right: 0;
    bottom: 0;
    z-index: 1;
  }
}
</style>