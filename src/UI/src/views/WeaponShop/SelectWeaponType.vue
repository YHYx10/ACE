<template>
  <div class="weapon-types">
    <div
      class="item"
      v-for="(name, key) of list"
      :key="key"
      :class="{ active: key === currentType }"
      @click="changeType(key)"
    >
      <img :src="getIcon(key)" alt="" />
      <div class="title">
        {{ name }}
      </div>
    </div>
  </div>
</template>
<script>
import SniperSVG from './assets/img/weaponTypes/sniper.svg'
import KnifeSVG from './assets/img/weaponTypes/knife.svg'
import GunSVG from './assets/img/weaponTypes/pistols.svg'
import ShotgunSVG from './assets/img/weaponTypes/shotgun.svg'
import SmgSVG from './assets/img/weaponTypes/smg.svg'
import RifleSVG from './assets/img/weaponTypes/rifle.svg'
import AmmoSVG from './assets/img/weaponTypes/ammo.svg'
import ArmorSVG from './assets/img/weaponTypes/armor.svg'

export default {
  props: {
    list: Object,
    currentType: String,
  },
  methods: {
    changeType(key) {
      this.$emit('onSelect', key)
    },
    getIcon(type) {
      switch (type) {
        case 'steelArms':
          return KnifeSVG
        case 'sniper':
          return SniperSVG
        case 'gun':
          return GunSVG
        case 'shotgun':
          return ShotgunSVG
        case 'smg':
          return SmgSVG
        case 'rifle':
          return RifleSVG
        case 'ammo':
          return AmmoSVG
        case 'armor':
          return ArmorSVG
      }
    },
  },
}
</script>

<style lang="scss" scoped>
.weapon-types {
  text-transform: none;
  display: flex;
  align-items: flex-start;
  gap: 0.09vh;
  // font-family: 'Inter';
  font-style: normal;
  font-weight: 600;
  font-size: 0.882vw;
  line-height: 2.22vh;
  letter-spacing: 0.06em;
  color: rgba(255, 255, 255, 0.24);

  .item {
    position: relative;
    padding: 9.074vh 0.939vw 1.435vh 0.939vw;
    display: flex;
    align-items: center;
    gap: 0.315vw;
    transition: 0.3s;
    &:hover {
      color: rgba(255, 255, 255, 0.5);
      transition: 0.1s;
      .title {
        color: rgba(255, 255, 255, 0.5);
        text-shadow: 0vh 0vh 1.67vh rgba(255, 255, 255, 0.5);
      }
      img {
        opacity: 0.5;
        filter: drop-shadow(0vh 0vh 0.83vh rgba(255, 255, 255, 0.5));
      }
    }
    &.active {
      &::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        box-sizing: content-box;
        width: 100%;
        height: 0.37vh;
        background: #FF0000;
      }
      &::after {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        height: 9.259vh;
        width: 100%;
        background: linear-gradient(180deg, rgba(71, 44, 132, 0.06) 0%, rgba(71, 44, 132, 0) 100%);
      }
      .title {
        color: #ffffff;
        text-shadow: 0vh 0vh 3.33vh #ffffff;
      }
      img {
        opacity: 1;
        filter: drop-shadow(0vh 0vh 1.67vh #ffffff);
      }
    }
    img {
      opacity: 0.24;
      width: 1.3vw;
      height: 1.25vw;
    }
  }
}

@media screen and (max-aspect-ratio: 3/4) {
  .weapon-types {
    .item {
      padding: 0.53vh 0.339vw;
      padding-top: 9.074vh;
    }
  }
}
</style>
