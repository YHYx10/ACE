<template>
  <div class="title-item">
    <div class="title">
      {{ title }}
    </div>
    <div class="text">
      {{ text }}
    </div>
  </div>
</template>

<script>
export default {
  props: {
    title: String,
    text: String,
  },
}
</script>

<style lang="scss" scoped>
.title-item {
  font-family: 'Akrobat';
  width: 100%;
  display: flex;
  flex-direction: column;
  font-weight: 700;
  gap: 0.463vh;
  .title {
    font-size: 2.963vh;
    line-height: 3.704vh;
    text-transform: uppercase;
  }
  .text {
    font-size: 1.852vh;
    line-height: 2.315vh;
    color: rgba(255, 255, 255, 0.44);
  }
}
</style>
