<template>
  <div class="segment-progress-bar">
    <div v-for="i in amount" :key="i" class="item">
      <div
        class="filling "
        :style="{ width: `${getFill(i, value, amount)}%`, background: '#fff' }"
      />
    </div>
  </div>
</template>

<script>
export default {
  props: {
    /** count of segments */
    amount: Number,
    /**
     * progress value
     * range 0-1
     * @example value = 0.24
     */
    value: Number,
  },
  methods: {
    /**
     *
     * @param {number} segmentIndex
     * @param {number} value
     * @param {number} amount
     */
    getFill(segmentIndex, value, amount) {
      if (amount < 1) return 0
      const length = 1 / amount
      if (segmentIndex * length <= value) return 100
      const prevSegment = segmentIndex - 1
      return prevSegment * length < value
        ? ((value - prevSegment * length) / length) * 100
        : 0
    },
  },
}
</script>

<style lang="scss" scoped>
.segment-progress-bar {
  display: flex;
  gap: 6px;
  .item {
    width: 100%;
    height: 8px;
    background: rgba(255, 255, 255, 0.21);
    .filling {
      width: 50%;
      height: 100%;
    }
  }
}
</style>
