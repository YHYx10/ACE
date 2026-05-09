<template>
  <div class="bar">
    <ExitCross @click="exit" class="cross-exit" />
    <div class="list">
      <BarProduct
        v-for="barProduct in productsList"
        :key="barProduct.id"
        :barProduct="barProduct"
        @onSelect="putInBasket"
      />
    </div>
    <ShoppingCarts
      class="basket"
      :list="basket"
      @deleteItem="(key) => deleteItem({ key })"
      @increase="(key) => changeInQuantityUp({ key })"
      @decrease="(key) => changeInQuantityDown({ key })"
      @onBuy="buyProducts"
    />
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import BarProduct from './BarProduct'
import ExitCross from '../UI/components/ExitCross'
import ShoppingCarts from '../UI/ShoppingCarts/ShoppingCarts.vue'
export default {
  name: 'Bar',

  components: {
    BarProduct,
    ExitCross,
    ShoppingCarts,
  },

  data: function() {
    return {
      currentGalleryIndex: 0,
      basket: [],
    }
  },

  computed: {
    ...mapState('bar', ['productsList']),
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    putInBasket({ name, img, price }) {
      console.log('putInBasket', this.basket)
      const basket = this.basket
      let newItemIndex = null
      for (let index = 0; index < basket.length; index++) {
        const item = basket[index]
        if (item.product === name) {
          newItemIndex = index
        }
      }
      if (newItemIndex === null) {
        this.basket.push({
          unicName: name,
          cost: price,
          image: img,
          product: name,
          count: 1,
        })
      } else {
        const existingItem = basket[newItemIndex]
        existingItem.count++
      }

      console.log('putInBasket', this.basket)
    },
    clearBasket() {
      this.basket.splice(0, this.basket.length)
    },
    exit(){
      window.mp.trigger("alco:bar:close");
    },
    deleteItem({ key }) {
      this.basket.splice(key, 1)
    },
    changeInQuantityUp({ key }) {
      this.basket[key].count++
    },
    changeInQuantityDown( { key }) {
      console.log(key)
      if (this.basket[key].count > 1) {
        this.basket[key].count--
      } else {
        this.basket.splice(key, 1)
      }
    },
    buyProducts(isPaymentBank){
      const items = [];
      this.basket.forEach(item => {
        items.push({
          Name: item.unicName,
          Count: item.count
        })
      });
      this.clearBasket();
      window.mp.triggerServer('alco:bar:buy', JSON.stringify(items), !isPaymentBank);
    }
  },

}
</script>

<style lang="scss" scoped>
.bar {
  width: 100vw;
  height: 100vh;
  background-image: url('/img/bar/bg-gradient.png');
  background-repeat: no-repeat;
  background-size: cover;
  display: flex;
  align-items: center;
  justify-content: space-between;
  .cross-exit {
    position: absolute;
    top: 3.704vh;
    right: 3.704vh;
  }

  .list {
    margin-left: 3.889vh;
    width: 85.648vh;
    height: 87.778vh;
    display: grid;
    row-gap: 0.926vh;
    grid-template-columns: repeat(3, 1fr);
    overflow-y: scroll;
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
  .basket {
    margin-right: 4.63vh;
  }
}
</style>
