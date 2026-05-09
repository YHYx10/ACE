<template>
  <div class="rent-vehicle">
    <div class="content">
      <div class="header">
        <TitleComponent
          :titlePrimary="'Car'"
          :titleSecondary="'Rental'"
        />
        <div class="nav">
          <div
            v-for="category in currentCategories"
            :key="category"
            @click="setCurrentFilter(category)"
            :class="[{ active: category === currentFilter }, 'item']"
          >
            <img
              :src="`/img/rentVehicle/${getCategory(category).Icon}.png`"
              alt=""
            />
            <div class="name">{{ loc(getCategory(category).Name) }}</div>
          </div>
        </div>
      </div>
      <div class="list">
        <VehicleCard
          v-for="item in filteredList"
          :key="item.id"
          :item="item"
          :category="currentFilter"
          @selectChoice="selectChoice"
        />
      </div>
    </div>
  </div>
</template>

<script>
import categories from '../../configs/vehicleRent/categories'
import { mapState, mapGetters } from 'vuex'
import TitleComponent from '../UI/components/TitleComponent.vue'
import VehicleCard from './VehicleCard.vue'

export default {
  name: 'RentVehicle',
  components: {
    TitleComponent,
    VehicleCard,
  },
  data() {
    return {
      currentChoice: null,
      currentFilter: null,
    }
  },
  computed: {
    ...mapState('rentVehicle', ['categoriesPoint']),
    ...mapGetters('localization', ['loc']),
    filteredList() {
      if (this.getCategory(this.currentFilter))
        return this.getCategory(this.currentFilter).Cars
      return []
    },
    currentCategories() {
      return this.categoriesPoint
        .slice()
        .filter((item) => categories[item] !== undefined)
    },
  },
  mounted() {
    this.currentFilter = this.categoriesPoint[0]
  },
  methods: {
    setCurrentFilter(value) {
      if (this.currentFilter !== value) {
        this.currentFilter = value
        this.currentChoice = null
      }
    },
    selectChoice(model) {
      this.currentChoice = model
    },
    getCategory(key) {
      return categories[key]
    },
  },
}
</script>

<style lang="scss" scoped>

.rent-vehicle {
  width: 100vw;
  height: 100vh;
  background: rgba($color: #000000, $alpha: .85);
  background-repeat: no-repeat;
  background-size: cover;
  display: flex;
  align-items: center;

  

  &::before {
    content: '';
    position: absolute;
    z-index: 0;
    background: url('/img/rentVehicle/key.png');
    background-size: cover;
    width: 30.648vh;
    height: 41.111vh;
    right: 0;
    bottom: 0;
  }

  .content {
    margin: 0 auto;
    width: 133.796vh;
    display: flex;
    flex-direction: column;
    gap: 4.63vh;
    position: relative;
  }
  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: 132.407vh;
  }

  .nav {
    display: flex;
    gap: 1.852vh;
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.481vh;
    line-height: 1.759vh;
    text-align: center;
    text-transform: uppercase;
    .item {
      width: 12.778vh;
      height: 10.185vh;
      background: rgba(255, 255, 255, 0.02);
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      gap: 0.741vh;
      img {
        width: 4.537vh;
        height: 4.537vh;
      }
      .name {
        color: white;
      }
      &:hover {
        background: rgba(255, 255, 255, 0.08);
      }
      &.active {
        background: linear-gradient(180deg, #301934  0%, #591b87 100%);
        box-shadow: 0vh 0vh 1.852vh rgba(71, 44, 132, 0.5);
      }
    }
  }

  .list {
    display: flex;
    height: 65.185vh;
    flex-wrap: wrap;
    gap: 2.778vh;
    overflow-y: scroll;

    &::-webkit-scrollbar {
      width: 0.28vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.23);
    }
    &::-webkit-scrollbar-thumb {
      background: #ff0000;
    }
  }
}
</style>
