<template>
  <div class="item">
    <div class="img" :style="{ backgroundImage: 'url(/img/roundTheClockShop/products/' + item.image + '.png)'}"></div>
    <div class="title">
      {{loc(item.name)}}
    </div>
    <div class="payment">
      <div class="price">
        <div class="price-title">
        price
        </div>
        <div class="price-value">
          $ {{item.cost}}
        </div>
      </div>
      <div class="btn-basket" @click="putInBasket({item: item.name, name: loc(item.name),  cost: item.cost, image: item.image, product: item.product})">
      In the basket
      </div>
    </div>
  </div>
</template>

<script>
import {mapState, mapMutations, mapGetters} from 'vuex'

export default {
  name: 'ProductsListItem',

  props: {
    item: Object,
    index: Number
  },

  computed: {
    ...mapState('roundTheClockShop', ['productsSell', 'basket']),
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    ...mapMutations('roundTheClockShop', ['putInBasket'])
  }
}
</script>

<style lang="scss" scoped>
.item {
  font-family: 'Akrobat';
  font-style: normal;
  font-weight: 700;
  width: 31.019vh;
  height: 27.87vh;
  background: rgba(255, 255, 255, 0.02);
  padding-left: 3.611vh;
  .img {
    width: 27.407vh;
    height: 14.167vh;
    margin-bottom: 1.389vh;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: right bottom;
    transform: scale(-1, 1,);
  }
  .title {
    font-size: 2.963vh;
    line-height: 3.704vh;
    text-transform: uppercase;
    margin-bottom: 1.667vh;
  }

  .payment {
    display: flex;
    justify-content: space-between;
    align-items: center;

    .price {
      .price-title {
        margin-bottom: 0.093vh;
        font-size: 1.296vh;
        line-height: 1.574vh;
        color: rgba(255, 255, 255, 0.55);
      }
      .price-value {
        color: #A0FF98;
        font-size: 2.963vh;
        line-height: 3.704vh;
      }
    }
    .btn-basket {
      width: 17.13vh;
      height: 6.944vh;
      background: rgba(255, 255, 255, 0.03);
      display: flex;
      justify-content: center;
      align-items: center;
      text-transform: uppercase;
      font-size: 2.222vh;
      line-height: 2.778vh;
      color: #FFFFFF;
      &:hover {
        background: rgba(255, 255, 255, 0.1);
      }
    }
  }
}
</style>
