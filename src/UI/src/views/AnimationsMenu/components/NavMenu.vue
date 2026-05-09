<template>
  <div class="nav-menu">
    <div
      class="item"
      v-for="(name, key) of list"
      :key="key"
      :class="{ active: key === current }"
      @click="changeNavItem(key)"
    >
      <div class="title">
        {{ name }}
      </div>
    </div>
  </div>
</template>
<script>
export default {
  props: {
    list: Array,
    current: Number,
  },
  methods: {
    changeNavItem(key) {
      this.$emit('onSelect', key)
    },
  },
}
</script>

<style lang="scss" scoped>
.nav-menu {
  text-transform: none;
  display: flex;
  align-items: flex-start;
  gap: 0.1875vw;
  font-family: 'Inter';
  font-weight: 600;
  font-size: 0.782vw;
  line-height: 2.22vh;
  letter-spacing: -0.01em;
  color: rgba(255, 255, 255, 0.24);

  .item {
    position: relative;
    padding: 6.074vh 1.5vw 1.435vh 1.5vw;
    display: flex;
    align-items: center;
    // gap: 0.315vw;
    transition: 0.3s;
    &:hover {
      color: rgba(255, 255, 255, 0.5);
      transition: 0.1s;
      .title {
        color: rgba(255, 255, 255, 0.5);
        text-shadow: 0vh 0vh 1.67vh rgba(255, 255, 255, 0.5);
      }
      img {
        opacity: 0.5;
        filter: drop-shadow(0vh 0vh 0.83vh rgba(255, 255, 255, 0.5));
      }
    }
    &.active {
      &::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        box-sizing: content-box;
        width: 100%;
        height: 0.37vh;
        background: #301934 ;
      }
      &::after {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        height: 9.259vh;
        width: 100%;
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.06) 0%,
          rgba(71, 44, 132, 0) 100%
        );
      }
      .title {
        color: #ffffff;
        text-shadow: 0vh 0vh 3.33vh #ffffff;
      }
      img {
        opacity: 1;
        filter: drop-shadow(0vh 0vh 1.67vh #ffffff);
      }
    }
    img {
      opacity: 0.24;
      width: 1.3vw;
      height: 1.25vw;
    }
  }
}
</style>
