<template>
  <transition name="homePurchase-purchase" appear>
    <div class="homePurchase" v-if="Object.keys(homeData).length > 0">
      <div class="homePurchase-main">
        <div class="homePurchase-main__header">
          <p>House</p>
          <p>Id {{ homeData.ID }}</p>
        </div>
        <div class="homePurchase-main__list">
          <div class="homePurchase-main__item">
            <div class="homePurchase-main__item-content">
              <p>owner</p>
              <p>{{ homeData.Owner }}</p>
            </div>
          </div>
          <div class="homePurchase-main__item">
            <div class="homePurchase-main__item-content">
              <p>House Class </p>
              <p>{{ homeData.Class }}</p>
            </div>
          </div>
          <div class="homePurchase-main__item">
            <div class="homePurchase-main__item-content">
              <p> The number of rooms</p>
              <p>{{ homeData.Roommates }}</p>
            </div>
          </div>
          <div class="homePurchase-main__item">
            <div class="homePurchase-main__item-content">
              <p>Garage Space</p>
              <p>{{ homeData.GarageSpace }}</p>
            </div>
          </div>
        </div>
        <div class="homePurchase-main__price">
          <div class="homePurchase-main__price-content">
            <p>Price</p>
            <p>$ {{ homeData.Price.toLocaleString("de-DE") }}</p>
          </div>
        </div>
        <div class="homePurchase-main__state" :class="{opened:!homeData.IsLocked, closed: homeData.IsLocked}">
          <p>{{ homeData.IsLocked ? "Closed" : "Open" }}</p>
        </div>
        <div class="homePurchase-main__buttons">
          <button class="homePurchase-main__buttons-item" :class="{opened:!homeData.IsLocked, closed: homeData.IsLocked}" :disabled="!homeData.CanEnter && !homeData.IsTarget" @click="enterMain">
            <p>{{(!homeData.CanEnter && homeData.IsTarget)? "Hack": "Enter"}}</p>
          </button>
          <button class="homePurchase-main__buttons-item" v-if="homeData.CanEnter" @click="enterGarage">
            <p>Enter the garage</p>
          </button>
        </div>
      </div>
      <div class="homePurchase-purchase">
        <div class="homePurchase-close" @click="close">
          <img src="./Assets/Icons/homePurchase-Close.svg" alt="" />
        </div>
        <div class="homePurchase-purchase__main" v-show="!homeData.IsSelled">
          <div class="homePurchase-money">
            <div class="homePurchase-money__item">
              <div class="homePurchase-money__item-image">
                <img src="./Assets/Icons/homePurchase-Cash.svg" alt="">
              </div>
              <div class="homePurchase-money__item-content">
                <p>Cash</p>
                <p>$ {{ userMoney.toLocaleString("de-DE") }}</p>
              </div>
            </div>
            <div class="homePurchase-money__item">
              <div class="homePurchase-money__item-image">
                <img src="./Assets/Icons/homePurchase-Card.svg" alt="">
              </div>
              <div class="homePurchase-money__item-content">
                <p>Card</p>
                <p>$ {{ userBank.toLocaleString("de-DE") }}</p>
              </div>
            </div>
          </div>
          <div class="homePurchase-purchase__type">
            <div class="homePurchase-purchase__type-item" 
              v-for="type in types" 
              :key="type.key"
              :class="{active: isTypeActive(type.key)}" 
              @click="setActiveType(type.key)"
            >
              <img :src="getTypeIcon(type.key)" alt="">
              <p>{{ type.text }}</p>
            </div>
          </div>
          <button class="homePurchase-purchase__button" @click="buy">
            <p>kaufen</p>
          </button>
        </div>
      </div>
    </div>
  </transition>
</template>

<script>
import { mapState } from 'vuex'
import Cash from "./Assets/Icons/homePurchase-Cash.svg";
import Card from "./Assets/Icons/homePurchase-Card.svg";
export default {
  computed: {
    ...mapState('homePurchase', ['homeData', 'userMoney', 'userBank']),
  },
  data() {
    return {
      types:[
        {
          key: "cash",
          text: "Cash"
        },
        {
          key: "card",
          text: "Card"
        }
      ],
      activeType: "cash",
    };
  },
  methods: {
    enterMain() {
      if (!this.homeData.CanEnter && this.homeData.IsTarget)
        window.mp.trigger("housepurchase::breakTheDoor")
      else
        window.mp.trigger("housepurchase::enter")
    },
    enterGarage() {
      window.mp.trigger("housepurchase::enterGarage")
    },
    isTypeActive(type) {
      return this.activeType == type;
    },
    getTypeIcon(type) {
      return type == "cash" ? Cash : Card;
    },
    setActiveType(type) {
      if(!this.isTypeActive(type)) this.activeType = type;
    },
    buy: function() {
      window.mp.trigger("housepurchase::buy", this.activeType)
    },
    close() {
      window.mp.trigger("housepurchase::close")
    }
  } 
}
</script>


<style lang="scss" src="./HomePurchase.scss"></style>