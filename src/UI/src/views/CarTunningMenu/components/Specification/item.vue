<template>
  <div class="specification-item">
    <div class="title">
      {{ title }}
    </div>
    <div class="value">
      <!-- {{ currentValue }}; {{ difference }}; {{ maxValue }} -->
      <div v-for="i in 5" :key="i" class="item">
        <div
          class="filled"
          :class="{ red: currentPercentage(i), gray: differencePercentage(i) }"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    currentValue: Number,
    difference: Number,
    maxValue: Number,
  },
  computed: {},
  methods: {
    currentPercentage: function(value) {
      return this.currentValue / this.maxValue > value / 5
    },
    differencePercentage: function(value) {
      return this.difference / this.maxValue > value / 5
    },
  },
}
</script>

<style lang="scss" scoped>
.specification-item {
  width: 19.537vh;
  height: 6.944vh;
  background: linear-gradient(
    270deg,
    rgba(7, 7, 7, 0.5) 0%,
    rgba(7, 7, 7, 0) 100%
  );
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  padding-right: 1.852vh;
  .title {
    margin-top: 0.741vh;
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 2.222vh;
    line-height: 2.778vh;
    text-align: right;
    text-transform: uppercase;
    color: #ffffff;
  }

  .value {
    margin-top: 0.463vh;
    display: flex;
    flex-direction: row-reverse;
    gap: 0.463vh;
    .item {
      width: 1.759vh;
      height: 1.759vh;
      background: rgba(255, 255, 255, 0.05);
      display: flex;
      align-items: center;
      justify-content: center;
      .filled {
        content: '';
        width: 1.019vh;
        height: 1.019vh;
        font-size: 1.111vh;
        &.gray {
          background: rgba($color: #ffffff, $alpha: 0.5);
          box-shadow: 0vh 0vh 1.355vh rgba($color: #ffffff, $alpha: 0.5);
        }
        &.red {
          background: #301934 ;
          box-shadow: 0vh 0vh 1.355vh rgba(71, 44, 132, 0.55);
        }
      }
    }
  }
}
</style>
