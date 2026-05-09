<template>
  <div class="weapon-component" @click="onclick" :class="{ active: isCurrent }">
    <div class="header">
      <div class="title">
        {{ title }}
      </div>
      <MoneyBlock :balance="price" minVersion class="price" />
    </div>
    <div class="item-img">
      <img :src="`/img/weaponShop/weapons/${img}.png`" :alt="img" />
    </div>

    <div class="description">
      <div class="name">
        {{ name }}
      </div>
      <div v-show="ammoType" class="params ammo">
        <!-- <AmmoSvg class="icon" :fill="'#fff'" :fillOpacity="'0.6'" /> -->
        <svg xmlns="http://www.w3.org/2000/svg" class="icon" fill="#fff" fillOpacity="0.6" viewBox="0 0 15 14">
          <g clip-path="url(#a)">
            <path
              d="M14.56 4.308v-.189c0-1.977-.79-3.58-1.764-3.58-.975 0-1.765 1.603-1.765 3.58 0 .063-.003.458 0 .189h3.529Z"
            />
            <path
              d="M14.973 12.552H10.67V7.269c0-.5.027-1 .083-1.497l.108-.974c.014-.121.105-.212.214-.212h3.493c.108 0 .2.09.213.212l.109.974c.055.496.083.996.083 1.497v5.283Z"
            />
            <path
              d="M14.758 14h-3.873c-.119 0-.215-.108-.215-.241 0-.134.096-.242.215-.242h3.873c.118 0 .215.108.215.242 0 .133-.097.241-.215.241Z"
            />
            <path
              d="M14.542 13.034H11.1v.483h3.442v-.482Zm-1.721-.482H10.67l.43.482h3.442l.43-.482h-2.15ZM11.25 4.308c-.006.158-.15.115-.15.278h3.442c0-.163-.198-.12-.204-.278H11.25Z"
            />
          </g>
          <g clip-path="url(#b)">
            <path
              d="M9.266 4.308v-.189c0-1.977-.79-3.58-1.764-3.58-.975 0-1.765 1.603-1.765 3.58 0 .063-.003.458 0 .189h3.529Z"
            />
            <path
              d="M9.679 12.552H5.376V7.269c0-.5.027-1 .083-1.497l.108-.974c.014-.121.105-.212.214-.212h3.493c.108 0 .2.09.213.212l.109.974c.055.496.083.996.083 1.497v5.283Z"
            />
            <path
              d="M9.463 14H5.591c-.12 0-.215-.108-.215-.241 0-.134.096-.242.215-.242h3.873c.118 0 .215.108.215.242 0 .133-.097.241-.216.241Z"
            />
            <path
              d="M9.248 13.034H5.806v.483h3.442v-.482Zm-1.721-.482H5.376l.43.482h3.442l.43-.482h-2.15ZM5.956 4.308c-.006.158-.15.115-.15.278h3.442c0-.163-.198-.12-.204-.278H5.956Z"
            />
          </g>
          <g clip-path="url(#c)">
            <path
              d="M3.971 4.308c.002-.063.002-.126.002-.189 0-1.977-.79-3.58-1.765-3.58-.975 0-1.765 1.603-1.765 3.58l-.001.189h3.53Z"
            />
            <path
              d="M4.385 12.552H.08V7.269c0-.5.028-1 .084-1.497l.108-.974c.013-.121.105-.212.213-.212H3.98c.109 0 .2.09.214.212l.108.974c.056.496.083.996.083 1.497v5.283Z"
            />
            <path
              d="M4.17 14H.296c-.12 0-.216-.108-.216-.241 0-.134.097-.242.216-.242h3.872c.12 0 .216.108.216.242 0 .133-.097.241-.216.241Z"
            />
            <path
              d="M3.954 13.034H.512v.483h3.442v-.482Zm-1.721-.482H.081l.43.482h3.443l.43-.482h-2.15ZM.662 4.308c-.006.158-.15.115-.15.278h3.442c0-.163-.198-.12-.204-.278H.662Z"
            />
          </g>
          <defs>
            <clipPath id="a">
              <path fill="#fff" d="M0 0h4.412v14H0z" transform="translate(10.588)" />
            </clipPath>
            <clipPath id="b">
              <path fill="#fff" d="M0 0h4.412v14H0z" transform="translate(5.294)" />
            </clipPath>
            <clipPath id="c">
              <path fill="#fff" d="M0 0h4.412v14H0z" />
            </clipPath>
          </defs>
        </svg>
        <div class="info">
          <span>{{ ammoType }}</span>
          <span>mm</span>
        </div>
      </div>
      <div v-show="weight" class="params weight">
        <!-- <WeightSvg class="icon" :fill="'#AEAEAE'" /> -->
        <svg class="icon" viewBox="0 0 15 14" fill="#AEAEAE" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M7.50002 2.95833C7.70071 2.95833 7.86906 2.89057 8.00506 2.75504C8.14059 2.61904 8.20835 2.45069 8.20835 2.25C8.20835 2.04931 8.14059 1.88096 8.00506 1.74496C7.86906 1.60943 7.70071 1.54167 7.50002 1.54167C7.29932 1.54167 7.13121 1.60943 6.99568 1.74496C6.85968 1.88096 6.79168 2.04931 6.79168 2.25C6.79168 2.45069 6.85968 2.61904 6.99568 2.75504C7.13121 2.89057 7.29932 2.95833 7.50002 2.95833ZM1.62085 12.875L3.03752 2.95833H5.49897C5.46356 2.84028 5.43404 2.72506 5.41043 2.61267C5.38682 2.50075 5.37502 2.37986 5.37502 2.25C5.37502 1.65972 5.58161 1.15799 5.99481 0.744792C6.408 0.331597 6.90974 0.125 7.50002 0.125C8.09029 0.125 8.59203 0.331597 9.00522 0.744792C9.41842 1.15799 9.62502 1.65972 9.62502 2.25C9.62502 2.37986 9.61321 2.50075 9.5896 2.61267C9.56599 2.72506 9.53647 2.84028 9.50106 2.95833H11.9625L13.3792 12.875H1.62085Z"
            fill="#AEAEAE"
          />
        </svg>
        <div class="info">
          <span>{{ weight }}</span>
          <span>kg</span>
        </div>
      </div>
    </div>

    <div class="corner corner-a" />
    <div class="corner corner-b" />
    <div class="corner corner-c" />
    <div class="corner corner-d" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import WeaponPng from './assets/img/weapon.png'
