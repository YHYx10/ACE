<template>
  <svg
    :width="size"
    :height="size"
    viewBox="0 0 100 100"
    xmlns="http://www.w3.org/2000/svg"
    class="progress"
  >
    <!-- Background Circle -->
    <circle
      class="progress-bg"
      cx="50"
      cy="50"
      :r="radius"
      fill="none"
      stroke="#0C0E14"
      stroke-width="10"
      stroke-opacity="0.4"
    />
    <!-- Progress Circle -->
    <circle
      class="progress-bar"
      cx="50"
      cy="50"
      :r="radius"
      fill="none"
      :stroke="strokeColor"
      stroke-width="10"
      stroke-linecap="round"
      :stroke-dasharray="circumference"
      :stroke-dashoffset="progressOffset"
    />
  </svg>
</template>

<script>
export default {
  props: {
    value: {
      type: Number,
      default: 0,
      validator: (v) => v >= 0 && v <= 100, // Ensure value stays in 0-100 range
    },
    strokeColor: {
      type: String,
      default: "white",
    },
    size: {
      type: Number,
      default: 100, // Size of the SVG
    },
  },
  computed: {
    radius() {
      return (this.size / 2) - 10; // Dynamic radius (accounts for stroke-width)
    },
    circumference() {
      return 2 * Math.PI * this.radius; // Circumference based on radius
    },
    progressOffset() {
      return this.circumference * (1 - this.value / 100); // Adjust progress based on value
    },
  },
};
</script>

<style scoped>
.progress {
  transform: rotate(-90deg); /* Rotate for proper orientation */
}

.progress-bg,
.progress-bar {
  transition: stroke-dashoffset 0.5s linear, stroke 0.5s linear; /* Smooth animations */
}
</style>
