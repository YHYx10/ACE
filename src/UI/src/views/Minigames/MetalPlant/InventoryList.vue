<template>
  <div class="mplant-inv-list">
    <div
      class="mplant-inv-list-item"
      v-for="(item, index) in itemList"
      :key="index"
      :class="{ active: item }"
      @click="select(item)"
    >
      <img
        :src="`/img/inventory/items/${image(item)}.png`"
        alt="item"
        v-if="item"
        @mouseenter="playHover"
      />
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";
export default {
  props: ["items"],
  computed: {
    ...mapState("gameMetalPlant", ["itemConfigs"]),
    itemList() {
      const list = [];
      for (const key in this.items) {
        const count = this.items[key];
        for (let index = 0; index < count; index++) {
          list.push(key);
        }
      }
      while (list.length < 15) {
        list.push(null);
      }
      return list;
    },
  },
  methods: {
    playHover() {
      this.$store.commit("sounds/play", { name: `dshop_hover`, volume: 0.1 });
    },
    playClick() {
      this.$store.commit("sounds/play", { name: `dshop_click`, volume: 0.1 });
    },
    select(item) {
      this.playClick();
      this.$emit("onitemselect", item);
    },
    image(id) {
      return this.itemConfigs[id].Image;
    },
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.mplant-inv-list {
  width: 100%;
  height: 100%;
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  grid-template-rows: repeat(3, 1fr);
  gap: conv(10);

  &-item {
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    width: 100%;
    height: 100%;
    background: radial-gradient(
      50% 50% at 50% 50%,
      rgba(255, 255, 255, 0.02) 0%,
      rgba(255, 255, 255, 0.05) 100%
    );
    border: 1px solid rgba(255, 255, 255, 0.1);

    &::after {
      background: radial-gradient(
        50% 50% at 50% 50%,
        rgba(255, 255, 255, 0.06) 0%,
        rgba(255, 255, 255, 0.1) 100%
      );
      content: "";
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
      height: 100%;
      opacity: 0;
      z-index: 2;
      transition: 0.2s ease;
    }

    img {
      z-index: 3;
      width: 60%;
      height: 60%;
      -webkit-user-drag: none;
    }

    &.active:hover::after {
      opacity: 1;
    }
  }
}
</style>