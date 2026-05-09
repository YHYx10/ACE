<template>
  <div class="menu-nav">
    <div
      :class="[{ active: item.key === currentPage }, 'menu-nav__item']"
      v-for="item in list"
      :key="item.id"
      @click="setCurrentPage(item.key)"
    >
      <div class="text">{{ loc(item.text) }}</div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "MenuNav",

  props: {
    list: Array,
    currentPage: String,
    setCurrentPage: Function,
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.menu-nav {
  width: conv(390);
  display: flex;
  flex-direction: column;

  &__item {
    display: flex;
    align-items: center;
    position: relative;
    transition: 0.2s ease;
    width: 100%;
    height: conv(94);
    min-height: conv(94);
    background: rgba(255, 255, 255, 0.02);
    padding-left: conv(67);

    &:not(:last-child) {
      margin-bottom: conv(10);
    }

    &::after {
      content: "";
      opacity: 0;
      z-index: 3;
      transition: 0.2s ease;
      background: linear-gradient(
        180deg,
        #301934  0%,
        #591b87 100%,
        #301934  100%
      );
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
    }

    &:hover{
      background: rgba(255, 255, 255, 0.05);
    }

    &.active {
      &::after {
        opacity: 1;
      }
    }

    .text {
      z-index: 5;
      position: relative;
      font-weight: 700;
      font-size: conv(24);
      line-height: conv(29);
      text-transform: uppercase;
      color: #ffffff;

      &::after {
        content: "";
        position: absolute;
        left: conv(-24.91);
        transform: translate(-100%, -50%);
        top: 50%;
        height: conv(27.18);
        width: conv(2.09);
        background: #ffffff;
        box-shadow: 0px 0px conv(14.6364) rgba(255, 255, 255, 0.55);
      }
    }
  }
}
</style>
