<template>
  <div class="vehicle-card" @click="acceptRent(item.Model)">
    <img class="preview" :src="`/img/vehicles/${item.Image}.png`" alt="" />
    <div class="about">
      <div class="name">
        <div class="title">
          Name
        </div>
        <div class="value">
          {{ item.Name }}
        </div>
      </div>
      <div class="price">
        <div class="title">
       rental price
        </div>
        <div class="value">$ {{ item.Price.toLocaleString('en-US') }}</div>
      </div>
    </div>
    <div class="payment">
      <div class="payment-type" @click="setPayment(0)">
        <img src="/img/common/payment/cash.svg" alt="" />
        <div class="type">Cash</div>
      </div>
      <div class="payment-type" @click="setPayment(1)">
        <img src="/img/common/payment/card.svg" alt="" />
        <div class="type">card</div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'RentVehicleItem',
  props: {
    item: Object,
    category: Number,
  },
  computed: {
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    acceptRent(model) {
      this.$emit('selectChoice', model)
    },
    setPayment(payment) {
      window.mp.trigger(
        'vehicleRent:acceptRent',
        this.item.Model,
        this.category,
        payment
      )
      this.$emit('selectChoice', null)
    },
  },
}
</script>

<style lang="scss" scoped>
.vehicle-card {
  display: flex;
  flex-direction: column;
  width: 31.019vh;
  height: 31.204vh;
  &:hover {
    .about {
      display: none;
      background: rgba(0, 0, 0, 0.5);
    }
    .payment {
      display: flex;
    }
  }

  .preview {
    width: 100%;
    height: 23.611vh;
    object-fit: cover;
  }
  .about {
    background: rgba(0, 0, 0, 0.3);
    display: flex;
    height: 7.593vh;
    padding: 1.481vh 1.852vh 1.019vh 1.852vh;
    justify-content: space-between;
    font-family: 'Akrobat';
    font-style: normal;
    font-weight: 700;
    .title {
      font-size: 1.111vh;
      line-height: 1.296vh;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }
    .name,
    .price {
      display: flex;
      flex-direction: column;
      align-items: flex-end;
    }
    .name {
      align-items: flex-start;
    }

    .name .value {
      font-size: 2.963vh;
      line-height: 3.519vh;
      display: flex;
      align-items: center;
      text-transform: uppercase;
      color: #ffffff;
    }

    .price .value {
      font-size: 2.963vh;
      line-height: 3.519vh;
      text-align: right;
      text-transform: uppercase;
      color: #a0ff98;
    }
  }
  .payment {
    background: rgba(0, 0, 0, 0.3);
    display: none;
    justify-content: space-between;
    color: #fff;
    text-transform: uppercase;
    .payment-type {
      width: 15.278vh;
      height: 7.593vh;
      display: flex;
      align-items: center;
      justify-content: center;
      flex-direction: column;
      background: rgba(255, 255, 255, 0.1);
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 700;
      font-size: 1.667vh;
      line-height: 2.037vh;
      img {
        width: 2.778vh;
        height: 2.778vh;
      }
      &:hover {
        background: #301934 ;
      }
    }
  }
}
</style>
