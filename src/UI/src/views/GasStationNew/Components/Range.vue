<template>
  <div class="Range">
    <div class="Range-track" @click="onMouseMove">
      <div class="Range-track__fill" :style="setFill"></div>
    </div>
    <div class="Range-thumb" ref="thumb" :style="setPosition" @mousedown="onMouseDown"></div>
  </div>
</template>

<script>
function validateNumber(value) {
  let innerValue = Number(value);
  return !isNaN(innerValue) ? true: false;
}
export default {
  props: {
    min: {
      type: [Number, String],
      default: 0,
      validator: validateNumber,
    },
    max: {
      type: [Number, String],
      default: 0,
      validator: validateNumber,
    },
    value: {
      type: [Number, String],
      default: 0,
      validator: validateNumber,
    },
  },
  data() {
    return {
      containerWidth: 0,
      thumbWidth: 0,
    }
  },
  computed: {
    computedMin() {
      return Number(this.min) < Number(this.max) ? Number(this.min): Number(this.max);
    },
    computedMax() {
      return Number(this.min) < Number(this.max) ? Number(this.max): Number(this.min);
    },
    innerValue() {
      return Number(this.value) <= this.computedMin ? this.computedMin : Number(this.value) >= this.computedMax ? this.computedMax : Number(this.value);
    },
    oneDivision() {
      return (this.containerWidth - this.thumbWidth) / (this.computedMax - this.computedMin)
    },
    currentPosition() {
      return Math.round(this.oneDivision * (this.innerValue - this.computedMin) + this.thumbWidth / 2);
    },
    setPosition() {
      return `left:${this.currentPosition}px`;
    },
    setFill() {
      return `width:${this.innerValue != this.computedMin ? this.currentPosition : 0}px`;
    }
  },
  methods: {
    initRange() {
      let { width, x } = this.$el.getBoundingClientRect();
      [this.containerWidth, this.containerX] = [width, x];
      this.thumbWidth = this.$refs.thumb.getBoundingClientRect().width;
    },
    onMouseDown() {
      this.initRange();
      window.addEventListener("mousemove", this.onMouseMove);
      window.addEventListener("mouseup", this.onMouseUp);
    },
    onMouseUp() {
      window.removeEventListener("mousemove", this.onMouseMove);
      window.removeEventListener("mouseup", this.onMouseUp);
    },
    onMouseMove(e) {
      let currentLeft = e.clientX - this.containerX - this.thumbWidth / 2;
      currentLeft = currentLeft <= 0 ? 0 : currentLeft >= this.containerWidth - this.thumbWidth ? this.containerWidth - this.thumbWidth : currentLeft;
      let newValue = Math.round(currentLeft / this.oneDivision + this.computedMin);
      if(newValue!==this.innerValue) {
        this.$emit("input", newValue);
      }
    }
  },
  mounted() {
    Array.from(this.$el.children).forEach(el => {el.ondragstart=()=>false;});
    window.addEventListener("resize", this.initRange);
    this.initRange();
  },
  unmounted() {
    window.removeEventListener("resize", this.initRange);
  },
}
</script>