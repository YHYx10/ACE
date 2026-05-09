<template>
  <div class="shop-items">
    <products-list-item
      v-for="item in itemsList"
      :key="item.id"
      :item="item"
    />
  </div>
</template>

<script>
import { mapState } from 'vuex'

import ProductsListItem from './ProductsListItem'

export default {
  name: 'ShopItems',

  components: {
    ProductsListItem
  },

  computed: {
    ...mapState('burgerShop', ['items', 'shoppingCart']),
    itemsList(){
      return this.items.filter(i=>i.cost > 0)
    }
  }
  
}
</script>

<style lang="scss" scoped>
.shop-items{
  display: flex;
  flex-direction: column;
  gap: 0.926vh;
  overflow-y: auto;
  height: 77.315vh;
  padding-right: 0.926vh;
  gap: 1.852vh 0.926vh;
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