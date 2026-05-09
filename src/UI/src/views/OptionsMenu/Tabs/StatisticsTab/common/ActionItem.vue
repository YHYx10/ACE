<template>
  <button :style="{ background: `center / cover no-repeat url(/img/optionsMenu/statisticsTab/backs/${item.bg}.png)`}"
          class="item" :disabled="!property[item.key] || (item.key === 'transport' && !property[item.key].length)" @click="item.event">
    <div class="item__info">
      <div class="category">Category </div>
      <div class="title">
        {{ loc(item.title) }}
        <span class="gray" v-if="item.key === 'house' && property[item.key]"> #{{ houseId }}</span>
        <span class="gray" v-else-if="item.key === 'business' && property[item.key] && property[item.key].number"> #{{ property[item.key].number }}</span>
      </div>
      <div v-if="item.key === 'house'" class="subtitle">
        <div v-if="property[item.key]" class="item-info">
          <!-- <div class="item-id"># </div> -->
          <div class="item-property">lock out <span :class="houseLocked ? 'red' : 'green'">{{houseLocked ? 'Closed' : 'Open'}}</span></div>
          <div v-if="currentGarage" class="item-property">Garage parking spaces <span>{{currentGarage.placesCount}}</span></div>
        </div>
        <div v-else class="item-empty">absent</div>
      </div>
      <div v-else-if="item.key === 'business'" class="subtitle">
        <div v-if="property[item.key]" class="item-info">
          <div class="item-property">name <span>{{ property[item.key].name }}</span></div>
          <div class="item-property">Steer <span>{{ property.business.taxCount + '/' + taxMax }}$</span></div>
          <!-- <div class="item-id"># {{ property[item.key].number }}</div> -->
        </div>
        <div v-else class="item-empty">abwesend</div>
      </div>
      <div v-else-if="item.key === 'transport'" class="subtitle">
        <div class="item-property">Crowd <span>{{ property[item.key] ? property[item.key].length : 0 }}</span>
        </div>
      </div>
    </div>
  </button>
</template>

<script>
import {mapState, mapGetters} from 'vuex'

export default {
  name: 'ActionsItem',

  props: {
    item: Object
  },

  computed: {
    ...mapState('optionsMenu', ['property', 'statistics']),
    ...mapState('homeMenu', ['currentGarage', 'houseId', 'houseLocked']),
    ...mapGetters('localization', ['loc']),
    taxMax() {
      return this.property.business.price / 100 * 0.013 * 1.9 * 24 * 14
    },
  },
}
</script>
<style lang="scss" scoped>
.item-property {
  display: flex;
  flex-flow: column nowrap;
  & span {
    font-weight: 600;
    color: #fff;
    font-size: 1.2rem;
    line-height: 1.45rem;
    margin-bottom: 0.5rem;
  }
}
.item-id {
  position: relative;
  display: inline-block;
  color: #fff;
  font-size: 1rem;
  line-height: 1.2rem;
  // &:after {
  //   content: "";
  //   position: absolute;
  //   right: -1.5rem;
  //   width: 1.1rem;
  //   height: 100%;
  //   background: url("/img/optionsMenu/statisticsTab/icons/location.svg") center / contain no-repeat;
  // }
}

.gray {
  color: rgba(180, 178, 178, 0.853);
  text-shadow: 0 0 2px black;
}
.item-property .green {
  color:  #30db12;
  font-family: "Russian";
  font-weight: 700;
}
.item-property .red {
  color: #301934 ;
  font-family: "Russian";
  font-weight: 700;
}
</style>