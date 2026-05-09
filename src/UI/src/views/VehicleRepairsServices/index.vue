<template>
  <div class="repair-vehicle">
    <ExitCross class="exit"  />
    <div class="left-bar">
      <TitleComponent :titlePrimary="'Luxe'" :titleSecondary="'Service'" />
      <div class="info">
        <div class="characteristics">
          <div class="name">{{ vehicleData.name }}</div>
          <div class="health">
            <img src="/img/vehicleRepairsServices/repair.svg" alt="" />
            <div class="about">
              <div class="title">Car condition</div>
              <div class="value">{{ vehicleData.StateEngine }}%</div>
            </div>
          </div>
          <div class="common-mileage">
            <img src="/img/vehicleRepairsServices/timer.svg" alt="" />
            <div class="about">
              <div class="title">Car mileage</div>
              <div class="value">{{ convertNumber(vehicleData.Mileage) }} КМ</div>
            </div>
          </div>
        </div>
        <div class="mileage">
          <div class="item">
            <div class="title">Mileage before changing the oil in the engine</div>
            <div class="value">{{ convertNumber(vehicleData.MileageToChangeOil) }} КМ</div>
          </div>
          <div class="item">
            <div class="title">Mileage before replacing transmission oil</div>
            <div class="value">
              {{ convertNumber(vehicleData.MileageToChangeOilTransm) }} КМ
            </div>
          </div>
          <div class="item">
            <div class="title">Mileage before replacing brake pads </div>
            <div class="value">{{ convertNumber(vehicleData.MileageToChangeBrake) }} КМ</div>
          </div>
        </div>
      </div>
    </div>
    <div class="list">
      <Card
        v-for="(item, index) of getList"
        :key="index"
        :value="item.value"
        :name="item.name"
        :img="item.img"
        :repairType="item.repairType"
        :price="item.price"
        :isSelected="selected === index"
        @onSelect="selected = index"
        @onBtn="repair"
      />
    </div>
  </div>
</template>

<script>
import TitleComponent from '../UI/components/TitleComponent.vue'
// import ExitButton from '../WarForEnterprice/components/ExitButton.vue'
import { mapState } from 'vuex'
import Card from './Card.vue'
import ExitCross from '../UI/components/ExitCross.vue'
export default {
  name: 'vehicleRepairsServices',
  computed: {
    ...mapState('vehicleRepairsServices', ['vehicleData']),
    getList() {
      return Object.entries(this.wear).map(([key, {img, name, repairType, priceKey}]) => {
        key
        return {
          value: this.vehicleData[key] ? this.vehicleData[key] : -1,
          img,
          name,
          repairType,
          price: this.vehicleData[priceKey]
        }
      })
    },
  },
  data() {
    return {
      wear: {
        carcase: {
          img: 'carcase',
          name: 'body',
          repairType: 'body',
          priceKey: 'PriceRepair'
        },
        StateEngine: {
          img: 'engine',
          name: 'engine',
          repairType: 'engine',
          priceKey: 'PriceRepairEngine'
        },
        StateEngineOil: {
          img: 'engine-oil',
          name: 'engine oil ',
          repairType: 'engineOil',
          priceKey: 'PriceChangeOil'
        },
        StateTransmOil: {
          img: 'transmission-oil',
          name: 'Transmission oil',
          repairType: 'transmisOil',
          priceKey: 'PriceChangeOilTransm'
        },
        StateBrake: {
          img: 'disc-brake',
          name: 'brake pads',
          repairType: 'break',
          priceKey: 'PriceChangeBrake'
        },
      },
      selected: -1,
    }
  },
  methods: {
    convertNumber(number){
      return number.toFixed(2).replace('.', ',')
    },
    repair(state) {
      console.log('this.selected', this.selected);
      this.selected = -1
      console.log('this.selected2', this.selected);
      window.mp.trigger('repair:buyService', state)
    }
  },
  components: { TitleComponent, Card, ExitCross },
}
</script>

<style lang="scss" scoped>
.repair-vehicle {
  width: 1080vw;
  height: 100vh;
  background: rgb(91, 89, 89);
  color: #fff;
  padding-left: 7.963vh;
  background: radial-gradient(50% 99.27% at 50% 50%, rgba(0, 0, 0, 0) 0%, rgba(0, 0, 0, 0.8) 100%);
  .exit {
    position: absolute;
    top: 3.704vh;
    right: 3.704vh;
  }
  .left-bar {
    margin-top: 3.889vh;
    width: fit-content;
    font-family: 'Akrobat';
    font-style: normal;
  }
  .info {
    margin-top: 14.259vh;
    display: flex;
    flex-direction: column;
    gap: 4.074vh;

    .title {
      font-weight: 700;
      font-size: 1.111vh;
      line-height: 1.296vh;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    .characteristics {
      display: flex;
      flex-direction: column;
      gap: 1.759vh;
      .name {
        font-weight: 700;
        font-size: 3.704vh;
        line-height: 4.444vh;
        text-transform: uppercase;
      }

      .health,
      .common-mileage {
        display: flex;
        align-items: flex-end;
        gap: 1.944vh;
        img {
          margin-bottom: 0.556vh;
        }
        .about {
          display: flex;
          flex-direction: column;
          gap: 0.278vh;
        }
      }
      .health {
        img {
          width: 2.5vh;
        }
        .about .value {
          font-weight: 900;
          font-size: 2.963vh;
          line-height: 3.519vh;
          text-transform: uppercase;
          color: #a0ff98;
        }
      }
      .common-mileage {
        img {
          width: 2.778vh;
          margin-bottom: 1.481vh;
        }
        .about .value {
          font-weight: 700;
          font-size: 2.963vh;
          line-height: 3.519vh;
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }

    .mileage {
      display: flex;
      flex-direction: column;
      gap: 2.315vh;
      .item {
        padding-left: 1.481vh;
        position: relative;
        &::before {
          content: '';
          position: absolute;
          left: 0;
          top: 0.278vh;
          width: 0.185vh;
          height: 2.407vh;
          background: white;
          box-shadow: 0vh 0vh 1.296vh rgba(255, 255, 255, 0.55);
        }
        .value {
          margin-top: 0.278vh;
          font-style: normal;
          font-weight: 700;
          font-size: 2.963vh;
          line-height: 3.519vh;
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }
  }

  .list {
    margin-top: 10.926vh;
    display: flex;
    align-items: flex-end;
    height: 20vh;
    gap: 3.704vh;
  }
}
</style>
