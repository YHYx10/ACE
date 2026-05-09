<template>
  <div class="exit" @click="exit"><span>ESC</span> - Close</div>
</template>

<script>
export default {
  name: 'ButtonExit',

  methods: {
    exit: function() {
      this.$emit('click')
      // window.mp.trigger('closeBarber')
    },
  },
}
</script>

<style lang="scss" scoped>
.exit {
  width: 100%;
  height: 2.22vh;
  font-family: 'Montserrat';
  font-style: normal;
  font-weight: 400;
  font-size: 1.85vh;
  line-height: 2.22vh;
  text-align: center;
  color: rgba(#ffffff, 0.8);
  transition: 0.3s ease;
  &:hover {
    color: #ffffff;
  }
  span {
    font-weight: 700;
  }
}
</style>
