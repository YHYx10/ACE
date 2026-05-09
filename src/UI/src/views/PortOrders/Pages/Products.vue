<template>
  <div class="product-page">
    
    <div class="products">
      <div class="title">
     Available for order
      </div>
      <div class="list">
        <ProductItem v-for="(item, key) in productsList" :key="key" :item="item" @click="putInBasket" />
      </div>
    </div>
    <BasketList />
  </div>
</template>

<script>
import BasketList from '../BasketList/index.vue'
import ProductItem from '../ProductItem.vue';
import {mapMutations, mapState} from 'vuex'
export default {
  name: 'Products',
  computed: {
    ...mapState('portOrders', ['productsList', 'userName', 'buissName']),
  },
  components: {
    BasketList,
    ProductItem
  },
  methods: {
    ...mapMutations('portOrders', ['putInBasket'])
  }
}
</script>

<style lang="scss" scoped>
.product-page {
  margin-top: 6.019vh;
  display: flex;
  justify-content: space-between;
  width: 100%;
  // gap: 13.426vh;
  .products {
    .title {
      font-weight: 700;
      font-size: 3.333vh;
      line-height: 3.981vh;
      margin-bottom: 2.13vh;
      color: #fff;
    }
    .list {
      overflow-y: auto;
      overflow-x: hidden;
      min-width: 96.759vh;
      height: 57.685vh;
      display: grid;
      grid-template-columns: repeat(3, 1fr);
      gap: 1.852vh;
      padding-right: 0.926vh;
      &::-webkit-scrollbar {
        width: 0.185vh;
        &-thumb {
          background: red;
        }
        &-track {
          background: gray;
        }
      }
    }
  }
}
</style>
