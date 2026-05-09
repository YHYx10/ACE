<template>
  <div :class="[{active: status}, 'settings-switch']" @click="switchSet(!status)"></div>
</template>

<script>
export default {
  props: ['status'],
  methods: {
    switchSet(status) {
      this.$emit("onSwitch", status);
    }
  },
}
</script>

<style lang="scss" scoped>
.settings-switch {
  position: relative;
  max-width: 2.5rem;
  min-width: 2.5rem;
  height: 1.44rem;
  background: rgba(255, 255, 255, 0.05);
  border-radius: 2.05rem;
  color: #fff;
  cursor: pointer;

  &:before {
    content: '';
    position: absolute;
    height: 1rem;
    width: 1rem;
    left: 0;
    top: 0;
    margin: 0.22rem 0.3rem;
    background: #301934 ;
    border-radius: 50%;
    transition: 0.2s ease;
  }

  &.active:before {
    left: calc(100% - 1.6rem);
    background: #5CFF80;
  }
}
</style>
