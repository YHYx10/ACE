<template>
  <div class="card" :class="{ active: isSelected }" @click="$emit('onSelect')">
    <div
      :class="value > 50 || value === -1 ? 'light-green' : 'light-red'"
      class="light"
    />
    <div class="about" :class="{ hide: value === -1 }">
      <div class="title">
        amount
      </div>
      <div class="value" :class="{ 'value-red': value <= 50 }">
        {{ value }}%
      </div>
    </div>
    <div class="price">$ {{ price.toLocaleString('ru') }}</div>
    <div v-if="!isSelected" class="name">
      {{ name }}
    </div>
    <DefaultBtn @click="$emit('onBtn', repairType)" v-else>
      <img
        src="/img/vehicleRepairsServices/repairWhite.svg"
        alt="repairWhite"
      />
      <div>fix</div>
    </DefaultBtn>
    <img class="img" :src="`/img/vehicleRepairsServices/${img}.png`" alt="" />
  </div>
</template>

<script>
import DefaultBtn from '../UI/button/DefaultBtn.vue'
export default {
  components: { DefaultBtn },
  props: {
    name: String,
    value: Number,
    price: Number,
    img: String,
    repairType: String,
    isSelected: Boolean,
  },
  data() {
    return {}
  },
}
</script>

<style lang="scss" scoped>
.card {
  position: relative;
  width: 29.352vh;
  height: 14.259vh;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  justify-content: left;
  color: white;
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  &.active {
    background: rgba(0, 0, 0, 0.8);
  }
  background: rgba(0, 0, 0, 0.5);
  transition: 0.2s;
  &:hover {
    transform: translate(0, -4%);
    .img {
      transform: translate(0, 2%);
    }
  }

  .light {
    position: absolute;
    width: 100%;
    height: 100%;
    // box-shadow: 0 0 0 0.093vh rgb(0, 255, 0);
    overflow: hidden;
    &::after {
      content: '';
      position: absolute;
      left: 10vh;
      bottom: 1.667vh;
      width: 25.185vh;
      height: 25.185vh;
      z-index: 0;
      filter: blur(8.241vh);
    }
    &-green::after {
      background: rgba(160, 255, 152, 0.2);
    }
    &-red::after {
      background: rgba(234, 5, 5, 0.2);
    }
  }

  .img {
    position: absolute;
    width: 22.5vh;
    height: 14.63vh;
    transition: 0.2s;
    bottom: 5.37vh;
    left: 8.426vh;
    z-index: 0;
  }
  .about {
    z-index: 1;
    padding-left: 2.685vh;
    margin-top: 1.296vh;
    &.hide {
      opacity: 0;
    }
    .title {
      font-size: 1.296vh;
      line-height: 1.574vh;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }
    .value {
      font-size: 2.963vh;
      line-height: 3.519vh;
      display: flex;
      align-items: center;
      text-transform: uppercase;
      color: #ffffff;
      &-red {
        color: #ff7d7d;
      }
    }
  }

  .price {
    margin-top: -5px;
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 1.852vh;
    text-transform: uppercase;
    color: #a0ff98;
    padding-left: 2.685vh;
  }

  .name {
    z-index: 1;
    margin-top: 0;
    padding-left: 2.685vh;
    font-size: 2.222vh;
    line-height: 2.622vh;
    display: flex;
    align-items: center;
    text-transform: uppercase;
    color: #ffffff;
  }

  button {
    font-family: 'Akrobat';
    font-weight: 700;
    z-index: 1;
    color: white;
    font-size: 2.222vh;
    line-height: 2.685vh;
    text-transform: uppercase;
    display: flex;
    gap: 1.481vh;
  }
}
</style>
