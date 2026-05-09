<template>
  <button
    :class="[{ column: button.isColumn }, 'dialog-menu-button']"
    @click="click(button.index)"
  >
    <!-- <img
      :src="`img/dialogMenu/${button.icon}.svg`"
      class="dialog-menu-button__icon"
      v-if="button.icon"
    /> -->
    <span class="dialog-menu-button__text">{{ loc(button.text) }}</span>
  </button>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "DialogMenuButton",

  props: { button: Object },

  methods: {
    click: function (index) {
      window.mp.trigger("dialog::buttonClick", index);
    },
  },

  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss" scoped>
.dialog-menu-button {
  width: 100%;
  height: 3.947rem;
  border: 1px solid transparent;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: 0.3s ease;
  background: rgba(255, 255, 255, 0.05);

  &:hover {
    background: linear-gradient(
      180deg,
      rgba(71, 44, 132, 0.25) 0%,
      rgba(75, 0, 130, 0.25) 100%
    );
    border: 1px solid #301934 ;
    box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
  }

  span {
    font-family: "Akrobat";
    font-weight: 700;
    font-size: 1.263rem;
    line-height: 1.526rem;
    text-transform: uppercase;
    color: #ffffff;
  }
}
</style>
