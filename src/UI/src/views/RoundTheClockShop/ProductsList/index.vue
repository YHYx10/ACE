<template>
  <div class="products-list">
    <ProductsListItem
      v-for="(item, index) in itemsList"
      :key="item.id"
      :item="item"
      :index="index"
    />
  </div>
</template>

<script>
import {mapState} from 'vuex'

import ProductsListItem from './ProductsListItem'

export default {
  name: 'ProductsList',

  components: {
    ProductsListItem
  },
  
  computed: {
    ...mapState('roundTheClockShop', ['productsSell']),
    itemsList(){
      return this.productsSell.filter(i=>i.cost > 0)
    }
  }
}
</script>

<style lang="scss" scoped>
  .products-list{
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 1.852vh;
    overflow-y: scroll;
    height: 100%;
    padding-right: 0.926vh;
    height: 57.593vh;
    &::-webkit-scrollbar {
        width: 0.463vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.04);
    }
    &::-webkit-scrollbar-thumb {
      background: #301934 ;
    }
  }
</style>
