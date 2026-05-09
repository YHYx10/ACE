<template>
  <div class="product-item">
    <div class="img" :style="{backgroundImage: 'url(/img/burgerShop/items/' + item.unicName + '.png'}"></div>
    <div class="info">
      <div class="title">
        {{item.name}}
      </div>
      <div class="price-title">
      price
      </div>
      <div class="price-value">
        $ {{item.cost}}
      </div>
    </div>
    <div class="btn-buy" @click="putInShoppingCart({ item: item.name, cost: item.cost, unicName: item.unicName })">
      <svg xmlns="http://www.w3.org/2000/svg" width="29" height="29" fill="none" viewBox="0 0 29 29">
        <path fill="#fff" :d="path"/>
      </svg>
    </div>
  </div>
</template>

<script>
import {mapState, mapMutations, mapGetters} from 'vuex'

export default {
  name: 'ProductsListItem',

  props: {
    item: Object
  },

  data() {
    return {
      path: 'm18.567 4.154 3.88 6.72h4.136v2.417h-1.41l-.914 10.976a1.208 1.208 0 0 1-1.204 1.108H5.945a1.209 1.209 0 0 1-1.203-1.108L3.826 13.29h-1.41v-2.416h4.136l3.88-6.721 2.094 1.208-3.183 5.513h10.313l-3.182-5.513 2.093-1.208Zm4.181 9.137H6.251l.806 9.667h14.885l.806-9.666Zm-7.04 2.417v4.833h-2.416v-4.833h2.416Zm-4.833 0v4.833H8.458v-4.833h2.417Zm9.667 0v4.833h-2.417v-4.833h2.417Z'
    }
  },

  computed: {
    ...mapState('burgerShop', ['items', 'shoppingCart']),
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    ...mapMutations('burgerShop', ['putInShoppingCart'])
  }
}
</script>

<style lang="scss" scoped>
.product-item {
  color: #fff;
  font-family: 'Akrobat';
  display: flex;
  font-weight: 700;
  position: relative;
  flex-shrink: 0;
  width: 36.944vh;
  height: 14.722vh;
  align-items: center;
  gap: 2.5vh;
  background: rgba(255, 255, 255, 0.05);
  .img{
    margin-left: 2.778vh;
    width: 13.241vh;
    height: 10.093vh;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
  }
  .info {
    text-transform: uppercase;
    .title {
      font-size: 2.963vh;
      line-height: 3.704vh;
    }
    .price-title {
      margin-top: 1.111vh;
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

  .btn-buy {
    position: absolute;
    bottom: 0;
    right: 0;
    width: 5.556vh;
    height: 5.556vh;
    background: rgba(255, 255, 255, 0.07);
    display: flex;
    justify-content: center;
    align-items: center;
    &:hover {
      background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    }
  }
}
.list-item{
  display: flex;
  flex-flow: column;
  align-items: flex-start;
  position: relative;
  padding: 2.25rem 2.25rem 5.25rem 2.25rem;
  background: linear-gradient(129.17deg, rgba(255, 255, 255, 0.1) 0%, rgba(255, 255, 255, 0) 98.82%);
  transition: .2s;
  &:hover{
    background: linear-gradient(157.12deg, #151515 17.04%, rgba(41, 41, 41, 0.15) 106.68%);
    transition: .2s; 
    .btn-buy{
      background-image: url('/img/burgerShop/button-hover.png');
    }
  }
  .price{
    font-size: 1rem;
    line-height: 1.2rem;
    color: #C9EA27;
    position: relative;
    &:before, &:after{
      content: '';
      background: rgba(255, 255, 255, 0.1);
      position: absolute;
      top: -1rem;
      left: -1rem;
    }
    &:before{
      height: 5rem;
      width: 0.093vh;
    }
    &:after{
      width: 5rem;
      height: 0.093vh;
    }
  }
  .img{
    position: absolute;
    right: -2rem;
    top: -0.093vh;
    width: 10rem;
    height: 10rem;
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
  }
  .title{
    margin: 3.3rem 0 .5rem 0;
    font-size: 1.35rem;
    line-height: 1.6rem;
    color: #FFFFFF;
  }
  .desc{
    font-weight: 300;
    font-size: .75rem;
    line-height: 1.05rem;
    color: rgba(255, 255, 255, 0.7);
    position: relative;
  }
  .btn-buy{
    height: 3.75rem;
    min-height: 3.75rem;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    position: absolute;
    bottom: 0;
    left: 0;
    text-transform: uppercase;
    font-weight: normal;
    font-size: 1.2rem;
    line-height: 1.45rem;
    letter-spacing: 0.02em;
    text-transform: uppercase;
    color: #FFFFFF;
    padding-top: .5rem;
    background-image: url('/img/burgerShop/button.png');
    background-size: cover;
    background-position: center bottom;
    background-repeat: no-repeat;
    transition: .2s;
  }
}
</style>
