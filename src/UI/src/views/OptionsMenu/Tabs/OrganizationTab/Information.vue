<template>
  <div class="frac-info">
    <div class="frac-info-heading">
      <div class="category">organization</div>
      <div class="title">{{ fractionName }}</div>
    </div>
    <div class="frac-info-menu">
      <div class="frac-info-menu-item"
           v-for="(item, index) in menuItems"
           v-show="showTab(item.tab)"
           :key="index"
           :class="{'active':index == currentTab}"
           @click="currentTab = index"
      >
        {{ loc(item.name) }}
      </div>
    </div>
    <component :is="menuItems[currentTab].tab"/>
  </div>
</template>

<script>
import {mapState, mapGetters} from 'vuex'
import FracMoney from './FracMoney'
import FracFuel from './FracFuel'
import FracAccess from './FracAccess'

export default {
  data() {
    return {
      names: {
        "-1": "Loading...",
        "0": "None",
        "1": "The Families",
        "2": "The Ballas Gang",
        "3": "Los Santos Vagos",
        "4": "Marabunta Grande",
        "5": "Blood Street",
        "6": "Cityhall",
        "7": "Police",
        "8": "Hospital",
        "9": "FIB",
        "10": "La Cosa Nostra",
        "11": "Mexican Mafia",
        "12": "Yakuza",
        "13": "Amogebuli Mafia",
        "14": "Army",
        "15": "News",
        "16": "The Lost",
        "17": "Government",
        "18": "Referee"
      },
      currentTab: 0,
      menuItems: [
        {name: "mmain:frac:info:mmoney", tab: "FracMoney"},
        {name: "mmain:frac:info:maccess", tab: "FracAccess"},
        {name: "fuel", tab: "FracFuel"},
      ],
      lastCheck: 0
    }
  },
  methods: {
    showTab(tab) {
      if (tab == "FracAccess" && !this.fraction.canAccess) return false;
      if (tab == "FracFuel") return false;
      return true;
    },
  },
  computed: {
    ...mapState('optionsMenu', ['fraction']),
    ...mapGetters('localization', ['loc']),
    fractionName() {
      return this.names[this.fraction.id] || "noInfo"
    }
  },
  components: {
    FracFuel,
    FracMoney,
    FracAccess
  }
}
</script>

<style lang="scss" scoped>
.frac-info {
  position: relative;
  min-width: 23rem;
  max-width: 23rem;
  margin: 0 5rem 0 0.7rem;

  &-menu {
    display: flex;
    justify-content: space-between;
    margin: 2rem 0;

    &-item {
      position: relative;
      cursor: pointer;
      flex: 1 1 33%;
      font-weight: 600;
      display: flex;
      justify-content: center;
      align-items: center;
      padding: 2rem 0;
      border: 1px solid rgba(255,255,255,0.1);
      color: #fff;

      &.active {
        background: rgba(255,255,255,0.05);
      }
    }
  }
}
</style>