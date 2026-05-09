<template>
  <div
    :class="[{ active: item.key === currentTab }, 'navigation-item']"
    @click="setCurrentTab(item.key)"
  >
    <div></div>
    <div class="icon">
      <img :src="`/img/cityHallWeb/${item.icon}.png`" alt="" />
      <img :src="`/img/cityHallWeb/${item.icon}.png`" alt="" />
    </div>
    <div class="text">{{ loc(item.text) }}</div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "NavItem",
  computed: {
    ...mapGetters("localization", ["loc"]),
  },
  props: {
    item: Object,
    currentTab: String,
    setCurrentTab: Function,
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.navigation-item {
  height: 100%;
  position: relative;
  width: conv(390);
  min-width: conv(390);
  padding-left: conv(153);
  padding-top: conv(51);
  transition: 0.2s ease;
  border: 1px solid rgba(255, 255, 255, 0.06);
  overflow: hidden;
  background: none;
  cursor: pointer;

  // &::after,
  &::before {
    pointer-events: none;
    content: "";
    z-index: 3;
    position: absolute;
    top: conv(-67);
    left: conv(-140);
    border-radius: 50%;
    width: conv(587);
    height: conv(588);
    transition: 0.3s ease;
    opacity: 1;
  }

  &::before {
    opacity: 1;
    z-index: 4;
  }

  & > div:first-child {
    pointer-events: none;
    content: "";
    z-index: 3;
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    opacity: 0;
    background: linear-gradient(180deg, #301934  0%, #591b87 100%, #591b87 100%);
    transition: 0.2s ease;
  }

  .text {
    font-weight: 700;
    font-size: conv(24);
    line-height: conv(29);
    text-transform: uppercase;
    color: #ffffff;
    position: relative;
    z-index: 5;

    &::after {
      z-index: 5;
      content: "";
      position: absolute;
      top: conv(-12.91);
      left: 0;
      transform: translateY(-100%);
      width: conv(27.18);
      height: conv(2.09);
      background: #ffffff;
      box-shadow: 0px 0px conv(14.6364) rgba(255, 255, 255, 0.55);
    }
  }

  .icon {
    position: absolute;
    pointer-events: none;

    img {
      &:first-child {
        width: 100%;
        height: 100%;
        z-index: 5;
        position: relative;
      }

      &:last-child {
        z-index: 4;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        width: 200%;
        height: 200%;
        opacity: 0.1;
      }
    }
  }

  &:first-child {
    .icon {
      width: conv(140);
      height: conv(140);
      bottom: conv(-31);
      left: conv(-26);
    }

    &::after,
    &::before {
      background: radial-gradient(
        50% 50% at 35% 50%,
        rgba(71, 44, 132, 0.2) 0%,
        rgba(71, 44, 132, 0) 100%
      );
    }
  }

  &:nth-child(2) {
    .icon {
      width: conv(261);
      height: conv(240);
      bottom: conv(-100);
      left: conv(-120);
    }

    &::after,
    &::before {
      background: radial-gradient(
        50% 50% at 35% 50%,
        rgba(255, 255, 255, 0.2) 0%,
        rgba(255, 255, 255, 0) 100%
      );
    }
  }

  &:nth-child(3) {
    .icon {
      width: conv(285);
      height: conv(285);
      bottom: conv(-141);
      left: conv(-90);
    }

    &::after,
    &::before {
      background: radial-gradient(
        50% 50% at 35% 50%,
        rgba(18, 123, 219, 0.2) 0%,
        rgba(18, 123, 219, 0) 100%
      );
    }
  }

  &:last-child {
    .icon {
      width: conv(173);
      height: conv(161);
      bottom: conv(-54);
      left: conv(-45);
    }

    &::after,
    &::before {
      background: radial-gradient(
        50% 50% at 35% 50%,
        rgba(219, 199, 18, 0.2) 0%,
        rgba(219, 199, 18, 0) 100%
      );
    }
  }

  &:not(.active):hover::before {
    // opacity: 1;
    transform: scale(1.5);
  }

  &.active {
    & > div:first-child {
      opacity: 1;
    }
  }

  /* &:not(:last-child) {
    margin-right: conv(20);
  } */

  /* &:hover,
  &.active {
    transition: 0.2s;
  }
  &:hover {
    opacity: 0.8;
  }
  &.active {
    opacity: 1;
  }
  .icon {
    min-width: 1.3rem;
    width: 1.3rem;
    height: 1.3rem;
    background-size: contain;
    background-position: center;
    background-repeat: no-repeat;
    margin-right: 0.9rem;
  }
  .text {
  } */
}
</style>
