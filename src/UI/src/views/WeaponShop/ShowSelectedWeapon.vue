<template>
  <div class="selected-weapon">
    <div class="about" :style="title === 'Unknown' ? 'opacity: 0' : ''">
      <div class="title">
        {{ title }}
      </div>
      <div class="model">
        {{ subtitle }}
      </div>
    </div>
  </div>
</template>

<script>
import WeaponPng from './assets/img/weaponPreview.png'
export default {
  props: {
    title: String,
    subtitle: String,
    img: String,
  },
  data: () => {
    return {
      WeaponPng,
    }
  },
}
</script>

<style lang="scss" scoped>
.selected-weapon {
  .about {
    font-family: 'Montserrat';
    font-style: normal;
    .title {
      color: #fff;
      font-weight: 700;
      font-size: 3.7vh;
      line-height: 4.54vh;
    }
    .model {
      font-weight: 400;
      font-size: 1.85vh;
      line-height: 2.22vh;
      color: rgba(255, 255, 255, 0.35);
    }
  }
  img {
    margin-top: 4.063vw;
  }
}
</style>
