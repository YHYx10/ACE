<template>
  <div v-if="currentItem" :class="[{ focused: focusedMenu }, 'menu-drop']">
    <input
      class="menu-drop__value"
      type="text"
      @click="focusedMenu = !focusedMenu"
      :value="loc(currentItem.text)"
      readonly
    />
    <div class="menu-drop__focused" v-if="focusedMenu">
      <div
        :class="[{ active: currentItem.key === item.key }, 'item']"
        v-for="item in dropFilter"
        :key="item.id"
        @click="selectCurrentItem(item)"
      >
        {{ loc(item.text) }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";
export default {
  name: "MenuDrop",

  props: {
    dropList: Array,
    currentItem: Object,
    setCurrentItem: Function,
  },

  data: function () {
    return {
      focusedMenu: false,
    };
  },

  computed: {
    ...mapGetters("localization", ["loc"]),
    dropFilter() {
      return this.dropList.filter((item) => this.currentItem.key !== item.key);
    },
  },

  methods: {
    selectCurrentItem: function (item) {
      this.focusedMenu = false;
      this.setCurrentItem(item);
    },
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.menu-drop {
  display: flex;
  align-items: center;
  width: 100%;
  align-items: center;
  position: relative;
  box-sizing: border-box;
  border-radius: 0.3rem;
  height: conv(89);
  min-height: conv(89);

  &.focused {
    &:after {
      transform: rotate(-180deg);
      transition: 0.2s;
    }
  }

  &__value {
    background: rgba(56, 59, 64, 0.98) !important;
    width: 100%;
    height: 100% !important;
    padding-left: conv(20) !important;
    font-weight: 600 !important;
    font-size: conv(24) !important;
    line-height: conv(29) !important;
    text-transform: uppercase;
    border-radius: 0 !important;
    color: #ffffff !important;
  }

  &:after {
    content: "";
    width: 1rem;
    height: 1rem;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
    background-image: url("/img/cityHallWeb/arr-drop.svg");
    position: absolute;
    right: 1rem;
    transform-origin: 50% 50%;
    z-index: 5;
    transition: 0.2s;
  }

  &__focused {
    display: flex;
    flex-flow: column;
    width: 100%;
    position: absolute;
    top: 100%;
    left: 0;
    background: rgba(56, 59, 64, 0.98);
    z-index: 2;
    padding-left: conv(20);
    padding-bottom: conv(10);

    .item {
      font-weight: 600;
      font-size: conv(24);
      line-height: conv(29);
      display: flex;
      align-items: center;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.6);
      transition: 0.2s ease;

      &:not(:last-child) {
        margin-bottom: conv(12);
      }

      &:not(.active):hover {
        color: white;
      }

      &.active {
        color: #ff7d7d;
      }

      /* &:hover,
      &.active {
        background: #f3f5f7;
      }
      &.active {
        color: #e00b29;
      } */
    }
  }
}
</style>