// import AmmoSVG from './assets/img/ammoIco.svg'
// import WeightSVG from './assets/img/weight.svg'

import MoneyBlock from '../UI/components/MoneyBlock.vue'
// import AmmoSvg from './componentsSVG/ammoSvg.vue'
// import WeightSvg from './componentsSVG/weightSvg.vue'

export default {
  props: {
    index: Number,
    isCurrent: Boolean,
    price: Number,
    title: String,
    name: String,
    ammoType: String,
    weight: Number,
    img: String,
  },
  data: () => {
    return {
      WeaponPng,
      // AmmoSVG,
      // WeightSVG,
    }
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    onclick() {
      this.$emit('onSelect', this.index)
    },
  },
  components: { MoneyBlock},
}
</script>

<style lang="scss" scoped>
.weapon-component {
  text-transform: none;
  color: #ffffff;
  width: 30.19vh;
  height: 13.52vh;
  box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 0.15);
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  overflow: hidden;
  transition: 0.3s;

  &:hover {
    transition: 0.1s;
    box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 0.55);
  }
  &.active {
    box-shadow: inset 0vh 0vh 0vh 0.09vh rgba(255, 255, 255, 1);
    background: url('./assets/img/highlight.png');
    background-repeat: no-repeat;
    background-size: cover;
  }
  .header {
    width: 100%;
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-top: 0.83vh;
    padding: 0 1.39vh;

    .title {
      font-family: 'Montserrat';
      font-style: normal;
      font-weight: 600;
      font-size: 1.48vh;
      line-height: 2.22vh;
    }
    .price {
      .balance svg {
        width: 1.67vh;
      }
    }
  }
  .item-img {
    width: 27.41vh;
    height: 8.33vh;
    margin-top: -0.28vh;
    display: flex;
    justify-content: center;
    align-items: center;
    img {
      height: 90%;
    }
  }
  .description {
    display: flex;
    justify-content: space-between;
    gap: 1.2vh;
    width: 100%;
    align-items: center;
    align-self: center;
    margin-top: -0.65vh;
    padding-left: 1.39vh;
    padding-right: 1.39vh;
    .name {
      margin-right: auto;
      width: 14.35vh;
      font-family: 'Montserrat';
      font-style: normal;
      font-weight: 400;
      font-size: 1.2vh;
      line-height: 2.22vh;
      height: 2.22vh;
      overflow: hidden;
    }
    .params {
      display: flex;
      align-items: center;
      .icon {
        width: 1.39vh;
        height: 1.3vh;
      }
      .info {
        display: flex;
        margin-top: 0.19vh;

        span:nth-child(1) {
          font-weight: 700;
          font-size: 1.11vh;
          line-height: 1.39vh;
          color: #e8e8e8;
        }
        span:nth-child(2) {
          font-weight: 400;
          font-size: 1.11vh;
          line-height: 1.39vh;
          color: rgba(232, 232, 232, 0.29);
          margin-left: 0.5vh;
        }
      }
    }
    .ammo {
      .icon {
        margin-right: 0.46vh;
      }
      .info {
        display: flex;
      }
    }
    .weight {
      .icon {
        margin-right: 0.56vh;
      }
    }
  }

  .corner {
    border-radius: 100%;
    position: absolute;
    width: 0.46vh;
    height: 0.46vh;
    background: #ffffff;
    box-shadow: 0vh 0vh 1.02vh #ffffff;
  }
  .corner-a {
    left: 0;
    top: 0;
    transform: translate(-50%, -50%);
  }
  .corner-b {
    right: 0;
    top: 0;
    transform: translate(50%, -50%);
  }
  .corner-c {
    right: 0;
    bottom: 0;
    transform: translate(50%, 50%);
  }
  .corner-d {
    left: 0;
    bottom: 0;
    transform: translate(-50%, 50%);
  }
}
</style>
