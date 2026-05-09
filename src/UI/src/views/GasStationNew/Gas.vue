<template>
  <div class="gas">
    <div class="gas-main">
      <p>Gas Station</p>
      <div class="gas-main__content">
        <p>Your kind of fuel {{activeFuel.title}}</p>
        <div class="gas-main__list">
          <div class="gas-card"
            v-for="fuel in fuelTypes"
            :key="fuel.key"
            :class="[fuel.key, {active: isFuelActive(fuel)}]"
            @click="setActiveFuel(fuel)"
          >
            <div class="gas-card__line">
              <div class="gas-card__line-item"></div>
              <svg viewBox="0 0 19 30" fill="none" xmlns="http://www.w3.org/2000/svg">
                <rect y="19" width="19" height="19" transform="rotate(-90 0 19)"/>
                <path d="M-4.80825e-07 19L-1.31134e-06 0L19 -8.30516e-07L19 19L9.5 30L-4.80825e-07 19Z"/>
              </svg>
              <div class="gas-card__line-item"></div>
            </div>
            <div class="gas-card__main" >
              <div class="gas-card__main-wrapper">
                <img :src="`./img/gasStation/Fuel/${fuel.img}`" :alt="fuel.key" />
                <div class="gas-card__main-content">
                  <p>{{fuel.title}}</p>
                  <p>${{fuel.cost}}</p>
                </div>
              </div>
              <div class="gas-card__waves"></div>
            </div>
          </div>
        </div>
        <div class="gas-purchase">
          <div class="gas-count">
            <div class="gas-count__title">
              <p>Choose the number of liters</p>
              <p>Use the runner or enter the number of liters and the amount manually</p>
            </div>
            <div class="gas-range">
              <Range :max="maxFuel-currentFuel" :min="0" v-model="liters" />
            </div>
            <div class="gas-count__inputs">
              <div class="gas-input">
                <input type="text" :value="litersAmount" @input="onFuelInput" @focus="onFuelFocus" @blur="onFuelBlur"/>
                <p>L</p>
              </div>
              <div class="gas-input">
                <input type="text" :value="currentPrice" :disabled="true"/>
                <p>$</p>
              </div>
            </div>
            <button class="gas-count__button" @click="setFullTank">
              <p>Full of tank</p>
            </button>
          </div>
          <div class="gas-payment">
            <div class="gas-payment__tab">
              <p>Check</p>
              <div class="gas-payment__tab-list">
                <div class="gas-payment__tab-item">
                  <p>fuel</p>
                  <p>{{activeFuel.title}}</p>
                </div>
                <div class="gas-payment__tab-item">
                  <p>liter</p>
                  <p>{{ liters }}</p>
                </div>
                <div class="gas-payment__tab-item">
                  <p>In total</p>
                  <p>${{ currentPrice }}</p>
                </div>
              </div>
            </div>
            <div class="gas-payment__method">
              <p>Choose a payment method</p>
              <div class="gas-payment__method-list">
                <div class="gas-payment__method-item" 
                  v-for="payment in paymentTypes" 
                  :key="payment.id"
                  :class="{active: isPaymentActive(payment)}"
                  @click="setActivePayment(payment)"
                >
                  <img :src="`./img/gasStation/Payment/${payment.img}`" :alt="payment.key" />
                  <p>{{ payment.title }}</p>
                </div>
              </div>
              <button @click="buyFuel" class="gas-payment__method-button">
                <p>Pay</p> 
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="gas-close" @click="closeGasStation">
      <img src="./Assets/Icons/Gas-Close.svg" alt="Close" />
    </div>
  </div>
</template>

<script>
import Range from "./Components/Range.vue";
export default {
  components: {
    Range,
  },
  data() {
    return {
      fuelTypes: [
        {
          srvkey: 1,
          key: 'standart',
          title: 'Standart',
          cost: 0,
          img: "Blob.png"
        },
        {
          srvkey: 2,
          key: 'standartPlus',
          title: 'Standart+',
          cost: 0,
          img: "Blob.png"
        },
        {
          srvkey: 3,
          key: 'diesel',
          title: 'Diesel',
          cost: 0,
          img: "Blob.png"
        },
        {
          srvkey: 4,
          key: 'deluxe',
          title: 'Deluxe',
          cost: 0,
          img: "Blob.png"
        },
        {
          srvkey: 5,
          key: 'electro',
          title: 'Electro',
          cost: 0,
          img: "Battery.png"
        }
      ],
      activeFuel: null,
      paymentTypes: [
        {
          id: 0,
          key: "cash",
          title: "Cash",
          img: "Cash.svg"
        },
        {
          id: 1,
          key: "card",
          title: "Card",
          img: "Card.svg"
        },
        {
          id: 2,
          key: "state",
          title: "State account",
          img: "State.svg"
        }
      ],
      activePayment: null,
      maxFuel: 100,
      currentFuel: 40,
      liters: 0
    };
  },
  computed: {
    litersAmount(){
      return this.liters < 0 ? 0 : this.liters
    },
    currentPrice() {
      const value = this.liters * this.activeFuel.cost
      return value < 0 ? 0 : value;
    }
  },
  methods: {
    isFuelActive(fuel) {
      return this.activeFuel == fuel;
    },
    setActiveFuel(fuel) {
      if(!this.isFuelActive(fuel)) this.activeFuel = fuel;
    },
    onFuelInput(e) {
      if(!/^[0-9]+$/.test(e.target.value)) {
        e.target.value = e.target.value.substring(0, e.target.value.length - 1);
      }
      if(Number(e.target.value) >= this.maxFuel-this.currentFuel) e.target.value = this.maxFuel-this.currentFuel;
      this.liters = e.target.value;
    },
    onFuelFocus(e) {
      if(!this.liters) e.target.value = "";
    },
    onFuelBlur(e) {
      if(!this.liters) e.target.value = 0;
    },
    setFullTank() {
      this.liters = this.maxFuel-this.currentFuel;
    },
    isPaymentActive(payment) {
      return this.activePayment == payment;
    },
    setActivePayment(payment) {
      if(!this.isPaymentActive(payment)) this.activePayment = payment;
    },
    closeGasStation: function() {
      window.mp.trigger('gasStation:close')
    },
    buyFuel: function() {
      console.log(this.activeFuel.srvkey)
      window.mp.trigger('gasStation:buyFuel', this.activeFuel.srvkey, this.liters, this.activePayment.id)
    },
    setData(value) 
	{
		this.maxFuel = value.max
		this.currentFuel = value.cur
		this.fuelTypes[0].cost = value.price1
		this.fuelTypes[1].cost = value.price2
		this.fuelTypes[2].cost = value.price3
		this.fuelTypes[3].cost = value.price4
		this.fuelTypes[4].cost = value.price5
		this.setActiveFuel(this.fuelTypes[value.active])
		this.setActivePayment(this.paymentTypes[0]);
    },
  },
  created() {
    this.$onClient("W:GasStation:SetData", this.setData);
    this.$callClient("W:GasStation:Created")
    // this.setActiveFuel(this.fuelTypes[0])
    // this.setActivePayment(this.paymentTypes[0]);
  },
}
</script>

<style lang="scss" src="./Gas.scss"></style>