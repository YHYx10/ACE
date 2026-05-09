<template>
  <input
    :class="['dialog-menu-input']"
    @input="$emit('input', type === 'number' ? ($event.target.value) : $event.target.value)"
    :type="type"
    :value="value"
    maxlength="20"
  />
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "DialogMenuInput",
  props: { value: [String, Number], type: String },

  computed: {
    ...mapGetters("localization", ["loc"]),
  },
};
</script>

<style lang="scss" scoped>
.dialog-menu-input {
  width: 100%;
  height: 65px;
  display: flex;
  justify-content: center;
  align-items: center;
  cursor: pointer;
  transition: 0.5s ease;
  background: transparent;
  color: #ffffff;
  text-align: center;

  &::-webkit-outer-spin-button,
  &::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
  }

  
  background: rgba(217, 217, 217, 0.05);


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
