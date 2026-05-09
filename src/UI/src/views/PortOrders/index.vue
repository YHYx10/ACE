<template>
  <div class="port-orders">
    <CrossExit @click="exit" class="cross-exit" />
    <div class="box">
      <NavMenu :page="currentPage" @toPage="toPage" />
      <component @toPage="toPage('Products')" :is="currentPage" />
    </div>
  </div>
</template>

<script>
import CrossExit from '../UI/components/ExitCross'
import NavMenu from './NavMenu.vue'
import ProductItem from './ProductItem.vue'
import Main from './Pages/Main.vue'
import Products from './Pages/Products.vue'
import About from './Pages/About.vue'

export default {
  name: 'PortOrders',
  components: {
    CrossExit,
    NavMenu,
    ProductItem,
    Main,
    Products,
    About,
  },
  data() {
    return {
      pages: ['Main', 'Products', 'About'],
      currentPage: 'Main',
    }
  },
  methods: {
    toPage(str) {
      this.currentPage = str.charAt(0).toUpperCase() + str.slice(1)
    },
    exit() {
      // mp.trigger ...
    }
  },
}
</script>

<style lang="scss" scoped>
.port-orders {
  * {
    font-family: 'Akrobat';
  }
  width: 100vw;
  height: 100vh;
  background: rgba(28, 29, 34, 0.95);
  display: flex;
  justify-content: center;
  .box {
    width: fit-content;
    display: flex;
    flex-direction: column;
    align-items: center;
    padding-top: 4.63vh;
  }
  .cross-exit {
    position: absolute;
    top: 3.704vh;
    right: 3.704vh;
  }
}
</style>
