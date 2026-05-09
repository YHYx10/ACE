<template>
  <div
    @click="$emit('click')"
    class="weapon-item"
    :class="{ active: isCurrent }"
  >
    <img :src="img" alt="" />
  </div>
</template>
<script>
export default {
  props: {
    isCurrent: Boolean,
    img: String,
  },
}
</script>

<style lang="scss" scoped>
.weapon-item {
  text-transform: none;
  width: 3.7vh;
  height: 3.7vh;
  display: flex;
  justify-content: center;
  align-items: center;
  color: white;
  box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 0.15);
  img {
    transform: scale(0.95);
  }
  &:hover {
    box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 0.5);
  }
  &.active {
    background: url('./assets/img/highlightMin.png');
    background-repeat: no-repeat;
    background-size: cover;
    box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 1);
  }
}
</style>
