<template>
  <button @click="$emit('click')" :class="{ secondary: secondary }">
    <slot></slot>
  </button>
</template>

<script>
export default {
  props: {
    secondary: {
      default: false,
      type: Boolean,
    },
  },
}
</script>

<style lang="scss" scoped>
button {
  width: 100%;
  height: 172px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-family: 'Akrobat';
  text-transform: uppercase;
  border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
  background: linear-gradient(
    rgba($color: #301934 , $alpha: 0.25),
    rgba($color: #591b87, $alpha: 0.25)
  );
  transition: 0.3s ease;
  box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

  &:hover {
    transition: 0.3s ease;
    box-shadow: inset 0vh 0vh 13.889vh #301934 ;
    filter: drop-shadow(0vh 0vh conv(15) rgba(71, 44, 132, 0.5));
  }
}
.secondary {
  background: rgba(255, 255, 255, 0.05);
  &:hover {
    background: rgba(255, 255, 255, 0.12);
  }
}
</style>
