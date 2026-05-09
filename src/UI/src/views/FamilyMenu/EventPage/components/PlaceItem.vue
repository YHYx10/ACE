<template>
  <div @click="$emit('onSelect')" class="place-item">
    <div class="img" :style="`background-image: url(${img})`"></div>
    <div class="select" :class="{ active: selected }">
      {{ text }}
    </div>
  </div>
</template>

<script>
export default {
  name: 'PlaceItem',
  props: {
    img: String,
    text: String,
    selected: Boolean,
  },
}
</script>

<style lang="scss" scoped>
.place-item {
  width: 23.796vh;
  .img {
    width: 100%;
    height: 14.51vh;
    background-size: cover;
  }
  &:hover .select {
    background: rgba(255, 255, 255, 0.1);
  }
  .select {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 5.556vh;
    background: rgba(255, 255, 255, 0.05);
    font-family: 'Akrobat';
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.315vh;
    text-transform: uppercase;

    color: #ffffff;

    &.active {
      background: #301934 ;
    }
  }
}
</style>
