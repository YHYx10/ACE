<template>
  <div class="back" @click="$emit('click')">
    <div class="item">
      <img src="/img/familyMenu/btn-back.svg" alt="">
    </div>
    <div class="text">
   Zurück
    </div>
  </div>
</template>

<script>
export default {
  name: 'Back'
}
</script>

<style lang="scss" scoped>
.back {
  display: flex;
  width: fit-content;
  height: min-content;
  
  .item {
    width: 4.352vh;
    height: 4.352vh;
    display: flex;
    justify-content: center;
    align-items: center;
    background: rgba(#fff, 0.07);
    img {
      width: 2.5vh;
      height: 2.222vh;
    }
  }

  &:hover .item {
    background: rgba(#fff, 0.1);
  }

  .text {
    display: flex;
    align-items: center;
    margin-left: 1.296vh;
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.222vh;
    color: #FFFFFF;
  }
}
</style>