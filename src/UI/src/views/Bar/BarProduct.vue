<template>
  <div class="bar-product">
    <div class="bg">
      <div
        class="img"
        :style="{ backgroundImage: `url(/img/bar/products/${barProduct.img})` }"
      ></div>
    </div>
    <div class="title">{{ barProduct.name }}</div>
    <div class="price-title">Price</div>
    <div class="price-value">{{ barProduct.price }}</div>
    <div class="btn-basket" @click="$emit('onSelect', barProduct)">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="29"
        height="29"
        fill="none"
        viewBox="0 0 29 29"
      >
        <path
          fill="#fff"
          d="m18.567 4.154 3.88 6.721h4.136v2.417h-1.41l-.914 10.975a1.208 1.208 0 0 1-1.204 1.108H5.945a1.208 1.208 0 0 1-1.203-1.108l-.916-10.975h-1.41v-2.417h4.136l3.88-6.72 2.094 1.208-3.183 5.512h10.313l-3.182-5.512 2.093-1.209Zm4.181 9.138H6.251l.806 9.666h14.885l.806-9.666Zm-7.04 2.416v4.834h-2.416v-4.834h2.416Zm-4.833 0v4.834H8.458v-4.834h2.417Zm9.667 0v4.834h-2.417v-4.834h2.417Z"
        />
      </svg>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'BarProduct',

  props: {
    barProduct: Object,
  },

  computed: {
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    buyProduct: function() {
      window.mp.triggerServer('alco:bar:buy', this.barProduct.id)
    },
  },
}
</script>

<style lang="scss" scoped>
.bar-product {
  width: 27.315vh;
  height: 43.426vh;
  font-family: 'Akrobat';
  font-weight: 700;
  text-transform: uppercase;
  display: flex;
  flex-direction: column;
  align-items: center;
  border: 0.093vh solid rgba(255, 255, 255, 0.07);
  .bg {
    margin-top: 3.611vh;
    width: 20vh;
    height: 20.093vh;
    background: rgba(255, 255, 255, 0.02);
    display: flex;
    justify-content: center;
    align-items: center;
    .img {
      width: 70%;
      height: 8.1rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
    }
  }

  .title {
    color: #ffffff;
    font-size: 2.963vh;
    line-height: 3.704vh;
    margin-top: 1.204vh;
  }

  .price-title {
    font-size: 1.296vh;
    line-height: 1.574vh;
    color: rgba(255, 255, 255, 0.55);
    margin-top: 1.019vh;
  }
  .price-value {
    color: #a0ff98;
    font-size: 2.963vh;
    line-height: 3.704vh;
    margin-top: 0.093vh;
  }
  .btn-basket {
    width: 20vh;
    height: 5.556vh;
    background: rgba(255, 255, 255, 0.07);
    display: none;
    justify-content: center;
    align-items: center;
    margin-top: 1.204vh;
    &:hover {
      background: rgba(255, 255, 255, 0.1);
    }
  }
  &:hover  .btn-basket{
    
    display: flex;
  }
}
</style>
