<template>
  <div class="main-page">
    <!-- <div class="main-page__info">
      <div class="greeting">{{loc('PortOrders_11')}}, <br/><span>{{userName}}</span>!</div>
      <div class="desc">{{loc('PortOrders_12')}}</div>
    </div>
    <div class="main-page__offers">
      <div class="title">{{buissName}}</div>
      <div class="desc">{{loc('PortOrders_13')}}</div>
      <div class="new-items">
        <products-list-item
          v-for="item in this.productsListBuiss"
          :key="item.index"
          :item="item"
        />
      </div>
      <div class="prompt">{{loc('PortOrders_14')}}</div>
      <div class="btn-go" @click="setCurrentPage('ProductPage')">{{loc('PortOrders_15')}}</div>
    </div> -->
    <div class="greeting">Welcome</div>
    <div class="username">{{ userName }}</div>
    <div class="line" />
    <div class="about">Our novelties </div>
    <div class="list">
      <ProductItem
        v-for="(item, index) in productsListBuiss"
        :key="index"
        :item="item"
      />
    </div>
    <div class="footer">
      <div class="text">
 Go to the catalog to see all the products
      </div>
      <button @click="$emit('toPage')">
       Go to the catalog
      </button>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import ProductItem from '../ProductItem.vue'
// import ProductsListItem from './ProductsList/ProductsListItem'

export default {
  name: 'Main',

  components: {
    ProductItem,
  },

  props: {
    setCurrentPage: Function,
  },

  computed: {
    ...mapState('portOrders', ['productsList', 'userName', 'buissName']),
    ...mapGetters('localization', ['loc']),
    productsListBuiss: function() {
      return this.productsList.slice(-4)
    },
  },
}
</script>

<style lang="scss" scoped>
$yellow: #ffcc47;
.main-page {
  margin-top: 6.667vh;
  width: 129.907vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  * {
    text-transform: uppercase;
    color: #ffffff;
  }

  .greeting {
    font-weight: 700;
    font-size: 4.444vh;
    line-height: 5.37vh;
    margin-bottom: 0.648vh;
  }
  .username {
    font-weight: 700;
    font-size: 3.333vh;
    line-height: 3.981vh;
  }

  .line {
    margin: 3.981vh 0 3.611vh;
    width: 6.944vh;
    height: 0.185vh;
    background: #ffffff;
    border: 0.097vh solid rgba(255, 255, 255, 0.09);
    box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
  }

  .about {
    font-weight: 700;
    font-size: 3.333vh;
    line-height: 3.981vh;
    color: $yellow;
  }
  .list {
    display: flex;
    gap: 1.852vh;
    margin-top: 3.056vh;
  }

  .footer {
    width: 100%;
    margin-top: 1.852vh;
    display: flex;
    justify-content: space-between;
    align-items: center;
    .text {
      font-weight: 700;
      font-size: 2.593vh;
      line-height: 3.148vh;
    }
    button {
      width: 31.019vh;
      height: 6.944vh;
      display: flex;
      align-items: center;
      justify-content: center;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.685vh;
      background: rgba(255, 255, 255, 0.03);
      &:hover {
       background: rgba(255, 255, 255, 0.06); 
      }
    }
  }
}
</style>
