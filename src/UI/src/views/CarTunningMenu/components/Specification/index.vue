<template>
  <div class="vehicle-specification">
    <div class="head-title">
      {{ specifications.title }}
    </div>
    <div class="mileage-block">
      <div class="about">
        <div class="title">
         Car mileage
        </div>
        <div class="value">
          9994,3 Km
        </div>
      </div>
      <img src="/img/carTunningMenu/stopwatch.svg" alt="" />
    </div>

    <div class="list">
      <Item
        :title="'speed'"
        :currentValue="specifications.speed"
        :difference="
          specificationItemResult(
            specifications.speed,
            specificationsPossible.speed
          ).value
        "
        :maxValue="specificationMaxValues.speed"
      />
      <Item
        :title="'braking'"
        :currentValue="specifications.braking"
        :difference="
          specificationItemResult(
            specifications.braking,
            specificationsPossible.braking
          ).value
        "
        :maxValue="specificationMaxValues.braking"
      />
      <Item
        :title="'acceleration'"
        :currentValue="specifications.acceleration"
        :difference="
          specificationItemResult(
            specifications.acceleration,
            specificationsPossible.acceleration
          ).value
        "
        :maxValue="specificationMaxValues.acceleration"
      />
      <Item
        :title="'management'"
        :currentValue="specifications.traction"
        :difference="
          specificationItemResult(
            specifications.traction,
            specificationsPossible.traction
          ).value
        "
        :maxValue="specificationMaxValues.traction"
      />
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import Item from './item.vue'

export default {
  name: 'Spec',
  computed: {
    ...mapState('carTunningMenu', [
      'specificationMaxValues',
      'specifications',
      'specificationsPossible',
    ]),
    ...mapGetters('localization', ['loc']),
  },
  components: { Item },
  methods: {
    specificationItemResult: function(current, future) {
      const result = { type: null, value: null }

      if (future > current) {
        result.type = 'up'
        result.value = Math.floor((future - current) * 10) / 10
      } else if (future < current) {
        result.type = 'down'
        result.value = Math.floor((current - future) * 10) / 10
      } else {
        result.type = null
        result.value = null
      }

      return result
    },
  },
}
</script>

<style lang="scss" scoped>
.vehicle-specification {
  width: min-content;
  div,
  button {
    font-family: 'Akrobat';
    text-transform: uppercase;
    font-weight: 700;
    color: #ffffff;
  }
  .head-title {
    font-size: 3.704vh;
    line-height: 4.63vh;
    text-align: right;
    width: fit-content;
    white-space: nowrap;
  }

  .mileage-block {
    margin-top: 2.13vh;
    display: flex;
    gap: 1.852vh;
    align-items: center;
    justify-content: right;
    .about {
      display: flex;
      flex-direction: column;
      gap: 0.278vh;
      .title {
        font-size: 1.111vh;
        line-height: 1.389vh;
        color: rgba(255, 255, 255, 0.55);
      }
      .value {
        font-size: 2.963vh;
        line-height: 3.704vh;
      }
    }
    svg {
      width: 2.778vh;
      height: 2.778vh;
    }
  }

  .list {
    margin-top: 2.87vh;
    display: flex;
    flex-direction: column;
    width: fit-content;
    gap: 0.463vh;
    margin-left: auto;
  }
}
</style>
