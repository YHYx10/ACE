<template>
  <transition-group
    name="list"
    tag="div"
    class="notify-list"
    :style="'zoom:' + zoomDesign + ''"
  >
    <notification
      v-for="item in items.length > 3 ? items.slice(-3) : items"
      :key="item.id"
      :item="item"
    />
  </transition-group>
</template>

<script>
import { mapState } from "vuex";

import Notification from "./components/Notification";

export default {
  components: { Notification },
  data() {
    return {
      zoomDesign: 1,
    };
  },

  computed: {
    ...mapState("notifyList", ["items"]),
  },

  mounted() {
    this.handleResize();
    window.addEventListener("resize", this.handleResize);
  },
  beforeDestroy() {
    window.removeEventListener("resize", this.handleResize);
  },
  methods: {
    handleResize() {
      let zoomCountOne = document.body.clientWidth / 1920; //1920;
      let zoomCountTwo = document.body.clientHeight / 1080; //1080;

      if (zoomCountOne < zoomCountTwo) this.zoomDesign = zoomCountOne;
      else this.zoomDesign = zoomCountTwo;
    },
  },
};
</script>

<style lang="scss" scoped>
.notify-list {
  position: absolute;
  right: 32px;
  bottom: 120px;
  width: 420px;
  max-width: calc(100vw - 64px);
  height: fit-content;
  display: flex;
  flex-direction: column-reverse;
  align-items: flex-end;
  gap: 13px;
  z-index: 10;
  overflow: visible;
  pointer-events: none;
}
.list-enter-active,
.list-leave-active {
  transition: opacity .62s cubic-bezier(.16, 1, .3, 1), transform .62s cubic-bezier(.16, 1, .3, 1), filter .62s ease;
}
.list-enter,
.list-leave-to {
  opacity: 0;
  filter: blur(.1rem);
  transform: translateX(4.4rem) translateY(.55rem) scale(.955);
}
.list-move {
  transition: transform .54s cubic-bezier(.16, 1, .3, 1), opacity .34s ease, filter .34s ease;
}
.notification:nth-child(2) {
  opacity: .94;
}
.notification:nth-child(3) {
  opacity: .88;
  filter: saturate(.96);
}
</style>
