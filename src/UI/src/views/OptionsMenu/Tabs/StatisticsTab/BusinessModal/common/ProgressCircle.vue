<template>
  <div :style="styles" class="circle">
    <svg class="circle__main">
      <circle :stroke-dasharray="appWidthUnit * 14.55" stroke="rgba(255, 255, 255, 0.1)" cx="50%" cy="50%" :r="appWidthUnit" :stroke-width="appWidthUnit * 0.25" fill="none" />
      <circle :stroke-dasharray="appWidthUnit * 14.55" :stroke-dashoffset="dashoffsetValue" stroke="#5CFF80" cx="50%" cy="50%" :r="appWidthUnit" :stroke-width="appWidthUnit * 0.25" fill="none" />
    </svg>
    <div class="circle__text">{{ curPercent }}%</div>
  </div>
</template>

<script>
export default {
  name: 'ProgressCircle',

  props: {
    maxVal: Number,
    curVal: Number,
    styles: Object
  },

  computed: {
    appWidthUnit: function () {
      const appWidth = document.getElementById('app').offsetWidth / 100
      return  Math.ceil(appWidth)
    },

    dashoffsetValue: function () {
      const maxCircleValue = this.appWidthUnit * 14.55
      const maxCircleValuePer = ((maxCircleValue - this.appWidthUnit * 5.7) / 100) / this.maxVal * 100
      const curCircleVal = maxCircleValue - (maxCircleValuePer * this.curVal)
      
      return curCircleVal
    },

    curPercent: function() {
      return (this.curVal / this.maxVal * 100).toFixed();
    }
  }
}
</script>

<style lang="scss" scoped>
.circle {
  width: 2.5rem;
  height: 2.5rem;
  position: relative;
  &__main {
    width: 100%;
    height: 100%;
  }
  &__text {
    position: absolute;
    transform: translate(-50%, -50%);
    top: 50%;
    left: 50%;
    font-size: 0.7rem;
    color: #FFFFFF;
    letter-spacing: 0.03em;
  }
}
</style>
