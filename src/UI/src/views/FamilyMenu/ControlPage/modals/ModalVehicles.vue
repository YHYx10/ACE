<template>
  <div class="modal-vehicle">
    <Back @click="closeModal" />
    <div class="content">
      <div class="title">
    Transport management
      </div>
      <div class="select-btns">
        <div
          class="action"
          v-for="(item, index) of vehicleOptionsData"
          :key="index"
          :style="{ opacity: !currentCar ? '0.05' : '1' }"
          @click="selectCarOption(item.key)"
        >
          <div
            class="icon"
            :style="{
              backgroundImage:
                'url(/img/familyMenu/vehicleOptions/icon-' + item.key + '.svg)',
            }"
          ></div>
          <div class="text">
            {{ loc(item.text) }}
          </div>
        </div>
        <div v-show="!currentCar" class="alert">
       Choose transport!
        </div>
      </div>
      <div class="vehicle-list">
        <div
          v-for="(item, index) of cars"
          :key="index"
          class="item"
          @click="setCurrentCar(item.key)"
          :class="{ active: currentCar ? currentCar.key === item.key : false }"
        >
          <div class="name">{{ item.carName }}</div>
          <div class="numberplate">{{ item.carNumber }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Back from '../../components/Back.vue'
import { mapGetters, mapState } from 'vuex'

export default {
  components: { Back },
  computed: {
    ...mapState('familyMenu', ['currentRankId', 'isLeader']),
    ...mapState('familyMenu/controlPage', ['ranksList']),
    ...mapGetters('localization', ['loc']),
    currentRank: function() {
      let currentRank = this.ranksList.find(
        (item) => item.rankId == this.currentRankId || this.isLeader
      )
      return currentRank
    },
    cars: function() {
      let carsFromRank = this.currentRank.accessCars.filter(
        (car) => car.access >= 0 || this.isLeader
      )
      return carsFromRank
    },
  },
  data() {
    return {
      vehicleOptionsData: [
        {
          key: 0,
          text: 'familyMenu_104',
          minRank: 1,
        },
        {
          key: 1,
          text: 'familyMenu_105',
          minRank: 1,
        },
        {
          key: 2,
          text: 'familyMenu_106',
          minRank: 3,
        },
        {
          key: 3,
          text: 'Gps',
          minRank: 0,
        },
        {
          key: 4,
          text: 'familyMenu_107',
          minRank: 3,
        },
      ],
      currentVehicleOptions: [],
      currentCar: null,
    }
  },
  methods: {
    setCurrentCar(key) {
      this.cars.forEach((car) => {
        if (car.key === key) {
          this.currentCar = car
        }
      })
      this.currentVehicleOptions = this.vehicleOptionsData.filter(
        (item) => item.minRank <= this.currentCar.access || this.isLeader
      )
    },
    closeModal: function() {
      this.currentCar = null
      this.currentVehicleOptions = []
      this.$emit('closeModal')
    },
    selectCarOption: function(key) {
      window.mp.trigger('familyMenu:selectCarOption', key, this.currentCar.key)
    },
  },
}
</script>

<style lang="scss" scoped>
.modal-vehicle {
  width: 100vw;
  height: 100vh;
  top: 0;
  left: 0;
  z-index: 99;
  background: rgb(30, 30, 30);
  position: fixed;
  display: flex;
  align-items: center;
  justify-content: center;
  .back {
    position: absolute;
    left: 8.889vh;
    top: 4.722vh;
  }
  .content {
    display: flex;
    flex-direction: column;
    .title {
      font-family: 'Akrobat';
      font-style: normal;
      font-weight: 800;
      font-size: 3.704vh;
      line-height: 4.444vh;
      display: flex;
      align-items: center;
      justify-content: center;
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: 2.963vh;
    }
    .select-btns {
      display: flex;
      gap: 0.926vh;
      position: relative;
      .alert {
        position: absolute;
        width: 100%;
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        color: #ff7d7d;
        font-family: 'Akrobat';
        font-style: normal;
        font-weight: 700;
        font-size: 2.222vh;
        line-height: 2.685vh;
      }
      .action {
        width: 9.444vh;
        height: 9.444vh;
        display: flex;
        flex-direction: column;
        align-items: center;
        background: rgba(255, 255, 255, 0.04);
        border: 0.093vh solid rgba(255, 255, 255, 0.09);
        &:hover {
          background: rgba(255, 255, 255, 0.12);
        }
        .icon {
          width: 4.074vh;
          height: 2.963vh;
          margin-top: 2.407vh;
          background-size: contain;
          background-repeat: no-repeat;
          background-position: center;
          margin-bottom: 0.926vh;
        }
        .text {
          text-align: center;
          font-family: 'Akrobat';
          font-style: normal;
          font-weight: 800;
          font-size: 1.111vh;
          line-height: 1.296vh;
          display: flex;
          align-items: center;
          text-align: center;
          text-transform: uppercase;

          color: #ffffff;
        }
      }
    }

    .vehicle-list {
      margin-top: 1.852vh;
      width: 50.926vh;
      display: flex;
      flex-direction: column;
      gap: 0.278vh;
      .item {
        display: flex;
        align-items: center;
        padding: 0 3.704vh 0 2.222vh;
        justify-content: space-between;
        font-weight: 700;
        font-size: 2.222vh;
        background: rgba(0, 0, 0, 0.2);
        height: 8.148vh;
        .name::before {
          content: '';
          width: 2.5vh;
          margin-right: 1.852vh;
          background: #ffffff;
          border: 0.093vh solid rgba(255, 255, 255, 0.09);
          box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
          transform: rotate(-90deg);
        }
        &:hover {
          background: rgba(0, 0, 0, 0.3);
        }
        &.active {
          background: linear-gradient(
            180deg,
            rgba(71, 44, 132, 0.5) 0%,
            rgba(75, 0, 130, 0.5) 100%
          );
        }
      }
    }
  }
}
</style>
