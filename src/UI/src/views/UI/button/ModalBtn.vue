<template>
  <button
    class="dialog-menu-button"
    @click="$emit('click', $event)"
  >
    <span class="dialog-menu-button__text">{{ loc(button.text) }}</span>
  </button>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "DialogMenuButton",

  props: { button: Object },

  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss" scoped>
.dialog-menu-button {
  width: 100%;
  height: 3.947rem;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: 0.5s ease;

  &.primary {
    background: linear-gradient(
      180deg,
      rgba(71, 44, 132, 0.25) 0%,
      rgba(75, 0, 130, 0.25) 100%
    );
    outline: 1px solid #301934 ;
    box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
    &:hover {
      box-shadow: inset 0px 0px 3.789rem rgba(75, 0, 130, 0.86);
      background: linear-gradient(
        180deg,
        rgba(71, 44, 132, 0.35) 0%,
        rgba(75, 0, 130, 0.35) 100%
      );
    }
  }
  background: rgba(255, 255, 255, 0.1);

  &:hover {
    background: rgba(255, 255, 255, 0.2);
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
