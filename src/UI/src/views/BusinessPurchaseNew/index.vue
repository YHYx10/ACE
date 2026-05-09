<template>
  <transition name="businessPurchaseTransition" appear>
    <div class="businessPurchase" v-if="Object.keys(businessData).length > 0">
      <div class="businessPurchase-main">
        <div class="businessPurchase-main__header">
          <p>id {{businessData.ID}}</p>
          <p>{{businessData.Description}}</p>
          <p>{{businessData.Name}}</p>
        </div>
        <div class="businessPurchase-main__list">
          <div class="businessPurchase-main__item">
            <div class="businessPurchase-main__item-content">
              <p>owner</p>
              <p>{{businessData.Owner}}</p>
            </div>
          </div>
          <div class="businessPurchase-main__item">
            <div class="businessPurchase-main__item-content">
              <p>owner</p>
              <p>{{ businessData.Overseer}}</p>
            </div>
          </div>
        </div>
        <div class="businessPurchase-main__price">
          <div class="businessPurchase-main__price-content">
            <p>Condition. Prize</p>
            <p>${{ businessData.Price.toLocaleString()}}</p>
          </div>
        </div>
        <div class="businessPurchase-purchase" v-if="businessData.Purchaseable">
          <div class="businessPurchase-money">
            <div class="businessPurchase-money__item">
              <div class="businessPurchase-money__item-image">
                <img src="./Assets/Icons/businessPurchase-Cash.svg" alt="Icon">
              </div>
              <div class="businessPurchase-money__item-content">
                <p>Cash</p>
                <p>{{userMoney.toLocaleString('ru')}}</p>
              </div>
            </div>
            <div class="businessPurchase-money__item">
              <div class="businessPurchase-money__item-image">
                <img src="./Assets/Icons/businessPurchase-Card.svg" alt="Icon">
              </div>
              <div class="businessPurchase-money__item-content">
                <p>Card</p>
                <p>{{userBank.toLocaleString('ru')}}</p>
              </div>
            </div>
          </div>
          <div class="businessPurchase-purchase__type">
            <div class="businessPurchase-purchase__type-item" 
              v-for="item in ['cash', 'card']" 
              :key="item"
              :class="{active: isTypeActive(item)}" 
              @click="setActiveType(item)"
            >
              <img :src="getTypeIcon(item)" alt="">
              <p>{{ item }}</p>
            </div>
          </div>
          <button class="businessPurchase-purchase__button" @click="buyBusiness">
            <p>Buy</p>
          </button>
        </div>
      </div>
      <div class="businessPurchase-close" @click="closeBusiness">
        <img src="./Assets/Icons/businessPurchase-Close.svg" alt="close" />
      </div>
    </div>
  </transition>
</template>

<script>
import { mapState } from "vuex";
import Cash from "./Assets/Icons/businessPurchase-Cash.svg";
import Card from "./Assets/Icons/businessPurchase-Card.svg";
export default {
  name: 'BusinessPurchase',
  data() {
    return {
      activeType: "cash",
    };
  },
  computed: {
    ...mapState('businessPurchase', ['businessData', 'userMoney', 'userBank']),
  },
  methods: {
    getTypeIcon(type) {
      return type == "cash" ? Cash : Card;
    },
    isTypeActive(type) {
      return this.activeType == type;
    },
    setActiveType(type) {
      if(!this.isTypeActive(type)) this.activeType = type;
    },
    closeBusiness() {
      window.mp.trigger('businesses::infoPanel_closeClick');
    },
    buyBusiness() {
      // if payment by cash - 'true', else - 'false'
      window.mp.trigger('businesses::infoPanel_buyClick', this.activeType === 'cash');
      // console.log("buyBusiness", this.activeType);
    }
  }
};
</script>

<style lang="scss" src="./BusinessPurchase.scss"></style>